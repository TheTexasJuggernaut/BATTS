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

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = LVM = new LoginViewModel();
            LVM.Title = "Login Page";
        }

        async public void DoLogin(object sender, EventArgs e)
        {

            bool IsVerified = await LVM.LoginCheckAsync(email.Text, pwd.Text);
            if (IsVerified)
            {
                string sessionID = await LVM.GetUserIDAsync(email.Text);
                // 11/1/20 : S.A. Update this code to utilize the user type to change what they have access to prior to loggin to the application
                await Navigation.PushModalAsync(new NavigationPage(new Menu(sessionID)));
            }
            else
            {
                notify.Text = "Wrong Credentials, Please try again. If new to BATSS please Register";
            }

        }

        async public void DoRegister(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Register()));

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