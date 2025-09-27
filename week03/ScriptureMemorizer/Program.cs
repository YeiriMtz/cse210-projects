using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = new List<Scripture>();

        // Scripture 1 
        Reference ref1 = new Reference("John", 3, 16);
        string text1 = "For God so loved the world that He gave His only Son, that whoever believes in Him shall not perish but have eternal life.";
        scriptures.Add(new Scripture(ref1, text1));

        // Scripture 2 
        Reference ref2 = new Reference("1 Nephi", 3, 7);
        string text2 = "And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them.";
        scriptures.Add(new Scripture(ref2, text2));

        // Scripture 3 
        Reference ref3 = new Reference("D & C", 14, 7);
        string text3 = "And, if you keep my commandments and endure to the end you shall have eternal life, which gift is the greatest of all the gifts of God.";
        scriptures.Add(new Scripture(ref3, text3));

        // Let the user select a scripture
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

            selectedScripture.HideRandomWords(3);
        }

        Console.Clear();
        Console.WriteLine(selectedScripture.GetDisplayText());
        Console.WriteLine("\nAll words are hidden. Program ended.");
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
