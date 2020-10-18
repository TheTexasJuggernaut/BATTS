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



namespace BATTS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        //public ObservableCollection<UserDataModel> Items { get; set; }

        Entry Email;
        Entry Password;

        private string newUserEmail;
        private string newUserPass;
        UserDataModel Test = new UserDataModel();

        //public LoginPage ()
        //{
        //	InitializeComponent ();
        //}

        //
        public UserDataModel User { get; set; }

        LoginViewModel vm;

        //public UserDataModel Items = new UserDataModel();

        public LoginPage()
        {
            InitializeComponent();
            User = new UserDataModel
            {
                FirstName = DateTime.Today.ToShortDateString(),
                LastName = DateTime.Today.ToShortTimeString(),
                Email = "Shawn",
                Password = "2"
            };

            BindingContext = vm = new LoginViewModel();
            vm.Title = "Login Page";
        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    var item = args.SelectedItem as UserDataModel;
        //    if (item == null)
        //        return;

        //    await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item, true)));

        //    // Manually deselect item.
        //    ItemsListView.SelectedItem = null;
        //}

        async public void DoLogin(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            // if (email.Text == "****@gmail.co" && password.Text == "12345" || email.Text == newUserEmail && password.Text == newUserPass)
            //
            /// {
            //     Toast.MakeText(this, "Login successfully done!", ToastLength.Long).Show();
            //      StartActivity(typeof(ViewActivity));
            // }
            //  else
            //  {
            //      Toast.MakeText(this, "Wrong credentials found!", ToastLength.Long).Show();
            //  }
            var getUser = await AzuCosmoDBManager.GetUserData();
            string xx = "xx";
            for (int x =0; x <= 100; ++x)
            {
                string xWx = "xx";
            }
            await Navigation.PushModalAsync(new NavigationPage(new Menu()));

        }
        async public void DoRegister(object sender, EventArgs e)
        {
            
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            //Items = new ObservableCollection<UserDataModel>();


            User.Email = email.Text;
            User.Password = pwd.Text;

            await AzuCosmoDBManager.InsertUserData(User);
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
       

        }
        async void NewTest(object sender, EventArgs e)
        {
            try
            {
                await AzuCosmoDBManager.InsertUserData(User);
                await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
            }
            catch
            {

            }
    

        }
        async void OnButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (vm.Items.Count == 0)
            {
                vm.RefreshCommand.Execute(null);
                vm.LoadItemsCommand.Execute(null);
               
            }
        }

        
    }
}