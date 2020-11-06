using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lost_in_Delvora___Text_Adventure
{
    enum LocationID
    {
        Nowhere,
        Inventory,
        House,
        HouseEntrance,
        Well,
        ToolShack,
        Barn,
        Field,
        Greenhouse,
        ElevatorShaft,
        Mine,
        Campfire
    }
    class LocationData
    {
        public LocationID ID;
        public string Name;
        public string Description;
    }

    class Program
    {
        const ConsoleColor NarrativeColor = ConsoleColor.Gray;
        const ConsoleColor PromptColor = ConsoleColor.White;
        const int PrintPauseMilliseconds = 20;

        // Data dictionaries
        static Dictionary<LocationID, LocationData> LocationsData = new Dictionary<LocationID, LocationData>();

        // Current state
        static LocationID CurrentLocationID = LocationID.House;


        static void Main(string[] args)
        {
            Initialization();
            Console.ForegroundColor = NarrativeColor;
            Intro();
        }
        static void Initialization()
        {
            string idText;
            LocationID id;

            string name;
            string description;

            string[] dataFileLines = File.ReadAllLines("Level Data.txt");
            
            idText = dataFileLines[0];
            id = Enum.Parse<LocationID>(idText);

            name = dataFileLines[1].Substring(6);
            description = dataFileLines[2].Substring(12);
                       
            var houseData = new LocationData();
            houseData.ID = id;
            houseData.Name = name;
            houseData.Description = description;



            houseData = new LocationData
            {
                ID = Enum.Parse<LocationID>(dataFileLines[0]),
                Name = dataFileLines[1].Substring(6),
                Description = dataFileLines[2].Substring(12),
            };

            LocationsData[houseData.ID] = houseData;

        }
        static void Intro()
        {
            Console.SetWindowSize(137, 50);
            Console.WriteLine("##::::::::'#######:::'######::'########::::'####:'##::: ##::::'########::'########:'##:::::::'##::::'##::'#######::'########:::::'###::::");
            Console.WriteLine("##:::::::'##.... ##:'##... ##:... ##..:::::. ##:: ###:: ##:::: ##.... ##: ##.....:: ##::::::: ##:::: ##:'##.... ##: ##.... ##:::'## ##:::");
            Console.WriteLine("##::::::: ##:::: ##: ##:::..::::: ##:::::::: ##:: ####: ##:::: ##:::: ##: ##::::::: ##::::::: ##:::: ##: ##:::: ##: ##:::: ##::'##:. ##::");
            Console.WriteLine("##::::::: ##:::: ##:. ######::::: ##:::::::: ##:: ## ## ##:::: ##:::: ##: ######::: ##::::::: ##:::: ##: ##:::: ##: ########::'##:::. ##:");
            Console.WriteLine("##::::::: ##:::: ##::..... ##:::: ##:::::::: ##:: ##. ####:::: ##:::: ##: ##...:::: ##:::::::. ##:: ##:: ##:::: ##: ##.. ##::: #########:");
            Console.WriteLine("##::::::: ##:::: ##:'##::: ##:::: ##:::::::: ##:: ##:. ###:::: ##:::: ##: ##::::::: ##::::::::. ## ##::: ##:::: ##: ##::. ##:: ##.... ##:");
            Console.WriteLine("########:. #######::. ######::::: ##:::::::'####: ##::. ##:::: ########:: ########: ########:::. ###::::. #######:: ##:::. ##: ##:::: ##:");
            Console.ReadKey();
            Console.Clear();
            bool canHear = false;
            do
            {
                Print("Can you hear me?");
                Console.WriteLine("Yes or No");
                string command = Console.ReadLine().ToLowerInvariant();
                string[] words = command.Split(' ');
                string answer = words[0];
                switch (answer)
                {
                    case "yes":
                    case "y":
                        Console.Clear();
                        Print("Good. Can you tell me your name?");
                        canHear = true;
                        break;

                    case "no":
                    case "n":
                        Console.Clear();
                        Print("Oh...");
                        break;

                    default:
                        Console.Clear();
                        Print("...");
                        break;
                }
            } while (!canHear);
           
            string playerName = Console.ReadLine();
            Print($"Oh...{playerName}...that's a nice name i guess. Anyway, you need to wake up and find out the truth of what happened.");
            Console.ReadKey();
            Console.Clear();
        }
        static void Print(string text)
        {
            int maximumLineLength = Console.WindowWidth - 1;
            MatchCollection lineMatches = Regex.Matches(text, @"(.{1," + maximumLineLength + @"})(?:\s|$)");

            foreach (Match match in lineMatches)
            {
                string line = match.Groups[0].Value;
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(PrintPauseMilliseconds);
                }
                Console.WriteLine();

            }
        }

        /*static void HandlePlayerAction()
        {
            // Ask the player what they want to do.
            Print("What now?");

            Console.ForegroundColor = PromptColor;
            Console.Write("> ");

            string command = Console.ReadLine().ToLowerInvariant();

            // Analyze the command by assuming the first word is a verb (or similar instruction).
            string[] words = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string verb = words[0];

            switch (verb)
            {
                case "north":
                case "n":
                    //TODO
                    break;

                case "northeast":
                case "ne":
                    //TODO
                    break;

                case "northwest":
                case "nw":
                    //TODO
                    break;

                case "west":
                case "w":
                    //TODO
                    break;

                case "southwest":
                case "sw":
                    //TODO
                    break;

                case "south":
                case "s":
                    //TODO
                    break;

                case "southeast":
                case "se":
                    //TODO
                    break;

                case "east":
                case "e":
                    //TODO
                    break;

                case "pick up":
                case "p":
                    //TODO
                    break;

                case "inspect":
                case "ins":
                    //TODO
                    break;

                case "inventory":
                case "i":
                    //TODO
                    break;

                case "talk":
                case "t":
                    //TODO
                    break;

                case "use":
                case "u":
                    //TODO
                    break;

                case "drop item":
                case "d":
                    //TODO
                    break;

                case "end":
                case "quit":
                case "exit":
                    Reply("Goodbye!");
                    ShouldQuit = true;
                    break;

                default:
                    Reply("I do not understand you.");
                    break;

            }
        }*/
        static void DisplayLocation()
        {
            Console.Clear();

            // Display current location description.
            //LocationData currentLocationData = LocationsData[CurrentLocation];
            //Print(currentLocationData.Description);
        }

    }
}