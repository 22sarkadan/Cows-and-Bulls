using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using static System.Formats.Asn1.AsnWriter;

namespace Cows_and_Bulls
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int topscore = 0;  // Ensures the first score is always beaten
            Random rand = new Random();
            bool playagain = true;
            int digitCount = 0;
            int tries = 0;
            bool Unique = false;
            char[] Digits = new char[digitCount]; // Added declaration for Digits
                                                  // Color menu
            Console.WriteLine("Do you want to change the colour?");
            string colourYN = Console.ReadLine();
            if (colourYN == "yes" || colourYN == "y")
            {
                Console.WriteLine("Would you like to view the list of valid colours?");
                string vcol = Console.ReadLine();
                if (vcol == "yes" || vcol == "y")
                {
                    string[] colors = {
                        "Black", "DarkBlue", "DarkGreen", "DarkCyan",
                        "DarkRed", "DarkMagenta", "DarkYellow", "Gray",
                        "DarkGray", "Blue", "Green", "Cyan",
                        "Red", "Magenta", "Yellow", "White" };
                    for (int i = 0; i < colors.Length; i++)
                    {
                        Console.WriteLine($"{colors[i]}, ");
                    }
                }
                Console.WriteLine("What colour?");
                string bcolor = Console.ReadLine();
                /*for (int i = 0; i < 6; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(1000);
                }*/
                if (Enum.TryParse(bcolor, true, out ConsoleColor chosenColor))
                {
                    Console.ForegroundColor = chosenColor; // Changes text colour
                    Console.WriteLine($"The console text is now {chosenColor}!");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Invalid colour. Please enter a valid colour name.");
                }
            }
            while (playagain) // Main game is in here
            {


                // Displays the menu
                Console.WriteLine("Select your mode: \r\n (1)Play Standard 4 Gamemode \r\n (2)Change Number of digits \r\n (3)Show top score \r\n (4)Quit");
                int UcaseMenu = int.Parse(Console.ReadLine());

                if (UcaseMenu == 2)
                {
                    Console.WriteLine("Enter the number of digits you want to play with (3-8):");
                    digitCount = int.Parse(Console.ReadLine());
                    while (digitCount < 3 || digitCount > 8)
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 3 and 8.");
                        digitCount = int.Parse(Console.ReadLine());
                    }
                    Digits = new char[digitCount];
                }
                else if (UcaseMenu == 1)
                {
                    digitCount = 4;
                    Digits = new char[digitCount];
                }
                else if (UcaseMenu == 3)
                {
                    //setup for padded score
                    int OEPad = 0;
                    if (topscore % 2 != 0)
                    {
                        OEPad = 2;

                    }
                    string topscoreL = $"THE TOP SCORE IS {topscore}";
                    int Swidth = 40;
                    int paddingleft = (Swidth - topscoreL.Length) / 2;
                    int paddingright = Swidth - topscoreL.Length - paddingleft - OEPad;
                    //padded score makes sure the outputed score is centered

                    string paddedscore = new string(' ', paddingleft) + topscoreL + new string(' ', paddingright);
                    Console.Clear();
                    Console.WriteLine($@"
                            ████████████████████████████████████████
                            █                                      █
                            █{paddedscore}█
                            █                                      █
                            ████████████████████████████████████████
                            ");
                    // Return to menu after showing top score
                    continue;
                }
                else if (UcaseMenu == 4)
                {
                    Environment.Exit(0);
                }

                // Reset game state for new game
                Unique = false;
                tries = 0;

                // Generate a unique number
                while (!Unique)
                {
                    int min = (int)Math.Pow(10, digitCount - 1);
                    int max = (int)Math.Pow(10, digitCount) - 1;

                    int numbers = rand.Next(min, max + 1);
                    Digits = numbers.ToString().ToCharArray();
                     Console.WriteLine(Digits); //js for testing
                    Unique = Digits.Distinct().Count() == digitCount;
                }

                if (UcaseMenu == 1 || UcaseMenu == 2)
                {
                    bool gameOver = false;

                    // Game loop for a 1 game
                    while (!gameOver)
                    {
                        char[] Guess = new char[digitCount];
                        bool isvalid = false;

                        // Ensures a valid guess
                        while (!isvalid)
                        {
                            Console.WriteLine("Enter your guess:");
                            string input = Console.ReadLine();
                            if (input.Length == digitCount)
                            {
                                Guess = input.ToCharArray();
                                if (Guess.Distinct().Count() == digitCount)
                                {
                                    isvalid = true;
                                }
                                else
                                {
                                    Console.WriteLine($"Invalid guess! Make sure it's {digitCount} unique digits.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Invalid guess! Make sure it's {digitCount} digits.");
                            }
                        }

                        // Gets Bulls
                        int Bulls = 0;
                        for (int i = 0; i < digitCount; i++)
                        {
                            if (Guess[i] == Digits[i])
                            {
                                Bulls++;
                            }
                        }

                        // Gets Cows
                        int Cows = 0;
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
                            if (tries < topscore || topscore == 0)
                            {
                                topscore = tries;
                                Console.WriteLine($"New High Score: {topscore} tries!");
                            }

                            Console.WriteLine("Would you like to play again? (y/n)");
                            char Uplay = char.ToLower(Console.ReadLine()[0]);
                            if (Uplay == 'y')
                            {
                                gameOver = true; // Ends game to return to while
                            }
                            else
                            {
                                gameOver = true;
                                playagain = false; // Exit the main game while
                            }
                        }
                        else
                        {
                            Console.WriteLine($"You have {Bulls} bulls and {Cows} cows.");
                        }
                    }
                }

                Console.Clear(); // Clears console before replaying
            }

        }
    }
}        
    

