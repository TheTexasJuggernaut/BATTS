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
using System.Diagnostics;

namespace BATTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Players : ContentPage
    {
        #region Declarations
        PlayersViewModel PVM;
        public string sessionID, teamID;
        PlayerDataModel player = new PlayerDataModel();
        #endregion


        public Players(string TeamId, string SessionID)
        {
            InitializeComponent();
            sessionID = SessionID;
            teamID = TeamId;
            BindingContext = PVM = new PlayersViewModel(teamID);
            PVM.Title = "Teams Page";

        }

        #region Event Trigger Functions
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

                player.Id = PlayerID.Text.ToString();
                await PVM.AddPlayerToTeamAsync(teamID, player.Id);
                PVM.LoadItemsCommand.Execute(null);
                Button ClickedButton = (Button)sender;

            }
        }
        public async void RemovePlayer(object sender, EventArgs e)
        {

            try
            {
                string playerid = PlayerID.Text.ToString();
                string playerretrun = await PVM.GetPlayerIDAsync(teamID);
                //if (playerid == "NTE")
                //{
                //    // Notify.Text = "No Team Found";
                //}
                //else if (playerid == "NCE")
                //{
                //    // Notify.Text = "No City Found for Team";
                //}
                //else
                //{

                //}
                bool worked = await PVM.RemovePlayerFromTeamAsync(playerid);
                PVM.LoadItemsCommand.Execute(null);

            }
            catch
            {
                Debug.WriteLine("Remove failed");
                //Notify.Text = "Failed to remove team, try again";
            }
            Button ClickedButton = (Button)sender;

        }
        #endregion

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