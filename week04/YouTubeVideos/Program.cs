using System;
using System.Collections.Generic;

// Comment class -----------------------------------------------
class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }

    public override string ToString()
    {
        return $"{CommenterName}: {CommentText}";
    }
}

// Video class -----------------------------------------------
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"\nTitle: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"  {comment}");
        }
        Console.WriteLine(); // Blank line for readability
    }
}

// Program class -----------------------------------------------
class Program
{
    static void Main()
    {
        // Create list of videos
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("HUAWEI WATCH GT 6 Series Review! Premium, Accurate, and Lasts Forever!", "Sean Talks Tech", 728);
        video1.AddComment(new Comment("@hunterthomas8787", "Can you reply with your iphone connected?"));
        video1.AddComment(new Comment("@MrManammal", "Have a stop watch facility? Like I’use football refree."));
        video1.AddComment(new Comment("@PadmaDorjee", "Thanks Sean!"));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("And the Glory of the Lord, from Handel's Messiah | October 2024 General Conference", "General Conference of The Church of Jesus Christ", 180);
        video2.AddComment(new Comment("@MrMywildside", "This was the most powerful conclusion to conference that I have heard in a long time."));
        video2.AddComment(new Comment("@memcgiffin", "This was so powerful after President Nelson’s talk. It feel so close!"));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("En memoria de la vida de Russell M. Nelson", "Iglesia de Jesucristo", 453);
        video3.AddComment(new Comment("@victormanuelplatacalero3603", "Siempre hay que recordar sus enseñanzas!"));
        video3.AddComment(new Comment("@cristinagimenez-yz3bq", "Mi amado Profeta gracias por todas sus enseñanzas."));
        video3.AddComment(new Comment("@leidyruthvallesgarcia1622", "Gracias por todo tu servicio y guía amado profeta."));
        videos.Add(video3);

        // Display all videos
        foreach (var video in videos)
        {
            video.DisplayInfo();
        }
    }
}