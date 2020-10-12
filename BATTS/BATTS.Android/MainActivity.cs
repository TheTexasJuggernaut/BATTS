using System;
using System.Threading;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using BATTS.Views;
using System.IO;
using SQLite;

namespace BATTS.Droid
{
    [Activity(Label = "Activity", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public int LoginPageResource { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            LoginPageResource = Resource.Layout.Login;

            base.OnCreate(savedInstanceState);

            // This MobileServiceClient has been configured to communicate with the Azure Mobile App and
            // Azure Gateway using the application url. You're all set to start working with your Mobile App!
            Microsoft.WindowsAzure.MobileServices.MobileServiceClient BATSSClient = new Microsoft.WindowsAzure.MobileServices.MobileServiceClient(
            "https://batss.azurewebsites.net");

            // This MobileServiceClient has been configured to communicate with the Azure Mobile App and
            // Azure Gateway using the application url. You're all set to start working with your Mobile App!
            Microsoft.WindowsAzure.MobileServices.MobileServiceClient website20201011091435Client = new Microsoft.WindowsAzure.MobileServices.MobileServiceClient(
            "https://website20201011091435.azurewebsites.net");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            StartActivity(typeof(LoginActivity));
            //StartActivity(typeof(ViewTeams));

            //Shawn Database Add https://www.youtube.com/watch?v=wtpm8OPHx5Q&feature=emb_logo
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbTest.db3");

            //Conection
            var db = new SQLiteConnection(dbPath);
            //Setup at able
            db.CreateTable<UserData>();

            ////Create a new contact Object 
            //UserData myData = new UserData("Shawn", "281-513-8574");
            ////var db = new SQLiteConnection(dbPath);
            //var table = db.Table<UserData>();

            //foreach (var item in table)
            //{
            //    UserData myUser = new UserData(item.Name, item.PhoneNumber);


            //}

        }

        
    }
}