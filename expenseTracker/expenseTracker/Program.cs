using System;

namespace expenseTracker
{
    class Program
    {
        static void Main()
        {
            bool isRunning = true;
            ExpenseRepository repo = new ExpenseRepository(); // link to second file

            while (isRunning)
            {
                Console.WriteLine("\n===== EXPENSES TRACKER=====");
                Console.WriteLine("1. Add Expense");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        repo.AddExpense();
                        break;
                    case "2":
                        repo.ViewExpenses();
                        break;
                    case "3":
                        repo.SaveExpenses();
                        Console.WriteLine("Exiting...");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
