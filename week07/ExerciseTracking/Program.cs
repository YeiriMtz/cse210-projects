using System;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        activities.Add(new Running(new DateTime(2022, 11, 3),30, 4.8));  // KM
        activities.Add(new Cycling(new DateTime(2022, 11, 3),45, 20));  // KPH
        activities.Add(new Swimming(new DateTime(2022, 11, 3), 60, 40));  // LAPS

        Console.WriteLine();
        Console.WriteLine("-----------------------------------------------------------------------------");

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }

        Console.WriteLine("-----------------------------------------------------------------------------");
        Console.WriteLine();
    }
}