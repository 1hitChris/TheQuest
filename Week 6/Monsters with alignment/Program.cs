using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Monsters_with_alignment
{
    class Program
    {
            static void Main(string[] args)
            {
                string manual = File.ReadAllText("Monster Manual.txt");

                String[] monster = manual.Split("\n\n");

                Console.WriteLine($"Monsters with a specific alignment: ");

                for (int i = 0; i < monster.Length; i++)
                {
                    String[] info = monster[i].Split("\n");

                    string pattern = "(, (lawful|neutral|chaotic) (good|neutral|evil))";
                    if (Regex.IsMatch(info[1], pattern))
                    {
                        string[] alignment = info[1].Split(", ");
                        Console.WriteLine($"{info[0]} ({alignment[1]})");
                    }
                }
            }
    }
}

