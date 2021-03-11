using System;
using System.Collections.Generic;
using System.IO;

namespace Monster_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            //Read text from file and puts it in a string
            string[] monsterManual = File.ReadAllLines("Monster Manual.txt");

            //Make a new list of string to store the monsters name in
            List<string> listOfMonsters = new List<string> { };
            List<bool> listOfMonstersCanFly = new List<bool> { };

            ListOfMonsterNames(listOfMonsters, monsterManual);
            ListOfMonsterCanFly(listOfMonstersCanFly, monsterManual);

            //Goes through the list and prints it out, one by one
            Console.WriteLine("Monsters in the manual are");
            for (int i = 0; i < listOfMonsters.Count; i++)
            {
                Console.WriteLine($"{listOfMonsters[i]} - Can fly: {listOfMonstersCanFly[i]}");
            }

            //Take the monster manual text and take out the names and store them in list of monsters
            void ListOfMonsterNames(List<string> monsterNames, string[] listOfMonsterData)
            {
                for (int i = 1; i < listOfMonsterData.Length; i++)
                {
                    if (listOfMonsterData[i - 1] == "")
                    {
                        listOfMonsters.Add(listOfMonsterData[i]);
                    }
                }
            }

            void ListOfMonsterCanFly(List<bool> monsterCanFly, string[] listOfMonsterData)
            {
                for (int i = 1; i < listOfMonsterData.Length; i++)
                {
                    if (listOfMonsterData[i - 1].Contains("Speed"))
                    {
                        if (listOfMonsterData[i - 1].Contains("fly"))
                        {
                            monsterCanFly.Add(true);
                        }
                        else
                        {
                            monsterCanFly.Add(false);
                        }
                    }
                }
            }
        }
    }
}
