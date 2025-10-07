using System;

namespace Homework.week05
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test base class
            Console.WriteLine("---------------------------------------");
            Assignment assignment = new Assignment("\nYeiri Martinez", "Multiplication");
            Console.WriteLine(assignment.GetSummary());
            Console.WriteLine();

            // Test MathAssignment
            MathAssignment math = new MathAssignment("Lara Da Silva Soares", "Fractions", "7.3", "8-19");
            Console.WriteLine(math.GetSummary());
            Console.WriteLine(math.GetHomeworkList());
            Console.WriteLine();

            // Test WritingAssignment
            WritingAssignment writing = new WritingAssignment("Oliver Martinez", "European History", "The Causes of World War II");
            Console.WriteLine(writing.GetSummary());
            Console.WriteLine(writing.GetWritingInformation());
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------");
        }
    }
}
