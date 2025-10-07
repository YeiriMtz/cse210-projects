using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // BASE FOR ALL ACTIVITIES
    abstract class Activity
    {
        private string _name;
        private string _description;
        private int _duration;

        public Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void SetDuration(int duration)
        {
            _duration = duration;
        }

        // STARTS ACTIVITY
        public void Start()
        {
            ShowStartingMessage();
            PerformActivity();
            ShowEndingMessage();
        }

        private void ShowStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"Starting {_name}...");
            Console.WriteLine(_description);
            Console.Write("Enter duration in seconds: ");
            int duration = int.Parse(Console.ReadLine() ?? "30");
            SetDuration(duration);
            Console.WriteLine("Get ready...");
            ShowPauseAnimation(3);
        }

        protected abstract void PerformActivity();

        private void ShowEndingMessage()
        {
            Console.WriteLine("\nWell done!");
            ShowPauseAnimation(3);
            Console.WriteLine($"You have completed {_name} for {_duration} seconds.");
            ShowPauseAnimation(3);
        }

        protected void ShowPauseAnimation(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        protected int GetDuration()
        {
            return _duration;
        }
    }

    // BREATHING ACTIVITY
    class BreathingActivity : Activity
    {
        public BreathingActivity() 
            : base("Breathing Activity", 
                  "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }

        protected override void PerformActivity()
        {
            int duration = GetDuration();
            int elapsed = 0;

            while (elapsed < duration)
            {
                Console.WriteLine("\nBreathe in...");
                Countdown(4);
                elapsed += 4;
                if (elapsed >= duration) break;

                Console.WriteLine("Breathe out...");
                Countdown(6);
                elapsed += 6;
            }
        }

        private void Countdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i + " ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }

    // REFLECTION ACTIVITY
    class ReflectionActivity : Activity
    {
        private List<string> prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private Random random = new Random();

        public ReflectionActivity()
            : base("Reflection Activity", 
                  "This activity will help you reflect on times in your life when you have shown strength and resilience.")
        { }

        protected override void PerformActivity()
        {
            Console.WriteLine("\nPrompt:");
            Console.WriteLine(prompts[random.Next(prompts.Count)]);
            ShowPauseAnimation(3);

            int duration = GetDuration();
            int elapsed = 0;

            while (elapsed < duration)
            {
                string question = questions[random.Next(questions.Count)];
                Console.WriteLine("\n" + question);
                ShowPauseAnimation(5); 
                elapsed += 5;
            }
        }
    }

    // LIST ACTIVTY
    class ListingActivity : Activity
    {
        private List<string> prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private Random random = new Random();

        public ListingActivity()
            : base("Listing Activity",
                  "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        { }

        protected override void PerformActivity()
        {
            Console.WriteLine("\nPrompt:");
            Console.WriteLine(prompts[random.Next(prompts.Count)]);
            Console.WriteLine("You have a few seconds to start thinking...");
            ShowPauseAnimation(5);

            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            List<string> items = new List<string>();

            while (DateTime.Now < endTime)
            {
                Console.Write("Write your answer: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                    items.Add(input);
            }

            Console.WriteLine($"\nYou listed {items.Count} items!");
        }
    }

    // MENU
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness App");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Quit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                Activity? activity = choice switch
                {
                    "1" => new BreathingActivity(),
                    "2" => new ReflectionActivity(),
                    "3" => new ListingActivity(),
                    "4" => null,
                    _ => null
                };

                if (choice == "4")
                    break;

                if (activity != null)
                    activity.Start();

                Console.WriteLine("\nPress Enter to return to menu...");
                Console.ReadLine();
            }
        }
    }
}