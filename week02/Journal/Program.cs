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

    // SAVES ENTRIES TO CSV
    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            // HEADER ROW FOR EXCEL
            outputFile.WriteLine("Date,Prompt,Response");

            foreach (Entry e in _entries)
            {
                string date = EscapeForCsv(e.Date);
                string prompt = EscapeForCsv(e.Prompt);
                string response = EscapeForCsv(e.Response);

                outputFile.WriteLine($"{date},{prompt},{response}");
            }
        }
    }

    // LOAD ENTRIES FROM CSV
    public void LoadFromFile(string filename)
    {
        _entries.Clear();
        string[] lines = File.ReadAllLines(filename);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = ParseCsvLine(line);

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

    // HELPERS FOR CSV HANDLING
    private string EscapeForCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\""))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }

    private string[] ParseCsvLine(string line)
    {
        List<string> fields = new List<string>();
        bool inQuotes = false;
        string current = "";

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current += '"';
                    i++;
                }

                else
                {
                    inQuotes = !inQuotes;
                }
            }

            else if (c == ',' && !inQuotes)
            {
                fields.Add(current);
                current = "";
            }

            else
            {
                current += c;
            }
        }
        fields.Add(current);
        return fields.ToArray();
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
