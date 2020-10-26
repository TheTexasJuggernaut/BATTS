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
        //Create a list object 10/18/20
        public List<UserDataModel> LoginDB = new List<UserDataModel>();   
                       
              
        public UserDataModel User { get; set; }

        LoginViewModel LVM;

        //public UserDataModel Items = new UserDataModel();

        public LoginPage()
        {
            InitializeComponent();
       
            BindingContext = LVM = new LoginViewModel();
            LVM.Title = "Login Page";
        }

      

        async public void DoLogin(object sender, EventArgs e)
        {
            //10/20/20 Repairs
            bool IsVerified = await LVM.LoginCheckAsync(email.Text, pwd.Text);
            if (IsVerified)
            {
                string sessionID = await LVM.GetUserIDAsync(email.Text);
                await Navigation.PushModalAsync(new NavigationPage(new Menu(sessionID)));
            }
            else
            {
                notify.Text = "Wrong Credentials, Please try again. If new to BATSS please Register";
            }

        }
     
        async public void DoRegister(object sender, EventArgs e)
        {

            //  await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            //Items = new ObservableCollection<UserDataModel>();
            await Navigation.PushModalAsync(new NavigationPage(new Register()));

           // User.Email = email.Text;
           // User.Password = pwd.Text;

          //  await AzuCosmoDBManager.InsertUserData(User);
         //   notify.Text = "New User Registered";
         //   await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));

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