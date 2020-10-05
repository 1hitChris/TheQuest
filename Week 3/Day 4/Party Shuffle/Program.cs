using System;
using System.Collections.Generic;

namespace Party_Shuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            var listOfNames = new List<string> { "Chris", "Mats", "Gabriel", "Johanna", "Johannes" };
            var shuffledList = new List<string> { };
            var random = new Random();
            Console.Write($"Signed-up participants: ");
            Console.WriteLine(string.Join(", ", listOfNames));
            // shuffledList = Shuffle(listOfNames);
            Shuffle(listOfNames);
            Console.Write($"Shuffled participants: ");
            Console.Write(string.Join(", ", listOfNames));
        }

        static List<string> Shuffle2 (List<string> listOfStrings)
        {
            var random = new Random();
            var shuffledList2 = new List<string> { };
            var loopNumber = listOfStrings.Count;
            for (int i = 0; i < loopNumber; i++)
            {
                int randomName = random.Next(0, listOfStrings.Count);
                shuffledList2.Add(listOfStrings[randomName]);
                listOfStrings.RemoveAt(randomName);
            }
            return shuffledList2;
        }
        static void Shuffle (List<string> a)
        {
            var random = new Random();
            int n = a.Count;
            for (int i = 0; i <= n - 2; i++)
            {
                int j = random.Next(i, n);
                (a[i], a[j]) = (a[j], a[i]);
            }
        }
    }
}
