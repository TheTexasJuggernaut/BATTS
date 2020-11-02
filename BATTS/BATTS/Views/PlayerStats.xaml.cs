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
    public partial class PlayerStats : ContentPage
    {
        public bool gameexist, datasaved;
        GameViewModel GVM;
        public string sessionID, playerID, teamID;
        GameModel Game = new GameModel();

        public PlayerStats(string teamid, string playerid, string SessionID)
        {
            InitializeComponent();
            sessionID = SessionID;
            teamID = teamid;
            playerID = playerid;
            BindingContext = GVM = new GameViewModel(sessionID);
            GVM.Title = "Teams Page";
        }
        async public void SaveData(object sender, EventArgs e)
        {
            try
            {
               
                Game.GameId = GameID.Text;
                Game.PlayerIDs = playerID;
                Game.AttemptedHits = Int32.Parse(AttemptedHits.Text);
                Game.Hits = Int32.Parse(Hits.Text);
                Game.Runs = Int32.Parse(Runs.Text);
                Game.Strikes = Int32.Parse(Strikes.Text);
                Double BattingAVG = Game.Hits / Game.AttemptedHits;
              
               
            }
            catch
            {

            }
         
            string game = await GVM.GetGameIDAsync(Game.GameId, Game.PlayerIDs);
            Game.Id = game; 
            gameexist = true;
           
            if(game =="GEPDNE")
            {
                
                gameexist = false;
                Game.Id = "";
            }
            if(game == "GDNE")
            {
                gameexist = false;
                Game.Id = "";
            }

             datasaved = await GVM.AddGameAsync(Game, gameexist);

            if (datasaved)
            {
                notify.TextColor = Color.Green;
                notify.Text = "Data Saved";
            }
            else
            {
                notify.TextColor = Color.Red;
                notify.Text = "Data Failed";
            }
            //GVM.LoadItemsCommand.Execute(null);
            //await Navigation.PushModalAsync(new NavigationPage(new Teams(sessionID)));
        }

        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Players(teamID,sessionID)));
        }
    }
}