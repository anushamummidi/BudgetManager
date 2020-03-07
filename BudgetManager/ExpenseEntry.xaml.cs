using System;
using System.Collections.Generic;
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
    }
}