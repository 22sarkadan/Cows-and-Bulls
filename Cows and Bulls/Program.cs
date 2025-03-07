namespace Cows_and_Bulls
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Generates a 4 digit unique number as a char array cos it makes things easier
            bool Unique = false;
            Random rand = new Random();
            char[] Digits = new char[4];
            while (!Unique)
            {
                int numbers = rand.Next(1000, 10000);
                Digits = numbers.ToString().ToCharArray();
                Unique = Digits.Distinct().Count() == 4;
            }
            //Gets guess, ensures it's correct
            bool isvalid = false;
            while (isvalid = false)
            {
                Console.WriteLine("Enter you're guess");
                char[] Guess = Console.ReadLine().ToCharArray();
                if (Unique = Guess.Distinct().Count() == 4 && Guess.Length == 4)
                {
                    isvalid = true;
                }
                break;
            }


        }
    }
}
