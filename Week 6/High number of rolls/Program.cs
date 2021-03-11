using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace High_number_of_rolls
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get the path to the monster manual
            string[] monsterData = File.ReadAllLines("Monster Manual.txt");

            //List with monsters and a list where it stores a bool if monster need 10+ dices for HP
            List<string> listOfMonsters = new List<string> { monsterData[0] };
            List<bool> monsterHighDiceRoll = new List<bool> { };

            //Methods for getting the names and checks if monster needs 10+ dices for HP
            ListOfMonsterNames(listOfMonsters, monsterData);
            ListOfHighDiceRoll(monsterHighDiceRoll, monsterData);
           
            //Prints out the result
            Console.WriteLine("Monsters in the manual are");
            for (int i = 0; i < listOfMonsters.Count; i++)
            {
                Console.WriteLine($"{listOfMonsters[i]} - 10+ dice rolls: {monsterHighDiceRoll[i]}");
            }

            //Gets the list of names
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

            //Checks all the monsters and see if the hp roll is 10+ dices
            void ListOfHighDiceRoll(List<bool> monsterHp, string[] listOfMonsterData)
            {
                string tenPlusRolls = "\\d\\dd";

                for (int i = 1; i < listOfMonsterData.Length; i++)
                {
                    if (monsterData[i - 1].Contains("Hit Points"))
                    {
                        if (Regex.IsMatch(monsterData[i - 1], tenPlusRolls))
                        {
                            monsterHp.Add(true);
                        }
                        else
                        {
                            monsterHp.Add(false);
                        }
                    }
                }

            }
        }
    }
}
