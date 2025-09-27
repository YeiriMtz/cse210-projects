using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Reference ref1 = new Reference("John", 3, 16);
        string text1 = "For God so loved the world that He gave His only Son, that whoever believes in Him shall not perish but have eternal life.";
        Scripture scripture1 = new Scripture(ref1, text1);

        Reference ref2 = new Reference("Proverbs", 3, 5, 6);
        string text2 = "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways acknowledge Him, and He will make your paths straight.";
        Scripture scripture2 = new Scripture(ref2, text2);

        Scripture scripture = scripture1;

        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(3);
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nAll words are hidden. Program ended.");
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
}
