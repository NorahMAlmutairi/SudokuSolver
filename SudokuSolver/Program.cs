
using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace Sudoku
    {
        class Program
        {
            //By Taif
            static int[,] FillTable()
            {
                int[,] table = new int[9, 9]; // Array is filled with zero by default
                Console.WriteLine("Can I get to know you? , please enter your name: ");
                string userName = Console.ReadLine();
                Console.WriteLine("Hello " + userName + " nice to meet you.. Enter the puzzle that you want me to solve... ^^");
                while (true)
                {
                    table = PuzzleInput(table);
                    if (!isValid(table)) // to check if the table is valid or not
                    {
                        PrintTable(table);
                        Console.WriteLine("Invalid table. Resetting ...");
                        table = new int[9, 9]; // if it's not valid create a new table
                    }
                    else
                    {
                        Console.WriteLine("You need to enter more (yes or no): ");
                        if (Console.ReadLine() == "no") break;
                    }
                }
                return table;
            }
            //By Rawabe
            static bool isValid(int[,] table) // check if the entered number is unique
            {
                // iterate over each number in the table
                for (int row = 0; row < 9; row++)
                    for (int col = 0; col < 9; col++)
                    {
                        int num = table[row, col];
                        if (num > 0)
                        { // if there is a number greater than 0 check it
                          //iterate each row, column, and subgrid
                            for (int k = 0; k < 9; k++)
                            {
                                // Check if a two similar numbers exist in the same row
                                if (num == table[row, k] && k != col)
                                    return false;
                                // Check if a two similar numbers exist in the same column
                                else if (num == table[k, col] && k != row)
                                    return false;
                                // Check if a two similar numbers exist in the same subgrid
                                else if (((row - row % 3) + (k / 3) != row && (col - col % 3) + (k % 3) != col) &&
                                    num == table[(row - (row % 3)) + (k / 3), (col - col % 3) + (k % 3)])
                                    return false;
                            }
                        }
                    }
                return true;
            }
            //By Taif
            static int[,] PuzzleInput(int[,] table)
            {
                try
                {
                    PrintTable(table);
                    Console.Write("Enter row number: ");
                    int row = int.Parse(Console.ReadLine());
                    while (row <= 0 || row > 9)
                    {
                        Console.Write("Enter row number again: ");
                        row = int.Parse(Console.ReadLine());
                    }
                    Console.Write("Enter colum number: ");
                    int col = int.Parse(Console.ReadLine());
                    while (col <= 0 || col > 9)
                    {
                        Console.Write("Enter column number again: ");
                        col = int.Parse(Console.ReadLine());
                    }
                    Console.Write("Enter cell value: ");
                    int val = int.Parse(Console.ReadLine());
                    while (val <= 0 || val > 9)
                    {
                        Console.Write("Enter cell value again: ");
                        val = int.Parse(Console.ReadLine());
                    }
                    table[row - 1, col - 1] = val; // -1 because array start from 0
                }
                catch (FormatException fexFormate) //print error meassage if the user enter space or char
                {
                    Console.WriteLine(fexFormate.Message);
                }
                return table;
            }
            //Rawabe
            static void PrintTable(int[,] table)
            {
                Console.WriteLine("");
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                        Console.Write($"{table[i, j]} ");
                    Console.WriteLine();
                }
            }
            //Norah
            static int[] GetPositionPossibleNumbers(int[,] table, int row, int col)
            {
                List<int> possibleNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                // Remove every number in the same row or column from the possible numbers.
                for (int i = 0; i < 9; i++)
                {
                    possibleNumbers.Remove(table[row, i]);
                    possibleNumbers.Remove(table[i, col]);
                }
                // remove every number in the same subgrid from the possible numbers.
                for (int i = 0; i < 9; i++)
                    possibleNumbers.Remove(table[(row - row % 3) + (i / 3), (col - col % 3) + (i % 3)]);
                return possibleNumbers.ToArray();
            }
            //Norah
            static bool Solve(int[,] table)
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        if (table[row, col] == 0)//check if index is empty
                        {
                            int[] possibleNumbers = GetPositionPossibleNumbers(table, row, col);
                            if (possibleNumbers.Length == 0) // if array is empty return false
                            {
                                return false;
                            }
                            else
                            {
                                foreach (var possibleNumber in possibleNumbers)
                                {
                                    int result = table[row, col];
                                    // PrintTable(table); // method that prints out the computer solution and the steps in detail
                                    table[row, col] = possibleNumber;
                                    // Recursive calls
                                    if (col == 8 && row == 8) return true; // iterated the whole table with valid values
                                    else if (col == 8 && Solve(table)) return true;
                                    else if (Solve(table)) return true;
                                    table[row, col] = result;
                                }
                                return false;
                            }
                        }
                    }
                }
                return false;
            }
            //By All
            static void Main(string[] args)
            {
                int[,] table = FillTable(); // Fill the table with the entered value from user and store it in the 2d array of table
                                            // now the table is filled and valid
                if (Solve(table))     // fill the empty indexs and solve the table
                {
                    Console.WriteLine("\n Solution:");
                    PrintTable(table);
                    Console.WriteLine("\n <<<<< The puzzle is solved >>>>>");
                }
                else
                {
                    Console.WriteLine("Could not solve this Sudoku. ><");
                }
            }
        }
    }