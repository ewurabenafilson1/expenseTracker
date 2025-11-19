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

        public void SaveExpenses()
        {
            string json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(file, json);
        }
    }
}
