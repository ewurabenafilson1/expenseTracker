using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace expenseTracker
{
    public class ExpenseRepository
    {
        private List<Expense> expenses = new List<Expense>();
        private string file = "expenses.json";

        public ExpenseRepository()
        {
            if (File.Exists(file))
            {
                string json = File.ReadAllText(file);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    try
                    {
                        expenses = JsonSerializer.Deserialize<List<Expense>>(json)!;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("JSON ERROR: " + ex.Message);
                    }
                }
            }
        }

        public void AddExpense()
        {
            Console.Write("Enter category: ");
            string category = Console.ReadLine()!;

            Console.Write("Enter description: ");
            string description = Console.ReadLine()!;

            Console.Write("Enter date (yyyy-mm-dd): ");
            string inputDate = Console.ReadLine()!;
            DateTime date = Convert.ToDateTime(inputDate);

            Console.Write("Enter expense amount: ");
            double amount = Convert.ToDouble(Console.ReadLine());

            int expenseID = 0;
            for (int i = 0; i < expenses.Count; i++)
            {
                if (expenses[i].ID > expenseID)
                    expenseID = expenses[i].ID;
            }
            expenseID++;

            Expense expense = new Expense(expenseID, category, description, date, amount);
            expenses.Add(expense);

            Console.WriteLine("\n✅ Expense added successfully!\n");
        }

        public void ViewExpenses()
        {
            Console.WriteLine("\n===EXPENSES===");
            if (expenses.Count == 0)
            {
                Console.WriteLine("No information available!\n");
                return;
            }

            foreach (var expense in expenses)
                Console.WriteLine(expense);
        }

         public void DeleteExpense()
    {
        if (expenses.Count == 0)
        {
            Console.WriteLine("\nNo expenses to delete.\n");
            return;
        }

        Console.Write("\nEnter the ID of the expense to delete: ");
        string idToDelete = Console.ReadLine()!;

        Expense expenseToRemove = expenses.Find(e => e.ID.ToString().Equals(idToDelete, StringComparison.OrdinalIgnoreCase))!;

        if (expenseToRemove != null)
        {
            expenses.Remove(expenseToRemove);
            Console.WriteLine("\n🗑️  Expense deleted successfully!\n");
        }
        else
        {
            Console.WriteLine("\n❌ No expense found with that ID.\n");
        }
    }

  public void UpdateExpense()
{
    if (expenses.Count == 0)
    {
        Console.WriteLine("\nNo expenses to update!\n");
        return;
    }

    Console.Write("Search expense by ID, category or description: ");
    string search = Console.ReadLine()!.ToLower();

    bool found = false;

    foreach (var expense in expenses)
    {
        if (expense.ID.ToString().Contains(search) ||
            expense.Category!.ToLower().Contains(search) ||
            expense.Description!.ToLower().Contains(search))
        {
            Console.WriteLine($"\nFound: ID: {expense.ID}, Category: {expense.Category}, Description: {expense.Description}, Date: {expense.Date}, Amount: {expense.Amount}");
            found = true;

            Console.WriteLine("\nEnter new details (leave blank to keep same value):");

            Console.Write("New category: ");
            string newCategory = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(newCategory))
                expense.Category = newCategory;

            Console.Write("New description: ");
            string newDesc = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(newDesc))
                expense.Description = newDesc;

            Console.Write("New date (yyyy-mm-dd): ");
            string newDate = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(newDate))
                expense.Date = Convert.ToDateTime(newDate);

            Console.Write("New amount: ");
            string newAmount = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(newAmount))
                expense.Amount = Convert.ToDouble(newAmount);

            Console.WriteLine("\n✅ Expense updated successfully!\n");
            break;
        }
    }

    if (!found)
        Console.WriteLine("\n❌ Expense not found!\n");
}


        public void SaveExpenses()
        {
            string json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(file, json);
        }
    }
}
