using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Full_bowling_score_sheet
{
    class Program
    {
        static void Main(string[] args)
        {
            var roll = new Random();
            // Creating one jagged array with 10 arrays.
            int[][] jaggedArray = new int[10][];

            // Creating 10 different arrays.
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                jaggedArray[i] = new int[2];
            }

            // Roll and put the results in the arrays.
            for (int n = 0; n < jaggedArray.Length; n++)
            {
                for (int k = 0; k < 1; k++)
                {
                    int roll1 = roll.Next(0, 11);
                    int roll2 = roll.Next(0, 11 - roll1);
                    int sumOfRolls = roll1 + roll2;
                    jaggedArray[n][0] = roll1;
                    if (roll1 == 10)
                    {
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.WriteLine("First roll: X");
                        Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                        Console.WriteLine();
                    }
                    else if (roll1 == 0)
                    {
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.WriteLine($"First roll: -");
                        jaggedArray[n][1] = roll2;
                        if (roll2 == 0)
                        {
                            Console.WriteLine($"Second roll : -");
                            Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                            Console.WriteLine();
                        }
                        else if (roll2 > 0)
                        {
                            Console.WriteLine($"Second roll : {jaggedArray[n][k + 1]}");
                            Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                            Console.WriteLine();
                        }
                    }
                    else if (roll2 == 0)
                    {
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.WriteLine($"First roll: {jaggedArray[n][k]}");
                        jaggedArray[n][1] = roll2;
                        Console.WriteLine($"Second roll : -");
                        Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                        Console.WriteLine();
                    }
                    else if (sumOfRolls == 0)
                    {
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.WriteLine($"First roll: -");
                        jaggedArray[n][1] = roll2;
                        Console.WriteLine($"Second roll : -");
                        Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                        Console.WriteLine();
                    }
                    else if (sumOfRolls < 10)
                    {
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.WriteLine($"First roll: {jaggedArray[n][k]}");
                        jaggedArray[n][1] = roll2;
                        Console.WriteLine($"Second roll : {jaggedArray[n][k + 1]}");
                        Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                        Console.WriteLine();
                    }
                    
                    else if (sumOfRolls == 10)
                    {
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.WriteLine($"First roll: {jaggedArray[n][k]}");
                        Console.WriteLine($"Second roll : /");
                        Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                        Console.WriteLine();
                    }
                    
                }

            }
        }
    }
}
