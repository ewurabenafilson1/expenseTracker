using System;

namespace expenseTracker
{
    public class Expense
    {
        public int ID { get; private set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

        public Expense() { } // for serialization

        public Expense(int id, string category, string description, DateTime date, double amount)
        {
            ID = id;
            Category = category;
            Description = description;
            Date = date;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"Unique Identifier: {ID} | Category: {Category} | Description: {Description} | Date: {Date.ToShortDateString()} | Amount: {Amount}";
        }
    }
}
