using System;
using System.Collections.Generic;

public class Despesa
{
    // Atributos
    private double valor;
    private DateTime data;
    private string categoria;

    // Construtor
    public Despesa(double valor, DateTime data, string categoria)
    {
        if (valor < 0)
        {
            throw new ArgumentException("O valor da despesa deve ser não negativo.");
        }

        if (string.IsNullOrWhiteSpace(categoria))
        {
            throw new ArgumentException("A categoria da despesa não pode ser nula ou vazia.");
        }

        this.valor = valor;
        this.data = data;
        this.categoria = categoria;
    }

    // Métodos Get como antes
    public double GetValor()
    {
        return valor;
    }

    public DateTime GetData()
    {
        return data;
    }

    public string GetCategoria()
    {
        return categoria;
    }
}

public class DespesaManager
{
    // Atributo
    private List<Despesa> listaDespesas;

    // Construtor
    public DespesaManager()
    {
        listaDespesas = new List<Despesa>();
    }

    // Método para calcular o total de despesas em um determinado mês
    public double CalcularTotalDespesasPorMes(int ano, int mes)
    {
        double total = 0.0;

        foreach (Despesa despesa in listaDespesas)
        {
            if (despesa.GetData().Year == ano && despesa.GetData().Month == mes)
            {
                total += despesa.GetValor();
            }
        }

        return total;
    }

    // Método para adicionar uma nova despesa à lista
    public void AdicionarDespesa(Despesa despesa)
    {
        listaDespesas.Add(despesa);
    }

    // Método para editar uma despesa existente
    public void EditarDespesa(int indice, Despesa novaDespesa)
    {
        if (indice >= 0 && indice < listaDespesas.Count)
        {
            listaDespesas[indice] = novaDespesa;
        }
        else
        {
            throw new IndexOutOfRangeException("Índice inválido para editar a despesa.");
        }
    }

    // Método para excluir uma despesa existente
    public void ExcluirDespesa(int indice)
    {
        if (indice >= 0 && indice < listaDespesas.Count)
        {
            listaDespesas.RemoveAt(indice);
        }
        else
        {
            throw new IndexOutOfRangeException("Índice inválido para excluir a despesa.");
        }
    }

    // Método para retornar a lista de despesas
    public List<Despesa> GetDespesas()
    {
        return listaDespesas;
    }

    // Método para gerar um relatório com os gastos totais por categoria
    public string GerarRelatorio()
    {
        string relatorio = "Relatório de despesas:\n";
        foreach (Despesa despesa in listaDespesas)
        {
            relatorio += $"Data: {despesa.GetData()}, Categoria: {despesa.GetCategoria()}, Valor: {despesa.GetValor():C}\n";
        }

        return relatorio;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Criar uma instância de DespesaManager
        DespesaManager manager = new DespesaManager();

        // Exibir menu
        bool sair = false;
        while (!sair)
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Adicionar despesa");
            Console.WriteLine("2. Editar despesa");
            Console.WriteLine("3. Excluir despesa");
            Console.WriteLine("4. Exibir todas as despesas");
            Console.WriteLine("5. Gerar relatório de despesas");
            Console.WriteLine("6. Sair");

            // Ler a opção do usuário
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    AdicionarDespesa(manager);
                    break;
                case "2":
                    EditarDespesa(manager);
                    break;
                case "3":
                    ExcluirDespesa(manager);
                    break;
                case "4":
                    ExibirTodasDespesas(manager);
                    break;
                case "5":
                    GerarRelatorio(manager);
                    break;
                case "6":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void AdicionarDespesa(DespesaManager manager)
    {
        Console.WriteLine("Informe o valor da despesa:");
        double valor = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Informe a categoria da despesa:");
        string categoria = Console.ReadLine();

        Console.WriteLine("Informe a data da despesa (formato: dd/mm/aaaa):");
        DateTime data = DateTime.Parse(Console.ReadLine());

        Despesa novaDespesa = new Despesa(valor, data, categoria);
        manager.AdicionarDespesa(novaDespesa);
        Console.WriteLine("Despesa adicionada com sucesso.");
    }

    static void EditarDespesa(DespesaManager manager)
    {
        Console.WriteLine("Informe o índice da despesa que deseja editar:");
        int indice = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Informe o novo valor da despesa:");
        double novoValor = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Informe a nova categoria da despesa:");
        string novaCategoria = Console.ReadLine();

        Console.WriteLine("Informe a nova data da despesa (formato: dd/mm/aaaa):");
        DateTime novaData = DateTime.Parse(Console.ReadLine());

        Despesa novaDespesa = new Despesa(novoValor, novaData, novaCategoria);
        manager.EditarDespesa(indice, novaDespesa);
        Console.WriteLine("Despesa editada com sucesso.");
    }

    static void ExcluirDespesa(DespesaManager manager)
    {
        Console.WriteLine("Informe o índice da despesa que deseja excluir:");
        int indice = Convert.ToInt32(Console.ReadLine());

        manager.ExcluirDespesa(indice);
        Console.WriteLine("Despesa excluída com sucesso.");
    }

    static void ExibirTodasDespesas(DespesaManager manager)
    {
        List<Despesa> despesas = manager.GetDespesas();
        Console.WriteLine("Todas as despesas:");
        foreach (Despesa despesa in despesas)
        {
            Console.WriteLine($"Data: {despesa.GetData()}, Categoria: {despesa.GetCategoria()}, Valor: {despesa.GetValor():C}");
        }
    }

    static void GerarRelatorio(DespesaManager manager)
    {
        string relatorio = manager.GerarRelatorio();
        Console.WriteLine(relatorio);
    }
}
