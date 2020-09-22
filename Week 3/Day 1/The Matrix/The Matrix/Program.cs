using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace The_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var streams = new List<int>();
            var symbols = "10356686745";

            for (int i = 0; i < 10; i++)
            {
                streams.Add(random.Next(0, 80));
            }
            Console.ForegroundColor = ConsoleColor.Magenta;

            while (true)
            {
                
                for (int x = 0; x < 80; x++)
                {
                    Console.Write(streams.Contains(x) ? symbols[random.Next(symbols.Length)] : ' ');

                }
                Thread.Sleep(100);
                Console.WriteLine();



                if (random.Next(3) == 0)
                {

                    streams.RemoveAt(random.Next(streams.Count));
                }
                if (random.Next(3) == 0)
                {
                    streams.Add(random.Next(0, 80));
                }
            }

            }
        }
}
