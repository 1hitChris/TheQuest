﻿using System;
using System.Collections.Generic;

namespace Parsing__the_try_catch_way
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string diceNotation = Console.ReadLine();
                var listOfRolls = new List<int> { };
                int numberOfThrows = 10;

                // Making a list of all the throws
                for (int throws = 0; throws < numberOfThrows; throws++)
                {
                    listOfRolls.Add(DiceRoll(diceNotation));
                }
                // Displaying the throws
                Console.WriteLine($"Throwing {diceNotation} ... {string.Join(" ", listOfRolls)}");
            }
        }
        // Method for the actuall roll
        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            var random = new Random();
            int diceSide;
            int sum = 0;

            // Rolling the dice
            for (var i = 0; i < numberOfRolls; i++)
            {
                diceSide = random.Next(1, diceSides + 1);
                sum += diceSide;
            }

            return sum + fixedBonus;
        }

        // Method for finding values from a string with standard dice notation
        static int DiceRoll(string diceNotation)
        {
            //Check diceNotation to see if it contains a 'd'

            //Check if other characters are present, like '-', '+' etc.

            // Making notation into strings with only one char
            string numberOfRollsString = diceNotation[0].ToString();
            string diceSidesString = diceNotation[2].ToString();
            string fixedBonusString;

            // Making the string into ints
            int numberOfRolls = Int32.Parse(numberOfRollsString);
            int diceSides = Int32.Parse(diceSidesString);
            int fixedBonus = 0;

            // Ckecking if the notation has an modifier
            if (diceNotation.Length > 3)
            {
                fixedBonusString = diceNotation[4].ToString();
                fixedBonus = Int32.Parse(fixedBonusString);
            }

            return DiceRoll(numberOfRolls, diceSides, fixedBonus);
        }
    }
}
