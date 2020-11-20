using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lost_in_Delvora___Text_Adventure
{
    enum LocationId
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
        Mines,
        Campfire
    }

    enum ThingsId
    {
        Knife,
        FireStarter,
        Key,
        Chest,
        Tiara,
        Pickaxe,
        GoldDeposit,
        GoldOre,
        Dynamite
    }

    enum Direction
    {
        North,
        South,
        West,
        East,
        NorthWest,
        NorthEast,
        SouthWest,
        SouthEast,
    }
    class LocationData
    {
        public LocationId ID;
        public string Name;
        public string Description;
        public Dictionary<Direction, LocationId> Directions;
    }
    class ThingData
    {
        public ThingsId ID;
        public string Name;
        public string Description;
        public LocationId StartingLocationId;
    }

    class Program
    {
        const ConsoleColor NarrativeColor = ConsoleColor.Gray;
        const ConsoleColor PromptColor = ConsoleColor.White;
        const int PrintPauseMilliseconds = 20;

        // Data dictionaries
        static Dictionary<LocationId, LocationData> LocationsData = new Dictionary<LocationId, LocationData>();
        static Dictionary<ThingsId, ThingData> ThingsData = new Dictionary<ThingsId, ThingData>();

        // Current state
        static LocationId CurrentLocationId = LocationId.House;
        static Dictionary<ThingsId, LocationId> ThingsLocations = new Dictionary<ThingsId, LocationId>();

        static void Main(string[] args)
        {
            Initialization();
            InitializeThingsState();
            Intro();
            Console.ForegroundColor = NarrativeColor;
            //Intro();
            DisplayLocation();
            while (true)
            {
                HandlePlayerAction();
            }
            
        }
        static void Initialization()
        {
            string[] dataFileLines = File.ReadAllLines("Level Data.txt");
            string[] thingsDataFileLines = File.ReadAllLines("Things Data.txt");

            int currentLocationStart = 0;
            while (currentLocationStart < dataFileLines.Length)
            {
                LocationId id;
                string name;
                string description;
                var directions = new Dictionary<Direction, LocationId>();
                string idText = dataFileLines[currentLocationStart];

                id = Enum.Parse<LocationId>(idText);
                name = dataFileLines[currentLocationStart + 1].Substring(6);
                description = dataFileLines[currentLocationStart + 2].Substring(12);

                // Read lines from dataFileLines[4] onwards until reaching an empty line (= while not reaching an empty line).
                int currentLineIndex = currentLocationStart + 4;

                while (dataFileLines[currentLineIndex] != "")
                {
                    // For each line, parse the direction and parse the location ID.
                    string currentDataLine = dataFileLines[currentLineIndex];
                    string[] currentDataLineParts = currentDataLine.Split(": ");

                    string directionText = currentDataLineParts[0];
                    string locationIdText = currentDataLineParts[1];

                    Direction direction = Enum.Parse<Direction>(directionText);
                    LocationId locationId = Enum.Parse<LocationId>(locationIdText);

                    // When parsed, add it to the dictionary.
                    directions.Add(direction, locationId);

                    currentLineIndex++;
                }

                var locationData = new LocationData
                {
                    ID = id,
                    Name = name,
                    Description = description,
                    Directions = directions
                };

                LocationsData[locationData.ID] = locationData;

                currentLocationStart = currentLineIndex + 1;
            }

            while (currentLocationStart < thingsDataFileLines.Length)
            {
                ThingsId id;
                string name;
                string description;
                string idText = thingsDataFileLines[currentLocationStart];

                id = Enum.Parse<ThingsId>(idText);
                name = thingsDataFileLines[currentLocationStart + 1].Substring(6);
                description = thingsDataFileLines[currentLocationStart + 2].Substring(12);

                // Read lines from dataFileLines[4] onwards until reaching an empty line (= while not reaching an empty line).
                int currentLineIndex = currentLocationStart + 4;

                while (thingsDataFileLines[currentLineIndex] != "")
                {
                    // For each line, parse the direction and parse the location ID.
                    string currentDataLine = thingsDataFileLines[currentLineIndex];
                    string[] currentDataLineParts = currentDataLine.Split(": ");
                    string thingsIdText = currentDataLineParts[1];
                    ThingsId thingsId = Enum.Parse<ThingsId>(thingsIdText);
                    currentLineIndex++;
                }

                var thingsData = new ThingData
                {
                    ID = id,
                    Name = name,
                    Description = description,
                };

                ThingsData[thingsData.ID] = thingsData;

                currentLocationStart = currentLineIndex + 1;
            }

        }
        static void InitializeThingsState()
        {
            // Set all things to their starting locations.
            foreach (KeyValuePair<ThingsId, ThingData> thingEntry in ThingsData)
            {
                ThingsLocations[thingEntry.Key] = thingEntry.Value.StartingLocationId;
            }
        }

        static void Intro()
        {
            Console.SetWindowSize(137, 50);
            Console.WriteLine(File.ReadAllText("Intro logo.txt"));
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
        static void HandlePlayerAction()
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
                    HandleMovement(Direction.North);
                    break;

                case "northeast":
                case "ne":
                    HandleMovement(Direction.NorthEast);
                    break;

                case "northwest":
                case "nw":
                    HandleMovement(Direction.NorthWest);
                    break;

                case "west":
                case "w":
                    HandleMovement(Direction.West);
                    break;

                case "southwest":
                case "sw":
                    HandleMovement(Direction.SouthWest);
                    break;

                case "south":
                case "s":
                    HandleMovement(Direction.South);
                    break;

                case "southeast":
                case "se":
                    HandleMovement(Direction.SouthEast);
                    break;

                case "east":
                case "e":
                    HandleMovement(Direction.East);
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
                   // Reply("Goodbye!");
                   // ShouldQuit = true;
                    break;

                default:
                   // Reply("I do not understand you.");
                    break;

            }
        }
        static void HandleMovement(Direction direction)
        {
            LocationData currentLocationData = LocationsData[CurrentLocationId];

            if (currentLocationData.Directions.ContainsKey(direction))
            {
                // Change current location ID to location ID at given direction
                LocationId newLocationId = currentLocationData.Directions[direction];
                CurrentLocationId = newLocationId;

                // Display current location
                DisplayLocation();
            }
            else
            {
                Console.WriteLine("Movement in this direction is not possible");
            }

        }
        static void DisplayLocation()
        {
            Console.Clear();

            //Display current location description.
            LocationData currentLocationData = LocationsData[CurrentLocationId];
            Print(currentLocationData.Description);
        }
        static IEnumerable<string> GetNames(IEnumerable<ThingsId> thingIds)
        {
            return thingIds.Select(thingId => ThingsData[thingId].Name);
        }

    }
}