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


/* W06 Prove: Developer—Project Milestone
Name: Roberto Vergilio Soares Junior
This program will help people with how to handle their own money.
So far I've written most of the class codes I had in mind.

Expense: This class represents a single expense, including the amount spent, the date of the expense, and the category to which it belongs.

Attributes:
value: stores the amount spent.
date: stores the date of the expense.
category: stores the category of the expense.
Methods:
getValor(): returns the value of the expense.
getData(): returns the date of the expense.
getCategoria(): returns the category of the expense.
ExpenseManager: This class is responsible for managing the list of expenses, including adding new expenses, retrieving existing expenses, and generating reports on spending.

Attributes:
expenseList: stores the list of expenses.
Methods:
addExpense(expense): adds a new expense to the list.
getExpenses(): returns the list of expenses.
generateReport(): generates a report with total expenses by category.
Here's the class diagram for these two classes:
+------------------+       +------------------+
|     Expense      |       | ExpenseManager   |
+------------------+       +------------------+
| - value: double  |       | - expenseList: List<Expense> |
| - date: DateTime |       +------------------+
| - category: string | 
+------------------+
| + getValor() : double  |
| + getData() : DateTime |
| + getCategory() : string |
+------------------+
              |
              | Contains
              |
              v
          +---------------+
          |   ExpenseManager |
          +---------------+
          | - expenseList: List<Expense> |
          +---------------+
          | + addExpense(expense: Expense) : void |
          | + getExpenses() : List<Expense> |
          | + generateReport() : string |
          +---------------+

The addExpense(expense) method of the ExpenseManager class allows adding new expenses to the expense list.
• The getExpenses() method of the ExpenseManager class allows retrieving the list of expenses.
• The generateReport() method of the ExpenseManager class generates a report with total expenses by category, using the expenses stored in the list.
*/






