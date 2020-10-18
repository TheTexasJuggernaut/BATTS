using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BATTS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Players : ContentPage
	{
		public Players ()
		{
			InitializeComponent ();
            
        }
        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Teams()));
        }
        private void AddItemsToUi(object sender, EventArgs e)
        {



            //var position = Coach.CursorPosition;
            //Coach.Text = Coach.Text.Insert(position, "Coach");
            //Logic for teams from database
            var players = new List<string>();
            players.Add("John J");
            players.Add("Johnathan Robins");
            players.Add("Hunter Woes");
            players.Add("Chase Chear");
            foreach (var item in players)
            {

                Button CurrentButton = new Button()
                {
                    Text = item,
                    TextColor = Color.White,
                    StyleId = item

                };

                CurrentButton.Clicked += MyButton_Clicked;
                PlayersViews.Children.Add(CurrentButton);

            }

        }
        private void MyButton_Clicked(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            ClickedButton.Text = "Player Stats: (PlaceHolder)";
        }
    }
}