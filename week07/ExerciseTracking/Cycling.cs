using System;

class Cycling : Activity
{
    private double _speed;  // KILOMETERS PER HOUR

    public Cycling(DateTime date, double minutes, double speed) 
        : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return _speed * GetMinutes() / 60;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        return 60 / _speed;
    }
}
