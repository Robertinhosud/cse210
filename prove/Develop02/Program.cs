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
            Console.WriteLine($"Erro ao carregar o arquivo: {ex.Message}");
        }
    }
}

// Classe principal do programa
class Program
{
    static void Main()
    {
        Diary diary = new Diary();

        while (true)
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Escrever uma nova entrada");
            Console.WriteLine("2. Exibir o diário");
            Console.WriteLine("3. Salvar o diário em um arquivo");
            Console.WriteLine("4. Carregar o diário de um arquivo");
            Console.WriteLine("5. Sair");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Escreva uma nova entrada:");
                    string prompt = GetRandomPrompt(); // Implemente a lógica para obter um prompt aleatório
                    Console.WriteLine($"Pergunta: {prompt}");
                    string response = Console.ReadLine();
                    diary.AddEntry(prompt, response);
                    break;

                case "2":
                    diary.DisplayEntries();
                    break;

                case "3":
                    Console.WriteLine("Digite o nome do arquivo para salvar:");
                    string saveFileName = Console.ReadLine();
                    diary.SaveToFile(saveFileName);
                    break;

                case "4":
                    Console.WriteLine("Digite o nome do arquivo para carregar:");
                    string loadFileName = Console.ReadLine();
                    diary.LoadFromFile(loadFileName);
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
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
            "Quem foi a pessoa mais interessante com quem interagi hoje?",
            "Qual foi a melhor parte do meu dia?",
            "Como vi a mão do Senhor em minha vida hoje?",
            "Qual foi a emoção mais forte que senti hoje?",
            "Se eu tivesse algo que pudesse fazer hoje, o que seria?"
        };

        Random random = new Random();
        int index = random.Next(prompts.Count);

        return prompts[index];
    }
}
