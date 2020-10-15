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
            // Variables and fun shitz
            var roll = new Random();
            int firstRoll = roll.Next(0, 11);
            int secondRoll = roll.Next(0, 11 - firstRoll);
            int secondRollLastFrame = roll.Next(0, 11);
            int thirdRoll = roll.Next(0, 11);
            int sumOfRolls1 = firstRoll + secondRoll;
            int sumOfFrame10 = firstRoll + secondRoll + thirdRoll;

            // Creating one jagged array with 10 arrays.
            int[][] jaggedArray = new int[10][];
            int[][] pointsGained = new int[10][];
            int[][] frameScore = new int[10][];

            // Creating jagged arrays.
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                jaggedArray[i] = new int[2];
                pointsGained[i] = new int[1];
                frameScore[i] = new int[1];
            }

            // Roll and put the results in the arrays.
            for (int n = 0; n < jaggedArray.Length - 1; n++)
            {
                for (int k = 0; k < 1; k++)
                {
                    int roll1 = roll.Next(0, 11);
                    int roll2 = roll.Next(0, 11 - roll1);
                    int sumOfRolls = roll1 + roll2;
                    jaggedArray[n][0] = roll1;
                    jaggedArray[n][1] = roll2;
                    pointsGained[n][0] = sumOfRolls;
                    frameScore[n][k] = pointsGained[0][0] + pointsGained[1][0] + pointsGained[2][0] + pointsGained[3][0] + pointsGained[4][0] + pointsGained[5][0] + pointsGained[6][0] + pointsGained[7][0] + pointsGained[8][0];
                    bool strike = roll1 == 10;
                    bool spare = sumOfRolls == 10;

                    if (strike)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("First roll: X");
                        Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                        Console.WriteLine($"Points gained: {pointsGained[n][k]}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[n][k]}");
                        Console.WriteLine();
                    }
                    else if (roll1 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"First roll: -");
                        if (roll2 == 0)
                        {
                            Console.WriteLine($"Second roll : -");
                            Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                            Console.WriteLine($"Points gained: {pointsGained[n][k]}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Frame score: {frameScore[n][k]}");
                            Console.WriteLine();
                        }
                        else if (roll2 == 10)
                        {
                            Console.WriteLine($"Second roll : /");
                            Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                            Console.WriteLine($"Points gained: {pointsGained[n][k]}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Frame score: {frameScore[n][k]}");
                            Console.WriteLine();
                        }
                        else if (roll2 > 0)
                        {
                            Console.WriteLine($"Second roll : {jaggedArray[n][k + 1]}");
                            Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                            Console.WriteLine($"Points gained: {pointsGained[n][k]}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Frame score: {frameScore[n][k]}");
                            Console.WriteLine();
                        }
                    }
                    else if (roll1 > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"First roll: {jaggedArray[n][k]}");
                        if (roll2 == 0)
                        {
                            Console.WriteLine($"Second roll : -");
                            Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                            Console.WriteLine($"Points gained: {pointsGained[n][k]}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Frame score: {frameScore[n][k]}");
                            Console.WriteLine();
                        }
                        else if (spare)
                        {
                            Console.WriteLine($"Second roll : /");
                            Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                            Console.WriteLine($"Points gained: {pointsGained[n][k]}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Frame score: {frameScore[n][k]}");
                            Console.WriteLine();
                        }
                        else if (roll2 > 0)
                        {
                            Console.WriteLine($"Second roll : {jaggedArray[n][k + 1]}");
                            Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                            Console.WriteLine($"Points gained: {pointsGained[n][k]}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Frame score: {frameScore[n][k]}");
                            Console.WriteLine();
                        }
                    }


                    else if (sumOfRolls == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"First roll: -");
                        Console.WriteLine($"Second roll : -");
                        Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                        Console.WriteLine($"Points gained: {pointsGained[n][k]}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[n][k]}");
                        Console.WriteLine();
                    }

                    else if (sumOfRolls == 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"First roll: {jaggedArray[n][k]}");
                        Console.WriteLine($"Second roll : /");
                        Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                        Console.WriteLine($"Points gained: {pointsGained[n][k]}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[n][k]}");
                        Console.WriteLine();
                    }
                    else if (sumOfRolls > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"FRAME {n + 1}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"First roll: {jaggedArray[n][k]}");
                        jaggedArray[n][1] = roll2;
                        Console.WriteLine($"Second roll : {jaggedArray[n][k + 1]}");
                        Console.WriteLine($"Knocked down pins: {sumOfRolls}");
                        Console.WriteLine($"Points gained: {pointsGained[n][k]}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[n][k]}");
                        Console.WriteLine();
                    }

                }

            }

            // Last frame. Test to see if it works with three throws
            pointsGained[9][0] = firstRoll + secondRoll;
            frameScore[9][0] = pointsGained[0][0] + pointsGained[1][0] + pointsGained[2][0] + pointsGained[3][0] + pointsGained[4][0] + pointsGained[5][0] + pointsGained[6][0] + pointsGained[7][0] + pointsGained[8][0] + pointsGained[9][0];
            if (firstRoll == 10)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"FRAME 10");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"First roll: X");
                if (secondRollLastFrame == 10)
                {
                    Console.WriteLine($"Second roll: X");

                    if (thirdRoll == 10)
                    {
                        Console.WriteLine("Third roll: X");
                        Console.WriteLine($"Knocked down pins: {firstRoll + secondRollLastFrame + thirdRoll}");
                        Console.WriteLine($"Points gained: {pointsGained[9][0] - secondRoll + secondRollLastFrame + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                    else if (thirdRoll == 0)
                    {
                        Console.WriteLine("Third roll: -");
                        Console.WriteLine($"Knocked down pins: {firstRoll + secondRollLastFrame + thirdRoll}");
                        Console.WriteLine($"Points gained: {pointsGained[9][0] - secondRoll + secondRollLastFrame + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($"Third roll: {firstRoll}");
                        Console.WriteLine($"Knocked down pins: {firstRoll + secondRollLastFrame + thirdRoll}");
                        Console.WriteLine($"Points gained: {pointsGained[9][0] - secondRoll + secondRollLastFrame + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                }
                else if (secondRollLastFrame == 0)
                {
                    Console.WriteLine($"Second roll: -");
                    if (thirdRoll == 0)
                    {
                        Console.WriteLine($"Third roll: -");
                        Console.WriteLine($"Knocked down pins: {firstRoll + secondRollLastFrame + thirdRoll}");
                        Console.WriteLine($"Points gained: {pointsGained[9][0] - secondRoll + secondRollLastFrame + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                    else if (thirdRoll == 10)
                    {
                        Console.WriteLine($"Third roll: X");
                        Console.WriteLine($"Knocked down pins: {firstRoll + secondRollLastFrame + thirdRoll}");
                        Console.WriteLine($"Points gained: {pointsGained[9][0] - secondRoll + secondRollLastFrame + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                    else if (thirdRoll < 10)
                    {
                        Console.WriteLine($"Third roll: {thirdRoll}");
                        Console.WriteLine($"Knocked down pins: {firstRoll + secondRollLastFrame + thirdRoll}");
                        Console.WriteLine($"Points gained: {pointsGained[9][0] - secondRoll + secondRollLastFrame + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                }
                else if (secondRollLastFrame > 0 && secondRollLastFrame < 10)
                {
                    Console.WriteLine($"Second roll: {secondRollLastFrame}");
                    Console.WriteLine($"Third roll: test{thirdRoll}");
                    Console.WriteLine($"Knocked down pins: {firstRoll + secondRollLastFrame + thirdRoll}");
                    Console.WriteLine($"Points gained: {pointsGained[9][0] - secondRoll + secondRollLastFrame + thirdRoll}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Frame score: {frameScore[9][0]}");
                    Console.WriteLine();
                }
            }

            else if (firstRoll == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"FRAME 10");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"First roll: -");
                if (sumOfRolls1 == 10)
                {
                    Console.WriteLine($"Second roll: /");
                    if (thirdRoll == 10)
                    {
                        Console.WriteLine($"Third roll: X");
                        Console.WriteLine($"Knocked down pins: {sumOfFrame10}");
                        Console.WriteLine($"Points gained: {pointsGained[9][0] + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                    else if (thirdRoll == 0)
                    {
                        Console.WriteLine($"Third roll: -");
                        Console.WriteLine($"Knocked down pins: {sumOfFrame10}");
                        Console.WriteLine($"Points gained: {pointsGained[9][0] + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                    else if (thirdRoll < 10)
                    {
                        Console.WriteLine($"Third roll: {thirdRoll}");
                        Console.WriteLine($"Knocked down pins: {sumOfFrame10}");
                        Console.WriteLine($"Points gained: {pointsGained[9][0] + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                }
                else if (secondRoll == 0)
                {
                    Console.WriteLine($"Second roll: -");
                    Console.WriteLine($"Knocked down pins: {sumOfRolls1}");
                    Console.WriteLine($"Points gained: {pointsGained[9][0]}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Frame score: {frameScore[9][0]}");
                    Console.WriteLine();
                }
                else if (secondRoll > 0)
                {
                    Console.WriteLine($"Second roll: {secondRoll}");
                    Console.WriteLine($"Knocked down pins: {sumOfRolls1}");
                    Console.WriteLine($"Points gained: {pointsGained[9][0]}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Frame score: {frameScore[9][0]}");
                    Console.WriteLine();
                }
                

                
                
            }

            else if (firstRoll < 10)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"FRAME 10");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"First roll: {firstRoll}");
                if (sumOfRolls1 == 10)
                {
                    Console.WriteLine($"Second roll: /");
                    if (thirdRoll == 10)
                    {
                        Console.WriteLine($"Third roll: X");
                        Console.WriteLine($"Knocked down pins: {sumOfFrame10}");
                        Console.WriteLine($"Points gained: {pointsGained[1][0] + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                    else if (thirdRoll == 0)
                    {
                        Console.WriteLine($"Third roll: -");
                        Console.WriteLine($"Knocked down pins: {sumOfFrame10}");
                        Console.WriteLine($"Points gained: {pointsGained[1][0] + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($"Third roll: {thirdRoll}");
                        Console.WriteLine($"Knocked down pins: {sumOfFrame10}");
                        Console.WriteLine($"Points gained: {pointsGained[1][0] + thirdRoll}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Frame score: {frameScore[9][0]}");
                        Console.WriteLine();
                    }
                }
                else if (secondRoll == 0)
                {
                    Console.WriteLine($"Second roll: -");
                    Console.WriteLine($"Knocked down pins: {sumOfRolls1}");
                    Console.WriteLine($"Points gained: {pointsGained[9][0]}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Frame score: {frameScore[9][0]}");
                    Console.WriteLine();
                }
                else if (secondRoll < 10)
                {
                    Console.WriteLine($"Second roll: {secondRoll}");
                    Console.WriteLine($"Knocked down pins: {sumOfRolls1}");
                    Console.WriteLine($"Points gained: {pointsGained[9][0]}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Frame score: {frameScore[9][0]}");
                    Console.WriteLine();
                }
            }

            for (int i = 0; i < frameScore.Length; i++)
            {
                
                Console.WriteLine($"{pointsGained[i][0]}");
                Console.WriteLine($"{frameScore[i][0]}");
                Console.WriteLine("");
                
            }
        }
    }
}





// Points gained and Frame score
/*
 * 
 * bool strike = if strike, take the 2 next rolls in the next frame and add it to the current frame. || frameScore[current][0] + pointsGained[current + 1][current + 1];
 * bool spare = if spare, take the next roll in the next frame and add it to the current frame. || frameScore[current][0] + pointsGained[current + 1][0];
 * 
 * frameScore[current][0] + frameScore[current + 1][0]; || Won't work cuz in the "for" loop the current "+ 1" does not yet exist.
 * 
 * 1. Roll all before checking for strikes and spares.
 *       - Got a strike? Add the next two rolls to that score.
 *       - Got a spare? Add the next roll to that score.
 * 
 * 2. Roll and if you get a strike or spare wait till next rolls then add score by using (if) statement.
 *       - Got a strike? Hold and wait for the next two rolls and then add to the strike score. (continue;?)
 *       - Got a spare? Hold and wait for the next roll and then add to the spare score. (continue;?)
 *       
 *       
 *       
 * if (strike)
 *  return a value of 0 to frameScore
 *  
 *  Next frame
 *  6 | 2
 *  Gained: 8
 *  Framescore: 8
 *  frameScore[n - 1][0] + 18 (10 + 8);
 */
