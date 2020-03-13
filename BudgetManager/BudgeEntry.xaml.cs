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

            string[] files = Directory.GetFiles(App.Folderpath, "MonthlyBudget.txt");

            if (files.Length != 0)
            {
                filename = files[0];
                string budget = File.ReadAllText(filename);
                editor.Text = budget;

                //Navigation.PushAsync(new ExpenseList());
            }
            else
            {
                editor.Text = string.Empty;
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