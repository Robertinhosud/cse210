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
            remainingTime -= 2; // Counting both inspire and expire as 1 second each
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
        "Pense em uma ocasião em que você defendeu outra pessoa.",
        "Pense em uma ocasião em que você fez algo realmente difícil.",
        "Pense em uma ocasião em que você ajudou alguém necessitado.",
        "Pense em uma época em que você fez algo verdadeiramente altruísta."
    };

    private List<string> questions = new List<string>
    {
        "Por que essa experiência foi significativa para você?",
        "Você já fez algo assim antes?",
        "Como você começou?",
        "Como você se sentiu quando terminou?",
        "O que tornou este momento diferente de outros momentos em que você não teve tanto sucesso?",
        "Qual é a sua coisa favorita nesta experiência?",
        "O que você poderia aprender com essa experiência que se aplica a outras situações?",
        "O que você aprendeu sobre si mesmo através dessa experiência?",
        "Como você pode manter essa experiência em mente no futuro?"
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
        "Quem são as pessoas que você aprecia?",
        "Quais são os seus pontos fortes pessoais?",
        "Quem são as pessoas que você ajudou esta semana?",
        "Quando você sentiu o Espírito Santo neste mês?",
        "Quem são alguns de seus heróis pessoais?"
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
            Console.WriteLine("4. Exit");

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
