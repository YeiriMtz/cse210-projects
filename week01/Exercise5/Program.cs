using System;

class Program
{
    // WELCOME
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program");
    }
    // USER'S NAME
    static string PromptUserName()
    {
        Console.Write("Enter your name: ");
        return Console.ReadLine();
    }
    // FAV NUMBER
    static int PromptUserNumber()
    {
        Console.Write("What is your favorite number? ");
        return int.Parse(Console.ReadLine());
    }
    // SQUARE NUM
    static int SquareNumber(int num)
    {
        return num * num;
    }
    // DISPLAYS
    static void DisplayResult(string name, int squared)
    {
        Console.WriteLine($"{name}, the square of your favorite number is: {squared}");
    }
    // MAIN
    static void Main(string[] args)
    {
        DisplayWelcome();

        string name = PromptUserName();
        int number = PromptUserNumber();
        int squareNumber = SquareNumber(number);

        DisplayResult(name, squareNumber);
    }
}