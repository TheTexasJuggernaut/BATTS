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

namespace BATTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel LVM;
        private bool isVerified, verifyResponse;
        private string sessionID, sessionIDResponse;
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = LVM = new LoginViewModel();
            LVM.Title = "Login Page";
        }

        // <UpdateNotification>
        /// <summary>
        /// Takes two inputs (string text you want to send & the integer control) and controls the XAML Notify Text Output
        /// 1 : Caution
        /// 2 : Warning
        /// 3 : Good Input 
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
                default:
                    notify.TextColor = Color.Green;
                    break;

            }
            
            notify.Text = statusInput;
        }

        public static bool inputValid(string input)
        {
            return String.IsNullOrWhiteSpace(input);
        }
       
        async public void navigationTo(string iDInput, string page)
        {
            if (page == "MENU")
            {
                // 11/1/20 : S.A. Update this code to utilize the user type to change what they have access to prior to loggin to the application
                await Navigation.PushModalAsync(new NavigationPage(new Menu(iDInput)));
            }
            if(page == "REGISTER")
            {
                await Navigation.PushModalAsync(new NavigationPage(new Register()));
            }
        }

        async public Task<string> getUserID()
        {
            sessionID = await LVM.GetUserIDAsync(email.Text);
            return sessionID;
        }

        async public Task<bool> verifyEmail()
        {
            isVerified = await LVM.LoginCheckAsync(email.Text, pwd.Text);
            return isVerified;
        }

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