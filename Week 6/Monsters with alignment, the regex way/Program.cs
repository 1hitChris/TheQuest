using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Monsters_with_alignment__the_regex_way
{
    class Program
    {
        static void Main(string[] args)
        {

            string manual = File.ReadAllText("Monster Manual.txt");

            String[] monster = manual.Split("\n\n");

            var namesByAlignment = new List<string>[3, 3];
            var namesOfUnaligned = new List<string>();
            var namesOfAnyAlignment = new List<string>();
            var namesOfSpecialCases = new List<string>();

            for (int axis1 = 0; axis1 < 3; axis1++)
                for (int axis2 = 0; axis2 < 3; axis2++)
                    namesByAlignment[axis1, axis2] = new List<string>();

            var firstAxis = new[] { "lawful", "neutral", "chaotic" };
            var secondAxis = new[] { "good", "neutral", "evil" };

            for (int i = 0; i < monster.Length; i++)
            {
                String[] info = monster[i].Split("\n");

                string pattern = "(, (lawful|neutral|chaotic) (good|neutral|evil))";
                if (Regex.IsMatch(info[1], pattern))
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            string regexPattern = $"(, ?({firstAxis[k]})? ({secondAxis[j]})";

                            if (Regex.IsMatch(info[1], pattern))
                            {
                                namesByAlignment[j, k].Add(info[0]);
                            }
                        }
                    }
                }
                else
                {
                    if (Regex.IsMatch(info[1], "(, unaligned)"))
                    {
                        namesOfUnaligned.Add(info[0]);
                        continue;
                    }

                    else if (Regex.IsMatch(info[1], "(, any alignment)"))
                    {
                        namesOfAnyAlignment.Add(info[0]);
                        continue;
                    }

                    else
                    {
                        string[] special = info[1].Split(", ");


                        namesOfSpecialCases.Add($"{info[0]} ({special[1]})");
                        continue;
                    }
                }
            }
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (k == 1 && j == 1) Console.WriteLine($"Monsters with alignment true {secondAxis[j]} are:");
                    else Console.WriteLine($"Monsters with alignment {firstAxis[k]} {secondAxis[j]} are:");
                    foreach (string thing in namesByAlignment[j, k])
                    {
                        Console.WriteLine(thing);
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Unaligned monsters are: ");
            foreach (string thing in namesOfUnaligned)
            {
                Console.WriteLine(thing);
            }

            Console.WriteLine();

            Console.WriteLine("Monsters which can be of any alignment are: ");
            foreach (string thing in namesOfAnyAlignment)
            {
                Console.WriteLine(thing);
            }

            Console.WriteLine();

            Console.WriteLine("Monsters with special cases are: ");
            foreach (string thing in namesOfSpecialCases)
            {
                Console.WriteLine(thing);
            }
        }
    }
}

