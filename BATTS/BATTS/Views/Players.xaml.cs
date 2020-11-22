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
        public string sessionID, teamID;
        PlayerDataModel Player = new PlayerDataModel();

        public Players(string TeamId, string SessionID)
        {
            InitializeComponent();
            sessionID = SessionID;
            teamID = TeamId;
            BindingContext = PVM = new PlayersViewModel(teamID);
            PVM.Title = "Teams Page";

        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var player = args.SelectedItem as PlayerDataModel;
            if (player == null)
                return;

            await Navigation.PushAsync(new PlayerStats(teamID, player.Id, sessionID));


        }
        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Teams(sessionID)));
        }
        private async void AddItemsToUi(object sender, EventArgs e)
        {
            PVM.LoadItemsCommand.Execute(null);
            //await PVM.CreateNewPlayerAsync();
            var players = new List<string>();



        }
        public async void AddPlayer(object sender, EventArgs e)
        {
            bool check = true;
            if (check == (String.IsNullOrWhiteSpace(PlayerID.Text)))
            {

                PlayerID.Text = null;

            }
            else
            {
                try
                {
                    Player.Id = PlayerID.Text.ToString();

                }
                catch
                {

                }

                await PVM.AddPlayerToTeamAsync(teamID, Player.Id);
                PVM.LoadItemsCommand.Execute(null);
                Button ClickedButton = (Button)sender;
                // var position = Player.CursorPosition;
                //  add = 1;
            }
        }
        public async void RemovePlayer(object sender, EventArgs e)
        {

            try
            {

                string playerid = PlayerID.Text.ToString();
                string playerretrun = await PVM.GetPlayerIDAsync(teamID);
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

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (PVM.PlayerDB.Count == 0)
            {
                
                PVM.LoadItemsCommand.Execute(null);
            }
        }
    }
}