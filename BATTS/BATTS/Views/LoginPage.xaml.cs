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
        private bool IsVerified, VerifyResponse;
        private string sessionID, SessionIDResponse;
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = LVM = new LoginViewModel();
            LVM.Title = "Login Page";
        }


        public void UpdateNotification(string StatusInput, int StatusType)
        {
            switch (StatusType)
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
            
            notify.Text = StatusInput;
        }
       
        async public void NavigationTo(string IDInput, string page)
        {
            if (page == "MENU")
            {
                // 11/1/20 : S.A. Update this code to utilize the user type to change what they have access to prior to loggin to the application
                await Navigation.PushModalAsync(new NavigationPage(new Menu(sessionID)));
            }
            if(page == "REGISTER")
            {
                await Navigation.PushModalAsync(new NavigationPage(new Register()));
            }
        }
        async public Task<string> GetUserID()
        {
            sessionID = await LVM.GetUserIDAsync(email.Text);
            return sessionID;
        }

        async public Task<bool> VerifyEmail()
        {
            IsVerified = await LVM.LoginCheckAsync(email.Text, pwd.Text);
            return IsVerified;
        }

        async public void DoLogin(object sender, EventArgs e)
        {

            VerifyResponse = await VerifyEmail();

            if (VerifyResponse)
            {
                SessionIDResponse = await GetUserID();
                NavigationTo(SessionIDResponse,"MENU");
            }
            else
            {
                UpdateNotification("Wrong Credentials, Please try again. If new to BATSS please Register", 1);
            }

        }

        public void DoRegister(object sender, EventArgs e)
        {
           NavigationTo(SessionIDResponse, "REGISTER");           

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