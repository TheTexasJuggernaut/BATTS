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
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbTest.db3");

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

            
            //Conection
            var db = new SQLiteConnection(dbPath);
            //Setup at able
            db.CreateTable<UserData>();

            ////Create a new contact Object 
            //UserData myData = new UserData("Shawn", "281-513-8574");
            





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
            //Create a new contact Object 
            UserData myData = new UserData(newUserEmail, newUserPass);


            var db = new SQLiteConnection(dbPath);
            var table = db.Table<UserData>();
           
            foreach (var item in table)
            {
                UserData myUser = new UserData(item.Name, item.PhoneNumber);
                
                
            }
            
            Toast.MakeText(this, "New User Registered. Please Login!", ToastLength.Long).Show();
        }
    }
}