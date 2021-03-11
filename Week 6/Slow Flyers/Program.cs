using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Slow_Flyers
{
    class Program
    {
        static void Main(string[] args)
        {
            //Read text from file and puts it in a string
            string[] monsterManual = File.ReadAllLines("Monster Manual.txt");

            //Make a new list of string to store the monsters name in
            List<string> listOfMonsters = new List<string> { };

            ListOfMonsterNames(listOfMonsters, monsterManual);
            
            //Goes through the list and prints it out, one by one
            Console.WriteLine("Monsters that can fly 10-40 feet per turn:");
            for (int i = 0; i < listOfMonsters.Count; i++)
            {
                Console.WriteLine($"{listOfMonsters[i]}");
            }


            void ListOfMonsterNames(List<string> monsterNames, string[] listOfMonsterData)
            {
                //Using RegEx to find fly speed between 10-40
                string flySpeedFom10To39 = "fly [1-3][0-9][^\\d]";
                string flySpeed40 = "fly 40[^\\d]";

                //Goes through the list and sort out the ones that has fly speed between 10-40
                for (int i = 1; i < listOfMonsterData.Length; i++)
                {
                    if (listOfMonsterData[i].Contains("Speed"))
                    {
                        if (Regex.IsMatch(listOfMonsterData[i], flySpeedFom10To39) || Regex.IsMatch(listOfMonsterData[i], flySpeed40))
                        {
                            listOfMonsters.Add(listOfMonsterData[i - 4]);
                        }
                    }
                }
            }

        }
    }
}
