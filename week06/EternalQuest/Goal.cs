using System;

namespace EternalQuest
{
    public enum GoalType { Simple, Eternal, Checklist }

    public class Goal
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Points { get; private set; }
        public GoalType Type { get; private set; }
        public bool IsComplete { get; private set; } = false;

        // Checklist specific
        private int _target = 0;
        private int _bonus = 0;
        private int _completed = 0;

        // Constructor for Simple/Eternal
        public Goal(string name, string desc, int points, GoalType type)
        {
            Name = name;
            Description = desc;
            Points = points;
            Type = type;
        }

        // Constructor for Checklist
        public Goal(string name, string desc, int points, int bonus, int target)
        {
            Name = name;
            Description = desc;
            Points = points;
            Type = GoalType.Checklist;
            _bonus = bonus;
            _target = target;
        }

        public int RecordEvent()
        {
            int earned = 0;
            switch (Type)
            {
                case GoalType.Simple:
                    if (!IsComplete)
                    {
                        IsComplete = true;
                        earned = Points;
                    }
                    break;
                case GoalType.Eternal:
                    earned = Points;
                    break;
                case GoalType.Checklist:
                    if (_completed < _target)
                    {
                        _completed++;
                        earned = Points;
                        if (_completed == _target) earned += _bonus;
                        if (_completed >= _target) IsComplete = true;
                    }
                    break;
            }
            return earned;
        }

        public string GetDetails()
        {
            return Type switch
            {
                GoalType.Simple => $"[{(IsComplete ? "X" : " ")}] {Name} - {Description}",
                GoalType.Eternal => $"[∞] {Name} - {Description}",
                GoalType.Checklist => $"[{(IsComplete ? "X" : " ")}] {Name} - {Description} ({_completed}/{_target})",
                _ => Name
            };
        }

        // Simple save format
        public string SaveString()
        {
            return Type switch
            {
                GoalType.Simple => $"Simple|{Name}|{Description}|{Points}|{IsComplete}",
                GoalType.Eternal => $"Eternal|{Name}|{Description}|{Points}",
                GoalType.Checklist => $"Checklist|{Name}|{Description}|{Points}|{_bonus}|{_completed}|{_target}",
                _ => ""
            };
        }

        public static Goal LoadFromString(string line)
        {
            var parts = line.Split('|');
            switch (parts[0])
            {
                case "Simple":
                    var s = new Goal(parts[1], parts[2], int.Parse(parts[3]), GoalType.Simple);
                    s.IsComplete = bool.Parse(parts[4]);
                    return s;
                case "Eternal":
                    return new Goal(parts[1], parts[2], int.Parse(parts[3]), GoalType.Eternal);
                case "Checklist":
                    return new Goal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[6]))
                    { _completed = int.Parse(parts[5]), IsComplete = int.Parse(parts[5]) >= int.Parse(parts[6]) };
                default: return null;
            }
        }
    }
}
