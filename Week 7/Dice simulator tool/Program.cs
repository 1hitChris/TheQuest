using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dice_simulator_tool
{
    class Program
    {
        static void Main(string[] args)
        {
            Roll();
        }
        //Roll main method
        static void Roll()
        {
            bool repeatRoll = false;
            bool newRoll = false;
            bool quit = false;
            do
            {
                newRoll = false;
                Console.WriteLine("DICE SIMULATOR");
                Console.WriteLine("\nEnter desired dice roll in standard dice notation: ");
                   
                string diceNotation = Console.ReadLine();
                // var listOfRolls = new List<int> { };
                // int numberOfThrows = 1;
                do
                {
                    repeatRoll = false;
                    if (IsStandardDiceNotation(diceNotation))
                    {
                        Console.WriteLine("\nSimulating...\n");

                        DiceRollNotation(diceNotation);

                        /* Console.ForegroundColor = ConsoleColor.White;
                         Console.WriteLine($"\nYou rolled {listOfRolls[0]}.");*/

                    }
                    else
                    {
                        Console.WriteLine($"\nYou did not use a standard dice notation. Try again: ");
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nDo you want to (r)epeat, enter a (n)ew roll or (q)uit?");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    //Switch case for what wanna do, R for repeat, N for new roll and Q for quit
                    var key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.R:
                            repeatRoll = true;
                            Console.Clear();
                            break;

                        case ConsoleKey.N:
                            newRoll = true;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;

                        case ConsoleKey.Q:
                            return;
                    }

                } while (repeatRoll == true);

            } while (newRoll == true);


        }
        //The dice roll method. Takes in how many rolls, how many sides a dice have as well as fixed bonus
        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            var random = new Random();
            int diceSide;
            int sum = 0;
            for (var i = 0; i < numberOfRolls; i++)
            {
                diceSide = random.Next(1, diceSides + 1);
                sum += diceSide;
                Console.WriteLine(DiceArt(diceSides, sum));
            }

            return sum + fixedBonus;
        }
        //Takes the dicenotation and use split on "d, + and -" and parse fixed bonus
        static int DiceRollNotation(string diceNotation)
        {
            string[] valuesOfDiceNotation = diceNotation.Split('d', '+', '-');

            string numberOfRollsString = valuesOfDiceNotation[0];
            string diceSidesString = valuesOfDiceNotation[1];

            if (numberOfRollsString == "")
            {
                numberOfRollsString = "1";
            }
            int numberOfRolls = Int32.Parse(numberOfRollsString);
            int diceSides = Int32.Parse(diceSidesString);

            string bonusString;
            int bonus = 0;
            if (valuesOfDiceNotation.Length > 2)
            {
                bonusString = valuesOfDiceNotation[2];
                bonus = Int32.Parse(bonusString);

                if (diceNotation.Contains('-'))
                {
                    bonus = 0 - bonus;
                }
            }

            return DiceRoll(numberOfRolls, diceSides, bonus);
        }
        //Checks the string/readline to see if it matches the dice notation and becomes true if it does
        static bool IsStandardDiceNotation(string text)
        {
            bool diceNotation = false;

            string notationRegex = @"^\d*d\d+[+-]?\d*$";
            if (Regex.IsMatch(text, notationRegex))
            {
                diceNotation = true;
            }

            return diceNotation;
        }

        //Checks how many sides the dice have and also the sum and picks out the right art
        static string DiceArt(int diceSides, int sum)
        {
            string errorMessage = "Try again";
            if (diceSides == 6)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                if (sum == 1)
                {
                    return
                    $"\n  ________ " +
                    $"\n /       /|" +
                    $"\n -------- |" +
                    $"\n|       | |" +
                    $"\n|   O   | |" +
                    $"\n|       | |" +
                    $"\n|_______|/ ";
                }
                else if (sum == 2)
                {
                    return
                   $"\n  ________ " +
                   $"\n /       /|" +
                   $"\n -------- |" +
                   $"\n| O     | |" +
                   $"\n|       | |" +
                   $"\n|     O | |" +
                   $"\n|_______|/ ";
                }
                else if (sum == 3)
                {
                    return
                   $"\n  ________ " +
                   $"\n /       /|" +
                   $"\n -------- |" +
                   $"\n| O     | |" +
                   $"\n|   O   | |" +
                   $"\n|     O | |" +
                   $"\n|_______|/ ";
                }
                else if (sum == 4)
                {
                    return
                   $"\n  ________ " +
                   $"\n /       /|" +
                   $"\n -------- |" +
                   $"\n| O   O | |" +
                   $"\n|       | |" +
                   $"\n| O   O | |" +
                   $"\n|_______|/ ";
                }
                else if (sum == 5)
                {
                    return
                   $"\n  ________ " +
                   $"\n /       /|" +
                   $"\n -------- |" +
                   $"\n| O   O | |" +
                   $"\n|   O   | |" +
                   $"\n| O   O | |" +
                   $"\n|_______|/ ";
                }
                else if (sum == 6)
                {
                    return
                   $"\n  ________ " +
                   $"\n /       /|" +
                   $"\n -------- |" +
                   $"\n| O   O | |" +
                   $"\n| O   O | |" +
                   $"\n| O   O | |" +
                   $"\n|_______|/ ";
                }
                return errorMessage;
            }
            

            if (diceSides == 4)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (sum == 1)
                {
                    return
                    $"\n    ." +
                    $"\n   . ." +
                    $"\n  . 1 ." +
                    $"\n .     ." +
                    $"\n.........";
                }
                else if (sum == 2)
                {
                    return
                   $"\n    ." +
                   $"\n   . ." +
                   $"\n  . 2 ." +
                   $"\n .     ." +
                   $"\n.........";
                }
                else if (sum == 3)
                {
                    return
                   $"\n    ." +
                   $"\n   . ." +
                   $"\n  . 3 ." +
                   $"\n .     ." +
                   $"\n.........";
                }
                else if (sum == 4)
                {
                    return
                   $"\n    ." +
                   $"\n   . ." +
                   $"\n  . 4 ." +
                   $"\n .     ." +
                   $"\n.........";
                }
                return errorMessage;
            }
            return errorMessage;
        }
    }
}
