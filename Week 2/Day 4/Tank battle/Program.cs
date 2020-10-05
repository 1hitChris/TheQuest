using System;
using System.Security.Cryptography.X509Certificates;

namespace Tank_battle
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Text
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("DANGER!!!");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("A tank is approaching our position. Your artilery unit is our only hope!");
            Console.ReadLine();
            Console.WriteLine("What is your name, Commander?");
            Console.ReadLine();
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Here is the map of the battlefield:");
            Console.ReadLine();
          
            //End of Text

            //Battlefield
            var random = new Random();
            int tankDistance = random.Next(10);
            int width = 80;
            

            for (int x = 0; x <= width; x++)
            {
                bool artilery = x == 1;
                bool verticalBorder = x < width;
                bool tank = x + 2 == tankDistance;
                if (artilery)
                {
                    Console.Write("/");
                }
                if (tank)
                {
                    Console.Write("T");
                }
                if (verticalBorder)
                {
                    Console.Write("_");
                }
            }
            Console.WriteLine("");
            //End of Battlefield

            //Artilery
            //1
            Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine($"Aim your shot, Commander {name}!");
            Console.Write("Enter distance: ");
            string numberText = Console.ReadLine();
            int number = Int32.Parse(numberText);
            
            if (number == tankDistance)
            {
                for (int x = 1; x <= width; x++)
                {
                    bool blank = x <= width;
                    bool shotDistance = x == number;

                    if (shotDistance)
                    {
                        Console.Write("*");
                    }
                    else if (blank)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine($"BOOM! You destroyed the tank! Good job, Commander {name}!");
            }
            else if (number < tankDistance)
            {
                for (int x = 1; x <= width; x++)
                {
                    bool blank = x <= width;
                    bool shotDistance = x == number;

                    if (shotDistance)
                    {
                        Console.Write("*");
                    }
                    else if (blank)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("Oh no, your shot was to short!");
                
            }
            else if (number > tankDistance)
            {

                for (int x = 1; x <= width; x++)
                {
                    bool blank = x <= width;
                    bool shotDistance = x == number;

                    if (shotDistance)
                    {
                        Console.Write("*");
                    }
                    else if (blank)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("Going for a hail mary, are we?");
            }
            //2
            var random2 = new Random();
            int tankDistance2 = tankDistance;
            int width2 = 80;
            int tankMovement = tankDistance - random2.Next(20);

            Console.WriteLine();
            Console.WriteLine("Here is the map of the battlefield:");
            for (int x = 0; x <= width2; x++)
            {
                bool artilery = x == 1;
                bool verticalBorder = x < width2;
                bool tank = x + 2 == tankMovement;
                if (artilery)
                {
                    Console.Write("/");
                }
                if (tank)
                {
                    Console.Write("T");
                }
                if (verticalBorder)
                {
                    Console.Write("_");
                }
            }
            Console.WriteLine("");

            Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine($"Aim your shot, Commander {name}!");
            Console.Write("Enter distance: ");
            string numberText2 = Console.ReadLine();
            int number2 = Int32.Parse(numberText2);

            if (number2 == tankDistance2)
            {
                for (int x = 1; x <= width2; x++)
                {
                    bool blank = x <= width2;
                    bool shotDistance = x == number2;

                    if (shotDistance)
                    {
                        Console.Write("*");
                    }
                    else if (blank)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine($"BOOM! You destroyed the tank! Good job, Commander {name}!");
            }
            else if (number2 < tankDistance2)
            {
                for (int x = 1; x <= width2; x++)
                {
                    bool blank = x <= width2;
                    bool shotDistance = x == number2;

                    if (shotDistance)
                    {
                        Console.Write("*");
                    }
                    else if (blank)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("Oh no, your shot was to short!");
            }
            else if (number2 > tankDistance2)
            {

                for (int x = 1; x <= width2; x++)
                {
                    bool blank = x <= width2;
                    bool shotDistance = x == number2;

                    if (shotDistance)
                    {
                        Console.Write("*");
                    }
                    else if (blank)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("Going for a hail mary, are we?");
            }
            //3
            var random3 = new Random();
            int tankDistance3 = tankMovement;
            int width3 = 80;
            int tankMovement2 = tankMovement - random3.Next(20);

            Console.WriteLine();
            Console.WriteLine("Here is the map of the battlefield:");
            for (int x = 0; x <= width; x++)
            {
                bool artilery = x == 1;
                bool verticalBorder = x < width3;
                bool tank = x + 2 == tankMovement2;
                if (artilery)
                {
                    Console.Write("/");
                }
                if (tank)
                {
                    Console.Write("T");
                }
                if (verticalBorder)
                {
                    Console.Write("_");
                }
            }
            Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine($"Aim your shot, Commander {name}!");
            Console.Write("Enter distance: ");
            string numberText3 = Console.ReadLine();
            int number3 = Int32.Parse(numberText3);

            if (number3 == tankDistance3)
            {
                for (int x = 1; x <= width3; x++)
                {
                    bool blank = x <= width3;
                    bool shotDistance = x == number3;

                    if (shotDistance)
                    {
                        Console.Write("*");
                    }
                    else if (blank)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine($"BOOM! You destroyed the tank! Good job, Commander {name}!");
            }
            else if (number3 < tankDistance3)
            {
                for (int x = 1; x <= width3; x++)
                {
                    bool blank = x <= width3;
                    bool shotDistance = x == number3;

                    if (shotDistance)
                    {
                        Console.Write("*");
                    }
                    else if (blank)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("Oh no, your shot was to short!");
            }
            else if (number3 > tankDistance3)
            {

                for (int x = 1; x <= width3; x++)
                {
                    bool blank = x <= width3;
                    bool shotDistance = x == number3;

                    if (shotDistance)
                    {
                        Console.Write("*");
                    }
                    else if (blank)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("Going for a hail mary, are we?");
            }


            //End of Artilery
        }

    }
}

