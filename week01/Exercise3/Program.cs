using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("");

        // USER SETS MAGIC NUMBER
        Console.Write("Set the magic number: ");
        string set_number = Console.ReadLine();
        // USER GUESSES THE NUMBER
        Console.Write("What is your guess? ");
        string guessing = Console.ReadLine();

        // CONVERTING INPUT INTO NUMBERS
        int magic_number = int.Parse(set_number);
        int guess = int.Parse(guessing);

        // LOOP
        while (guess != magic_number)
        {
            if (guess > magic_number)
            {
                Console.WriteLine("Lower");
                Console.Write("What is your guess? ");
            }

            else if (guess < magic_number)
            {
                Console.WriteLine("Higher");
                Console.Write("What is your guess? ");
            }

            guess = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("YOU GUESSED IT! 😁");
    }
}