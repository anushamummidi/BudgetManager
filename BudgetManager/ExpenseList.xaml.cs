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
    public partial class ExpenseList : ContentPage
    {
        public ExpenseList()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var myexpenses = new List<Expense>();
            var files = Directory.EnumerateFiles(App.Folderpath, "*.MonthlyExpenses.txt");
            
            foreach (var filename in files)
            {
                string expense = File.ReadAllText(filename);
                // expense will be like this:   "Costco groceries, $234.5, 3/3/2020, 2"
                string[] contentsOfExpense = expense.Split(new char[] {','});
                
                ExpenseCategory category = ExpenseCategory.Other;

                switch (contentsOfExpense[3])
                {
                    case "0": category = ExpenseCategory.Travel; break;
                    case "1": category = ExpenseCategory.Clothing; break;
                    case "2": category = ExpenseCategory.Groceries; break;
                    case "3": category = ExpenseCategory.Education; break;
                    case "4": category = ExpenseCategory.Children; break;
                }

                myexpenses.Add(new Expense
                {
                    Text = contentsOfExpense[0],
                    Amount = contentsOfExpense[1],
                    //Date = contentsOfExpense[2]
                });
            }

            listView.ItemsSource = myexpenses.OrderBy(e => e.Amount).ToList();
        }

        async void OnExpenseAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExpenseEntry
            {
                BindingContext = new Expense()
            });
        }

        private async void OnListViewItemSelected(object sender,
            SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ExpenseEntry
                {
                    BindingContext = e.SelectedItem as Expense
                });
            }
        }
    }
}