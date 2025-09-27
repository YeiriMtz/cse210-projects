using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction f1 = new Fraction();
        Console.WriteLine(f1.GetFractionString());
        Console.WriteLine(f1.GetDecimalValue());

        Fraction f2 = new Fraction(5);
        Console.WriteLine(f2.GetFractionString());
        Console.WriteLine(f2.GetDecimalValue());

        Fraction f3 = new Fraction(3, 4);
        Console.WriteLine(f3.GetFractionString());
        Console.WriteLine(f3.GetDecimalValue());

        Fraction f4 = new Fraction(1, 3);
        Console.WriteLine(f4.GetFractionString());
        Console.WriteLine(f4.GetDecimalValue());
    }
}

public class Fraction
{
    private int top;
    private int bottom;

    // Constructor 1: no parameters (1/1)
    public Fraction()
    {
        top = 1;
        bottom = 1;
    }

    // Constructor 2: one parameter (top/1)
    public Fraction(int numerator)
    {
        top = numerator;
        bottom = 1;
    }

    // Constructor 3: two parameters (top/bottom)
    public Fraction(int numerator, int denominator)
    {
        top = numerator;
        bottom = denominator;
    }

    // Getter and Setter for top
    public int GetTop()
    {
        return top;
    }

    public void SetTop(int value)
    {
        top = value;
    }

    // Getter and Setter for bottom
    public int GetBottom()
    {
        return bottom;
    }

    public void SetBottom(int value)
    {
        bottom = value;
    }

    // Method: return fraction as string
    public string GetFractionString()
    {
        return $"{top}/{bottom}";
    }

    // Method: return decimal value
    public double GetDecimalValue()
    {
        return (double)top / bottom;
    }
}
