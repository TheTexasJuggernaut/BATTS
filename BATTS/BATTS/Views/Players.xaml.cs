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
using System.Windows.Input;

namespace BATTS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Players : ContentPage
	{
        PlayersViewModel PVM;
        public string sessionID;
        PlayerDataModel Player= new PlayerDataModel();
        public int remove = 0;
        public int add = 0;
        public Players (string SessionID)
		{
            InitializeComponent();
            sessionID = SessionID;
            BindingContext = PVM = new PlayersViewModel(sessionID);
            PVM.Title = "Teams Page";

        }
        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Teams("test")));
        }
        private void AddItemsToUi(object sender, EventArgs e)
        {
            PVM.LoadItemsCommand.Execute(null);

            var players = new List<string>();
           // players.Add("John J");
          //  players.Add("Johnathan Robins");
          //  players.Add("Hunter Woes");
         //   players.Add("Chase Chear");
            
            //if (remove == 0 && add == 0)
            //{
            //    if (Player.Text == null)
            //    {
            //        foreach (var item in players)
            //        {

            //            Button CurrentButton = new Button()
            //            {
            //                Text = item,
            //                TextColor = Color.White,
            //                StyleId = item

            //            };

            //            CurrentButton.Clicked += MyButton_Clicked;
            //            PlayersViews.Children.Add(CurrentButton);

            //        }
            //    }
            //    else
            //    {
            //        foreach (var item in players)
            //        {

            //            Button CurrentButton = new Button()
            //            {
            //                Text = item,
            //                TextColor = Color.White,
            //                StyleId = item

            //            };

            //            CurrentButton.Clicked += MyButton_Clicked;
            //            PlayersViews.Children.Add(CurrentButton);

            //        }
            //    }
            //}
            //if (remove == 1 && add == 0)
            //{
            //    if (Player.Text == null)
            //    {
            //        foreach (var item in players)
            //        {

            //            Button CurrentButton = new Button()
            //            {
            //                Text = item,
            //                TextColor = Color.White,
            //                StyleId = item

            //            };

            //            CurrentButton.Clicked += MyButton_Clicked;
            //            PlayersViews.Children.Add(CurrentButton);

            //        }
            //    }
            //    else
            //    {
            //        foreach (var item in players)
            //        {
            //            if (Player.Text == item)
            //            {
            //                players.Remove(Player.Text);
            //            }
            //        }
            //        foreach (var item in players)
            //        {

            //            Button CurrentButton = new Button()
            //            {
            //                Text = item,
            //                TextColor = Color.White,
            //                StyleId = item

            //            };

            //            CurrentButton.Clicked += MyButton_Clicked;
            //            PlayersViews.Children.Add(CurrentButton);

            //        }
            //    }
            //}
            //if (remove == 0 && add == 1)
            //{
            //    if (Player.Text == null)
            //    {
            //        foreach (var item in players)
            //        {

            //            Button CurrentButton = new Button()
            //            {
            //                Text = item,
            //                TextColor = Color.White,
            //                StyleId = item

            //            };

            //            CurrentButton.Clicked += MyButton_Clicked;
            //            PlayersViews.Children.Add(CurrentButton);

            //        }
            //    }
            //    else
            //    {
            //        players.Add(Player.Text);
            //        foreach (var item in players)
            //        {

            //            Button CurrentButton = new Button()
            //            {
            //                Text = item,
            //                TextColor = Color.White,
            //                StyleId = item

            //            };

            //            CurrentButton.Clicked += MyButton_Clicked;
            //            PlayersViews.Children.Add(CurrentButton);

            //        }
            //    }
            //}
            //if (remove == 1 && add == 1)
            //{
            //    remove = 0;
            //    add = 0;
            //}


        }
        private async void MyButton_Clicked(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            await Navigation.PushModalAsync(new NavigationPage(new PlayerInfo()));
            ClickedButton.Text = "Player Stats: (PlaceHolder)";
        }
        public async void AddPlayer(object sender, EventArgs e)
        {
            try
            {
                Player.Id = PlayerID.Text.ToString();
                
            }
            catch
            {

            }
                       
            await PVM.AddPlayerToTeamAsync(sessionID, Player.Id);
            PVM.LoadItemsCommand.Execute(null);
            Button ClickedButton = (Button)sender;
           // var position = Player.CursorPosition;
          //  add = 1;
        }
        public async void RemovePlayer(object sender, EventArgs e)
        {

            try
            {
                // TeamName.Text;
                string playerid = PlayerID.Text.ToString();
                string playerretrun = await PVM.GetPlayerIDAsync(sessionID);
                if (playerid == "NTE")
                {
                   // Notify.Text = "No Team Found";
                }
                if (playerid == "NCE")
                {
                   // Notify.Text = "No City Found for Team";
                }
                bool worked = await PVM.RemovePlayerFromTeamAsync(playerid);
                PVM.LoadItemsCommand.Execute(null);

            }
            catch
            {
                //Notify.Text = "Failed to remove team, try again";
            }
            Button ClickedButton = (Button)sender;
            //var position = Player.CursorPosition;
            //if (Player.Text.Insert(position, "player") == null)
            //{
            //    ClickedButton.Text = "No player entered";
            //}
            //remove = 1;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (PVM.PlayerDB.Count == 0)
            {
                //TVM.RefreshCommand.Execute(null);
                PVM.LoadItemsCommand.Execute(null);
            }
        }
    }
}