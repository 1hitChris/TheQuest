using System;

namespace ASCII_chart
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i <= 255; i++)
            {
                var chars = new[]
               {
                    (char)(i)
                };
                Console.Write($"{i} = ");
                Console.WriteLine(chars);
            }
            

        }

    }
}
