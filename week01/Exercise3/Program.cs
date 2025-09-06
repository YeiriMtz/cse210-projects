using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("");

        // CODE IF USER SETS MAGIC NUMBER 👇🏼
        // Console.Write("Set the magic number: ");
        // string set_number = Console.ReadLine();

        // RANDOM MAGIC NUMBER 👇🏼
        Random randomGenerator = new Random();
        int magic_number = randomGenerator.Next(1, 101);

        Console.WriteLine("Guess the random magic number 😜");
        Console.WriteLine("");

        // USER GUESSES THE NUMBER
        Console.Write("What is your guess? ");
        string guessing = Console.ReadLine();

        // CONVERTING INPUT INTO NUMBERS
        int guess = int.Parse(guessing);
        // ONLY IF USER SETS MAGIC NUMBER 👉🏼 int magic_number = int.Parse(set_number); 👈🏼

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
        
        Console.WriteLine("");
        Console.WriteLine("YOU GUESSED IT! 😁");
        Console.WriteLine("");
    }
}