using System;

abstract class Activity
{
    private DateTime _date;
    private double _minutes;

    public Activity(DateTime date, double minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime GetDate() => _date;
    public double GetMinutes() => _minutes;

    // KILOMETERS
    public abstract double GetDistance();  // KM
    public abstract double GetSpeed();     // KM PER HOUR
    public abstract double GetPace();      // MINUTES PER KM

    // SUMMARY METHOD
    public virtual string GetSummary()
    {
        return $"{_date:dd MMM yyyy} {this.GetType().Name} ({_minutes} min) - " +
               $"Distance: {GetDistance():0.0} km, " +
               $"Speed: {GetSpeed():0.0} kph, " +
               $"Pace: {GetPace():0.2} min/km";
    }
}
