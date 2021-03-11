using System;


namespace City_generator
{
    class Program
    {
        static void GenerateIntersection(bool[,] roads, int x, int y)
        {
            var Random = new Random();
            int n = Random.Next(0, 100);
            int e = Random.Next(0, 100);
            int s = Random.Next(0, 100);
            int w = Random.Next(0, 100);

            if (n <= 70) GenerateRoad(roads, x, y, 0);
            if (e <= 70) GenerateRoad(roads, x, y, 1);
            if (s <= 70) GenerateRoad(roads, x, y, 2);
            if (w <= 70) GenerateRoad(roads, x, y, 3);
        }
        static void GenerateRoad(bool[,] roads, int x, int y, int direction)
        {
            roads[x, y] = true;

            int sizeX = roads.GetLength(0);
            int sizeY = roads.GetLength(1);

            if (direction == 0) //NORTH
            {
                for (int i = y; i > 0; i--)
                    roads[x, i] = true;
            }

            if (direction == 1) // EAST
            {
                for (int i = x; i < sizeX; i++)
                    roads[i, y] = true;
            }

            if (direction == 2) // SOUTH
            {
                for (int i = y; i < sizeY; i++)
                    roads[x, i] = true;
            }

            if (direction == 3) // WEST
            {
                for (int i = x; i > 0; i--)
                    roads[i, y] = true;
            }
        }
        static void DrawMap(int width, int height)
        {
            var Random = new Random();

            // ROAD CALCULATION
            var roads = new bool[width, height];

            // GENERATE ROADS
            for (int i = 0; i < 4; i++)
            {
                int x = Random.Next(1, width);
                int y = Random.Next(1, height);

                GenerateIntersection(roads, x, y);
            }

            // HERE IS WHERE THE DRAWING ITSELF IS DONE
            for (int y = 0; y < height + 1; y++)
            {
                for (int x = 0; x < width + 1; x++)
                {
                    // DRAW BORDER?
                    if (y == 0 || x == 0 || y == height || x == width)
                    {
                        if (y == 0 && x == 0 || y == height && x == 0)
                        {
                            Console.Write("┼");
                            continue;
                        }

                        if (y == 0 && x == width || y == height && x == width)
                        {
                            Console.WriteLine("┼");
                            continue;
                        }

                        if (y == 0 && x != 0 && x != width || y == height && x != 0 && x != width)
                        {
                            Console.Write("─");
                            continue;
                        }

                        if (x == 0 && y != 0 && y != height)
                        {
                            Console.Write("│");
                            continue;
                        }

                        if (x == width && y != 0 && y != height)
                        {
                            Console.WriteLine("│");
                            continue;
                        }
                    }

                    // DRAW TITLE?
                    string title = "CITY MAP";
                    if (y == 1 && x == width / 2 - title.Length / 2 - 1)
                    {
                        Console.Write($"{title}");
                        x += title.Length - 1;
                        continue;
                    }

                    // DRAW ROAD? 
                    bool n;
                    bool e;
                    bool s;
                    bool w;

                    if (roads[x, y] == true)
                    {
                        // MAKE SURE WITHIN INDEX BOUNDS
                        if (y > 0)
                            n = roads[x, y - 1];
                        else
                            n = false;

                        if (x < width - 1)
                            e = roads[x + 1, y];
                        else
                            e = false;

                        if (y < height - 1)
                            s = roads[x, y + 1];
                        else
                            s = false;

                        if (x > 0)
                            w = roads[x - 1, y];
                        else
                            w = false;

                        // CHECK N S W E FOR ROADS
                        if (n == true && s == true && w == true && e == true)
                        {
                            Console.Write("╬");
                            continue;
                        }

                        if (n == true && s == true && w == true)
                        {
                            Console.Write("╣");
                            continue;
                        }

                        if (n == true && s == true && e == true)
                        {
                            Console.Write("╠");
                            continue;
                        }

                        if (n == true && w == true && e == true)
                        {
                            Console.Write("╩");
                            continue;
                        }

                        if (s == true && w == true && e == true)
                        {
                            Console.Write("╦");
                            continue;
                        }

                        if (n == true && w == true)
                        {
                            Console.Write("╝");
                            continue;
                        }

                        if (s == true && w == true)
                        {
                            Console.Write("╗");
                            continue;
                        }

                        if (n == true && e == true)
                        {
                            Console.Write("╚");
                            continue;
                        }

                        if (s == true && e == true)
                        {
                            Console.Write("╔");
                            continue;
                        }

                        if (n == true || s == true)
                        {
                            Console.Write("║");
                            continue;
                        }

                        if (w == true || e == true)
                        {
                            Console.Write("═");
                            continue;
                        }
                    }

                    // NO? THEN DRAW NOTHING
                    Console.Write(" ");

                }
            }
        }
        static void Main(string[] args)
        {
            Console.Clear();

            // MAP SIZE
            int width = 50;
            int height = 25;

            // DRAWING THE MAP
            DrawMap(width, height);

            Console.ReadKey();
        }
    }
}

