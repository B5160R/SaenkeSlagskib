using System;

namespace SaenkeSlagskibe
{
    class Program
    {
        static int playerHits = 0;
        static int computerHits = 0;
        static int[,] computerTrys = new int[9, 9];

        static void Main(string[] args)
        {
            int[,] board;

            if (Welcome())
            {
                board = SetupGame();
            }
            else
            {
                board = AutoSetup();

                PrintBoard(board);
                Console.WriteLine("");
                Console.WriteLine("Accept setup? y/n: ");
                if (Console.ReadLine() == "n")
                {
                    Console.WriteLine("Too bad, but we don't care :) ");
                    Console.ReadKey();
                }
                Console.Clear();
            }

            BeginGame(board);
        }

        static bool Welcome()
        {
            bool manualSetup = false;
            Console.WriteLine("To place ships manually        type '1'");
            Console.WriteLine("To place ships automatically   type '2'");

            if (Console.ReadLine() == "1")
            {
                manualSetup = true;
            }
            return manualSetup;
        }

        static int[,] AutoSetup()
        {
            int[,] board = new int[9, 9];
            int[] typeOfShipPlaced = new int[5];
            Random rand = new Random();
            int shipsPlaced = 0;

            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 0;


            // index 0: Aircraft Carrier: OOOOO -- 0
            // index 1: Battleship: OOOO -- 1
            // index 2: Cruiser: OOO -- 2
            // index 3: Destroyers: OO  OO  -- 3 / 4
            // index 4: Submarines: O  O -- 5 / 6

            while (shipsPlaced < 7)
            {
                if (shipsPlaced == 0)
                {
                    startX = rand.Next(0, 8);
                    startY = rand.Next(0, 8);

                    if (startX > 4)
                    {
                        endX = startX;
                        endY = startY + 4;
                    }
                    else
                    {
                        endX = startX + 4;
                        endY = startY;
                    }
                }
                if (shipsPlaced == 1)
                {
                    startX = rand.Next(0, 8);
                    startY = rand.Next(0, 8);

                    if (startX > 5)
                    {
                        endX = startX;
                        endY = startY + 3;
                    }
                    else
                    {
                        endX = startX + 3;
                        endY = startY;
                    }
                }
                if (shipsPlaced == 2)
                {
                    startX = rand.Next(0, 8);
                    startY = rand.Next(0, 8);

                    if (startX > 6)
                    {
                        endX = startX;
                        endY = startY + 2;
                    }
                    else
                    {
                        endX = startX + 2;
                        endY = startY;
                    }
                }
                if (shipsPlaced == 3 | shipsPlaced == 4)
                {
                    startX = rand.Next(0, 8);
                    startY = rand.Next(0, 8);

                    if (startX > 8)
                    {
                        endX = startX;
                        endY = startY + 1;
                    }
                    else
                    {
                        endX = startX + 1;
                        endY = startY;
                    }
                }
                if (shipsPlaced == 5 | shipsPlaced == 6)
                {
                    startX = rand.Next(0, 8);
                    startY = rand.Next(0, 8);
                    endX = startX;
                    endY = startY;
                }

                if (startX == endX || startY == endY)
                {
                    if (PositionFree(startX, startY, endX, endY, board))
                    {

                        if (PlacedShips(startX, startY, endX, endY, ref typeOfShipPlaced))
                        {
                            PlaceShip(startX, startY, endX, endY, board);
                            shipsPlaced++;
                        }
                    }
                }

            }
            return board;
        }

        static int[,] SetupGame()
        {
            int shipsPlaced = 0;
            int[,] board = new int[9, 9];
            int[] typeOfShipPlaced = new int[5];

            while (shipsPlaced < 7)
            {
                PrintBoard(board);
                ShowShips(typeOfShipPlaced);
                Console.WriteLine("Place ships");
                Console.WriteLine("Type coordinates for FRONT of ship: ");
                Console.WriteLine("x: ");
                int startX = Convert.ToInt32(Console.ReadLine()) - 1;

                Console.WriteLine("y: ");
                int startY = Convert.ToInt32(Console.ReadLine()) - 1;

                Console.WriteLine("Type coordinates for END of ship: ");
                Console.WriteLine("x: ");
                int endX = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.WriteLine("y: ");
                int endY = Convert.ToInt32(Console.ReadLine()) - 1;

                if (startX == endX || startY == endY)
                {
                    if (PositionFree(startX, startY, endX, endY, board))
                    {

                        if (PlacedShips(startX, startY, endX, endY, ref typeOfShipPlaced))
                        {
                            PlaceShip(startX, startY, endX, endY, board);
                            shipsPlaced++;
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("All ships of this size are placed!");
                            Console.WriteLine("");
                        }
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Ships are not allowed to overlap!");
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Ship cannot be diagonally placed!");
                    Console.WriteLine("");
                }
            }
            return board;
        }



        static bool PlacedShips(int startX, int startY, int endX, int endY, ref int[] typeOfShipPlaced)
        {
            bool shipCanBePlaced = false;
            // Aircraft Carrier: OOOOO
            if ((endX - startX + 1 == 5 || endY - startY + 1 == 5) && typeOfShipPlaced[0] == 0)
            {
                typeOfShipPlaced[0] = 1;
                shipCanBePlaced = true;
            }

            // Battleship: OOOO
            if ((endX - startX + 1 == 4 || endY - startY + 1 == 4) && typeOfShipPlaced[1] == 0)
            {
                typeOfShipPlaced[1] = 1;
                shipCanBePlaced = true;
            }

            // Cruiser: OOO
            if ((endX - startX + 1 == 3 || endY - startY + 1 == 3) && typeOfShipPlaced[2] == 0)
            {
                typeOfShipPlaced[2] = 1;
                shipCanBePlaced = true;
            }

            // Destroyers: OO  OO
            if ((endX - startX + 1 == 2 || endY - startY + 1 == 2) && typeOfShipPlaced[3] < 2)
            {
                typeOfShipPlaced[3] += 1;
                shipCanBePlaced = true;
            }

            // Submarines: O  O
            if ((endX - startX == 0 && endY - startY == 0) && typeOfShipPlaced[4] < 2 && !shipCanBePlaced)
            {
                typeOfShipPlaced[4] += 1;
                shipCanBePlaced = true;
            }
            return shipCanBePlaced;

        }

        static void ShowShips(int[] typeOfShipPlaced)
        {
            // index 0: Aircraft Carrier: OOOOO
            // index 1: Battleship: OOOO
            // index 2: Cruiser: OOO
            // index 3: Destroyers: OO  OO
            // index 4: Submarines: O  O

            Console.WriteLine("");
            for (int i = 0; i < typeOfShipPlaced.Length; i++)
            {
                if (typeOfShipPlaced[i] == 0 && i == 0)
                {
                    Console.WriteLine("Aircraft Carrier: OOOOO");
                }
                else if (typeOfShipPlaced[i] == 1 && i == 0)
                {
                    Console.WriteLine("Aircraft Carrier: X");
                }

                if (typeOfShipPlaced[i] == 0 && i == 1)
                {
                    Console.WriteLine("Battleship: OOOO");
                }
                else if (typeOfShipPlaced[i] == 1 && i == 1)
                {
                    Console.WriteLine("Battleship: X");
                }

                if (typeOfShipPlaced[i] == 0 && i == 2)
                {
                    Console.WriteLine("Cruiser: OOO");
                }
                else if (typeOfShipPlaced[i] == 1 && i == 2)
                {
                    Console.WriteLine("Cruiser: X");
                }

                if (typeOfShipPlaced[i] == 0 && i == 3)
                {
                    Console.WriteLine("Destroyers: OO  OO");
                }

                else if (typeOfShipPlaced[i] == 1 && i == 3)
                {
                    Console.WriteLine("Destroyers: X  OO");
                }
                else if (typeOfShipPlaced[i] == 2 && i == 3)
                {
                    Console.WriteLine("Destroyers: X  X");
                }

                if (typeOfShipPlaced[i] == 0 && i == 4)
                {
                    Console.WriteLine("Submarines: O  O");
                }

                else if (typeOfShipPlaced[i] == 1 && i == 4)
                {
                    Console.WriteLine("Submarines: X  O");
                }
                else if (typeOfShipPlaced[i] == 2 && i == 4)
                {
                    Console.WriteLine("Submarines: X  X");
                }
            }
            Console.WriteLine("");
        }

        static int[,] PlaceShip(int startX, int startY, int endX, int endY, int[,] board)
        {
            if (startX == endX)
            {
                while (startX <= endX)
                {
                    while (startY <= endY)
                    {
                        board[startX, startY] = 1;
                        startY++;
                    }
                    startX++;
                }
            }
            else
            {
                while (startY <= endY)
                {
                    while (startX <= endX)
                    {
                        board[startX, startY] = 1;
                        startX++;
                    }
                    startY++;
                }
            }
            return board;
        }

        static bool PositionFree(int startX, int startY, int endX, int endY, int[,] board)
        {
            bool positionFree = true;

            while (startX <= endX)
            {
                while (startY <= endY)
                {
                    if (board[startX, startY] == 1)
                    {
                        positionFree = false;
                        break;
                    }
                    startY++;
                }
                startX++;
            }
            return positionFree;
        }

        static void PrintBoard(int[,] board)
        {
            Console.WriteLine("   Y  1   2   3   4   5   6   7   8   9   ");
            Console.WriteLine("X  _______________________________________ ");

            for (int row = 0; row < 9; row++)
            {
                Console.WriteLine("  |                                       |");
                Console.Write($"{row + 1} |   ");

                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0) //Sea
                    {
                        Console.Write(".   ");
                    }
                    else if (board[row, col] == 1) //Ship
                    {
                        Console.Write("0   ");
                    }
                    else if (board[row, col] == 2) //Hit
                    {
                        Console.Write("X   ");
                    }
                    else if (board[row, col] == 3) //Miss
                    {
                        Console.Write("o   ");
                    }
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("  |                                       |");
            Console.WriteLine("  |_______________________________________|");
        }

        static void BeginGame(int[,] board)
        {
            Console.WriteLine("");
            PrintBoard(board);
            Console.WriteLine("");
            Console.WriteLine("Begin Game? y/n: ");
            if (Console.ReadLine() == "y")
            {
                Start(board);
            }
        }

        static void Start(int[,] playersBoard)
        {
            int[,] computersBoard = AutoSetup();

            int[,] playersBattleBoard = new int[9, 9];
            int[,] computersBattleBoard = new int[9, 9];
            bool gameOn = true;

            while (gameOn)
            {
                Console.Clear();
                PlayersMove(playersBoard, ref playersBattleBoard, ref computersBoard);
                if (playerHits == 18)
                {
                    gameOn = false;
                }
                Console.Clear();
                ComputersMove(computersBoard, ref computersBattleBoard, ref playersBoard);
                if (computerHits == 18)
                {
                    gameOn = false;
                }
            }
            Console.Clear();

            if (computerHits == 18)
            {
                PrintBoard(computersBattleBoard);
                Console.WriteLine("");
                Console.WriteLine("COMPUTER WINS!");
            }
            else
            {
                PrintBoard(playersBattleBoard);
                Console.WriteLine("");
                Console.WriteLine("PLAYER WINS!");
            }
        }

        static void PlayersMove(int[,] playersBoard, ref int[,] playersBattleBoard, ref int[,] computersBoard)
        {
            Console.WriteLine("Your Ships: ");
            PrintBoard(playersBoard);

            Console.WriteLine("");
            Console.WriteLine("Your Battle Sea:");
            PrintBoard(playersBattleBoard);

            Console.WriteLine("");
            Console.WriteLine($"Computer Score: {computerHits}");
            Console.WriteLine($"  Player Score: {playerHits}");
            Console.WriteLine("");
            Console.WriteLine("Type x and y to fire");

            Console.WriteLine("x: ");
            int x = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.WriteLine("y: ");
            int y = Convert.ToInt32(Console.ReadLine()) - 1;

            if (computersBoard[x, y] == 1)
            {
                playersBattleBoard[x, y] = 2; //HIT
                computersBoard[x, y] = 2;
                playerHits++;
            }
            else
            {
                playersBattleBoard[x, y] = 3; //MISS
            }
        }

        static void ComputersMove(int[,] computersBoard, ref int[,] computersBattleBoard, ref int[,] playersBoard)
        {
            Random rand = new Random();

            bool moveSuccess = false;

            while (!moveSuccess)
            {
                int x = rand.Next(0, 9);
                int y = rand.Next(0, 9);

                if (computersBattleBoard[x, y] == 0)
                {
                    if (playersBoard[x, y] == 1)
                    {
                        computersBattleBoard[x, y] = 2; // Hit
                        playersBoard[x, y] = 2;
                        computerHits++;
                    }
                    if (playersBoard[x, y] == 0)
                    {
                        computersBattleBoard[x, y] = 3; // Miss
                        playersBoard[x, y] = 3;
                    }
                    moveSuccess = true;
                }
            }
        }

        static void AutoPlayersMove(int[,] playersBoard, ref int[,] playersBattleBoard, ref int[,] computersBoard)
        {
            Console.WriteLine("Your Ships: ");
            PrintBoard(playersBoard);

            Console.WriteLine("");
            Console.WriteLine("Your Battle Sea:");
            PrintBoard(playersBattleBoard);

            Console.WriteLine("");
            Console.WriteLine($"Computer Score: {computerHits}");
            Console.WriteLine($"  Player Score: {playerHits}");
            Console.WriteLine("");

            Console.ReadKey();

            Random rand = new Random();

            bool moveSuccess = false;

            while (!moveSuccess)
            {
                int x = rand.Next(0, 9);
                int y = rand.Next(0, 9);

                if (playersBattleBoard[x, y] == 0)
                {
                    if (playersBoard[x, y] == 1)
                    {
                        playersBattleBoard[x, y] = 2; // Hit
                        playersBoard[x, y] = 2;
                        playerHits++;
                    }
                    if (playersBoard[x, y] == 0)
                    {
                        playersBattleBoard[x, y] = 3; // Miss
                        playersBoard[x, y] = 3;
                    }
                    moveSuccess = true;
                }
            }
        }
    }
}
