using System;
using System.Collections.Generic;
using System.IO;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Open the file to read from
            string readText = File.ReadAllText("Message.txt");

            // Split the text from the file into a list of words
            string[] message = readText.Split(' ', ',', '.');

            // A list to store phone numbers in
            var phoneNumbers = new List<string> { };

            // Check if every word is a phone number and if they are, add them to the phone number list
            foreach (string text in message)
            {
                if (IsPhoneNumber(text))
                {
                    phoneNumbers.Add(text);
                }
            }

            // Display the found phone numbers
            Console.Write($"The phone numbers present in the file are:\n{string.Join("\n", phoneNumbers)}");
            Console.WriteLine();
        }

        static bool IsPhoneNumber(string text)
        {
            bool isPhoneNumber = true;

            // Creating a list of valid symbols for a phone number
            var symbols = new List<int> { };
            // Adding numbers 0 - 9
            for (int i = 48; i < 58; i++)
            {
                symbols.Add(i);
            }
            // Adding - to valid chars
            symbols.Add(45);

            // If the word string is empty return false
            if (text == "")
            {
                return false;
            }

            // Is there only valid symbols int the word?
            for (int i = 0; i < text.Length; i++)
            {
                char checkChar = text[i];

                // If the symbols in the word is not in the valid symbols list then return false
                if (!symbols.Contains(checkChar))
                {
                    isPhoneNumber = false;
                    break;
                }
            }

            return isPhoneNumber;
        }
      
    }
}

/*
// If the word string is empty return false
if (letter.Contains(numbers))
{
    return true;
}
else
{
    return false;
}*/
