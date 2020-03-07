using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManager.Models
{
    public enum ExpenseCategory
    {
        Travel,
        Clothing,
        Groceries,
        Education,
        Children
    };
    public class Expense
    {
        public string Filename { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
        public ExpenseCategory Category { get; set; }
        public string CategoryIconPath { get; set; }

    }

}
