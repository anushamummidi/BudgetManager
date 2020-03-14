using BudgetManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetManager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetEntry : ContentPage
    {
        private string filename;
        public BudgetEntry()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            double budget = 0.0;
            string[] files = Directory.GetFiles(App.Folderpath, "MonthlyBudget.txt");

            if (files.Length != 0)
            {
                filename = files[0];
                string budgetFromFile = File.ReadAllText(filename);
                editor.Text = budgetFromFile;
                budget = Double.Parse(budgetFromFile);
                //Navigation.PushAsync(new ExpenseList());
            }
            else
            {
                editor.Text = string.Empty;
            }


            string[] expenseFiles = Directory.GetFiles(App.Folderpath, "*.MonthlyExpenses.txt");
            double expenses = 0.0;
            foreach (string file in expenseFiles)
            {
                string expenseFileText = File.ReadAllText(file);

                string[] expenseContent = expenseFileText.Split(new char[] { ',' });
                if (!string.IsNullOrEmpty(expenseContent[1]))
                {
                    expenses += Double.Parse(expenseContent[1]);
                }
            }

            ExpenseTotalLabel.Text = "$" + expenses.ToString();

            double balance = budget - expenses;
            if (balance < 0.0)
            {
                BalanceLabel.Text = "-$" + Math.Abs(balance).ToString();
            }
            else
            { 
                BalanceLabel.Text = "$" + balance.ToString(); 
            }
        }

        async void OnSaveButtonClicked(object sender,EventArgs e)
        {
            if (!editor.Text.Equals(string.Empty))
            {
                Budget budget = new Budget();
                if (string.IsNullOrWhiteSpace(budget.Filename))
                {
                    filename = Path.Combine(App.Folderpath, "MonthlyBudget.txt");

                    budget.Filename = filename;
                    budget.Amount = editor.Text;

                    File.WriteAllText(filename, editor.Text);
                }
                else
                {
                    budget.Amount = editor.Text;
                    File.WriteAllText(budget.Filename, editor.Text);
                }

                await Navigation.PushAsync(new ExpenseList());
            }
        }

        private void OnCancelButtonClicked(object sender,EventArgs e)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            editor.Text = string.Empty;
        }

    }
}