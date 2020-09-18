using System;
using System.Collections.Generic;

namespace Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            var names = new List<string> { "Arthas", "Jaina", "Uther", "Anduin" };
            
            {
                Console.WriteLine($"A party of warriors ({names[0]}, {names[1]}, {names[2]} and {names[3]}) descends into the dungeon.");
            }
            int randomNumber;
            int bosshp = 16;
            Random rndm = new Random();
            randomNumber = rndm.Next(1, 7);

            for (int i = 0; i < 8; i++)
            {

                int d8 = rndm.Next(1, 9);
                bosshp = bosshp + d8;
               
            }
            
            Console.WriteLine($"A basilisk with {bosshp} HP appears!");


            int damage;
            Random dmg = new Random();
            damage = dmg.Next(1, 7);
            int sworddmg = damage + damage;

            while (bosshp > 0)
            {
                Console.Write($"{names[0]} deals {sworddmg} dmg.");
                Console.WriteLine($" Basilisk has {bosshp - sworddmg} HP left.");
            }
            



        }
    }
}
