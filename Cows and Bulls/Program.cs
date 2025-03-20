using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
            //the colour menu
            Console.WriteLine("Do you want to chnage the colour");
            string colourYN = Console.ReadLine();
            if (colourYN  == "yes" || colourYN == "y")
            {
                Console.WriteLine("Would you like to view the list of valid colours?");
                string vcol = Console.ReadLine(); 
                if ( vcol == "yes" || vcol == "y")
                {
                    string[] colors = {
    "Black", "DarkBlue", "DarkGreen", "DarkCyan",
    "DarkRed", "DarkMagenta", "DarkYellow", "Gray",
    "DarkGray", "Blue", "Green", "Cyan",
    "Red", "Magenta", "Yellow", "White"
};

                }
                Console.WriteLine("What colour?");
                string bcolor = Console.ReadLine();
                for (int i = 0; i <  6; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(1000);
                }
                if (Enum.TryParse(bcolor, true, out ConsoleColor chosenColor))
                {
                    Console.ForegroundColor = chosenColor; // Change text colour
                    Console.WriteLine($"The console text is now {chosenColor}!");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Invalid colour. Please enter a valid colour name.");
                }
            }
            // the menu
            Console.WriteLine("Select your mode: \r\n (1)Play Standard 4 Gamemode \r\n (2)Change Number of digits \r\n (3)Show top score \r\n (4)Quit");
            int UcaseMenu = int.Parse(Console.ReadLine());
            playagain = true;
            while (playagain)
            {
               


                if (UcaseMenu == 2 && tries == 0)
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
                    Console.Clear();
                    Console.WriteLine($@"

                                        ████████████████████████████████████████
                                        █                                      █
                                        █      THE TOP SCORE IS {topscore}     █
                                        █                                      █
                                        ████████████████████████████████████████
                                        ");
                    playagain = false;
                }else if (UcaseMenu == 4)
                {
                    Environment.Exit(0);
                }

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
                    char[] Guess = new char[digitCount];
                    bool UniqueGuess = false;
                    int Bulls = 0;
                    int Cows = 0;
                    if (tries == 0)
                    {
                        tries = 0;
                    }
                    bool play = true;
                    bool isvalid = false;

                    // Unique number with chosen count
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

                    // Gets Bulls
                    Bulls = 0;
                    for (int i = 0; i < digitCount; i++)
                    {
                        if (Guess[i] == Digits[i])
                        {
                            Bulls++;
                        }
                    }

                    // Gets Cows
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
                        if (tries < topscore || topscore == 0)
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
                            Unique = false;  // Forces new rng
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
                        tries++;
                        Unique = true;
                    }

                }
            }

        }
    }
}        
    

