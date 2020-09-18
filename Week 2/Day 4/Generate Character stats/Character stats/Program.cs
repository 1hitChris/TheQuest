using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace day4d
{
    class Program
    {
        static void Main(string[] args)
        {
            var listOfCalculatedCharacteristics = new List<int> { };
            var random = new Random();
            var charactersisticSorter = new List<int> { };
            var charactersisticSorter2 = new List<int> { };
            var charactersisticSorter3 = new List<int> { };
            int sumOfCharacteristics = 0;
            int sumOfCharacteristics2 = 0;
            int sumOfCharacteristics3 = 0;




            Console.Write("You roll ");
            for (int j = 0; j < 4; j++)
            {
                int characteristic = random.Next(1, 7);
                charactersisticSorter.Add(characteristic);
                Console.Write(characteristic);
                if (j < 3)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.Write(". ");
                }
            }

            charactersisticSorter.Sort();
            charactersisticSorter.RemoveAt(0);




            foreach (var chara in charactersisticSorter)
            {
                sumOfCharacteristics += chara;
            }



            Console.WriteLine("The ability score is " + sumOfCharacteristics + ".");
            listOfCalculatedCharacteristics.Add(sumOfCharacteristics);

            Console.Write("You roll ");
            for (int j = 0; j < 4; j++)
            {
                int characteristic = random.Next(1, 7);
                charactersisticSorter2.Add(characteristic);
                Console.Write(characteristic);
                if (j < 3)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.Write(". ");
                }
            }

            charactersisticSorter2.Sort();
            charactersisticSorter2.RemoveAt(0);




            foreach (var chara in charactersisticSorter2)
            {
                sumOfCharacteristics2 += chara;
            }



            Console.WriteLine("The ability score is " + sumOfCharacteristics2 + ".");
            listOfCalculatedCharacteristics.Add(sumOfCharacteristics2);

            Console.Write("You roll ");
            for (int j = 0; j < 4; j++)
            {
                int characteristic = random.Next(1, 7);
                charactersisticSorter3.Add(characteristic);
                Console.Write(characteristic);
                if (j < 3)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.Write(". ");
                }
            }

            charactersisticSorter3.Sort();
            charactersisticSorter3.RemoveAt(0);




            foreach (var chara in charactersisticSorter3)
            {
                sumOfCharacteristics3 += chara;
            }



            Console.WriteLine("The ability score is " + sumOfCharacteristics3 + ".");
            listOfCalculatedCharacteristics.Add(sumOfCharacteristics3);




            listOfCalculatedCharacteristics.Sort();
            Console.Write("Your available ability scores are ");
            int test = 0;
            foreach (var chara in listOfCalculatedCharacteristics)
            {
                Console.Write(chara);
                // int test = 0;
                if (test < 2)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.Write(".");
                }
                test++;
            }

        }
    }
}