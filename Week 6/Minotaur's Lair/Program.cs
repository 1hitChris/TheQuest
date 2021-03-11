using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Minotaur_s_Lair
{
    class Program
    {
        static bool gameIsOver;
        static int width;
        static int height;
        static char[,] map;

        static int playerPosX;
        static int playerPosY;
        static int[,] playerPos;

        static void Main(string[] args)
        {
            gameIsOver = false;
            playerPos = new int[playerPosX, playerPosY];

            // READ THE FILE
            string mapPath = "MazeLevel.txt";
            string mapText = File.ReadAllText(mapPath);

            // DIVIDE FILE INTO SEPARATE LINES
            string[] mapLines = mapText.Split('\n');

            // GET LEVEL NAME FROM FIRST LINE
            string levelName = mapLines[0];

            // GET THE MAP SIZE FROM THE SECOND LINE
            string patternSize = @"(\d+)x(\d+)";
            Match Size = Regex.Match(mapLines[1], patternSize);
            width = Int32.Parse(Size.Groups[1].ToString());
            height = Int32.Parse(Size.Groups[2].ToString());

            // TURN THE LINES WITH THE MAP INTO SEPARATE CHARS
            map = new char[width, height];

            // ASSIGNS THE CHARACTHERS TO A 2D ARRAY AND SKIPS THE FIRST TWO LINES
            for (int y = 0; y < height; y++)
            {
                char[] mapChara = mapLines[y + 2].ToCharArray();

                for (int x = 0; x < width; x++)
                {
                    map[x, y] = mapChara[x];

                    if (map[x, y] == 'S')
                    {
                        playerPosX = x;
                        playerPosY = y;

                        map[x, y] = ' ';
                    }
                }
            }

            // Player
            var player = '.';
            var minotaur = 'M';

            // ASSIGN SPECIAL SYMBOLS TO ARRAYS
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Player position
                    if (playerPosX == x && playerPosY == y)
                    {
                        map[x, y] = player;
                    }

                    // Minotaur position
                    else if (map[x, y] == minotaur)
                    {
                        map[x, y] = minotaur;
                    }

                    // Can we draw a tree?
                    else if (y < 3)
                    {
                        var Random = new Random();
                        int random = Random.Next(0, 10);

                        // Will we draw a tree?
                        if (random < 2 - (y ^ y))
                        {                          
                                map[x, y] = 'A';
                        }
                    }
                }
            }
            Console.WriteLine(mapLines[0]);
            Console.WriteLine("\nPress any key to start");
            Console.ReadKey();
            DrawMap();

            //While game is not over, keep looping
            while(gameIsOver == false)
            {
                while (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();

                    switch (key.Key)
                    {
                        case System.ConsoleKey.LeftArrow:
                            {
                                if (playerPosX > 0 && map[playerPosX - 1, playerPosY] == ' ' || map[playerPosX - 1, playerPosY] == 'M')
                                {
                                    playerPosX -= 1;
                                }
                            }
                            break;

                        case System.ConsoleKey.RightArrow:
                            {
                                if (playerPosX < width - 1 && map[playerPosX + 1, playerPosY] == ' ' || map[playerPosX + 1, playerPosY] == 'M')
                                {
                                    playerPosX += 1 ;
                                }
                            }
                            break;

                        case System.ConsoleKey.UpArrow:
                            {
                                if (playerPosY > 0 && map[playerPosX, playerPosY - 1] == ' ' || map[playerPosX, playerPosY - 1] == 'M')
                                {
                                    playerPosY -= 1;
                                }
                            }
                            break;
                        case System.ConsoleKey.DownArrow:
                            {
                                if (playerPosY < height - 1 && map[playerPosX, playerPosY + 1] == ' ' || map[playerPosX, playerPosY + 1] == 'M')
                                {
                                    playerPosY += 1;
                                }
                            }
                            break;
                        case System.ConsoleKey.Escape:
                            {
                                Console.WriteLine("GGame Over");
                                return;
                            }
                        default:
                            break;
                    }
                    if (map[playerPosX, playerPosY] == 'M')
                    {
                        gameIsOver = true;
                    }
                }

                //Draw map
                DrawMap();

              /*  if (playerPos[playerPosX, playerPosY] == minotaur)
                {
                    Console.WriteLine("You have reached the minotaur! You Win");
                    return;
                }*/
            }

            //If game is over, display game over and end the game
            if (gameIsOver == true)
            {
                Console.Clear();
                Console.WriteLine("GAME OVER!!");
            }
        }

        static void DrawMap()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == playerPosX && y == playerPosY)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(".");
                    }
                    else if (map[x, y] == 'M')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (map[x, y] == 'A')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
