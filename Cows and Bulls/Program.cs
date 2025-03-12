using System;
using System.Linq;

namespace Cows_and_Bulls
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int topscore = int.MaxValue;  // Ensures the first score is always beaten
            Random rand = new Random();
            bool playagain = true;

            while (playagain)
            {
                Console.WriteLine("Enter the number of digits you want to play with (3-8):");
                int digitCount;
                while (!int.TryParse(Console.ReadLine(), out digitCount) || digitCount < 3 || digitCount > 8)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 3 and 8.");
                }

                char[] Guess = new char[digitCount];
                char[] Digits = new char[digitCount];
                bool Unique = false;
                int Bulls = 0;
                int Cows = 0;
                int tries = 0;
                bool play = true;
                bool isvalid = false;

                // Generate a unique random number with the chosen digit count
                while (!Unique)
                {
                    int min = (int)Math.Pow(10, digitCount - 1); // Smallest number with 'digitCount' digits
                    int max = (int)Math.Pow(10, digitCount) - 1; // Largest number with 'digitCount' digits

                    int numbers = rand.Next(min, max + 1);
                    Digits = numbers.ToString().ToCharArray();
                    Console.WriteLine(Digits); // Display for testing/debugging
                    Unique = Digits.Distinct().Count() == digitCount;
                }
                while (!isvalid)
                {
                    Console.WriteLine("Enter your guess:");
                    Guess = Console.ReadLine().ToCharArray();
                    if (Guess.Distinct().Count() == digitCount && Guess.Length == digitCount)
                    {
                        isvalid = true;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid guess! Make sure it's {digitCount} unique digits.");
                    }
                }

                // Calculate Bulls
                Bulls = 0;
                for (int i = 0; i < digitCount; i++)
                {
                    if (Guess[i] == Digits[i])
                    {
                        Bulls++;
                    }
                }

                // Calculate Cows
                Cows = 0;
                foreach (char c in Guess)
                {
                    if (Digits.Contains(c))
                    {
                        Cows++;
                    }
                }
                Cows -= Bulls;

                tries++;

                if (Bulls == digitCount)
                {
                    Console.WriteLine($"Well done! You guessed correctly in {tries} tries.");
                    if (tries < topscore)
                    {
                        topscore = tries;
                        Console.WriteLine($"New High Score: {topscore} tries!");
                    }

                    Console.WriteLine("Would you like to play again? (y/n)");
                    char Uplay = char.ToLower(Console.ReadLine()[0]);
                    if (Uplay == 'y')
                    {
                        Bulls = 0;
                        Cows = 0;
                        tries = 0;
                        Unique = false;  // Forces new number generation
                        play = true;
                    }
                    else
                    {
                        play = false;
                        playagain = false;
                    }
                }
                else
                {
                    Console.WriteLine($"You have {Bulls} bulls and {Cows} cows.");
                }
            }
        }
    }
}

