using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        // Crie uma instância do seu gerenciador de metas e inicie seu programa a partir daqui
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

    // Construtor
    public Goal(string shortName, string description, int points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
    }

    // Métodos abstratos a serem implementados nas classes derivadas
    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStringRepresentation();
}

public class SimpleGoal : Goal
{
    private bool _isComplete;

    // Construtor
    public SimpleGoal(string shortName, string description, int points) : base(shortName, description, points)
    {
        _isComplete = false; // Inicializa como não completo
    }

    // Método para registrar o evento
    public override void RecordEvent()
    {
        _isComplete = true;
        Console.WriteLine($"Evento registrado para meta simples: {ShortName}");
    }

    // Método para verificar se a meta está completa
    public override bool IsComplete()
    {
        return _isComplete;
    }

    // Método para obter a representação da string
    public override string GetStringRepresentation()
    {
        return $"[SimpleGoal] {_shortName}: {_description}";
    }
}

public class EternalGoal : Goal
{
    // Construtor
    public EternalGoal(string shortName, string description, int points) : base(shortName, description, points)
    {
        // Nenhuma variável específica necessária para metas eternas
    }

    // Métodos de implementação
    public override void RecordEvent()
    {
        Console.WriteLine($"Evento registrado para meta eterna: {ShortName}");
    }

    public override bool IsComplete()
    {
        // As metas eternas nunca são concluídas
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
    private GoalManager _manager; // Adicionando um campo para armazenar a referência para o GoalManager

    // Construtor
    public ChecklistGoal(string shortName, string description, int points, int target, int bonus, GoalManager manager) 
        : base(shortName, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
        _manager = manager; // Armazenando a referência para o GoalManager
    }

    // Getters e setters para os atributos
    public int Target { get { return _target; } }
    public int Bonus { get { return _bonus; } }
    public int AmountCompleted 
    { 
        get { return _amountCompleted; } 
        set { _amountCompleted = value; } // Adicionando um setter para permitir a modificação
    }

    // Métodos de implementação
    public override void RecordEvent()
    {
        _amountCompleted++;
        Console.WriteLine($"Evento registrado para meta de checklist: {ShortName}");

        if (IsComplete())
        {
            _manager.UpdateScore(_bonus); // Adicionando o bônus à pontuação usando o método da classe GoalManager
            Console.WriteLine($"Meta de checklist '{ShortName}' concluída! Bônus de {_bonus} pontos adicionado à pontuação.");
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

    // Construtor
    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    // Métodos para interação do usuário
    public void Start()
    {
        Console.WriteLine("Bem-vindo ao Eternal Quest!");

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nSelecione uma opção:");
            Console.WriteLine("1. Visualizar informações do jogador");
            Console.WriteLine("2. Listar metas disponíveis");
            Console.WriteLine("3. Criar nova meta");
            Console.WriteLine("4. Registrar evento para uma meta");
            Console.WriteLine("5. Salvar metas em um arquivo");
            Console.WriteLine("6. Carregar metas de um arquivo");
            Console.WriteLine("7. Sair");

            Console.Write("Opção: ");
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
                    Console.WriteLine("Obrigado por jogar o Eternal Quest!");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Por favor, selecione uma opção válida.");
                    break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"Pontuação atual: {_score}");
        Console.WriteLine($"Número total de metas concluídas: {_goals.Count(g => g.IsComplete())}");
        Console.WriteLine($"Número total de metas em andamento: {_goals.Count(g => !g.IsComplete())}");

        Console.WriteLine("\nDetalhes das Metas:");
        foreach (var goal in _goals)
        {
            Console.WriteLine($"- {goal.ShortName}: {(goal.IsComplete() ? "Concluída" : "Em andamento")}");
        }
    }

    public void ListGoalDetails()
    {
        foreach (var goal in _goals)
        {
            Console.WriteLine($"Nome: {goal.ShortName}");
            Console.WriteLine($"Descrição: {goal.Description}");
            Console.WriteLine($"Pontos: {goal.Points}");
            Console.WriteLine($"Status: {(goal.IsComplete() ? "Concluída" : "Pendente")}");
            Console.WriteLine();
        }
    }

    public void CreateGoalMenu()
    {
        Console.WriteLine("Qual tipo de meta você deseja criar?");
        Console.WriteLine("1. Meta Simples: Uma meta que pode ser marcada como concluída ou não.");
        Console.WriteLine("2. Meta Eterna: Uma meta que nunca pode ser concluída.");
        Console.WriteLine("3. Meta de Checklist: Uma meta que requer um certo número de tarefas a serem concluídas.");
        Console.Write("Escolha uma opção: ");
        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
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
                Console.WriteLine("Opção inválida. Tente novamente.");
                break;
        }
    }

    private void CreateSimpleGoal()
    {
        Console.Write("Nome da meta: ");
        string shortName = Console.ReadLine();
        Console.Write("Descrição da meta: ");
        string description = Console.ReadLine();
        Console.Write("Pontuação da meta: ");
        int points;
        if (!int.TryParse(Console.ReadLine(), out points))
        {
            Console.WriteLine("Valor inválido para a pontuação. Tente novamente.");
            return;
        }

        SimpleGoal simpleGoal = new SimpleGoal(shortName, description, points);
        _goals.Add(simpleGoal);
        Console.WriteLine("Meta Simples criada com sucesso!");
    }

    private void CreateEternalGoal()
    {
        Console.Write("Nome da meta: ");
        string shortName = Console.ReadLine();
        Console.Write("Descrição da meta: ");
        string description = Console.ReadLine();
        Console.Write("Pontuação da meta: ");
        int points;
        if (!int.TryParse(Console.ReadLine(), out points))
        {
            Console.WriteLine("Valor inválido para a pontuação. Tente novamente.");
            return;
        }

        EternalGoal eternalGoal = new EternalGoal(shortName, description, points);
        _goals.Add(eternalGoal);
        Console.WriteLine("Meta Eterna criada com sucesso!");
    }

    private void CreateChecklistGoal()
    {
        Console.Write("Nome da meta: ");
        string shortName = Console.ReadLine();
        Console.Write("Descrição da meta: ");
        string description = Console.ReadLine();
        Console.Write("Pontuação da meta: ");
        int points;
        if (!int.TryParse(Console.ReadLine(), out points))
        {
            Console.WriteLine("Valor inválido para a pontuação. Tente novamente.");
            return;
        }
        Console.Write("Informe a meta alvo para a Meta de Checklist: ");
        int target;
        if (!int.TryParse(Console.ReadLine(), out target))
        {
            Console.WriteLine("Valor inválido para a meta alvo. Tente novamente.");
            return;
        }
        Console.Write("Informe o bônus para a Meta de Checklist: ");
        int bonus;
        if (!int.TryParse(Console.ReadLine(), out bonus))
        {
            Console.WriteLine("Valor inválido para o bônus. Tente novamente.");
            return;
        }

        ChecklistGoal checklistGoal = new ChecklistGoal(shortName, description, points, target, bonus, this);
        _goals.Add(checklistGoal);
        Console.WriteLine("Meta de Checklist criada com sucesso!");
    }

    public void RecordEventMenu()
    {
        Console.Write("Digite o nome da meta: ");
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
            Console.WriteLine($"Evento registrado para a meta: {goalName}");
            Console.WriteLine($"Pontuação atualizada: {_score}");
        }
        else
        {
            Console.WriteLine("Meta não encontrada.");
        }
    }

    public void SaveGoalsMenu()
    {
        Console.Write("Digite o nome do arquivo para salvar as metas: ");
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

                    // Se a meta for uma meta de checklist, escreva os detalhes adicionais
                    if (goal is ChecklistGoal checklistGoal)
                    {
                        writer.WriteLine($"{checklistGoal.Target},{checklistGoal.Bonus},{checklistGoal.AmountCompleted}");
                    }
                }
            }
            Console.WriteLine("Metas salvas com sucesso!");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Arquivo não encontrado: {fileName}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Erro ao salvar metas: {ex.Message}");
        }
    }

    public void LoadGoalsMenu()
    {
        Console.Write("Digite o nome do arquivo para carregar as metas: ");
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
                            throw new ArgumentException($"Tipo de meta desconhecido: {typeName}");
                    }

                    _goals.Add(goal);
                }
            }
            Console.WriteLine("Metas carregadas com sucesso!");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Arquivo não encontrado: {fileName}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Erro ao carregar metas: {ex.Message}");
        }
    }

    public void UpdateScore(int scoreDelta)
    {
        _score += scoreDelta;
    }
}
