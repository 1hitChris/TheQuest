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
        public string Type;
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
        // Read from the monster manual
        static string manual = File.ReadAllText("Monster Manual.txt");

        // Split the manual into individual monsters
        static String[] monster = manual.Split("\n\n");

        // Read from the armor type
        static string pathArmor = "Armor Types.txt";
        static string armors = File.ReadAllText(pathArmor);

        // Split armors into individuals
        static String[] armor = armors.Split("\n");

        // List for matching search result
        static List<string> searchResults = new List<string>();

        static void Main(string[] args)
        {
            bool searchAgain;
            do
            {
                var monsterEntries = new List<MonsterEntry>();
                var armorTypeEntries = new Dictionary<ArmorType, ArmorTypeEntry>();

                MonsterData(monsterEntries);
                ArmorData(armorTypeEntries);
            retrySearchBy:
                Console.WriteLine("Do you want to search by (n)ame or (a)rmor?");
                string searchBy = Console.ReadLine();

                if (searchBy == "n")
                {
                    Console.Clear();
                retrySearch:
                    Console.WriteLine("Enter a query to search monsters by name:");
                    string search = Console.ReadLine();

                    nameSearch(monsterEntries, search);

                    if (searchResults.Count == 0)
                    {
                        Console.WriteLine("\nNo monsters were found. Press any key to try again.");
                        Console.ReadKey();
                        Console.Clear();
                        goto retrySearch;
                    }

                    else if (searchResults.Count == 1)
                    {
                        MonsterInfo(monsterEntries, armorTypeEntries, searchResults[0]);
                    }

                    else
                    {
                        string result = SearchResult();
                        MonsterInfo(monsterEntries, armorTypeEntries, result);
                    }
                }

                else if (searchBy == "a")
                {
                    Console.WriteLine("\nWhich armor type do you want to display?");
                    string[] armorTypes = Enum.GetNames(typeof(ArmorType));

                    for (int i = 0; i < armorTypes.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}: {armorTypes[i]}");
                    }

                    Console.WriteLine("\nEnter number:");
                retryArmorNumber:
                    int chosen;
                    try
                    {
                        chosen = Int32.Parse(Console.ReadLine()) - 1;
                        armorSearch(monsterEntries, armorTypes[chosen]);

                        if (searchResults.Count == 0)
                        {
                            goto retryArmorNumber;
                        }

                        else if (searchResults.Count == 1)
                        {
                            MonsterInfo(monsterEntries, armorTypeEntries, searchResults[0]);
                        }

                        else
                        {
                            string result = SearchResult();
                            MonsterInfo(monsterEntries, armorTypeEntries, result);
                        }

                    }

                    catch
                    {
                        int cursor = Console.CursorTop;
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new String(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, cursor - 1);
                        goto retryArmorNumber;
                    }
                }

                else
                {
                    int cursor = Console.CursorTop;
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new String(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, cursor - 2);
                    goto retrySearchBy;
                }

            askAgain:
                Console.WriteLine($"\nDo you want to search again? Y/N");
                string again = Console.ReadLine().ToLower();

                if (again == "y" || again == "n")
                {
                    if (again == "y")
                    {
                        searchAgain = true;
                        searchResults.Clear();
                        Console.Clear();
                    }

                    else
                    {
                        searchAgain = false;
                    }
                }

                else
                {
                    int cursor = Console.CursorTop;
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new String(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, cursor - 3);
                    goto askAgain;
                }

            } while (searchAgain == true);


        }

        static void nameSearch(List<MonsterEntry> monsterEntries, string search)
        {
            for (int i = 0; i < monsterEntries.Count; i++)
            {
                if (Regex.IsMatch(monsterEntries[i].Name, $"({search} ?)", RegexOptions.IgnoreCase))
                {
                    searchResults.Add(monsterEntries[i].Name);
                }
            }
        }

        static void armorSearch(List<MonsterEntry> monsterEntries, string search)
        {
            for (int i = 0; i < monsterEntries.Count; i++)
            {
                if (Regex.IsMatch(monsterEntries[i].Name, $"({search} ?)", RegexOptions.IgnoreCase))
                {
                    searchResults.Add(monsterEntries[i].Name);
                }
            }

        }

        static string SearchResult()
        {
            Console.WriteLine("\nWhich monster did you want to look up?");
            for (int i = 0; i < searchResults.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {searchResults[i]}");
            }
            Console.WriteLine("\nEnter number:");
        retryNumber:
            int chosen;
            try
            {
                chosen = Int32.Parse(Console.ReadLine()) - 1;
                return searchResults[chosen];
            }

            catch
            {
                int cursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, cursor - 1);
                goto retryNumber;
            }
        }
        static void MonsterInfo(List<MonsterEntry> monsterEntries, Dictionary<ArmorType, ArmorTypeEntry> armorTypeEntries, string result)
        {
            int index = -1;
            for (int i = 0; i < monsterEntries.Count; i++)
            {
                if (Regex.IsMatch(monsterEntries[i].Name, $"(^{result}$)", RegexOptions.IgnoreCase))
                {
                    index = i;
                    break;
                }
            }

            Console.WriteLine($"\nDisplaying information for {monsterEntries[index].Name}.\n");
            Console.WriteLine($"Name: {monsterEntries[index].Name}");
            Console.WriteLine($"Description: {monsterEntries[index].Description}");
            Console.WriteLine($"Alignment: {monsterEntries[index].Alignment}");
            Console.WriteLine($"Hit points: {monsterEntries[index].HitPoints}");
            Console.WriteLine($"Armor class: {monsterEntries[index].Armor.Class}");

            if (monsterEntries[index].Armor.Type == ArmorType.Unspecified.ToString())
            {
                Console.WriteLine($"Armor type: {monsterEntries[index].Armor.Type}");
            }

            else if (monsterEntries[index].Armor.Type == ArmorType.Other.ToString())
            {
                Console.WriteLine($"Armor type: {monsterEntries[index].Armor.Type}");
            }

            else if (monsterEntries[index].Armor.Type == ArmorType.Natural.ToString())
            {
                Console.WriteLine($"Armor type: {monsterEntries[index].Armor.Type}");
            }

            else
            {
                ArmorTypeEntry armorTypeEntry = armorTypeEntries[Enum.Parse<ArmorType>(monsterEntries[index].Armor.Type)];

                Console.WriteLine($"Armor type: {armorTypeEntry.Name}");
                Console.WriteLine($"Armor category: {armorTypeEntry.Category}");
                Console.WriteLine($"Armor weight: {armorTypeEntry.Weight}");
            }
        }

        static void MonsterData(List<MonsterEntry> monsterEntries)
        {
            for (int i = 0; i < monster.Length; i++)
            {
                var monsterEntry = new MonsterEntry();
                String[] info = monster[i].Split("\n");
                monsterEntry.Name = info[0];

                String[] description = info[1].Split(", ");
                monsterEntry.Description = description[0];
                monsterEntry.Alignment = description[1];

                string diceNotation = @"\d+ \(\d+\w\d+(\+\d+\)|\))";
                monsterEntry.HitPoints = Regex.Match(monster[i], diceNotation).ToString();

                Match armorClass = Regex.Match(info[3], @" (\d+)");
                monsterEntry.Armor.Class = Int32.Parse(armorClass.Groups[1].ToString());

                Match armorType = Regex.Match(info[3], @" \(+(.+)\)+");

                if (Regex.IsMatch(info[3], @" \(+(.+)\)+"))
                {

                    if (Regex.IsMatch(armorType.Groups[1].ToString(), "Natural", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.Natural.ToString();
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Studded Armor", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.StuddedLeather.ToString();
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Leather", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.Leather.ToString();
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Hide", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.Hide.ToString();
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Chain Shirt", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.ChainShirt.ToString();
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Chain Mail", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.ChainMail.ToString();
                    }

                    else if (Regex.IsMatch(armorType.Groups[1].ToString(), "Plate", RegexOptions.IgnoreCase))
                    {
                        monsterEntry.Armor.Type = ArmorType.Plate.ToString();
                    }

                    else
                    {
                        monsterEntry.Armor.Type = ArmorType.Other.ToString();
                    }
                }

                else
                {
                    monsterEntry.Armor.Type = ArmorType.Unspecified.ToString();
                }

                monsterEntries.Add(monsterEntry);
            }
        }
        static void ArmorData(Dictionary<ArmorType, ArmorTypeEntry> armorTypeEntries)
        {

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


