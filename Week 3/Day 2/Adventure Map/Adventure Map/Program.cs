using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;


namespace Adventure_Map
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 35;
            int width = 100;
            DrawMap(width, height);
        }


        static void DrawMap(int width, int height)
        {
            int h = height / 2;
            var random = new Random();
            var roadPartLengths = new List<int> { };
            var roadPartStartsX = new List<int> { };
            var roadPartStartsY = new List<int> { };
            int xStart = 1;
            int yStart = random.Next(10, height / 2);
            int lStart = random.Next(3, 10);
            roadPartStartsX.Add(xStart);
            roadPartStartsY.Add(yStart);
            roadPartLengths.Add(lStart);
            int currentRoadPartIndex = 1;

            while (roadPartStartsX[currentRoadPartIndex - 1] + roadPartLengths[currentRoadPartIndex - 1] < width - 1)
            {
                int previousRoadPartIndex = currentRoadPartIndex - 1;
                int previousRoadStartY = roadPartStartsY[previousRoadPartIndex];
                int previousRoadLength = roadPartLengths[previousRoadPartIndex];
                int previousRoadStartX = roadPartStartsX[previousRoadPartIndex];
                bool yTop = previousRoadStartY == 0;
                bool yBot = previousRoadStartY == height - 1;
                roadPartStartsX.Add((previousRoadStartX) + (previousRoadLength));
                roadPartLengths.Add(random.Next(4, 10));

                int yRandom = random.Next(2);
                if (yRandom == 0)
                {
                    roadPartStartsY.Add(previousRoadStartY + 1);
                }
                else if (yRandom == 1)
                {
                    roadPartStartsY.Add(previousRoadStartY - 1);
                }
                currentRoadPartIndex++;
            }
            roadPartLengths[currentRoadPartIndex - 1] = width - 1 - roadPartStartsX[currentRoadPartIndex - 1];
            int roadPartsCount = roadPartStartsX.Count;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //Title
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    if (x == width / 3 + 7 && y == 1)
                    {
                        x += 13 - 1;
                        Console.Write("ADVENTURE MAP");
                        continue;
                    }
                    //End of Title

                    //Border
                    bool verticalBorder = x == 0 || x == width - 1;
                    bool horizontalBorder = y == 0 || y == height - 1;
                    if (verticalBorder && horizontalBorder)
                    {
                        Console.Write("+");
                        continue;
                    }
                    else if (horizontalBorder)
                    {
                        Console.Write("-");
                        continue;
                    }
                    else if (verticalBorder)
                    {
                        Console.Write("|");
                        continue;
                    }
                    //End of Border

                    //Bridge
                    Console.ForegroundColor = ConsoleColor.White;
                    bool bridgeX1 = x == roadPartStartsX[10] + 1 & y == roadPartStartsY[10] - 1 || x == roadPartStartsX[10] + 1 & y == roadPartStartsY[10] + 1;
                    bool bridgeX2 = x == roadPartStartsX[10] + 2 & y == roadPartStartsY[10] - 1 || x == roadPartStartsX[10] + 2 & y == roadPartStartsY[10] + 1;
                    bool bridgeX3 = x == roadPartStartsX[10] + 3 & y == roadPartStartsY[10] - 1 || x == roadPartStartsX[10] + 3 & y == roadPartStartsY[10] + 1;
                    bool bridgeX4 = x == roadPartStartsX[10] + 4 & y == roadPartStartsY[10] - 1 || x == roadPartStartsX[10] + 4 & y == roadPartStartsY[10] + 1;
                    bool bridgeX5 = x == roadPartStartsX[10] & y == roadPartStartsY[10] - 1 || x == roadPartStartsX[10] & y == roadPartStartsY[10] + 1;
                    if (bridgeX1 || bridgeX2 || bridgeX3 || bridgeX4 || bridgeX5)
                    {
                        Console.Write("=");
                        continue;
                    }
                    //End of Bridge

                    //Road
                    Console.ForegroundColor = ConsoleColor.White;
                    bool roadDrawn = false;
                    for (int i = 0; i < roadPartsCount; i++)
                    {
                        int currentRoadPartLength = roadPartLengths[i];
                        int currentRoadPartStartX = roadPartStartsX[i];
                        int currentRoadPartStartY = roadPartStartsY[i];

                        if (x == currentRoadPartStartX && y == currentRoadPartStartY)
                        {
                            x += currentRoadPartLength - 1;

                            for (int j = 0; j < currentRoadPartLength; j++)
                            {
                                Console.Write("#");
                            }
                            roadDrawn = true;
                        }
                    }
                    if (roadDrawn)
                    {
                        continue;
                    }
                    //End of Road

                    //River
                    Console.ForegroundColor = ConsoleColor.Blue;
                    bool riverX = x == roadPartStartsX[10] + 1 || x == roadPartStartsX[10] + 2 || x == roadPartStartsX[10] + 3;
                    if (riverX)
                    {
                        Console.Write("V");
                        continue;
                    }
                    //End of River

                    //Wall
                    Console.ForegroundColor = ConsoleColor.Gray;
                    bool wallX = x == width / 3 || x == width / 3 + 2;
                    if (wallX)
                    {
                        Console.Write("|");
                        continue;
                    }
                    //End of Wall

                    //Ballista
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    bool ballista = x == width / 3 + 1;
                    if (ballista)
                    {
                        int yRandom = random.Next(2);
                        if (yRandom == 0)
                        {
                            Console.Write("<");
                            continue;
                        }
                        else
                        {
                            Console.Write("");
                        }

                    }
                    //End of Ballista

                    //Rocks
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    bool rocks = x == random.Next(width);
                    if (rocks)
                    {
                        Console.Write("O");
                        continue;
                    }
                    //End of Rocks



                    //Trees
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    bool verticaltrees = x <= random.Next(width / 4);
                    if (verticaltrees)
                    {
                        Console.Write("A");
                        continue;
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
                //End of Trees
            }
        }



    }

}
