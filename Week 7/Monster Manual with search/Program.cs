using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Monster_Manual_with_search
{
    class MonsterEntry
    {
        public string Name;
        public string Description;
        public string Alignment;
        public string HitPoints;
        public ArmorInformation Armor = new ArmorInformation();
    }

    class ArmorInformation
    {
        public int Class;
        public ArmorType Type;
    }

    enum ArmorType
    {
        Unspecified,
        Natural,
        Leather,
        StuddedLeather,
        Hide,
        ChainShirt,
        ChainMail,
        ScaleMail,
        Plate,
        Other
    }

    class ArmorTypeEntry
    {
        public string Name;
        public ArmorCategory Category;
        public int Weight;
    }

    enum ArmorCategory
    {
        Light,
        Medium,
        Heavy
    }
    class Program
    {
        static List<MonsterEntry> monsterEntries = new List<MonsterEntry>();
        static Dictionary<ArmorType, ArmorTypeEntry> armorTypeEntries = new Dictionary<ArmorType, ArmorTypeEntry>();

        static void Main(string[] args)
        {
            //Initialize all data
            ParseMonsterData();
            ParseArmorData();

            bool searchAgain = false;

            do
            {
                bool skipListIfOneMonster = false;

                List<MonsterEntry> matchedMonsterEntries;

                //Choose what you wanna search with, name or armor
                Console.WriteLine("Do you want to search by (n)ame or (a)rmor?");
                string searchBy = Console.ReadLine();

                //If you press n, you search by name
                if (searchBy == "n")
                {
                    skipListIfOneMonster = true;
                    Console.Clear();

                    do
                    {
                        Console.WriteLine("Enter a query to search monsters by name:");
                        string search = Console.ReadLine();

                        matchedMonsterEntries = FindMonstersByName(search);

                        if (matchedMonsterEntries.Count == 0)
                        {
                            Console.WriteLine("\nNo monsters were found. Press any key to try again.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            break;
                        }

                    } while (true);
                }
                //If you press a, you search by armor
                else if (searchBy == "a")
                {
                    Console.WriteLine("\nWhich armor type do you want to display?");

                    ArmorType[] armorTypes = (ArmorType[])Enum.GetValues(typeof(ArmorType));

                    for (int i = 0; i < armorTypes.Length; i++)
                    {
                        if (armorTypeEntries.ContainsKey(armorTypes[i]))
                        {
                            Console.WriteLine($"{i + 1}: {armorTypeEntries[armorTypes[i]].Name}");
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1}: {armorTypes[i]}");
                        }
                       
                    }

                    Console.WriteLine("\nEnter number:");

                    int chosenArmorTypeIndex = Int32.Parse(Console.ReadLine()) - 1;
                    ArmorType chosenArmorType = armorTypes[chosenArmorTypeIndex];

                    matchedMonsterEntries = FindMonstersByArmorType(chosenArmorType);
                }
                else
                {
                    continue;
                }

                MonsterEntry chosenMonsterEntry;
                if (skipListIfOneMonster && matchedMonsterEntries.Count == 1)
                {
                    chosenMonsterEntry = matchedMonsterEntries[0];
                }
                else
                {
                    //Let user choose a matched monster
                    chosenMonsterEntry = ChooseMonsterFromList(matchedMonsterEntries);
                }

                // Display chosen monster
                DisplayMonsterInfo(chosenMonsterEntry);

                // Ask to search again
                Console.WriteLine($"\nDo you want to search again? Y/N");

                do
                {
                    string again = Console.ReadLine().ToLower();

                    if (again == "y" || again == "n")
                    {
                        if (again == "y")
                        {
                            searchAgain = true;
                            Console.Clear();
                        }

                        else
                        {
                            searchAgain = false;
                        }

                        break;
                    }

                    else
                    {
                        int cursor = Console.CursorTop;
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new String(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, cursor - 1);
                    }
                } while (true);

            } while (searchAgain);

        }

        //Search by name of monster
        static List<MonsterEntry> FindMonstersByName(string search)
        {
            var searchResults = new List<MonsterEntry>();

            foreach (MonsterEntry monsterEntry in monsterEntries)
            {
                if (Regex.IsMatch(monsterEntry.Name, $"({search} ?)", RegexOptions.IgnoreCase))
                {
                    searchResults.Add(monsterEntry);
                }
            }

            return searchResults;
        }

        //Search by armor of monster
        static List<MonsterEntry> FindMonstersByArmorType(ArmorType armorType)
        {
            var searchResults = new List<MonsterEntry>();

            foreach (MonsterEntry monsterEntry in monsterEntries)
            {
                if (monsterEntry.Armor.Type == armorType)
                {
                    searchResults.Add(monsterEntry);
                }
            }

            return searchResults;
        }

        //Result of the search, able to chose a number if more than one option in the search
        static MonsterEntry ChooseMonsterFromList(List<MonsterEntry> monsterEntries)
        {
            Console.WriteLine("\nWhich monster did you want to look up?");

            for (int i = 0; i < monsterEntries.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {monsterEntries[i].Name}");
            }
            Console.WriteLine("\nEnter number:");

            int chosenIndex;

            chosenIndex = Int32.Parse(Console.ReadLine()) - 1;
            return monsterEntries[chosenIndex];
        }

        //Display the monsters name, description, alignment, hitpoints and armor class and type
        static void DisplayMonsterInfo(MonsterEntry monsterEntry)
        {
            Console.WriteLine($"\nDisplaying information for {monsterEntry.Name}.\n");
            Console.WriteLine($"Name: {monsterEntry.Name}");
            Console.WriteLine($"Description: {monsterEntry.Description}");
            Console.WriteLine($"Alignment: {monsterEntry.Alignment}");
            Console.WriteLine($"Hit points: {monsterEntry.HitPoints}");
            Console.WriteLine($"Armor class: {monsterEntry.Armor.Class}");

            if (armorTypeEntries.ContainsKey(monsterEntry.Armor.Type))
            {
                ArmorTypeEntry armorTypeEntry = armorTypeEntries[monsterEntry.Armor.Type];

                Console.WriteLine($"Armor type: {armorTypeEntry.Name}");
                Console.WriteLine($"Armor category: {armorTypeEntry.Category}");
                Console.WriteLine($"Armor weight: {armorTypeEntry.Weight}");
            }
            else
            {
                Console.WriteLine($"Armor type: {monsterEntry.Armor.Type}");
            }
        }

        //All the monster data from the monster manual. Name, description, alignment, hitpoints and armor
        static void ParseMonsterData()
        {
            // Read from the monster manual
            string manual = File.ReadAllText("Monster Manual.txt");

            // Split the manual into individual monsters
            String[] monster = manual.Split("\n\n");

            for (int i = 0; i < monster.Length; i++)
            {
                var monsterEntry = new MonsterEntry();
                String[] info = monster[i].Split("\n");
                monsterEntry.Name = info[0];

                String[] description = info[1].Split(", ");
                monsterEntry.Description = description[0];
                monsterEntry.Alignment = description[1];

                Match hitPoints = Regex.Match(info[2], @"Hit Points: (\d+ \S*)");
                monsterEntry.HitPoints = hitPoints.Groups[1].Value;

                Match armorClass = Regex.Match(info[3], @" (\d+)");
                monsterEntry.Armor.Class = Int32.Parse(armorClass.Groups[1].Value);

                Match armorType = Regex.Match(info[3], @"\((.*?)(,.*)?\)");

                if (armorType.Success)
                {

                    if (Regex.IsMatch(armorType.Groups[1].ToString(), "Natural", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.Natural;
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Studded", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.StuddedLeather;
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Leather", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.Leather;
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Hide", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.Hide;
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Chain Shirt", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.ChainShirt;
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Chain Mail", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.ChainMail;
                    }
                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Scale", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.ScaleMail;
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Plate", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.Plate;
                    }

                    else
                    {
                        monsterEntry.Armor.Type = ArmorType.Other;
                    }
                }

                else
                {
                    monsterEntry.Armor.Type = ArmorType.Unspecified;
                }

                monsterEntries.Add(monsterEntry);
            }
        }

        //Dictionary with all the different armor typings
        static void ParseArmorData()
        {
            // Read from the armor type
            string pathArmor = "Armor Types.txt";
            string armors = File.ReadAllText(pathArmor);

            // Split armors into individuals
            String[] armor = armors.Split("\n");

            string[] armorTypes = Enum.GetNames(typeof(ArmorType));

            string armorTypeText = "";

            for (int i = 0; i < armorTypes.Length - 3; i++)
            {

                var armorTypeEntry = new ArmorTypeEntry();

                string[] info = armor[i].Split(",");

                if (i == 0)
                    armorTypeText = ArmorType.Leather.ToString();

                else if (i == 1)
                    armorTypeText = ArmorType.StuddedLeather.ToString();

                else if (i == 2)
                    armorTypeText = ArmorType.Hide.ToString();

                else if (i == 3)
                    armorTypeText = ArmorType.ChainShirt.ToString();

                else if (i == 4)
                    armorTypeText = ArmorType.ScaleMail.ToString();

                else if (i == 5)
                    armorTypeText = ArmorType.ChainMail.ToString();

                else if (i == 6)
                    armorTypeText = ArmorType.Plate.ToString();

                var armorType = (ArmorType)Enum.Parse(typeof(ArmorType), info[0]);

                armorTypeEntry.Name = info[1];

                if (info[2] == "Light")
                    armorTypeEntry.Category = ArmorCategory.Light;

                else if (info[2] == "Medium")
                    armorTypeEntry.Category = ArmorCategory.Medium;

                else if (info[2] == "Heavy")
                    armorTypeEntry.Category = ArmorCategory.Heavy;

                armorTypeEntry.Weight = Int32.Parse(info[3]);

                armorTypeEntries[armorType] = armorTypeEntry;
            }
        }

    }
}