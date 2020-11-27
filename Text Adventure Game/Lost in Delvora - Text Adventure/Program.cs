using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;


namespace Lost_in_Delvora___Text_Adventure
{
    #region Data types
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
    enum ThingId
    {
        Knife,
        FireStarter,
        Key,
        Chest,
        Tiara,
        Pickaxe,
        GoldDeposit,
        GoldOre,
        Dynamite,
        MemoryFragment,
        Lunera,
        Papa
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
        public ThingId ID;
        public string Name;
        public string Description;
        public LocationId StartingLocationId;
    }
    #endregion
        class Program
    {
        #region Fields
        const ConsoleColor NarrativeColor = ConsoleColor.Gray;
        const ConsoleColor PromptColor = ConsoleColor.White;
        const int PrintPauseMilliseconds = 20;
        const int NumberOfFragments = 7;

        // Data dictionaries
        static Dictionary<LocationId, LocationData> LocationsData = new Dictionary<LocationId, LocationData>();
        static Dictionary<ThingId, ThingData> ThingsData = new Dictionary<ThingId, ThingData>();

        // Current state
        static LocationId CurrentLocationId;
        static Dictionary<ThingId, LocationId> CurrentThingsLocations = new Dictionary<ThingId, LocationId>();
        static int NumberOfFragmentsFound;
        static int talkedToLuneraCount;

        // Thing helpers
        static Dictionary<string, ThingId> ThingIdsByName = new Dictionary<string, ThingId>() 
        { 
            { "knife", ThingId.Knife },
            { "tiara", ThingId.Tiara },
            { "pickaxe", ThingId.Pickaxe },
            { "firestarter", ThingId.FireStarter },
            { "dynamite", ThingId.Dynamite },
            { "gold", ThingId.GoldOre },
            { "ore", ThingId.GoldOre },
            { "lunera", ThingId.Lunera },
            { "papa", ThingId.Papa },
        };
        static ThingId[] ThingsYouCanGet = { ThingId.Knife, ThingId.Tiara, ThingId.Pickaxe };
        static ThingId[] ThingsYouCanTalkTo = { ThingId.Papa, ThingId.Lunera };

        // Bool used to end the game
        static bool quitGame;
        #endregion

        #region Program Start
        static void Main(string[] args)
        {
            ReadDataFiles();
            InitializeState();
            Console.ForegroundColor = NarrativeColor;
            Intro();
            DisplayLocation();
            while (!quitGame)
            {
                HandlePlayerAction();
                ApplyGameRules();
            }
        }
        static void ReadDataFiles()
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
                description = dataFileLines[currentLocationStart + 2].Substring(13);

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

            int currentThingsStart = 0;
            while (currentThingsStart < thingsDataFileLines.Length)
            {
                ThingId id;
                string name;
                string description;
                string idText = thingsDataFileLines[currentThingsStart];
                LocationId startingLocationId;
                string startingLocationIdText;

                id = Enum.Parse<ThingId>(idText);
                name = thingsDataFileLines[currentThingsStart + 1].Substring(6);
                description = thingsDataFileLines[currentThingsStart + 2].Substring(13);
                startingLocationIdText = thingsDataFileLines[currentThingsStart + 3].Substring(18);
                startingLocationId = Enum.Parse<LocationId>(startingLocationIdText);
                
                var thingsData = new ThingData
                {
                    ID = id,
                    Name = name,
                    Description = description,
                    StartingLocationId = startingLocationId
                };

                ThingsData[thingsData.ID] = thingsData;

                currentThingsStart += 5;
            }

        }
        static void InitializeState()
        {
            // Set starting location
            CurrentLocationId = LocationId.House;

            // Set all things to their starting locations.
            foreach (KeyValuePair<ThingId, ThingData> thingEntry in ThingsData)
            {
                CurrentThingsLocations[thingEntry.Key] = thingEntry.Value.StartingLocationId;
            }

            // Set fragments 
            NumberOfFragmentsFound = 0;

            // Set event variables
            talkedToLuneraCount = 0;

            
        }
        static void Intro()
        {
            Console.SetWindowSize(137, 50);
            Console.WriteLine(File.ReadAllText("IntroLogo.txt"));
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
            Print(File.ReadAllText("IntroStory.txt"));
            Console.ReadKey();
        }
        static void ApplyGameRules()
        {
            
        }

    #endregion

        #region Output Helpers
    /// <summary>
    /// Writes the specified text to the output.
    /// </summary>
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
        #endregion

        #region Interaction
        static void HandlePlayerAction()
        {
            // Ask the player what they want to do.
            Print("What now?");

            Console.ForegroundColor = PromptColor;
            Console.Write("> ");

            string command = Console.ReadLine().ToLowerInvariant();

            // Analyze the command by assuming the first word is a verb (or similar instruction).
            string[] words = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string verb = "";
            if (command != "")
            {
                verb = words[0];
            }

            // Getting names of things from the command.
            List<ThingId> thingIdsFromCommand = GetThingIdsFromWords(words);

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

                //Verbs
                case "pick":
                case "p":
                    HandleGet(thingIdsFromCommand);
                    break;

                case "look":
                case "inspect":
                    HandleLook(thingIdsFromCommand);
                    break;

                case "inventory":
                case "i":
                    HandleInventory();
                    break;

                case "talk":
                case "t":
                    HandleTalk(thingIdsFromCommand);
                    break;

                case "use":
                case "u":
                    //TODO
                    break;

                case "drop item":
                case "d":
                    HandleDrop(thingIdsFromCommand);
                    break;

                case "end":
                case "quit":
                case "exit":
                    {
                        Print("Thanks for playing!");
                        quitGame = true;
                    }
                    break;

                default:
                    Print("I do not understand you.");
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
        static void HandleGet(List<ThingId> thingIds)
        {
            // Check that we have a thing
            if (thingIds.Count == 0)
            {
                Print("Try again");
                return;
            }

            // First thing to be picked up
            ThingId thingToBePicked = thingIds[0];
            // Get name of thing
            string thingName = GetThingName(thingToBePicked);

            // Check if we have thing
            if (HaveThing(thingToBePicked))
            {
                Print($"{thingName} is already in your possession.");
                return;
            }

            // Make sure the thing is at this location.
            if (!ThingIsAtCurrentLocation(thingToBePicked))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Checks if thing can be picked up
            if (!ThingsYouCanGet.Contains(thingToBePicked))
            {
                Print($"You can't pick up {thingName}");
                return;
            }
            // Everything seems to be OK, take the thing.
            GetThing(thingToBePicked);
            Print($"You picked up {thingName}");


        }
        static void HandleDrop(List<ThingId> thingIds)
        {
            // Check that we have a thing
            if (thingIds.Count == 0)
            {
                Print("That's not an item");
                return;
            }

            // First thing to be picked up
            ThingId thingToBePicked = thingIds[0];
            // Get name of thing
            string thingName = GetThingName(thingToBePicked);

            // Check if we have thing
            if (HaveThing(thingToBePicked))
            {
                Print($"{thingName} is already in your possession.");
                return;
            }

            // Make sure the thing is at this location.
            if (!ThingIsAtCurrentLocation(thingToBePicked))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Checks if thing can be picked up
            if (!ThingsYouCanGet.Contains(thingToBePicked))
            {
                Print($"You can't pick up {thingName}");
                return;
            }
            // Everything seems to be OK, take the thing.
            DropThing(thingToBePicked);

        }
        static void HandleLook(List<ThingId> thingIds)
        {
            ThingId thingToBeLooked = thingIds[0];
            string thingName = GetThingName(thingToBeLooked);

            // Make sure the thing is at this location.
            if (ThingAvailable(thingToBeLooked))
            {
                Print(ThingsData[thingToBeLooked].Description);
            }
            else
            {
                Print($"{thingName} is not here.");
            }
        }
        static void HandleTalk(List<ThingId> thingIds)
        {
            // Check that we have a thing
            if (thingIds.Count == 0)
            {
                Print("");
                return;
            }
            // First thing to be talked to
            ThingId thingToBeTalkedTo = thingIds[0];
            // Get name of thing
            string thingName = GetThingName(thingToBeTalkedTo);

            if (!ThingsYouCanTalkTo.Contains(thingToBeTalkedTo))
            {
                Print("You can't talk to that.");
                return;
            }

            // Make sure the NPC is present.
            if (!ThingIsAtCurrentLocation(thingToBeTalkedTo))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeTalkedTo)
            {
                case ThingId.Lunera:
                    TalkToLunera();
                    break;

                case ThingId.Papa:
                    TalkToPapa();
                    break;
            }

        }
        static void HandleInventory()
        {
            // Go through all the things and find the ones that have inventory as location.
            foreach (KeyValuePair<ThingId, LocationId> item in CurrentThingsLocations)
            {
                if (item.Value == LocationId.Inventory)
                {
                    Print(GetThingName(item.Key));
                }
            }
        }
        #endregion

        #region Interaction Helpers
        static List<ThingId> GetThingIdsFromWords(string[] words)
        {
            var thingIds = new List<ThingId>();

            foreach (string word in words)
            {
                if (ThingIdsByName.ContainsKey(word))
                {
                    thingIds.Add(ThingIdsByName[word]);
                }
            }

            return thingIds;
        }
        static IEnumerable<ThingId> GetThingsAtLocation(LocationId locationId)
        {
            // Returns all the ThingIds for things at the given location.
            return CurrentThingsLocations.Keys.Where(thingId => CurrentThingsLocations[thingId] == locationId);
        }
        static string GetName(ThingId thingId)
        {
            // Returns the name of a thing.
            return ThingsData[thingId].Name;
        }
        static IEnumerable<string> GetNames(IEnumerable<ThingId> thingIds)
        {
            return thingIds.Select(thingId => ThingsData[thingId].Name);
        }
        #endregion

        #region Display Helpers
        static void DisplayLocation()
        {
            Console.Clear();

            //Display current location description.
            LocationData currentLocationData = LocationsData[CurrentLocationId];
            Print(currentLocationData.Description);
        }
        static void DisplayThing(ThingId thing)
        {
            Console.Clear();

            //Display Thing data
            ThingData currentThingData = ThingsData[thing];
            Print(currentThingData.Description);
        }
        static void DisplayInventory()
        {

        }
        static string GetThingName(ThingId thingId)
        {
            return ThingsData[thingId].Name;
        }


        #endregion

        #region Event Helpers
        static bool ThingAt(ThingId thingId, LocationId locationId)
        {
            LocationId currentLocationOfThing = CurrentThingsLocations[thingId];
            return currentLocationOfThing == locationId;
        }
        static bool ThingIsAtCurrentLocation(ThingId thingId)
        {
            return ThingAt(thingId, CurrentLocationId);
        }
        static bool ThingAvailable(ThingId thingId)
        {
            return ThingIsAtCurrentLocation(thingId) || HaveThing(thingId);
        }
        static bool HaveThing(ThingId thingId)
        {
           return ThingAt(thingId, LocationId.Inventory);
        }
        static void MoveThing(ThingId thingId, LocationId locationId)
        {
            CurrentThingsLocations[thingId] = locationId;
        }
        static void GetThing(ThingId thingId)
        {
            MoveThing(thingId, LocationId.Inventory);
        }
        static void DropThing(ThingId thingId)
        {
            MoveThing(thingId, CurrentLocationId);
        }
        
        #endregion

        #region NPC Interactions
        static void TalkToLunera()
        {
            if (talkedToLuneraCount > 0)
            {
                // Dialogue number 2
            }
            else
            {
                Console.Clear();
                string dialogue1 = File.ReadAllText("LuneraFirstDialogue.txt");
                // Dialogue number 1
                Print(dialogue1);
                talkedToLuneraCount++;
            }
        }

        static void TalkToPapa()
        {

        }

    }
    #endregion

}
