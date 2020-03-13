using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetManager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseEntry : ContentPage
    {
        public ExpenseEntry()
        {
            InitializeComponent();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            string expenseName = ExpenseEditor.Text;
            string expenseValue = AmountEditor.Text;
            string dateTimeOfExpense = ExpenseDatePicker.Date.Date.ToString();
            string category = "5";
            if (CategoryPicker.SelectedIndex >= 0)
            {
                category = CategoryPicker.SelectedIndex.ToString();
            }

            string filename = Path.Combine(App.Folderpath,
                        $"{Path.GetRandomFileName()}.MonthlyExpenses.txt");

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(expenseName + ",");
            stringBuilder.Append(expenseValue + ",");
            stringBuilder.Append(dateTimeOfExpense + ",");
            stringBuilder.Append(category);

            File.WriteAllText(filename, stringBuilder.ToString());

            await Navigation.PopAsync();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}