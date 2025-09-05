using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("");

        // USER INPUT
        Console.Write("What was your grade percentage? ");
        string user_grade = Console.ReadLine();

        // CONVERTING INPUT INTO INTEGER
        int grade = int.Parse(user_grade);

        // STEP #3, Changing code
        string letter;

        // A
        if (grade >= 90)
        {
            letter = "A";
        }
        // B
        else if (grade >= 80)
        {
            letter = "B";
        }
        // C
        else if (grade >= 70)
        {
            letter = "C";
        }
        // D
        else if (grade >= 60)
        {
            letter = "D";
        }
        // F
        else
        {
            letter = "F";
        }

        // PRINTING GRADE
        Console.WriteLine($"Your grade is {letter}");

        // PASSED OR NOT MESSAGE

        if (grade >= 70)
        {
            Console.WriteLine("Congratulations! YOU PASSED!");
        }

        else
        {
            Console.WriteLine("YOU FAILED, keep trying");
        }
        
        Console.WriteLine("");
    }
}