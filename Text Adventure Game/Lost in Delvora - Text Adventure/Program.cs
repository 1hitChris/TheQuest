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
        BurnedDownToolShack,
        BlownUpToolShack,
        Barn,
        Field,
        Greenhouse,
        ElevatorShaft,
        Mines,
        MinesNoRight,
        MinesNoLeft,
        Campfire,
        LeftTunnel,
        RightTunnel,
    }
    enum ThingId
    {
        Knife,
        FireStarter,
        Campfire,
        Key,
        Herbs,
        Potion,
        Book,
        Joint,
        Rope,
        Well,
        Chest,
        Tiara,
        Pickaxe,
        GoldDeposit,
        GoldOre,
        Dynamite,
        LitDynamite,
        Tools,
        Cloth,
        Bundle,
        Dough,
        Bread,
        Thread,
        Dress,
        Water,
        Bell,
        Shirt,
        Weeds,
        Wheat,
        Doll,
        Meat,
        CookedMeat,
        LunerasMemoryFragment,
        WilbertsMemoryFragment,
        EvalinasMemoryFragment,
        GillhardtsMemoryFragment,
        GustofsMemoryFragment,
        SloansMemoryFragment,
        PapasMemoryFragment,
        Lunera,
        Papa,
        Wilbert,
        Evalina,
        Gillhardt,
        Gustof,
        Sloan
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
        Down,
        Up,
        Left,
        Right,
        Back,
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
        const ConsoleColor LuneraDialogueColor = ConsoleColor.Blue;
        const ConsoleColor PapaDialogueColor = ConsoleColor.DarkMagenta;
        const ConsoleColor WilbertDialogueColor = ConsoleColor.DarkRed;
        const ConsoleColor EvalinaDialogueColor = ConsoleColor.DarkGreen;
        const ConsoleColor GillhardtDialogueColor = ConsoleColor.Yellow;
        const ConsoleColor GustofDialogueColor = ConsoleColor.DarkYellow;
        const ConsoleColor SloanDialogueColor = ConsoleColor.Cyan;
        const int PrintPauseMilliseconds = 5;
        const int NumberOfFragments = 7;

        // Data dictionaries
        static Dictionary<LocationId, LocationData> LocationsData = new Dictionary<LocationId, LocationData>();
        static Dictionary<ThingId, ThingData> ThingsData = new Dictionary<ThingId, ThingData>();

        // Current state
        static LocationId CurrentLocationId;
        static Dictionary<ThingId, LocationId> CurrentThingsLocations = new Dictionary<ThingId, LocationId>();
        static int NumberOfFragmentsFound;
        static int talkedToLuneraCount;
        static int talkedToEvalinaCount;
        static int talkedToWilbertCount;
        static int talkedToGillhardtCount;
        static int talkedToGustofCount;
        static bool toolsInWell;
        static bool toolsBurned;
        static bool toolsDestroyed;
        static bool GustofBread;
        static bool GustofMeat;
        static bool EvalinaDress;
        static bool EvalinaDoll;
        static bool GillhardtJoint;
        static bool GillhardtPotion;
        static bool LuneraTiara;
        static bool LuneraGold;
        static bool SloanBell;

        // Thing helpers
        static Dictionary<string, ThingId> ThingIdsByName = new Dictionary<string, ThingId>() 
        { 
            { "knife", ThingId.Knife },
            { "tiara", ThingId.Tiara },
            { "pickaxe", ThingId.Pickaxe },
            { "firestarter", ThingId.FireStarter },
            { "dynamite", ThingId.Dynamite },
            { "lit", ThingId.LitDynamite },
            { "gold", ThingId.GoldOre },
            { "cloth", ThingId.Cloth },
            { "dress", ThingId.Dress },
            { "potion", ThingId.Potion },
            { "book", ThingId.Book },
            { "herbs", ThingId.Herbs },
            { "joint", ThingId.Joint },
            { "shirt", ThingId.Shirt },
            { "weeds", ThingId.Weeds },
            { "wheat", ThingId.Wheat },
            { "bread", ThingId.Bread },
            { "bundle", ThingId.Bundle },
            { "water", ThingId.Water },
            { "dough", ThingId.Dough },
            { "thread", ThingId.Thread },
            { "ore", ThingId.GoldOre },
            { "tools", ThingId.Tools },
            { "meat", ThingId.Meat },
            { "rope", ThingId.Rope },
            { "doll", ThingId.Doll },
            { "key", ThingId.Key },
            { "chest", ThingId.Chest },
            { "golddeposit", ThingId.GoldDeposit },
            { "deposit", ThingId.GoldDeposit },
            { "campfire", ThingId.Campfire },
            { "lunera", ThingId.Lunera },
            { "papa", ThingId.Papa },
            { "wilbert", ThingId.Wilbert },
            { "evalina", ThingId.Evalina },
            { "gillhardt", ThingId.Gillhardt },
            { "gustof", ThingId.Gustof },
            { "sloan", ThingId.Sloan },
        };
        static ThingId[] ThingsYouCanGet = { ThingId.Knife, ThingId.Tiara, ThingId.Pickaxe, ThingId.Dynamite, ThingId.GoldOre, ThingId.Key, ThingId.LunerasMemoryFragment, ThingId.Tools, 
        ThingId.Dress, ThingId.Wheat, ThingId.Weeds, ThingId.Bell, ThingId.Shirt, ThingId.Cloth, ThingId.Thread, ThingId.Bundle, ThingId.Dough, ThingId.Doll, ThingId.Bread, ThingId.Meat, 
        ThingId.CookedMeat, ThingId.Book, ThingId.Herbs, ThingId.Joint, ThingId.Potion, ThingId.LunerasMemoryFragment, ThingId.PapasMemoryFragment, ThingId.WilbertsMemoryFragment, ThingId.EvalinasMemoryFragment,
        ThingId.GillhardtsMemoryFragment, ThingId.GustofsMemoryFragment, ThingId.SloansMemoryFragment};
        static ThingId[] ThingsYouCanTalkTo = { ThingId.Papa, ThingId.Lunera, ThingId.Wilbert, ThingId.Evalina, ThingId.Gillhardt, ThingId.Gustof, ThingId.Sloan };
        static ThingId[] ThingsYouCanUse = { ThingId.Key, ThingId.FireStarter, ThingId.Dynamite, ThingId.LitDynamite, ThingId.Pickaxe, ThingId.Cloth, ThingId.Shirt, ThingId.Bundle,
        ThingId.Weeds, ThingId.Wheat, ThingId.Thread, ThingId.Water, ThingId.Dough, ThingId.Campfire, ThingId.Meat, ThingId.Knife, ThingId.Book,};

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
            talkedToEvalinaCount = 0;
            talkedToWilbertCount = 0;
            talkedToGillhardtCount = 0;
            talkedToGustofCount = 0;
            toolsInWell = false;
            toolsBurned = false;
            toolsDestroyed = false;
            GustofBread = false;
            GustofMeat = false;
            EvalinaDress = false;
            EvalinaDoll = false;
            GillhardtJoint = false;
            GillhardtPotion = false;
            LuneraTiara = false;
            LuneraGold = false;
            SloanBell = false;
        }
        static void Intro()
        {
            Console.SetWindowSize(137, 50);
            Console.WriteLine(File.ReadAllText("Textfiles/Intro/Warning.txt"));
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine(File.ReadAllText("Textfiles/Intro/IntroLogo.txt"));
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
            Print(File.ReadAllText("Textfiles/Intro/IntroStory.txt"));
            Console.ReadKey();
            Console.Clear();
            Print(File.ReadAllText("Textfiles/Intro/IntroStoryPart2.txt"));
            Console.ReadKey();
            Console.Clear();
        }
        static void ApplyGameRules()
        {
            // Using firestarter on dynamite makes it a lit dynamite
            if (HaveThing(ThingId.LitDynamite))
            {
                SwapThing(ThingId.Dynamite, ThingId.LitDynamite);

                // We also need the word dynamite to refer to lit dynamite now.
                ThingIdsByName["dynamite"] = ThingId.LitDynamite;
            }

            if (HaveThing(ThingId.CookedMeat))
            {
                SwapThing(ThingId.Meat, ThingId.CookedMeat);

                // We also need the word meat to refer to cooked meat now.
                ThingIdsByName["meat"] = ThingId.CookedMeat;
            }

            if (NumberOfFragmentsFound < NumberOfFragments)
            {
                return;
            }
            else
            {
                if (LuneraGold == true)
                {

                }
                if (LuneraTiara == true)
                {

                }
                if (toolsInWell == true)
                {

                }
                if (toolsBurned == true)
                {

                }
                if (toolsDestroyed == true)
                {

                }
                if (EvalinaDoll == true)
                {

                }
                if (EvalinaDress == true)
                {

                }
                if (GillhardtJoint == true)
                {

                }
                if (GillhardtPotion == true)
                {

                }
                if (GustofBread == true)
                {

                }
                if (GustofMeat == true)
                {

                }
                if (SloanBell == true)
                {

                }
                if (toolsDestroyed == true)
                {

                }
                string credits = File.ReadAllText("Credits.txt");
                Print(credits);
                quitGame = true;
            }
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
                bool nextCharIsCommand = false;
                string line = match.Groups[0].Value;
                foreach (char c in line)
                {
                    if (nextCharIsCommand)
                    {
                        switch (c)
                        {
                            case '0':
                                Console.ForegroundColor = NarrativeColor;
                                break;

                            case '1':
                                Console.ForegroundColor = PapaDialogueColor;
                                break;

                            case '2':
                                Console.ForegroundColor = LuneraDialogueColor;
                                break;

                            case '3':
                                Console.ForegroundColor = WilbertDialogueColor;
                                break;

                            case '4':
                                Console.ForegroundColor = EvalinaDialogueColor;
                                break;

                            case '5':
                                Console.ForegroundColor = GillhardtDialogueColor;
                                break;

                            case '6':
                                Console.ForegroundColor = GustofDialogueColor;
                                break;

                            case '7':
                                Console.ForegroundColor = SloanDialogueColor;
                                break;
                        }
                        nextCharIsCommand = false;
                    }
                    else
                    {
                        if (c == '$')
                        {
                            nextCharIsCommand = true;
                            continue;
                        }
                        Console.Write(c);
                        Thread.Sleep(PrintPauseMilliseconds);
                    }

                }
                Console.WriteLine();

            }
        }
        static void PrintPrompt(string text)
        {
            Console.ForegroundColor = PromptColor;
            Print(text);
            Console.ForegroundColor = NarrativeColor;
        }

        #endregion

        #region Interaction
        static void HandlePlayerAction()
        {
            // Ask the player what they want to do.
            PrintPrompt("What now?");

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

                case "down":
                    HandleMovement(Direction.Down);
                    break;

                case "up":
                    HandleMovement(Direction.Up);
                    break;

                case "left":
                    HandleMovement(Direction.Left);
                    break;

                case "right":
                    HandleMovement(Direction.Right);
                    break;

                case "back":
                    HandleMovement(Direction.Back);
                    break;

                //Verbs
                case "pick":
                case "take":
                case "get":
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
                    HandleUse(thingIdsFromCommand);
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
                Console.Clear();
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
            // Check that we have a thing
            if (thingIds.Count == 0)
            {
                Print("Try again");
                return;
            }
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

                case ThingId.Wilbert:
                    TalkToWilbert();
                    break;

                case ThingId.Evalina:
                    TalkToEvalina();
                    break;

                case ThingId.Gillhardt:
                    TalkToGillhardt();
                    break;

                case ThingId.Gustof:
                    TalkToGustof();
                    break;

                case ThingId.Sloan:
                    TalkToSloan();
                    break;
            }

        }
        static void HandleSwap()
        {

        }
        static void HandleInventory()
        {
            Print("INVENTORY:");
            // Go through all the things and find the ones that have inventory as location.
            foreach (KeyValuePair<ThingId, LocationId> item in CurrentThingsLocations)
            {
                if (item.Value == LocationId.Inventory)
                {
                    
                    Print(GetThingName(item.Key));
                }
            }
            Console.WriteLine();
        }
        static void HandleUse(List<ThingId> thingIds)
        {
            // Check that command includes a thing
            if (thingIds.Count == 0)
            {
                Print("Try again");
                return;
            }
            // First thing to be used
            ThingId thingToBeUsed = thingIds[0];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsed);

            // Make sure this thing can be used
            if (!ThingsYouCanUse.Contains(thingToBeUsed))
            {
                Print("You can't use that.");
                return;
            }

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsed))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsed)
            {
                case ThingId.Key:
                    UseKey(thingIds);
                    break;

                case ThingId.FireStarter:
                    UseFireStarter(thingIds);
                    break;

                case ThingId.LitDynamite:
                    UseLitDynamite(thingIds);
                    break;

                case ThingId.Pickaxe:
                    UsePickaxe(thingIds);
                    break;

                case ThingId.Cloth:
                    UseCloth(thingIds);
                    break;

                case ThingId.Shirt:
                    UseShirt(thingIds);
                    break;

                case ThingId.Thread:
                    UseThread(thingIds);
                    break;

                case ThingId.Water:
                    UseWater(thingIds);
                    break;

                case ThingId.Dough:
                    UseDough(thingIds);
                    break;

                case ThingId.Meat:
                    UseMeat(thingIds);
                    break;

                case ThingId.Tools:
                    UseTools(thingIds);
                    break;

                case ThingId.Knife:
                    UseKnife(thingIds);
                    break;

                case ThingId.Book:
                    UseBook(thingIds);
                    break;
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
        static void SwapThing(ThingId thingId1, ThingId thingId2)
        {
           // LocationId locationOfThing1 = CurrentThingsLocations[thingId1];
            MoveThing(thingId1, LocationId.Nowhere);
            MoveThing(thingId2, LocationId.Inventory);
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

        #region NPC/Thing Interactions
        static void TalkToLunera()
        {
            if (HaveThing(ThingId.Tiara))
            {
                Console.Clear();
                string dialogueTiara = File.ReadAllText("Textfiles/Lunera/LuneraTiaraDialogue.txt");
                // Dialogue Tiara
                Print(dialogueTiara);
                Console.ReadKey();
                Console.Clear();
                // Dialogue Tiara Memory Fragment
                string dialogueTiaraFragment = File.ReadAllText("Textfiles/Lunera/TiaraMemoryFragment.txt");
                Print(dialogueTiaraFragment);
                GetThing(ThingId.LunerasMemoryFragment);
                NumberOfFragmentsFound++;
                LuneraTiara = true;
                return;
            }
            if (HaveThing(ThingId.GoldOre))
            {
                Console.Clear();
                string dialogueGold = File.ReadAllText("Textfiles/Lunera/LuneraGoldDialogue.txt");
                // Dialogue Gold ore
                Print(dialogueGold);
                Console.ReadKey();
                Console.Clear();
                string dialogueGoldFragment = File.ReadAllText("Textfiles/Lunera/GoldMemoryFragment.txt");
                // Dialogue Memory Fragment
                Print(dialogueGoldFragment);
                MoveThing(ThingId.GoldOre, LocationId.Nowhere);
                GetThing(ThingId.LunerasMemoryFragment);
                NumberOfFragmentsFound++;
                LuneraGold = true;
                return;
            }
            if (HaveThing(ThingId.LunerasMemoryFragment))
            {
                Console.Clear();
                string dialogueMemoryFragment = File.ReadAllText("Textfiles/Lunera/LuneraFragmentDialogue.txt");
                // Dialogue number 2
                Print(dialogueMemoryFragment);
                return;
            }
            if (talkedToLuneraCount > 0)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Lunera/LuneraSecondDialogue.txt");
                // Dialogue number 2
                Print(dialogue2);
            }
            else
            {
                Console.Clear();
                string dialogue1 = File.ReadAllText("Textfiles/Lunera/LuneraFirstDialogue.txt");
                // Dialogue number 1
                Print(dialogue1);
                MoveThing(ThingId.FireStarter, LocationId.Inventory);
                talkedToLuneraCount++;
            }
        }
        static void TalkToPapa()
        {
            if (NumberOfFragmentsFound == 6)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Papa/PapaLastDialogue.txt");
                // Dialogue number 2
                Print(dialogue2);
                return;
            }
            if (talkedToLuneraCount > 0)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Papa/PapaDialogueAfterLunera.txt");
                // Dialogue number 2
                Print(dialogue2);
                return;
            }
            else
            {
                Console.Clear();
                string dialogue1 = File.ReadAllText("Textfiles/Papa/PapaFirstDialogue.txt");
                // Dialogue number 1
                Print(dialogue1);
                return;
            }
        }
        static void TalkToWilbert()
        {
            if (toolsInWell == true)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Wilbert/WilbertWellDialogue.txt");
                // Dialogue number 2
                Print(dialogue2);
                talkedToWilbertCount++;
                GetThing(ThingId.WilbertsMemoryFragment);
                NumberOfFragmentsFound++;
                return;
            }

            if (toolsBurned == true)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Wilbert/WilbertFireDialogue.txt");
                // Dialogue Burned toolshack
                Print(dialogue2);
                talkedToWilbertCount++;
                GetThing(ThingId.WilbertsMemoryFragment);
                NumberOfFragmentsFound++;
                return;
            }

            if (toolsBurned == true && talkedToWilbertCount > 1)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Wilbert/WilbertNoMoreDialogue.txt");
                Print(dialogue2);
            }
            if (toolsDestroyed == true && talkedToWilbertCount > 1)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Wilbert/WilbertNoMoreDialogue.txt");
                Print(dialogue2);
            }

            if (toolsDestroyed == true)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Wilbert/WilbertDynamiteDialogue.txt");
                // Dialogue Burned toolshack
                Print(dialogue2);
                talkedToWilbertCount++;
                return;
            }

            if (talkedToWilbertCount > 0)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Wilbert/WilbertSecondDialogue.txt");
                // Dialogue number 2
                Print(dialogue2);
                return;
            }
            else
            {
                Console.Clear();
                string dialogue1 = File.ReadAllText("Textfiles/Wilbert/WilbertFirstDialogue.txt");
                // Dialogue number 1
                Print(dialogue1);
                talkedToWilbertCount++;
                return;
            }
        }
        static void TalkToEvalina()
        {
            if (HaveThing(ThingId.Doll))
            {
                Console.Clear();
                string dialogueDoll = File.ReadAllText("Textfiles/Evalina/EvalinaDollDialogue.txt");
                // Dialogue number 2
                Print(dialogueDoll);
                MoveThing(ThingId.Doll, LocationId.Nowhere);
                GetThing(ThingId.EvalinasMemoryFragment);
                NumberOfFragmentsFound++;
                EvalinaDoll = true;
                return;
            }

            if (HaveThing(ThingId.Dress))
            {
                Console.Clear();
                string dialogueDress = File.ReadAllText("Textfiles/Evalina/EvalinaDressDialogue.txt");
                // Dialogue number 2
                Print(dialogueDress);
                MoveThing(ThingId.Dress, LocationId.Nowhere);
                GetThing(ThingId.EvalinasMemoryFragment);
                NumberOfFragmentsFound++;
                EvalinaDress = true;
                return;
            }
            if (talkedToEvalinaCount > 0)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Evalina/EvalinaSecondDialogue.txt");
                // Dialogue number 2
                Print(dialogue2);
                return;
            }
            else
            {
                Console.Clear();
                string dialogue1 = File.ReadAllText("Textfiles/Evalina/EvalinaFirstDialogue.txt");
                // Dialogue number 1
                Print(dialogue1);
                talkedToEvalinaCount++;
                return;
            }
        }
        static void TalkToGillhardt()
        {
            if (HaveThing(ThingId.Joint))
            {
                Console.Clear();
                string dialogueJoint = File.ReadAllText("Textfiles/Gillhardt/GillhardtJointDialogue.txt");
                // Dialogue number 2
                Print(dialogueJoint);
                MoveThing(ThingId.Joint, LocationId.Nowhere);
                GetThing(ThingId.GillhardtsMemoryFragment);
                NumberOfFragmentsFound++;
                GillhardtJoint = true;
                return;
            }

            if (HaveThing(ThingId.Potion))
            {
                Console.Clear();
                string dialoguePotion = File.ReadAllText("Textfiles/Gillhardt/GillhardtPotionDialogue.txt");
                // Dialogue number 2
                Print(dialoguePotion);
                MoveThing(ThingId.Potion, LocationId.Nowhere);
                GetThing(ThingId.GillhardtsMemoryFragment);
                NumberOfFragmentsFound++;
                GillhardtPotion = true;
                return;
            }

            if (talkedToGillhardtCount > 0)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Gillhardt/GillhardtSecondDialogue.txt");
                // Dialogue number 2
                Print(dialogue2);
                return;
            }
            else
            {
                Console.Clear();
                string dialogue1 = File.ReadAllText("Textfiles/Gillhardt/GillhardtFirstDialogue.txt");
                // Dialogue number 1
                Print(dialogue1);
                talkedToGillhardtCount++;
                return;
            }
        }
        static void TalkToGustof()
        {
            if (HaveThing(ThingId.Bread))
            {
                Console.Clear();
                string dialogueBread = File.ReadAllText("Textfiles/Gustof/GustofBreadDialogue.txt");
                // Dialogue number 2
                Print(dialogueBread);
                MoveThing(ThingId.Bread, LocationId.Nowhere);
                GetThing(ThingId.GustofsMemoryFragment);
                NumberOfFragmentsFound++;
                GustofBread = true;
                return;
            }

            if (HaveThing(ThingId.Meat))
            {
                Console.Clear();
                string dialogueMeat = File.ReadAllText("Textfiles/Gustof/GustofMeatDialogue.txt");
                // Dialogue number 2
                Print(dialogueMeat);
                MoveThing(ThingId.CookedMeat, LocationId.Nowhere);
                GetThing(ThingId.GustofsMemoryFragment);
                NumberOfFragmentsFound++;
                GustofMeat = true;
                return;
            }

            if (talkedToGustofCount > 0)
            {
                Console.Clear();
                string dialogue2 = File.ReadAllText("Textfiles/Gustof/GustofSecondDialogue.txt");
                // Dialogue number 2
                Print(dialogue2);
                return;
            }
            else
            {
                Console.Clear();
                string dialogue1 = File.ReadAllText("Textfiles/Gustof/GustofFirstDialogue.txt");
                // Dialogue number 1
                Print(dialogue1);
                talkedToGustofCount++;
                return;
            }
        }
        static void TalkToSloan()
        {
            if (HaveThing(ThingId.Bell))
            {
                Console.Clear();
                string dialogueBell = File.ReadAllText("Textfiles/Sloan/SloanBellDialogue.txt");
                // Dialogue number 2
                Print(dialogueBell);
                MoveThing(ThingId.Bell, LocationId.Nowhere);
                GetThing(ThingId.SloansMemoryFragment);
                NumberOfFragmentsFound++;
                SloanBell = true;
                return;
            }

            if (toolsDestroyed == true)
            {
                Console.Clear();
                string dialogueBoom = File.ReadAllText("Textfiles/Sloan/SloanExplosionDialogue.txt");
                // Dialogue number 2
                Print(dialogueBoom);
                GetThing(ThingId.SloansMemoryFragment);
                NumberOfFragmentsFound++;
                return;
            }
            else
            {
                Console.Clear();
                string dialogue1 = File.ReadAllText("Textfiles/Sloan/SloanFirstDialogue.txt");
                // Dialogue number 1
                Print(dialogue1);
                return;
            }
        }
        static void UseKey(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("What do you want to unlock?");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Chest:
                    Console.Clear();
                    Print($"You unlocked the chest and found a {ThingId.Tiara}. When you lift up the tiara, you hear a clicking sound and the entire room starts to rumble, after a breif moment it stops.");
                    GetThing(ThingId.Tiara);
                    LocationsData[LocationId.LeftTunnel].Directions[Direction.Back] = LocationId.MinesNoRight;
                    break;

                case ThingId.Lunera:
                    Console.Clear();
                    Print("You can't use it on that kind of chest");
                    break;

                default:
                    Console.Clear();
                    Print("You can't unlock that");
                    break;
            }
        }
        static void UseFireStarter(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("What do you wanna use it on?");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Dynamite:
                    Console.Clear();
                    Print("You ignited the fuse.");
                    GetThing(ThingId.LitDynamite);
                    MoveThing(ThingId.Dynamite, LocationId.Nowhere);
                    break;

                case ThingId.Tools:
                    Console.Clear();
                    Print("You set the tools on fire and in the progress, the entire toolshack");
                    Console.ReadKey();
                    MoveThing(ThingId.Tools, LocationId.Nowhere);
                    toolsBurned = true;
                    // Gets out the directions from adjacent locations and change them so they go to the new LocationId
                    LocationData well = LocationsData[LocationId.Well];
                    well.Directions[Direction.SouthEast] = LocationId.BurnedDownToolShack;
                    // Gets out the directions from adjacent locations and change them so they go to the new LocationId (Same thing, just coded differently)
                    LocationsData[LocationId.HouseEntrance].Directions[Direction.East] = LocationId.BurnedDownToolShack;
                    CurrentLocationId = LocationId.BurnedDownToolShack;
                    DisplayLocation();
                    break;


                default:
                    Console.Clear();
                    Print("I don't think that's a good idea");
                    break;
            }
        }
        static void UseLitDynamite(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("What do you wanna use it on?");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }
            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Chest:
                    Console.Clear();
                    Print("You run away fast and after a couple of seconds the chest blows away, leaving only a tiara standing. (Yeah it doesn't make sense, it's a game...duh)");
                    GetThing(ThingId.Tiara);
                    MoveThing(ThingId.LitDynamite, LocationId.Nowhere);
                    LocationsData[LocationId.LeftTunnel].Directions[Direction.Back] = LocationId.MinesNoRight;
                    break;

                case ThingId.GoldDeposit:
                    Console.Clear();
                    Print("You run away fast and after a couple of seconds the dynamite blows up, leaving only pieces of rocks and gold ore scattered.");
                    GetThing(ThingId.Tiara);
                    MoveThing(ThingId.LitDynamite, LocationId.Nowhere);
                    LocationsData[LocationId.RightTunnel].Directions[Direction.Back] = LocationId.MinesNoLeft;
                    break;

                case ThingId.Tools:
                    Console.Clear();
                    Print("You blew up the tools but also left a big hole where the shack once stood.");
                    Console.ReadKey();
                    toolsDestroyed = true;
                    MoveThing(ThingId.LitDynamite, LocationId.Nowhere);
                    // Gets out the directions from adjacent locations and change them so they go to the new LocationId
                    LocationData well = LocationsData[LocationId.Well];
                    well.Directions[Direction.SouthEast] = LocationId.BlownUpToolShack;
                    // Gets out the directions from adjacent locations and change them so they go to the new LocationId (Same thing, just coded differently)
                    LocationsData[LocationId.HouseEntrance].Directions[Direction.East] = LocationId.BlownUpToolShack;
                    CurrentLocationId = LocationId.BlownUpToolShack;
                    DisplayLocation();
                    break;

                default:
                    Console.Clear();
                    Print("I don't think that's a good idea");
                    break;
            }
        }
        static void UsePickaxe(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("What do you want to mine?");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.GoldDeposit:
                    Console.Clear();
                    Print($"You manage to mine some gold, but you break the pickaxe. You also feel a rumble in the tunnel, but stops after a brief moment.");
                    GetThing(ThingId.GoldOre);
                    MoveThing(ThingId.Pickaxe, LocationId.Nowhere);
                    LocationsData[LocationId.RightTunnel].Directions[Direction.Back] = LocationId.MinesNoLeft;
                    break;

                default:
                    Console.Clear();
                    Print("You can't use it on that");
                    break;
            }
        }
        static void UseCloth(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("You can't use cloth on that thing");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Weeds:
                    Console.Clear();
                    Print("You wrap the cloth around the weeds to create a small bundle.");
                    GetThing(ThingId.Bundle);
                    MoveThing(ThingId.Cloth, LocationId.Nowhere);
                    MoveThing(ThingId.Weeds, LocationId.Nowhere);
                    break;

                case ThingId.Wheat:
                    Console.Clear();
                    Print("You wrap the cloth around the wheat to create a small bundle.");
                    GetThing(ThingId.Bundle);
                    MoveThing(ThingId.Cloth, LocationId.Nowhere);
                    MoveThing(ThingId.Wheat, LocationId.Nowhere);
                    break;

                default:
                    Console.Clear();
                    Print("You can't use it on that");
                    break;
            }
        }
        static void UseShirt(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("You can't use shirt on that thing");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Weeds:
                    Console.Clear();
                    Print("You wrap the shirt around the weeds to create a small bundle.");
                    GetThing(ThingId.Bundle);
                    MoveThing(ThingId.Shirt, LocationId.Nowhere);
                    MoveThing(ThingId.Weeds, LocationId.Nowhere);
                    break;

                case ThingId.Wheat:
                    Console.Clear();
                    Print("You wrap the shirt around the wheat to create a small bundle.");
                    GetThing(ThingId.Bundle);
                    MoveThing(ThingId.Shirt, LocationId.Nowhere);
                    MoveThing(ThingId.Wheat, LocationId.Nowhere);
                    break;

                default:
                    Console.Clear();
                    Print("You can't use it on that");
                    break;
            }
        }
        static void UseThread(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("Try again");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Bundle:
                    Console.Clear();
                    Print("You just made a very ugly doll.");
                    GetThing(ThingId.Doll);
                    MoveThing(ThingId.Thread, LocationId.Nowhere);
                    MoveThing(ThingId.Bundle, LocationId.Nowhere);
                    break;

                case ThingId.Cloth:
                    Console.Clear();
                    Print("You just made a very decent looking dress.");
                    GetThing(ThingId.Dress);
                    MoveThing(ThingId.Thread, LocationId.Nowhere);
                    MoveThing(ThingId.Cloth, LocationId.Nowhere);
                    break;

                default:
                    Console.Clear();
                    Print("You can't use it on that");
                    break;
            }
        }
        static void UseWater(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("Try again");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Wheat:
                    Console.Clear();
                    Print("You made a ball of dough");
                    GetThing(ThingId.Potion);
                    MoveThing(ThingId.Water, LocationId.Nowhere);
                    MoveThing(ThingId.Wheat, LocationId.Nowhere);
                    break;

                case ThingId.Herbs:
                    Console.Clear();
                    Print("You made a sleeping potion");
                    GetThing(ThingId.Dough);
                    MoveThing(ThingId.Water, LocationId.Nowhere);
                    MoveThing(ThingId.Herbs, LocationId.Nowhere);
                    break;

                default:
                    Console.Clear();
                    Print("You can't use it on that");
                    break;
            }
        }
        static void UseDough(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("Try again");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Campfire:
                    Console.Clear();
                    Print("You baked some bread, it is only burned a little bit. Kudos!");
                    GetThing(ThingId.Bread);
                    MoveThing(ThingId.Dough, LocationId.Nowhere);
                    break;

                default:
                    Console.Clear();
                    Print("You can't use it on that");
                    break;
            }
        }
        static void UseMeat(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("Try again");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Campfire:
                    Console.Clear();
                    Print("You cooked some meat, now time to beat it");
                    GetThing(ThingId.CookedMeat);
                    MoveThing(ThingId.Meat, LocationId.Nowhere);
                    break;

                default:
                    Console.Clear();
                    Print("You can't use it on that");
                    break;
            }
        }
        static void UseTools(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("Try again");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Well:
                    Console.Clear();
                    Print("You threw the tools down the well. They are now sleeping with the fishes.");
                    MoveThing(ThingId.Tools, LocationId.Nowhere);
                    break;

                default:
                    Console.Clear();
                    Print("You can't use it on that");
                    break;
            }
        }
        static void UseKnife(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("Try again");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Rope:
                    Console.Clear();
                    Print("You cut the rope that held up the bell and you put the bell in your pocket");
                    MoveThing(ThingId.Rope, LocationId.Nowhere);
                    GetThing(ThingId.Bell);
                    break;

                default:
                    Console.Clear();
                    Print("You can't use it on that");
                    break;
            }
        }
        static void UseBook(List<ThingId> thingIds)
        {
            // Check that command includes a second thing
            if (thingIds.Count < 2)
            {
                Print("Try again");
                return;
            }
            // Second thing to be use on
            ThingId thingToBeUsedOn = thingIds[1];
            // Get name of thing
            string thingName = GetThingName(thingToBeUsedOn);

            // Make sure the this is present.
            if (!ThingAvailable(thingToBeUsedOn))
            {
                Print($"{thingName} is not here.");
                return;
            }

            // Everything seems to be OK, proceed to the talk event with the specific NPC.
            switch (thingToBeUsedOn)
            {
                case ThingId.Herbs:
                    Console.Clear();
                    Print("You ripped out a piece of paper from the book and rolled up some herbs");
                    MoveThing(ThingId.Herbs, LocationId.Nowhere);
                    GetThing(ThingId.Joint);
                    break;

                default:
                    Console.Clear();
                    Print("You can't use it on that");
                    break;
            }
        }
    }
    #endregion
}
