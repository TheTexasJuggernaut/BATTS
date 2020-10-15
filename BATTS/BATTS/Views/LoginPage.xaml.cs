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

namespace BATTS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{


       
        //public LoginPage ()
        //{
        //	InitializeComponent ();
        //}

        //

        LoginViewModel vm;


        public LoginPage()
        {
            InitializeComponent();

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



        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            // vm.PushData.Execute(null);

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



        //protected async void listItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var todoItem = e.SelectedItem as UserDataModel;

        //    if (todoItem == null)
        //        return;

        //    await Navigation.PushAsync(new ItemDetailPage(todoItem, false));
        //}

        //protected async void AddNewClicked(object sender, EventArgs e)
        //{
        //    var toDo = new Item();
        //    var todoPage = new ItemDetailPage(toDo, true);

        //    await Navigation.PushModalAsync(new NavigationPage(todoPage));
        //}
    }
}