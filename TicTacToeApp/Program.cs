using System;

namespace TicTacToe
{
    class Program
    {
        static char[,] board = new char[3, 3];
        static char currentPlayer = 'X';

        static void Main(string[] args)
        {
            InitializeBoard();

            while (true)
            {
                Console.Clear();
                PrintBoard();

                Console.WriteLine($"Player {currentPlayer}, enter your move (row and column: 1 1 for top-left):");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid input, try again.");
                    continue;
                }

                var parts = input.Split(' ');
                if (parts.Length != 2 || 
                    !int.TryParse(parts[0], out int row) || 
                    !int.TryParse(parts[1], out int col) ||
                    row < 1 || row > 3 || col < 1 || col > 3)
                {
                    Console.WriteLine("Please enter two numbers between 1 and 3 separated by a space.");
                    Console.ReadKey();
                    continue;
                }

                row -= 1; 
                col -= 1; 

                if (board[row, col] != ' ')
                {
                    Console.WriteLine("Cell already occupied, try again.");
                    Console.ReadKey();
                    continue;
                }

                board[row, col] = currentPlayer;

                if (CheckWin())
                {
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    break;
                }
                else if (CheckDraw())
                {
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine("It's a draw!");
                    break;
                }

                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }

            Console.WriteLine("Game over. Press any key to exit.");
            Console.ReadKey();
        }

        static void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = ' ';
        }

        static void PrintBoard()
        {
            Console.WriteLine("  1   2   3 ");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + 1);
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($" {board[i, j]} ");
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine(" ---+---+---");
            }
        }

        static bool CheckWin()
        {
            // Check rows and columns
            for (int i = 0; i < 3; i++)
            {
                if (board[i,0] == currentPlayer && board[i,1] == currentPlayer && board[i,2] == currentPlayer)
                    return true;
                if (board[0,i] == currentPlayer && board[1,i] == currentPlayer && board[2,i] == currentPlayer)
                    return true;
            }

            // Check diagonals
            if (board[0,0] == currentPlayer && board[1,1] == currentPlayer && board[2,2] == currentPlayer)
                return true;
            if (board[0,2] == currentPlayer && board[1,1] == currentPlayer && board[2,0] == currentPlayer)
                return true;

            return false;
        }

        static bool CheckDraw()
        {
            foreach (var cell in board)
                if (cell == ' ')
                    return false;
            return true;
        }
    }
}
