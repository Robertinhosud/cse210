using System;
using System.Collections.Generic;

public class Expense
{
    // Attributes
    private double amount;
    private DateTime date;
    private string category;

    // Constructor
    public Expense(double amount, DateTime date, string category)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Expense amount must be non-negative.");
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException("Expense category cannot be null or empty.");
        }

        this.amount = amount;
        this.date = date;
        this.category = category;
    }

    // Get methods as before
    public double GetAmount()
    {
        return amount;
    }

    public DateTime GetDate()
    {
        return date;
    }

    public string GetCategory()
    {
        return category;
    }
}

public class ExpenseManager
{
    // Attribute
    private List<Expense> expenseList;

    // Constructor
    public ExpenseManager()
    {
        expenseList = new List<Expense>();
    }

    // Method to calculate the total expenses in a given month
    public double CalculateTotalExpensesByMonth(int year, int month)
    {
        double total = 0.0;

        foreach (Expense expense in expenseList)
        {
            if (expense.GetDate().Year == year && expense.GetDate().Month == month)
            {
                total += expense.GetAmount();
            }
        }

        return total;
    }

    // Method to add a new expense to the list
    public void AddExpense(Expense expense)
    {
        expenseList.Add(expense);
    }

    // Method to edit an existing expense
    public void EditExpense(int index, Expense newExpense)
    {
        if (index >= 0 && index < expenseList.Count)
        {
            expenseList[index] = newExpense;
        }
        else
        {
            throw new IndexOutOfRangeException("Invalid index to edit expense.");
        }
    }

    // Method to delete an existing expense
    public void DeleteExpense(int index)
    {
        if (index >= 0 && index < expenseList.Count)
        {
            expenseList.RemoveAt(index);
        }
        else
        {
            throw new IndexOutOfRangeException("Invalid index to delete expense.");
        }
    }

    // Method to return the list of expenses
    public List<Expense> GetExpenses()
    {
        return expenseList;
    }

    // Method to generate a report with total expenses by category
    public string GenerateReport()
    {
        string report = "Expense Report:\n";
        foreach (Expense expense in expenseList)
        {
            report += $"Date: {expense.GetDate()}, Category: {expense.GetCategory()}, Amount: {expense.GetAmount():C}\n";
        }

        return report;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of ExpenseManager
        ExpenseManager manager = new ExpenseManager();

        // Display menu
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add expense");
            Console.WriteLine("2. Edit expense");
            Console.WriteLine("3. Delete expense");
            Console.WriteLine("4. Display all expenses");
            Console.WriteLine("5. Generate expense report");
            Console.WriteLine("6. Exit");

            // Read user option
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddExpense(manager);
                    break;
                case "2":
                    EditExpense(manager);
                    break;
                case "3":
                    DeleteExpense(manager);
                    break;
                case "4":
                    DisplayAllExpenses(manager);
                    break;
                case "5":
                    GenerateReport(manager);
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void AddExpense(ExpenseManager manager)
    {
        Console.WriteLine("Enter expense amount:");
        double amount = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter expense category:");
        string category = Console.ReadLine();

        Console.WriteLine("Enter expense date (format: dd/mm/yyyy):");
        DateTime date = DateTime.Parse(Console.ReadLine());

        Expense newExpense = new Expense(amount, date, category);
        manager.AddExpense(newExpense);
        Console.WriteLine("Expense added successfully.");
    }

    static void EditExpense(ExpenseManager manager)
    {
        Console.WriteLine("Enter the index of the expense you want to edit:");
        int index = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter the new expense amount:");
        double newAmount = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter the new expense category:");
        string newCategory = Console.ReadLine();

        Console.WriteLine("Enter the new expense date (format: dd/mm/yyyy):");
        DateTime newDate = DateTime.Parse(Console.ReadLine());

        Expense newExpense = new Expense(newAmount, newDate, newCategory);
        manager.EditExpense(index, newExpense);
        Console.WriteLine("Expense edited successfully.");
    }

    static void DeleteExpense(ExpenseManager manager)
    {
        Console.WriteLine("Enter the index of the expense you want to delete:");
        int index = Convert.ToInt32(Console.ReadLine());

        manager.DeleteExpense(index);
        Console.WriteLine("Expense deleted successfully.");
    }

    static void DisplayAllExpenses(ExpenseManager manager)
    {
        List<Expense> expenses = manager.GetExpenses();
        Console.WriteLine("All expenses:");
        foreach (Expense expense in expenses)
        {
            Console.WriteLine($"Date: {expense.GetDate()}, Category: {expense.GetCategory()}, Amount: {expense.GetAmount():C}");
        }
    }

    static void GenerateReport(ExpenseManager manager)
    {
        string report = manager.GenerateReport();
        Console.WriteLine(report);
    }
}
