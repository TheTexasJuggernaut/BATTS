using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;
using BATTS.Droid.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
//using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace BATTS.Droid
{

    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        EditText email;
        EditText password;
        private string newUserEmail;
        private string newUserPass;

        //Shawn Database Add https://www.youtube.com/watch?v=wtpm8OPHx5Q&feature=emb_logo
        //https://www.youtube.com/watch?v=1j9OzWs89oo&feature=emb_logo
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            //Get email & password values from edit text  
            email = FindViewById<EditText>(Resource.Id.txtEmail);
            password = FindViewById<EditText>(Resource.Id.txtPassword);
            //Trigger click event of Login Button  
            var button = FindViewById<Button>(Resource.Id.btlogin);
            var buttonReg = FindViewById<Button>(Resource.Id.btregister);
            
            buttonReg.Click += DoRegister;
            button.Click += DoLogin;

            //Path String for the DB File 
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbTest.db3");
            // Setup the connection 
            var db = new SQLiteConnection(dbPath);

            //Setup a table
            db.CreateTable<UserData>();

            UserData myData = new UserData("Shawn", "2222");

            db.Insert(myData);

        }
        public void DoLogin(object sender, EventArgs e)
        {
            
            if (email.Text == "****@gmail.co" && password.Text == "12345" || email.Text == newUserEmail && password.Text == newUserPass)

            {
                Toast.MakeText(this, "Login successfully done!", ToastLength.Long).Show();
                StartActivity(typeof(ViewActivity));
            }
            else
            {
                Toast.MakeText(this, "Wrong credentials found!", ToastLength.Long).Show();
            }
        }
        public void DoRegister(object sender, EventArgs e)
        {

            newUserEmail = email.Text;
            newUserPass = password.Text;


            
            Toast.MakeText(this, "New User Registered. Please Login!", ToastLength.Long).Show();
        }

       

     

    }
}
