using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\nEnter a list of numbers, type 0 when done.");
        Console.WriteLine();

        // CREATING LIST
        List<int> numbers = new List<int>();
        int number = -1;

        while (true)
        {
            Console.Write("Type a number: ");
            string input = Console.ReadLine();

            bool isValid = int.TryParse(input, out number);

            if (!isValid)
            {
                Console.WriteLine("Please enter a valid number ❌");
                continue;
            }

            if (number == 0)
            {
                break;
            }

            numbers.Add(number);

        }

        // SUM
        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }

        Console.WriteLine($"\nThe sum is: {sum}");

        // AVERAGE
        if (numbers.Count > 0)
        {
            double average = (double)sum / numbers.Count;
            Console.WriteLine($"The average is: {average}");
        }

        // LARGEST NUMBER IN LIST
        int max = numbers[0];
        foreach (int num in numbers)
        {
            if (num > max)
            {
                max = num;
            }
        }
        Console.WriteLine($"The largest number is: {max}");
        Console.WriteLine();
    }
}