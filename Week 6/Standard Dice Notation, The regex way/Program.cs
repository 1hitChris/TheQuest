using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Standard_Dice_Notation__The_regex_way
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
                
                

                if (IsStandardDiceNotation(diceNotation))
                {
                    for (int throws = 0; throws < numberOfThrows; throws++)
                    {
                        listOfRolls.Add(DiceRoll(diceNotation));
                    }
                    Console.WriteLine($"Throwing {diceNotation} ... {string.Join(" ", listOfRolls)}");
                }
                else
                {
                    Console.WriteLine($"Can't throw {diceNotation}, it is not in standard dice notation.");
                }
            }
        }

        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            var random = new Random();
            int diceSide;
            int sum = 0;
            for (var i = 0; i < numberOfRolls; i++)
            {
                diceSide = random.Next(1, diceSides + 1);
                sum += diceSide;
            }

            return sum + fixedBonus;
        }

        static int DiceRoll(string diceNotation)
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

        static bool IsStandardDiceNotation(string text)
        {
            bool diceNotation = false;

            string notationRegex = "^\\d*d\\d+[+-]?\\d*$";
            if (Regex.IsMatch(text, notationRegex))
            {
                diceNotation = true;
            }

            return diceNotation;
        }

    }
}
