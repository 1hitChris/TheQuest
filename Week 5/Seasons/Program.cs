using System;

namespace Seasons
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CreateDayDescription(30, 2, 1990));
            Console.WriteLine(CreateDayDescription(12, 0, 1123));
            Console.WriteLine(CreateDayDescription(2, 3, 10));
        }
        static string CreateDayDescription(int day, int season, int year)
        {
            string[] seasons = { "Spring", "Summer", "Fall", "Winter" };
            return $"{OrdinalNumbers(day)} day of {seasons[season]} in the year {year}";
        }

        static string OrdinalNumbers(int number)
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
