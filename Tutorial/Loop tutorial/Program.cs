using System;

namespace Loop_tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            //The number input
            int height = 5;

            for (int i = 0; i < height; i++)
            {
                for (int j = height - i; j > 0; j--)
                {
                    Console.Write(" ");
                }
                for (int j = 0; j < i; j++)
                {
                    Console.Write("O" + " ");
                }
                Console.WriteLine();
            }
            
        }
    }
}


using System;
using System.Collections.Generic;

namespace Bowling_Drawing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> pinsStanding = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int height = 5;

            for (int i = 0; i < height; i++)
            {
                for (int j = height - i; j > 0; j--)
                {
                    Console.Write(" ");
                }
                for (int j = 0; j < i; j++)
                {
                    Console.Write($"{pinsStanding[i]}" + " ");
                }
                Console.WriteLine();
            }
        }
    }
}