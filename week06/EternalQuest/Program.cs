using System;
using System.Collections.Generic;

namespace EternalQuest
{
    class Program
    {
        static List<Goal> goals = new List<Goal>();
        static int score = 0;

        static void Main()
        {
            while (true)
            {
                Console.WriteLine($"\nEternal Quest - Score: {score}");
                Console.WriteLine("\n1. Create Goal 📝");
                Console.WriteLine("2. List Goal 📋");
                Console.WriteLine("3. Save 💿");
                Console.WriteLine("4. Load 📂");
                Console.WriteLine("5. Record Event ✏️");
                Console.WriteLine("6. Quit 👋🏻");
                Console.Write("\nYour choice: ");
                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1": CreateGoal(); break;
                    case "2": ListGoals(); break;
                    case "3": SaveGoals(); break;
                    case "4": LoadGoals(); break;
                    case "5": RecordEvent(); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid choice ❌, choose any number between 1 and 6."); break;
                }
            }
        }

        static void CreateGoal()
        {
            Console.Write("\nGoal type (1: Simple, 2: Eternal, 3: Checklist): ");
            var t = Console.ReadLine();
            Console.Write("Name: "); var name = Console.ReadLine();
            Console.Write("Description: "); var desc = Console.ReadLine();

            switch (t)
            {
                case "1":
                    Console.Write("Points: "); int p1 = int.Parse(Console.ReadLine());
                    goals.Add(new Goal(name, desc, p1, GoalType.Simple));
                    break;
                case "2":
                    Console.Write("Points per record: "); int p2 = int.Parse(Console.ReadLine());
                    goals.Add(new Goal(name, desc, p2, GoalType.Eternal));
                    break;
                case "3":
                    Console.Write("Points per completion: "); int p3 = int.Parse(Console.ReadLine());
                    Console.Write("Bonus: "); int b = int.Parse(Console.ReadLine());
                    Console.Write("Target count: "); int target = int.Parse(Console.ReadLine());
                    goals.Add(new Goal(name, desc, p3, b, target));
                    break;
            }
        }

        static void ListGoals()
        {
            for (int i = 0; i < goals.Count; i++)
                Console.WriteLine($"{i + 1}. {goals[i].GetDetails()}");
        }

        static void RecordEvent()
        {
            if (goals.Count == 0) { Console.WriteLine("No goals created."); return; }
            ListGoals();
            Console.Write("Goal number: "); int sel = int.Parse(Console.ReadLine()) - 1;
            if (sel < 0 || sel >= goals.Count) { Console.WriteLine("Invalid."); return; }
            int pts = goals[sel].RecordEvent();
            score += pts;
            Console.WriteLine($"Earned {pts} points! Total: {score}");
        }

        static void SaveGoals()
        {
            Console.Write("Filename (default goals.txt): ");
            var file = Console.ReadLine(); if (string.IsNullOrWhiteSpace(file)) file = "goals.txt";
            using (var w = new StreamWriter(file))
            {
                foreach (var g in goals) w.WriteLine(g.SaveString());
                w.WriteLine($"Score|{score}");
            }
            Console.WriteLine("Saved ✅");
        }

        static void LoadGoals()
        {
            Console.Write("Filename (default goals.txt): ");
            var file = Console.ReadLine(); if (string.IsNullOrWhiteSpace(file)) file = "goals.txt";
            if (!File.Exists(file)) { Console.WriteLine("File not found."); return; }
            goals.Clear();
            foreach (var line in File.ReadAllLines(file))
            {
                if (line.StartsWith("Score|")) { score = int.Parse(line.Split('|')[1]); continue; }
                var g = Goal.LoadFromString(line);
                if (g != null) goals.Add(g);
            }
            Console.WriteLine("Loaded 📂");
        }
    }
}

