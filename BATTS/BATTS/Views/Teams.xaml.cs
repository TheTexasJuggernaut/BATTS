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
        public Teams ()
		{
			InitializeComponent ();
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



            //Create Buttons dynamically
            /*for (int i = 0; i < 10; i++)
                {
                    Button CurrentButton = new Button()
                        {
                            Text = "Button " + (i + 1),
                            StyleId = (i + 1).ToString()
                        };

                    CurrentButton.Clicked += MyButton_Clicked;

                    TeamViews.Children.Add(CurrentButton);
                }*/
        }

        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Menu()));
        }
        private void AddItemsToUi(object sender, EventArgs e)
        {

            //Logic for teams from database
            var cities = new List<string>();
            cities.Add("New York Jags");
            cities.Add("London Robins");
            cities.Add("Mumbai Wolves");
            cities.Add("Chicago Bears");
            foreach (var item in cities)
            {
                Button CurrentButton = new Button()
                {
                    Text = item,
                    TextColor = Color.White
                    
                };
                CurrentButton.Clicked += MyButton_Clicked;

                TeamViews.Children.Add(CurrentButton);
               
            }

        }


        async private void MyButton_Clicked(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            await Navigation.PushModalAsync(new NavigationPage(new Players()));
        }


    }
}