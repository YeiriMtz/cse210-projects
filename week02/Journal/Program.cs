using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public void Display()
    {
        Console.WriteLine($"{Date} - {Prompt}");
        Console.WriteLine(Response);
        Console.WriteLine();
    }
}

class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        foreach (Entry e in _entries)
        {
            e.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry e in _entries)
            {
                outputFile.WriteLine($"{e.Date} | {e.Prompt} | {e.Response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        _entries.Clear();
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split(" | ");
            if (parts.Length == 3)
            {
                Entry newEntry = new Entry
                {
                    Date = parts[0].Trim(),
                    Prompt = parts[1].Trim(),
                    Response = parts[2].Trim()
                };
                _entries.Add(newEntry);
            }
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        Random rand = new Random();
        List<string> prompts = new List<string>
        {
             "Who was the most interesting person I interacted with today?",
             "What was the best part of my day?",
             "How did I see the hand of the Lord in my life today?",
             "What was the strongest emotion I felt today?",
             "If I had one thing I could do over today, what would it be?"
        };
        int choice = 0;
        while (choice != 5)
        {
            Console.WriteLine("\nJournal Menu");
            Console.WriteLine("1. Write 🖋️");
            Console.WriteLine("2. Display 📖");
            Console.WriteLine("3. Save 💾");
            Console.WriteLine("4. Load 📂");
            Console.WriteLine("5. Quit 👋🏼");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();
            int.TryParse(input, out choice);
            switch (choice)
            {
                case 1:
                    string prompt = prompts[rand.Next(prompts.Count)];
                    Console.WriteLine(prompt);
                    Console.Write("> ");
                    string response = Console.ReadLine();
                    Entry entry = new Entry
                    {
                        Date = DateTime.Now.ToShortDateString(),
                        Prompt = prompt,
                        Response = response
                    };
                    journal.AddEntry(entry);
                    break;
                case 2:
                    journal.DisplayAll();
                    break;
                case 3:
                    Console.Write("Enter filename: ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    Console.WriteLine("Journal SAVED ✅");
                    break;
                case 4:
                    Console.Write("Enter filename: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    Console.WriteLine("Journal LOADED ✅");
                    break;
                case 5:
                    Console.WriteLine("Goodbye! 🙋🏻‍♂️");
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again");
                    break;
            }
        }
    }
}
