using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

namespace Full_bowling_score_sheet
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Score Calculator
            var roll = new Random();
            // Creating one jagged array with 10 arrays.
            var rolls = new int[10][];
            var pointsGained = new int[10];
            var frameScore = new int[10];

            // Creating jagged arrays.
            for (int i = 0; i < rolls.Length; i++)
            {
                rolls[i] = new int[2];
            }

            // Calculating the rolls
            for (int frameIndex = 0; frameIndex < 10; frameIndex++)
            {
                if (frameIndex == 9)
                {
                    int roll1 = roll.Next(0, 11);
                    if (roll1 == 10)
                    {
                        //Strike
                        int roll2 = roll.Next(0, 11);
                        if (roll2 == 10)
                        {
                            int roll3 = roll.Next(0, 11);
                            rolls[9] = new int[] { roll1, roll2, roll3 };
                        }
                        else
                        {
                            //Not strike
                            int roll3 = roll.Next(0, 11 - roll2);
                            rolls[9] = new int[] { roll1, roll2, roll3 };
                        }
                    }
                    else
                    {
                        //Not a strike
                        int roll2 = roll.Next(0, 11 - roll1);
                        if (roll1 + roll2 == 10)
                        {
                            // Spare
                            int roll3 = roll.Next(0, 11);
                            rolls[9] = new int[] { roll1, roll2, roll3 };
                        }
                        else
                        {
                            rolls[9] = new int[] { roll1, roll2 };
                        }
                    }
                }
                else
                {
                    int roll1 = roll.Next(0, 11);
                    int roll2 = roll.Next(0, 11 - roll1);
                    rolls[frameIndex][0] = roll1;
                    rolls[frameIndex][1] = roll2;
                }
            }

            // Writing out the results
            for (int frameIndex = 0; frameIndex < 10; frameIndex++)
            {


                int[] currentFrameRolls = rolls[frameIndex];
                int roll1 = currentFrameRolls[0];
                int roll2 = currentFrameRolls[1];

                if (frameIndex == 9 || roll1 + roll2 < 10)
                {
                    int sum = 0;
                    for (int i = 0; i < currentFrameRolls.Length; i++)
                    {
                        sum += currentFrameRolls[i];
                    }
                    pointsGained[frameIndex] = sum;
                }

                else if (roll1 == 10)
                {
                    // Strike
                    if (rolls[frameIndex + 1][0] == 10 && frameIndex < 8)
                    {
                        pointsGained[frameIndex] = 20 + rolls[frameIndex + 2][0];
                    }
                    else
                    {
                        pointsGained[frameIndex] = 10 + rolls[frameIndex + 1][0] + rolls[frameIndex + 1][1];
                    }
                }

                else
                {
                    // Spare
                    pointsGained[frameIndex] = 10 + rolls[frameIndex + 1][0];
                }

                if (frameIndex == 0)
                {
                    frameScore[0] = pointsGained[0];
                }
                else
                {
                    frameScore[frameIndex] = pointsGained[frameIndex] + frameScore[frameIndex - 1];

                }
            }
            // Writing out the results for last frame
            for (int frameIndex = 0; frameIndex < 10; frameIndex++)
            {
                int[] currentFrameRolls = rolls[frameIndex];
                int roll1 = currentFrameRolls[0];
                int roll2 = currentFrameRolls[1];

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"FRAME {frameIndex + 1}");
                Console.ForegroundColor = ConsoleColor.White;

                // Last Frame
                if (frameIndex == 9)
                {
                    if (roll1 == 0)
                    {
                        Console.WriteLine("First roll: -");

                        if (roll2 == 0)
                        {
                            Console.WriteLine("Second roll: -");
                        }
                        else if (roll2 == 10)
                        {
                            Console.WriteLine("Second roll: /");

                            int roll3 = currentFrameRolls[2];
                            if (roll3 == 0)
                            {
                                Console.WriteLine("Third roll: -");
                            }
                            else if (roll3 == 10)
                            {
                                Console.WriteLine("Third roll: X");
                            }
                            else
                            {
                                Console.WriteLine($"Third roll: {roll3}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Second roll: {roll2}");
                        }
                    }
                    else if (roll1 == 10)
                    {
                        Console.WriteLine("First roll: X");

                        if (roll2 == 10)
                        {
                            Console.WriteLine("Second roll: X");

                            int roll3 = currentFrameRolls[2];
                            if (roll3 == 10)
                            {
                                Console.WriteLine("Third roll: X");
                            }
                            else if (roll3 == 0)
                            {
                                Console.WriteLine("Third roll: -");
                            }
                            else
                            {
                                Console.WriteLine($"Third roll: {roll3}");
                            }
                        }
                        else if (roll2 == 0)
                        {
                            Console.WriteLine("Second roll: -");

                            int roll3 = currentFrameRolls[2];
                            if (roll3 == 0)
                            {
                                Console.WriteLine("Third roll: -");
                            }
                            else if (roll3 == 10)
                            {
                                Console.WriteLine("Third roll: /");
                            }
                            else
                            {
                                Console.WriteLine($"Third roll: {roll3}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Second roll: {roll2}");

                            int roll3 = currentFrameRolls[2];
                            if (roll3 == 10)
                            {
                                Console.WriteLine("Third roll: /");
                            }
                            else if (roll3 == 0)
                            {
                                Console.WriteLine("Third roll: -");
                            }
                            else
                            {
                                Console.WriteLine($"Third roll: {roll3}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"First roll: {roll1}");

                        if (roll2 == 0)
                        {
                            Console.WriteLine($"Second roll: -");
                        }
                        else if (roll1 + roll2 == 10)
                        {
                            Console.WriteLine($"Second roll: /");

                            int roll3 = currentFrameRolls[2];
                            if (roll3 == 0)
                            {
                                Console.WriteLine($"Third roll: -");
                            }
                            else if (roll3 == 10)
                            {
                                Console.WriteLine($"Third roll: X");
                            }
                            else
                            {
                                Console.WriteLine($"Third roll: {roll3}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Second roll: {roll2}");
                        }
                    }
                }

                // Frame 1 - 9
                else
                {
                    if (roll1 == 0)
                    {
                        Console.WriteLine("First roll: -");

                        if (roll2 == 0)
                        {
                            Console.WriteLine("Second roll: -");
                        }
                        else if (roll2 == 10)
                        {
                            Console.WriteLine("Second roll: /");
                        }
                        else
                        {
                            Console.WriteLine($"Second roll: {roll2}");
                        }
                    }

                    else if (roll1 == 10)
                    {
                        Console.WriteLine("First roll: X");

                        if (roll2 == 10)
                        {
                            Console.WriteLine("Second roll: X");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"First roll: {roll1}");

                        if (roll2 == 0)
                        {
                            Console.WriteLine("Second roll: -");
                        }
                        else if (roll1 + roll2 == 10)
                        {
                            Console.WriteLine("Second roll: /");
                        }
                        else
                        {
                            Console.WriteLine($"Second roll: {roll2}");
                        }
                    }
                }

                if (frameIndex < 9)
                {
                    Console.WriteLine($"Knocked down pins: {roll1 + roll2}");
                    Console.WriteLine($"Points gained: {pointsGained[frameIndex]}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Frame score: {frameScore[frameIndex]}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Knocked down pins: {pointsGained[9]}");
                    Console.WriteLine($"Points gained: {pointsGained[frameIndex]}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Frame score: {frameScore[frameIndex]}");
                    Console.WriteLine();
                }
            }
            #endregion

            #region Border
            // Draw top border
            Console.Write("┌─┬─┬─");
            for (int frameIndex = 1; frameIndex < 10; frameIndex++)
            {
                Console.Write("┬─┬─┬─");
                if (frameIndex == 9) Console.Write("┬─");
            }
            Console.WriteLine("┐");

            //Draw middle with score
            Console.Write("|");
            for (int frameIndex = 1; frameIndex < 10; frameIndex++)
            {
                Console.Write($" |{rolls[frameIndex - 1][0]}|{rolls[frameIndex - 1][1]}|");
                if (frameIndex == 9) Console.Write($" │{rolls[9][0]}│{rolls[9][1]}│ ");
            }
            Console.WriteLine("|");

            Console.Write("|");
            for (int frameIndex = 1; frameIndex < 10; frameIndex++)
            {
                Console.Write(" └─┴─┤");
                if (frameIndex == 9) Console.Write(" └─┴─┴");
            }
            Console.WriteLine("─┤");

            Console.Write("│");
            for (int frameIndex = 1; frameIndex < 10; frameIndex++)
            {
                Console.Write("     |");
                if (frameIndex == 9) Console.Write("       ");
            }
            Console.WriteLine("|");

            // Draw bottom border
            Console.Write("└─────");
            for (int frameIndex = 1; frameIndex < 10; frameIndex++)
            {
                Console.Write("┴─────");
                if (frameIndex == 9) Console.Write("──");
            }
            Console.WriteLine("┘");
            #endregion
        }
    }
}