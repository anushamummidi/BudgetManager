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

                string category = "\U0001F4B2"; // General

                switch (contentsOfExpense[3])
                {
                    case "0": category = "\u2708"; break; // Travel
                    case "1": category = "\U0001F457"; break; // Clothing
                    case "2": category = "\U0001F966"; break; // Groceries
                    case "3": category = "\U0001F4DA"; break; // Education
                    case "4": category = "\U0001FA7A"; break; // Health
                }

                myexpenses.Add(new Expense
                {
                    Name = contentsOfExpense[0],
                    Amount = contentsOfExpense[1],
                    Date = contentsOfExpense[2],
                    Category = category
                });
            }

            // Create the ListView.
            ListView expenseListView = new ListView
            {
                // Source of data items.
                ItemsSource = myexpenses.OrderBy(e => e.Amount).ToList(),

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label expenseNameLabel = new Label();
                    expenseNameLabel.SetBinding(Label.TextProperty, "Name");

                    Label amountLabel = new Label();
                    amountLabel.SetBinding(Label.TextProperty, "Amount");

                    Label dateLabel = new Label();
                    dateLabel.SetBinding(Label.TextProperty, "Date");

                    Label categoryLabel = new Label();
                    categoryLabel.FontFamily = "Segoe Color Emoji";
                    categoryLabel.FontSize = 12;
                    categoryLabel.SetBinding(Label.TextProperty, "Category");
                    
                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                                {
                                    categoryLabel,
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        Spacing = 0,
                                        Children =
                                        {
                                            expenseNameLabel,
                                            dateLabel
                                        }
                                    },
                                    amountLabel
                                }
                        }
                    };
                })
            };

            expenseListView.ItemSelected += ExpenseListView_ItemSelected;

            this.Content = new StackLayout
            {
                Children =
                {
                    expenseListView
                }
            };
        }

        async void OnExpenseAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExpenseEntry
            {
                BindingContext = new Expense()
            });
        }

        private async void ExpenseListView_ItemSelected(object sender,
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