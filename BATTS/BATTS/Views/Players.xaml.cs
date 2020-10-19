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
        public int remove = 0;
        public int add = 0;
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


            var players = new List<string>();
            players.Add("John J");
            players.Add("Johnathan Robins");
            players.Add("Hunter Woes");
            players.Add("Chase Chear");
            
            if (remove == 0 && add == 0)
            {
                if (Player.Text == null)
                {
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
                else
                {
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
            }
            if (remove == 1 && add == 0)
            {
                if (Player.Text == null)
                {
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
                else
                {
                    foreach (var item in players)
                    {
                        if (Player.Text == item)
                        {
                            players.Remove(Player.Text);
                        }
                    }
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
            }
            if (remove == 0 && add == 1)
            {
                if (Player.Text == null)
                {
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
                else
                {
                    players.Add(Player.Text);
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
            }
            if (remove == 1 && add == 1)
            {
                remove = 0;
                add = 0;
            }


        }
        private void MyButton_Clicked(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            ClickedButton.Text = "Player Stats: (PlaceHolder)";
        }
        public void CreatePlayer(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            var position = Player.CursorPosition;
            add = 1;
        }
        public void RemovePlayer(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            var position = Player.CursorPosition;
            if (Player.Text.Insert(position, "player") == null)
            {
                ClickedButton.Text = "No player entered";
            }
            remove = 1;
        }
    }
}