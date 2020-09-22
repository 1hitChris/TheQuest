using System;
using System.Collections.Generic;

namespace Ordinal_numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 220; i++)
            {
                Console.WriteLine(Ordinalnumbers(i));
            }

        }
        static string Ordinalnumbers(int number)
        {

            int lastnumber = number % 10;


            if (number > 10)
            {

                int nmb2 = number / 10;
                int secondlastnmb = nmb2 % 10;
                if (secondlastnmb == 1)
                {
                    return number + "th";
                }
            }

            if (lastnumber == 1)
            {
                return number + "st";
            }
            else if (lastnumber == 2)
            {
                return number + "nd";
            }
            else if (lastnumber == 3)
            {
                return number + "rd";
            }

            return number + "th";



        }
    }
}
