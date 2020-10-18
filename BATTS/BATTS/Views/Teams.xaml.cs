using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


using BATTS.Models;


namespace BATTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Teams : ContentPage

    {
        Button button;
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<string> menuItems = new List<string>();
        public Teams ()
		{
			InitializeComponent ();

            menuItems.Add("Example Team");
            //TeamViews.ItemsSource = menuItems;
            //ListView.ItemsSource = menuItems;
            //ListViewMenu.SelectedItem = menuItems[0];
            //ListViewMenu.ItemSelected += async (sender, e) =>
            //{
             //   if (e.SelectedItem == null)
              //      return;

              //  var id = (int)((HomeMenuItem)e.SelectedItem).Id;
               // await RootPage.NavigateFromMenu(id);
           // };

        }
        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Menu()));
        }
        private void AddItemsToUi(object sender, EventArgs e)
        {
            var cities = new List<string>();
            cities.Add("New York");
            cities.Add("London");
            cities.Add("Mumbai");
            cities.Add("Chicago");
            foreach (var item in cities)
            {
                button = new Button
                {
                    Text = item,
                    TextColor = Color.White
                };
                button.Clicked += Button_Clicked;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = (sender as Button);
            button.BackgroundColor = Color.Red;
        }
        protected override void OnDisappearing()
        {
            button.Clicked -= Button_Clicked;
            base.OnDisappearing();
        }
    }
}