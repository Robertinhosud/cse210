using System;
using System.Collections.Generic;
using System.IO;

// Classe para representar uma entrada no diário
public class DiaryEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }

    public DiaryEntry(string prompt, string response, DateTime date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()} - {Prompt}: {Response}";
    }
}

// Classe para gerenciar o diário
public class Diary
{
    private List<DiaryEntry> entries = new List<DiaryEntry>();

    public void AddEntry(string prompt, string response)
    {
        DiaryEntry entry = new DiaryEntry(prompt, response, DateTime.Now);
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (DiaryEntry entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string fileName)
    {
        using (StreamWriter outputFile = new StreamWriter(fileName))
        {
            foreach (DiaryEntry entry in entries)
            {
                outputFile.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }
    }

    public void LoadFromFile(string fileName)
    {
        entries.Clear(); // Limpa as entradas existentes ao carregar de um arquivo novo

        try
        {
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                DateTime date = DateTime.Parse(parts[0]);
                string prompt = parts[1];
                string response = parts[2];

                DiaryEntry entry = new DiaryEntry(prompt, response, date);
                entries.Add(entry);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading the file: {ex.Message}");
        }
    }
}

// Classe principal do programa
class Program
{
    static void Main()
    {
        Diary diary = new Diary();
            Console.WriteLine ("Welcome to JOURNAL Program! your diary app that helps you with your daily memories.");

        while (true)
        {
            // Adicionando saudação com base na hora do dia
            GreetUserBasedOnTime();
            Console.WriteLine("Please Select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display the diary");
            Console.WriteLine("3. Save the diary to a file");
            Console.WriteLine("4. Upload the diary from a file");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Write a new entry::");
                    string prompt = GetRandomPrompt(); // Implemente a lógica para obter um prompt aleatório
                    Console.WriteLine($"Question: {prompt}");
                    string response = Console.ReadLine();
                    diary.AddEntry(prompt, response);
                    
                    
                    break;

                case "2":
                    diary.DisplayEntries();
                    break;

                case "3":
                    Console.WriteLine("Enter the name of the file to save:");
                    string saveFileName = Console.ReadLine();
                    diary.SaveToFile(saveFileName);
                    break;

                case "4":
                    Console.WriteLine("Enter the name of the file to upload:");
                    string loadFileName = Console.ReadLine();
                    diary.LoadFromFile(loadFileName);
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    // Método para obter um prompt aleatório (substitua com sua lógica)
    static string GetRandomPrompt()
    {
        // Implemente a lógica para obter um prompt aleatório da lista fornecida
        // Neste exemplo, estou usando uma lista fixa de prompts para simplificar
        List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the Lord's hand in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do today, what would it be?",
            "If I could choose one thing to improve on my journey today, what would it be?",
            "What challenges have I faced and how do I deal with them today?",
            "What do I think God would say to me today?"
        };

        Random random = new Random();
        int index = random.Next(prompts.Count);

        return prompts[index];
    }
    // Método para cumprimentar o usuário com base na hora do dia
    static void GreetUserBasedOnTime()
    {
        DateTime now = DateTime.Now;
        int hour = now.Hour;

        if (6 <= hour && hour < 12)
        {
            Console.WriteLine("Good Morning!");
        }
        else if (12 <= hour && hour < 18)
        {
            Console.WriteLine("Good Afternoon!");
        }
        else
        {
            Console.WriteLine("Good Evening!");
        }
    }
}

