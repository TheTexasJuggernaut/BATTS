using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BATTS.Services;
using BATTS.Views;
using BATTS.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BATTS
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //public static string AzureBackendUrl = "https://batssdb.azurewebsites.net";
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();

            
            //Navigation to MainPage
            MainPage = new MainPage();// --> Bypass

            //MainPage = new LoginPage(); 
        }

         protected override void OnStart()
        {
           
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
