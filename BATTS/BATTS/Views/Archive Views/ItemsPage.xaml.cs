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

namespace BATTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {

        ItemsViewModel vm;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = vm = new ItemsViewModel();
            vm.Title = "To Do Items";
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as UserDataModel;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item, true)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
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

        

        protected async void listItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var todoItem = e.SelectedItem as UserDataModel;

            if (todoItem == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(todoItem, false));
        }

        protected async void AddNewClicked(object sender, EventArgs e)
        {
            var toDo = new UserDataModel();
            var todoPage = new ItemDetailPage(toDo, true);

            await Navigation.PushModalAsync(new NavigationPage(todoPage));
        }
    }
}