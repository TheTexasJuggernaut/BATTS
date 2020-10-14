using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATTS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BATTS.Models;


namespace BATTS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompletedItemsPage : ContentPage
	{

        UserDataModel Item;
        bool IsNew;
       // ItemDetailViewModel ViewModel;
        //ItemDetailViewModel viewModel;
        CompletedItemViewModel viewModel;
        //CompletedItemViewModel ViewModel;

        public CompletedItemsPage(CompletedItemViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public CompletedItemsPage(UserDataModel item, bool isNew)
        {
            InitializeComponent();

            Item = item;
            IsNew = isNew;

            viewModel = new CompletedItemViewModel(Item, IsNew);
            viewModel.SaveComplete += Handle_SaveComplete;

            BindingContext = viewModel;
        }

        public CompletedItemsPage()
        {
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    viewModel.RefreshCommand.Execute(null);
        //}
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            viewModel.SaveComplete -= Handle_SaveComplete;
        }

        async void Handle_SaveComplete(object sender, EventArgs e)
        {
            await DismissPage();
        }

        protected async void Handle_CancelClicked(object sender, EventArgs e)
        {
            await DismissPage();
        }

        async Task DismissPage()
        {
            if (IsNew)
                await Navigation.PopModalAsync();
            else
                await Navigation.PopAsync();
        }
    }
}