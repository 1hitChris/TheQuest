using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling_Drawing
{
    class Program
    {
        static void Main(string[] args)
        {
            var pinsStanding = new List<bool> { true, true, true, true, true, true, true, true, true, true };
            var roll = new Random();
            int firstRoll = roll.Next(1, 11);

            int lane = 1;
            int knockedPinsFirstRoll = 0;
            int knockedPinsSecondRoll = 0;

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Current pins:\n");
                Console.WriteLine($"" +
                    $"{CheckPinStanding(pinsStanding[6])}   {CheckPinStanding(pinsStanding[7])}   {CheckPinStanding(pinsStanding[8])}    {CheckPinStanding(pinsStanding[9])}\n" +
                    $"\n  {CheckPinStanding(pinsStanding[3])}   {CheckPinStanding(pinsStanding[4])}   {CheckPinStanding(pinsStanding[5])}\n" +
                    $"\n    {CheckPinStanding(pinsStanding[1])}   {CheckPinStanding(pinsStanding[2])}\n" +
                    $"\n      {CheckPinStanding(pinsStanding[0])}");
                Console.WriteLine("\n1 2 3 4 5 6 7");
               
                if (i < 2 && knockedPinsFirstRoll < 10)
                {
                    Console.Write($"\nEnter where you roll the ball (1-7): ");
                    string chooseLane = Console.ReadLine();
                    lane = Int32.Parse(chooseLane);

                    //First roll
                    if (i == 0)
                    {
                        knockedPinsFirstRoll = KnockedPins(lane, pinsStanding);
                        Console.WriteLine(knockedPinsFirstRoll + "\n");
                    }

                    // Second roll
                    if (i == 1)
                    {
                        knockedPinsSecondRoll = KnockedPins(lane, pinsStanding);
                        Console.WriteLine(knockedPinsSecondRoll + "\n");
                    }
                }



            }

            static string CheckPinStanding(bool standing)
            {
                string symbol;

                if (standing == true)
                {
                    symbol = "O";
                }
                else
                {
                    symbol = " ";
                }
                return symbol;
            }
            static int KnockedPins(int lane, List<bool> pinsStanding)
            {
                var random = new Random();

                if (lane == 1 && pinsStanding[6] == true)
                {
                    int knockedPinsCount = 1;
                    pinsStanding[6] = false;

                    for (int pinAndBall = 0; pinAndBall < 2; pinAndBall++)
                    {
                        int newPath = random.Next(5);
                        if (newPath < 2)
                        {
                            knockedPinsCount += KnockedPins(lane + 1, pinsStanding);
                        }
                        else if (newPath > 2)
                        {
                            knockedPinsCount += KnockedPins(lane - 1, pinsStanding);
                        }
                        else
                        {
                            knockedPinsCount += KnockedPins(lane, pinsStanding);
                        }
                    }

                    return knockedPinsCount;
                }
               
                else if (lane == 2 && pinsStanding[3] == true)
                {
                    int knockedPinsCount = 1;
                    pinsStanding[3] = false;

                    for (int pinAndBall = 0; pinAndBall < 2; pinAndBall++)
                    {
                        int newPath = random.Next(5);
                        if (newPath < 2)
                        {
                            knockedPinsCount += KnockedPins(lane + 1, pinsStanding);
                        }
                        else if (newPath > 2)
                        {
                            knockedPinsCount += KnockedPins(lane - 1, pinsStanding);
                        }
                        else
                        {
                            knockedPinsCount += KnockedPins(lane, pinsStanding);
                        }
                    }

                    return knockedPinsCount;
                }
                
                else if (lane == 3)
                {
                    if (pinsStanding[1] == true)
                    {
                        int knockedPinsCount = 1;
                        pinsStanding[1] = false;

                        for (int pinAndBall = 0; pinAndBall < 2; pinAndBall++)
                        {
                            int newPath = random.Next(5);
                            if (newPath < 2)
                            {
                                knockedPinsCount += KnockedPins(lane + 1, pinsStanding);
                            }
                            else if (newPath > 2)
                            {
                                knockedPinsCount += KnockedPins(lane - 1, pinsStanding);
                            }
                            else
                            {
                                knockedPinsCount += KnockedPins(lane, pinsStanding);
                            }
                        }

                        return knockedPinsCount;
                    }
                    else if (pinsStanding[7] == true)
                    {
                        int knockedPinsCount = 1;
                        pinsStanding[7] = false;

                        for (int pinAndBall = 0; pinAndBall < 2; pinAndBall++)
                        {
                            int newPath = random.Next(5);
                            if (newPath < 2)
                            {
                                knockedPinsCount += KnockedPins(lane + 1, pinsStanding);
                            }
                            else if (newPath > 2)
                            {
                                knockedPinsCount += KnockedPins(lane - 1, pinsStanding);
                            }
                            else
                            {
                                knockedPinsCount += KnockedPins(lane, pinsStanding);
                            }
                        }

                        return knockedPinsCount;
                    }
                }
                
                else if (lane == 4)
                {
                    if (pinsStanding[0] == true)
                    {
                        int knockedPinsCount = 1;
                        pinsStanding[0] = false;

                        for (int pinAndBall = 0; pinAndBall < 2; pinAndBall++)
                        {
                            int newPath = random.Next(5);
                            if (newPath < 2)
                            {
                                knockedPinsCount += KnockedPins(lane + 1, pinsStanding);
                            }
                            else if (newPath > 2)
                            {
                                knockedPinsCount += KnockedPins(lane - 1, pinsStanding);
                            }
                            else
                            {
                                knockedPinsCount += KnockedPins(lane, pinsStanding);
                            }
                        }

                        return knockedPinsCount;
                    }
                    else if (pinsStanding[4] == true)
                    {
                        int knockedPinsCount = 1;
                        pinsStanding[4] = false;

                        for (int pinAndBall = 0; pinAndBall < 2; pinAndBall++)
                        {
                            int newPath = random.Next(5);
                            if (newPath < 2)
                            {
                                knockedPinsCount += KnockedPins(lane + 1, pinsStanding);
                            }
                            else if (newPath > 2)
                            {
                                knockedPinsCount += KnockedPins(lane - 1, pinsStanding);
                            }
                            else
                            {
                                knockedPinsCount += KnockedPins(lane, pinsStanding);
                            }
                        }

                        return knockedPinsCount;
                    }
                }
                
                else if (lane == 5)
                {
                    if (pinsStanding[2] == true)
                    {
                        int knockedPinsCount = 1;
                        pinsStanding[2] = false;

                        for (int pinAndBall = 0; pinAndBall < 2; pinAndBall++)
                        {
                            int newPath = random.Next(5);
                            if (newPath < 2)
                            {
                                knockedPinsCount += KnockedPins(lane + 1, pinsStanding);
                            }
                            else if (newPath > 2)
                            {
                                knockedPinsCount += KnockedPins(lane - 1, pinsStanding);
                            }
                            else
                            {
                                knockedPinsCount += KnockedPins(lane, pinsStanding);
                            }
                        }

                        return knockedPinsCount;
                    }
                    else if (pinsStanding[8] == true)
                    {
                        int knockedPinsCount = 1;
                        pinsStanding[8] = false;

                        for (int pinAndBall = 0; pinAndBall < 2; pinAndBall++)
                        {
                            int newPath = random.Next(5);
                            if (newPath < 2)
                            {
                                knockedPinsCount += KnockedPins(lane + 1, pinsStanding);
                            }
                            else if (newPath > 2)
                            {
                                knockedPinsCount += KnockedPins(lane - 1, pinsStanding);
                            }
                            else
                            {
                                knockedPinsCount += KnockedPins(lane, pinsStanding);
                            }
                        }

                        return knockedPinsCount;
                    }
                }
                
                else if (lane == 6 && pinsStanding[5] == true)
                {
                    int knockedPinsCount = 1;
                    pinsStanding[5] = false;

                    for (int pinAndBall = 0; pinAndBall < 2; pinAndBall++)
                    {
                        int newPath = random.Next(5);
                        if (newPath < 2)
                        {
                            knockedPinsCount += KnockedPins(lane + 1, pinsStanding);
                        }
                        else if (newPath > 2)
                        {
                            knockedPinsCount += KnockedPins(lane - 1, pinsStanding);
                        }
                        else
                        {
                            knockedPinsCount += KnockedPins(lane, pinsStanding);
                        }
                    }

                    return knockedPinsCount;
                }
                
                else if (lane == 7 && pinsStanding[9] == true)
                {
                    int knockedPinsCount = 1;
                    pinsStanding[9] = false;

                    for (int pinAndBall = 0; pinAndBall < 2; pinAndBall++)
                    {
                        int newPath = random.Next(5);
                        if (newPath < 2)
                        {
                            knockedPinsCount += KnockedPins(lane + 1, pinsStanding);
                        }
                        else if (newPath > 2)
                        {
                            knockedPinsCount += KnockedPins(lane - 1, pinsStanding);
                        }
                        else
                        {
                            knockedPinsCount += KnockedPins(lane, pinsStanding);
                        }
                    }
                    // Total knocked pins in one roll
                    return knockedPinsCount;
                }
                // Miss
                return 0;
            }

        }
    }

}

