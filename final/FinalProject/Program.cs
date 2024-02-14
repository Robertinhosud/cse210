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
        this.valor = valor;
        this.data = data;
        this.categoria = categoria;
    }

    // Métodos
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

    // Método para adicionar uma nova despesa à lista
    public void AdicionarDespesa(Despesa despesa)
    {
        listaDespesas.Add(despesa);
    }

    // Método para retornar a lista de despesas
    public List<Despesa> GetDespesas()
    {
        return listaDespesas;
    }

    // Método para gerar um relatório com os gastos totais por categoria
    public string GerarRelatorio()
    {
        // Aqui você pode implementar a lógica para gerar o relatório
        // Por exemplo, pode percorrer a lista de despesas, calcular os gastos por categoria e formatar o relatório
        // Este é apenas um esboço básico
        return "Relatório de despesas";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Exemplo de uso das classes

        // Criar uma instância de DespesaManager
        DespesaManager manager = new DespesaManager();

        // Adicionar algumas despesas de exemplo
        manager.AdicionarDespesa(new Despesa(50.0, DateTime.Now, "Alimentação"));
        manager.AdicionarDespesa(new Despesa(30.0, DateTime.Now.AddDays(-2), "Transporte"));
        manager.AdicionarDespesa(new Despesa(100.0, DateTime.Now.AddDays(-5), "Lazer"));

        // Obter a lista de despesas e exibir
        List<Despesa> despesas = manager.GetDespesas();
        Console.WriteLine("Lista de despesas:");
        foreach (Despesa despesa in despesas)
        {
            Console.WriteLine($"Valor: {despesa.GetValor()}, Data: {despesa.GetData()}, Categoria: {despesa.GetCategoria()}");
        }

        // Gerar e exibir o relatório de despesas
        string relatorio = manager.GerarRelatorio();
        Console.WriteLine("\nRelatório de despesas:");
        Console.WriteLine(relatorio);
    }
}
