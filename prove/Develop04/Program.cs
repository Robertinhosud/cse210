using System;
using System.Threading;
using System.Collections.Generic;

// Classe base para atividades de mindfulness
class MindfulnessActivity
{
    protected string name;
    protected string description;
    protected int duration;

    public MindfulnessActivity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public virtual void StartActivity()
    {
        Console.WriteLine($"Starting {name} activity...");
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());

        // Start the activity
        Console.WriteLine("Get ready...");
        Thread.Sleep(3000); // Pause for 3 seconds before starting
        Console.WriteLine("Start!");
        Thread.Sleep(1000); // Pause for 1 second before beginning
    }

    public virtual void EndActivity()
    {
        Console.WriteLine($"Congratulations! You've completed the {name} activity.");
        Thread.Sleep(2000); // Pause for 2 seconds before ending
        Console.WriteLine($"Activity duration: {duration} seconds");
    }
}

// Subclasse para atividade respiratória
class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by inhaling and exhaling slowly. Clear your mind and focus on your breath.")
    {
    }

    public override void StartActivity()
    {
        base.StartActivity();

        Console.WriteLine("Get ready to breathe deeply...");
        Thread.Sleep(3000); // Pause for 3 seconds before starting

        int remainingTime = duration;
        while (remainingTime > 0)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(3000); // Pause for 1 second
            Console.WriteLine("Breathe out...");
            Thread.Sleep(4000); // Pause for 1 second
            remainingTime -= 5; // Counting both inspire and expire as 1 second each
        }
    }

    public override void EndActivity()
    {
        base.EndActivity();
    }
}

// Subclasse para atividade de reflexão

class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        " * Think of a time when you stood up for someone else.",
        " * Think of a time when you did something really difficult.",
        " * Think of a time when you helped someone in need.",
        " * Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done something like this before?",
        "How did you start?",
        "How did you feel when you finished?",
        "What made this moment different from other times when you weren't as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What have you learned about yourself through this experience?",
        "How can you keep this experience in mind for the future?"
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on moments in your life where you demonstrated strength and resilience.")
    {
    }

    public override void StartActivity()
    {
        base.StartActivity();
        Console.WriteLine("Get ready to reflect...");

        Random random = new Random();
        int durationInSeconds = duration;
        int remainingTime = durationInSeconds;

        while (remainingTime > 0)
        {
            string prompt = prompts[random.Next(prompts.Count)];
            string question = questions[random.Next(questions.Count)];

            Console.WriteLine(prompt);
            Thread.Sleep(4000); // Pause for 2 seconds before showing the question

            Console.WriteLine($"Question: {question}");
            Thread.Sleep(6000); // Pause for 3 seconds before showing the next question

            Console.WriteLine("");

            remainingTime -= 5; // Deducting time for prompt and question
        }
    }

    public override void EndActivity()
    {
        base.EndActivity();
    }
}


// Subclasse para atividade de listagem
class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are the people you appreciate?",
        "What are your personal strengths?",
        "Who are the people you helped this week?",
        "When did you feel the Holy Spirit this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by listing as many things as you can in a particular area.")
    {
    }

    public override void StartActivity()
    {
        base.StartActivity();
        Console.WriteLine("Get ready to list...");

        Random random = new Random();
        int durationInSeconds = duration;
        int remainingTime = durationInSeconds;

        while (remainingTime > 0)
        {
            string prompt = prompts[random.Next(prompts.Count)];
            Console.WriteLine(prompt);
            Console.WriteLine("Start listing...");

            int itemsCount = 0;
            DateTime startTime = DateTime.Now;

            while (remainingTime > 0)
            {
                Console.Write("Enter an item (or type 'done' to finish listing): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "done")
                    break;

                itemsCount++;
                remainingTime = (int)(durationInSeconds - (DateTime.Now - startTime).TotalSeconds); // Update remaining time
            }

            Console.WriteLine($"You listed {itemsCount} items.");
            remainingTime -= 5; // Deducting time for prompt and ending message
        }
    }

    public override void EndActivity()
    {
        base.EndActivity();
    }
}

class GratitudeActivity : MindfulnessActivity
{
    public GratitudeActivity() : base("Gratitude", "This activity will help you cultivate gratitude by reflecting on the things you are thankful for in your life.")
    {
    }

    public override void StartActivity()
    {
        base.StartActivity();
        Console.WriteLine("Get ready to express gratitude...");

        int durationInSeconds = duration;
        int remainingTime = durationInSeconds;
        int itemsCount = 0;
        DateTime startTime = DateTime.Now;

        while (remainingTime > 0)
        {
            Console.Write("What are you grateful for? (or type 'done' to finish): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "done")
                break;

            itemsCount++;
            remainingTime = (int)(durationInSeconds - (DateTime.Now - startTime).TotalSeconds); // Update remaining time
        }

        Console.WriteLine($"You expressed gratitude for {itemsCount} things.");
    }

    public override void EndActivity()
    {
        base.EndActivity();
    }
}


class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("Welcome to the Mindfulness Program!");
            Console.WriteLine("Please choose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Gratitude");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
 int choice = Convert.ToInt32(Console.ReadLine());

            MindfulnessActivity activity = null;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity();
                    break;
                case 2:
                    activity = new ReflectionActivity();
                    break;
                case 3:
                    activity = new ListingActivity();
                    break;
                case 4:
                    activity = new GratitudeActivity();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            activity.StartActivity();
            activity.EndActivity();
        }
    }
}
