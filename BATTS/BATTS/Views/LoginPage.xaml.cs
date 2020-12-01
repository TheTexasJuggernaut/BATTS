using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BATTS.Models;
using BATTS.Views;
using BATTS.ViewModels;
using BATTS.Services;


using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;

namespace BATTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        #region Declarations       
        LoginViewModel LVM;
        private bool isVerified, verifyResponse;
        private string sessionID, sessionIDResponse;
        #endregion

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = LVM = new LoginViewModel();
            LVM.Title = "Login Page";
        }

        #region Functions
      
        /// <summary>
        /// Controls the Notify Text on GUI, 2 inputs String and Int(1: Orange 2: Red 3: Green)
        /// </summary>
        /// <returns></returns>
        public void updateNotification(string statusInput, int statusType)
        {
            switch (statusType)
            {
                case 1:
                    notify.TextColor = Color.Orange;
                    break;
                case 2:
                    notify.TextColor = Color.Red;
                    break;
                case 3:
                    notify.TextColor = Color.Green;
                    break;
                default:
                    Debug.WriteLine("Recieved a unnacepted case"); ;
                    break;

            }

            notify.Text = statusInput;
        }
     
        /// <summary>
        /// Checks to see if user entris not a null or blank, returns true if null or blank is found
        /// </summary>
        /// <returns></returns>
        public static bool inputValid(string input)
        {
            return String.IsNullOrWhiteSpace(input);
        }
        /// <summary>
        /// Controls the navigation and page you want to travel too and takes session ID as well
        /// </summary>
        /// <returns></returns>
        async public void navigationTo(string iDInput, string page)
        {
            if (page == "MENU")
            {
                // 11/1/20 : S.A. Update this code to utilize the user type to change what they have access to prior to loggin to the application
                await Navigation.PushModalAsync(new NavigationPage(new Menu(iDInput)));
            }
            else if (page == "REGISTER")
            {
                await Navigation.PushModalAsync(new NavigationPage(new Register()));
            }
            else
            {
                Debug.WriteLine("No Valid Inputs");
            }
        }
        /// <summary>
        /// Using the users email pulls the unique ID for user from database
        /// </summary>
        /// <returns></returns>
        async public Task<string> getUserID()
        {
            sessionID = await LVM.GetUserIDAsync(email.Text);
            return sessionID;
        }
        /// <summary>
        /// Checks to see if the users email exists
        /// </summary>
        /// <returns></returns>
        async public Task<bool> verifyEmail()
        {
            isVerified = await LVM.LoginCheckAsync(email.Text, pwd.Text);
            return isVerified;
        }
        #endregion

        #region EvenTriggerFunctions
        async public void doLogin(object sender, EventArgs e)
        {
            if (!inputValid(email.Text) && !inputValid(pwd.Text))
            {
                verifyResponse = await verifyEmail();

                if (verifyResponse)
                {
                    sessionIDResponse = await getUserID();
                    navigationTo(sessionIDResponse, "MENU");
                }
                else
                {
                    updateNotification("Wrong Credentials, Please try again. If new to BATSS please Register", 1);
                }
            }
            else
            {
                updateNotification("Please enter a valid email or password", 2);
            }

        }

        public void doRegister(object sender, EventArgs e)
        {
            navigationTo(sessionIDResponse, "REGISTER");

        }
        #endregion
        
        //Unused function 
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (vm.Items.Count == 0)
            //{
            //  //  vm.RefreshCommand.Execute(null);
            // //   vm.LoadItemsCommand.Execute(null);

            //}
        }


    }
}