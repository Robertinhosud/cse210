using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        // Create an instance of your goal manager and start your program from here
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}

public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    private int _points;

    public string ShortName { get { return _shortName; } }
    public string Description { get { return _description; } }
    public int Points { get { return _points; } }

    // Constructor
    public Goal(string shortName, string description, int points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
    }

    // Abstract methods to be implemented in derived classes
    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStringRepresentation();
}

public class SimpleGoal : Goal
{
    private bool _isComplete;

    // Constructor
    public SimpleGoal(string shortName, string description, int points) : base(shortName, description, points)
    {
        _isComplete = false; // Initialize as not complete
    }

    // Method to record the event
    public override void RecordEvent()
    {
        _isComplete = true;
        Console.WriteLine($"Event recorded for simple goal: {ShortName}");
    }

    // Method to check if the goal is complete
    public override bool IsComplete()
    {
        return _isComplete;
    }

    // Method to get the string representation
    public override string GetStringRepresentation()
    {
        return $"[SimpleGoal] {_shortName}: {_description}";
    }
}

public class EternalGoal : Goal
{
    // Constructor
    public EternalGoal(string shortName, string description, int points) : base(shortName, description, points)
    {
        // No specific variables needed for eternal goals
    }

    // Implementation methods
    public override void RecordEvent()
    {
        Console.WriteLine($"Event recorded for eternal goal: {ShortName}");
    }

    public override bool IsComplete()
    {
        // Eternal goals are never completed
        return false;
    }

    public override string GetStringRepresentation()
    {
        return $"[EternalGoal] {_shortName}: {_description}";
    }
}

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;
    private GoalManager _manager; // Adding a field to store the reference to the GoalManager

    // Constructor
    public ChecklistGoal(string shortName, string description, int points, int target, int bonus, GoalManager manager) 
        : base(shortName, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
        _manager = manager; // Storing the reference to the GoalManager
    }

    // Getters and setters for the attributes
    public int Target { get { return _target; } }
    public int Bonus { get { return _bonus; } }
    public int AmountCompleted 
    { 
        get { return _amountCompleted; } 
        set { _amountCompleted = value; } // Adding a setter to allow modification
    }

    // Implementation methods
    public override void RecordEvent()
    {
        _amountCompleted++;
        Console.WriteLine($"Event recorded for checklist goal: {ShortName}");

        if (IsComplete())
        {
            _manager.UpdateScore(_bonus); // Adding the bonus to the score using the method from the GoalManager class
            Console.WriteLine($"Checklist goal '{ShortName}' completed! {_bonus} point bonus added to the score.");
        }
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetStringRepresentation()
    {
        return $"[ChecklistGoal] {_shortName}: {_description}";
    }
}

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    // Constructor
    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    // User interaction methods
    public void Start()
    {
        Console.WriteLine("Welcome to Eternal Quest!");

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. View player information");
            Console.WriteLine("2. List available goals");
            Console.WriteLine("3. Create a new goal");
            Console.WriteLine("4. Record an event for a goal");
            Console.WriteLine("5. Save goals to a file");
            Console.WriteLine("6. Load goals from a file");
            Console.WriteLine("7. Exit");

            Console.Write("Option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayPlayerInfo();
                    break;
                case "2":
                    ListGoalDetails();
                    break;
                case "3":
                    CreateGoalMenu();
                    break;
                case "4":
                    RecordEventMenu();
                    break;
                case "5":
                    SaveGoalsMenu();
                    break;
                case "6":
                    LoadGoalsMenu();
                    break;
                case "7":
                    exit = true;
                    Console.WriteLine("Thank you for playing Eternal Quest!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"Current score: {_score}");
        Console.WriteLine($"Total number of completed goals: {_goals.Count(g => g.IsComplete())}");
        Console.WriteLine($"Total number of goals in progress: {_goals.Count(g => !g.IsComplete())}");

        Console.WriteLine("\nGoal Details:");
        foreach (var goal in _goals)
        {
            Console.WriteLine($"- {goal.ShortName}: {(goal.IsComplete() ? "Completed" : "In Progress")}");
        }
    }

    public void ListGoalDetails()
    {
        foreach (var goal in _goals)
        {
            Console.WriteLine($"Name: {goal.ShortName}");
            Console.WriteLine($"Description: {goal.Description}");
            Console.WriteLine($"Points: {goal.Points}");
            Console.WriteLine($"Status: {(goal.IsComplete() ? "Completed" : "Pending")}");
            Console.WriteLine();
        }
    }

    public void CreateGoalMenu()
    {
        Console.WriteLine("What type of goal do you want to create?");
        Console.WriteLine("1. Simple Goal: A goal that can be marked as completed or not.");
        Console.WriteLine("2. Eternal Goal: A goal that can never be completed.");
        Console.WriteLine("3. Checklist Goal: A goal that requires a certain number of tasks to be completed.");
        Console.Write("Choose an option: ");
        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Invalid option. Please try again.");
            return;
        }

        switch (choice)
        {
            case 1:
                CreateSimpleGoal();
                break;
            case 2:
                CreateEternalGoal();
                break;
            case 3:
                CreateChecklistGoal();
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    private void CreateSimpleGoal()
    {
        Console.Write("Goal name: ");
        string shortName = Console.ReadLine();
        Console.Write("Goal description: ");
        string description = Console.ReadLine();
        Console.Write("Goal points: ");
        int points;
        if (!int.TryParse(Console.ReadLine(), out points))
        {
            Console.WriteLine("Invalid value for points. Please try again.");
            return;
        }

        SimpleGoal simpleGoal = new SimpleGoal(shortName, description, points);
        _goals.Add(simpleGoal);
        Console.WriteLine("Simple Goal created successfully!");
    }

    private void CreateEternalGoal()
    {
        Console.Write("Goal name: ");
        string shortName = Console.ReadLine();
        Console.Write("Goal description: ");
        string description = Console.ReadLine();
        Console.Write("Goal points: ");
        int points;
        if (!int.TryParse(Console.ReadLine(), out points))
        {
            Console.WriteLine("Invalid value for points. Please try again.");
            return;
        }

        EternalGoal eternalGoal = new EternalGoal(shortName, description, points);
        _goals.Add(eternalGoal);
        Console.WriteLine("Eternal Goal created successfully!");
    }

    private void CreateChecklistGoal()
    {
        Console.Write("Goal name: ");
        string shortName = Console.ReadLine();
        Console.Write("Goal description: ");
        string description = Console.ReadLine();
        Console.Write("Goal points: ");
        int points;
        if (!int.TryParse(Console.ReadLine(), out points))
        {
            Console.WriteLine("Invalid value for points. Please try again.");
            return;
        }
        Console.Write("Enter the target for the Checklist Goal: ");
        int target;
        if (!int.TryParse(Console.ReadLine(), out target))
        {
            Console.WriteLine("Invalid value for the target. Please try again.");
            return;
        }
        Console.Write("Enter the bonus for the Checklist Goal: ");
        int bonus;
        if (!int.TryParse(Console.ReadLine(), out bonus))
        {
            Console.WriteLine("Invalid value for the bonus. Please try again.");
            return;
        }

        ChecklistGoal checklistGoal = new ChecklistGoal(shortName, description, points, target, bonus, this);
        _goals.Add(checklistGoal);
        Console.WriteLine("Checklist Goal created successfully!");
    }

    public void RecordEventMenu()
    {
        Console.Write("Enter the goal name: ");
        string goalName = Console.ReadLine();
        RecordEvent(goalName);
    }

    public void RecordEvent(string goalName)
    {
        Goal goal = _goals.Find(g => g.ShortName == goalName);

        if (goal != null)
        {
            goal.RecordEvent();
            _score += goal.Points;
            Console.WriteLine($"Event recorded for the goal: {goalName}");
            Console.WriteLine($"Updated score: {_score}");
        }
        else
        {
            Console.WriteLine("Goal not found.");
        }
    }

    public void SaveGoalsMenu()
    {
        Console.Write("Enter the file name to save the goals: ");
        string fileName = Console.ReadLine();
        SaveGoals(fileName);
    }

    public void SaveGoals(string fileName)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (Goal goal in _goals)
                {
                    string goalType = goal.GetType().Name;
                    writer.WriteLine($"{goalType},{goal.ShortName},{goal.Description},{goal.Points}");

                    // If the goal is a checklist goal, write the additional details
                    if (goal is ChecklistGoal checklistGoal)
                    {
                        writer.WriteLine($"{checklistGoal.Target},{checklistGoal.Bonus},{checklistGoal.AmountCompleted}");
                    }
                }
            }
            Console.WriteLine("Goals saved successfully!");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {fileName}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    public void LoadGoalsMenu()
    {
        Console.Write("Enter the file name to load the goals from: ");
        string fileName = Console.ReadLine();
        LoadGoals(fileName);
    }

    public void LoadGoals(string fileName)
    {
        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    string typeName = parts[0];
                    string shortName = parts[1];
                    string description = parts[2];
                    int points = int.Parse(parts[3]);

                    Goal goal;

                    switch (typeName)
                    {
                        case "SimpleGoal":
                            goal = new SimpleGoal(shortName, description, points);
                            break;
                        case "EternalGoal":
                            goal = new EternalGoal(shortName, description, points);
                            break;
                        case "ChecklistGoal":
                            int target = int.Parse(parts[4]);
                            int bonus = int.Parse(parts[5]);
                            int amountCompleted = int.Parse(parts[6]);
                            goal = new ChecklistGoal(shortName, description, points, target, bonus, this);
                            ((ChecklistGoal)goal).AmountCompleted = amountCompleted;
                            break;
                        default:
                            throw new ArgumentException($"Unknown goal type: {typeName}");
                    }

                    _goals.Add(goal);
                }
            }
            Console.WriteLine("Goals loaded successfully!");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {fileName}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
        }
    }

    public void UpdateScore(int scoreDelta)
    {
        _score += scoreDelta;
    }
}
