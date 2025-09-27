using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Load scriptures from the file
        List<Scripture> scriptures = LoadScripturesFromFile("scriptures.txt");

        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found. Program ended.");
            return;
        }

        // Let user select a scripture
        Scripture selectedScripture = SelectScripture(scriptures);
        if (selectedScripture == null)
        {
            Console.WriteLine("No scripture selected. Program ended.");
            return;
        }

        while (!selectedScripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(selectedScripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            selectedScripture.HideRandomWords(3); // hides 3 words at a time
        }

        Console.Clear();
        Console.WriteLine(selectedScripture.GetDisplayText());
        Console.WriteLine("\nAll words are hidden. Program ended.");
    }

    // Loads scriptures from a file
    static List<Scripture> LoadScripturesFromFile(string filename)
    {
        List<Scripture> scriptures = new List<Scripture>();

        if (!File.Exists(filename))
        {
            Console.WriteLine($"File {filename} not found.");
            return scriptures;
        }

        string[] lines = File.ReadAllLines(filename);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split('|');
            if (parts.Length != 2) continue;

            string referenceText = parts[0].Trim();
            string scriptureText = parts[1].Trim();

            Reference reference = ParseReference(referenceText);
            if (reference != null)
            {
                scriptures.Add(new Scripture(reference, scriptureText));
            }
        }

        return scriptures;
    }

    static Reference ParseReference(string referenceText)
    {
        try
        {
            string[] bookAndVerse = referenceText.Split(' ');
            string book = bookAndVerse[0];

            string chapterAndVerse = bookAndVerse[1];
            string[] chapterSplit = chapterAndVerse.Split(':');
            int chapter = int.Parse(chapterSplit[0]);

            if (chapterSplit[1].Contains("-"))
            {
                string[] verses = chapterSplit[1].Split('-');
                int startVerse = int.Parse(verses[0]);
                int endVerse = int.Parse(verses[1]);
                return new Reference(book, chapter, startVerse, endVerse);
            }
            else
            {
                int verse = int.Parse(chapterSplit[1]);
                return new Reference(book, chapter, verse);
            }
        }
        catch
        {
            Console.WriteLine($"Failed to parse reference: {referenceText}");
            return null;
        }
    }

    static Scripture SelectScripture(List<Scripture> scriptures)
    {
        Console.WriteLine("Available Scriptures:");
        for (int i = 0; i < scriptures.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {scriptures[i].GetReferenceText()}");
        }

        Console.WriteLine("\nEnter the number of the scripture to practice:");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int choice))
        {
            if (choice >= 1 && choice <= scriptures.Count)
            {
                return scriptures[choice - 1];
            }
        }

        return null;
    }
}

public class Reference
{
    private string book;
    private int chapter;
    private int startVerse;
    private int? endVerse;

    public Reference(string book, int chapter, int verse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = verse;
        this.endVerse = null;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = startVerse;
        this.endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (endVerse.HasValue)
            return $"{book} {chapter}:{startVerse}-{endVerse}";
        else
            return $"{book} {chapter}:{startVerse}";
    }
}

public class Word
{
    private string text;
    private bool hidden;

    public Word(string text)
    {
        this.text = text;
        hidden = false;
    }

    public void Hide()
    {
        hidden = true;
    }

    public bool IsHidden()
    {
        return hidden;
    }

    public string GetDisplayText()
    {
        if (hidden)
            return new string('_', text.Length);
        return text;
    }
}

public class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = new List<Word>();
        string[] splitWords = text.Split(' ');
        foreach (var w in splitWords)
        {
            words.Add(new Word(w));
        }
    }

    public void HideRandomWords(int count)
    {
        Random rand = new Random();
        int hiddenCount = 0;

        List<Word> notHiddenWords = words.FindAll(w => !w.IsHidden());
        if (notHiddenWords.Count == 0)
            return;

        while (hiddenCount < count && notHiddenWords.Count > 0)
        {
            int index = rand.Next(notHiddenWords.Count);
            notHiddenWords[index].Hide();
            hiddenCount++;
            notHiddenWords.RemoveAt(index);
        }
    }

    public string GetDisplayText()
    {
        string scriptureText = reference.GetDisplayText() + " ";
        foreach (var w in words)
        {
            scriptureText += w.GetDisplayText() + " ";
        }
        return scriptureText.Trim();
    }

    public bool AllWordsHidden()
    {
        foreach (var w in words)
        {
            if (!w.IsHidden())
                return false;
        }
        return true;
    }

    public string GetReferenceText()
    {
        return reference.GetDisplayText();
    }
}
