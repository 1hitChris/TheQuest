using System;
using System.Collections.Generic;
using System.Threading;

namespace A_better_Join
{
    class Program
    {
        static void Main(string[] args)
        {

            var heroes = new List<string> { "Arthas", "Jaina", "Uther", "Anduin" };
            string result = JoinWithAnd(heroes, true);
            string result2 = JoinWithAnd(heroes, false);
            Console.WriteLine(result2);
            Console.WriteLine(result);
           
        }

        static string JoinWithAnd(List<string> items, bool useSerialComma = true)
        {              
            
            int count = items.Count;

            if (count == 0)
            {
                return "";
            }
            else if (count == 1)
            {
                return items[0];
            }
            else if (count == 2)
            {
                return items[0] + " and " + items[1];    
            }

            else
            {
                var itemsCopy = new List<string>(items);
                if (useSerialComma == true)
                {
                    string lastitem = itemsCopy[itemsCopy.Count - 1];
                    lastitem = "and " + lastitem;
                    itemsCopy[itemsCopy.Count - 1] = lastitem;

                    // itemsCopy[itemsCopy.Count - 1] = " and " + itemsCopy[itemsCopy.Count - 1];
                }
                else
                {
                    string last2items = items[items.Count -2] + " and " + items[items.Count - 1];
                    itemsCopy[items.Count -2] = last2items;
                    itemsCopy.RemoveAt(itemsCopy.Count -1);
                                  
                
                }
                
                return String.Join(", ", itemsCopy);
                
            }
            //return "";
        }


    }
}
