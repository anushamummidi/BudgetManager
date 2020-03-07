using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetManager
{
    public partial class App : Application
    {
        public static string Folderpath { get; set; }

        public App()
        {
            InitializeComponent();
            Folderpath = Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData);

            MainPage = new NavigationPage(new BudgetEntry());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
