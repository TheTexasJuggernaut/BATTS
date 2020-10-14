using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BATTS.Models;
using BATTS.ViewModels;

namespace BATTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        Item Item;
        bool IsNew;
        ItemDetailViewModel ViewModel;
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage(Item item, bool isNew)
        {
            InitializeComponent();

            Item = item;
            IsNew = isNew;

            ViewModel = new ItemDetailViewModel(Item, IsNew);
            ViewModel.SaveComplete += Handle_SaveComplete;

            BindingContext = ViewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.SaveComplete -= Handle_SaveComplete;
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