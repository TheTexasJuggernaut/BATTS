using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATTS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BATTS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompletedItemsPage : ContentPage
	{
        CompletedItemViewModel ViewModel;
        public CompletedItemsPage()
        {
            InitializeComponent();

            ViewModel = new CompletedItemViewModel();
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.RefreshCommand.Execute(null);
        }
    }
}