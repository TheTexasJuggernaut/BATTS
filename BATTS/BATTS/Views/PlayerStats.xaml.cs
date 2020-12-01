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
    public partial class PlayerStats : ContentPage
    {

        #region Declarations
        public bool gameexist, dataSaved;
        public string sessionID, playerID, teamID, game;
        GameViewModel GVM;
        GameModel Game = new GameModel();
        #endregion

        public PlayerStats(string teamid, string playerid, string SessionID)
        {
            InitializeComponent();
            sessionID = SessionID;
            teamID = teamid;
            playerID = playerid;
            BindingContext = GVM = new GameViewModel(playerID, sessionID);
            GVM.Title = "Teams Page";
        }

        #region Functions
        /// <summary>
        /// Controls the Notify Text on GUI, 2 inputs String and Int(1: Orange 2: Red 3: Green)
        /// </summary>
        /// <returns></returns>
        public void updateNotification(string statusInput, int statusType)
        {
            switch (statusType)
            {
                case 1:
                    notify.TextColor = Color.Orange;
                    break;
                case 2:
                    notify.TextColor = Color.Red;
                    break;
                case 3:
                    notify.TextColor = Color.Green;
                    break;
                default:
                    Debug.WriteLine("Recieved a unnacepted case"); ;
                    break;

            }

            notify.Text = statusInput;
        }

        /// <summary>
        /// Checks to see if user entris not a null or blank, returns true if null or blank is found
        /// </summary>
        /// <returns></returns>
        public static bool inputValid(string input)
        {
            return String.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Pull data from GUI
        /// </summary>
        /// <returns></returns>
        public void inputFromGUI()
        {
            try
            {

                Game.GameId = GameID.Text;
                Game.PlayerIDs = playerID;
                Game.AttemptedHits = Int32.Parse(AttemptedHits.Text);
                Game.Hits = Int32.Parse(Hits.Text);
                Game.Runs = Int32.Parse(Runs.Text);
                Game.Strikes = Int32.Parse(Strikes.Text);
                Double BattingAVG = Convert.ToDouble(Game.Hits / Game.AttemptedHits);

            }
            catch
            {
                Debug.WriteLine("Data Pull Failed"); 
            }
        }

        /// <summary>
        /// Null all entries
        /// </summary>
        /// <returns></returns>
        public void inputNULLGUI()
        {
            try
            {

                GameID.Text = null;
                AttemptedHits.Text = null;
                Hits.Text = null;
                Strikes.Text = null;
                Runs.Text = null;

            }
            catch
            {
                Debug.WriteLine("Null Input Failed");
            }
        }

        /// <summary>
        /// Updates Game Data Model 
        /// </summary>
        /// <returns></returns>
        public void updateGame(string game)
        {
            Game.Id = game;
            gameexist = true;

            if (game == "GEPDNE")
            {

                gameexist = false;
                Game.Id = "";
            }
            else if (game == "GDNE")
            {
                gameexist = false;
                Game.Id = "";
            }
            else
            {
                Debug.WriteLine("Game not found error");
            }
        }
        #endregion

        #region Event Trigger Functions
        async public void saveData(object sender, EventArgs e)
        {
            if (!inputValid(GameID.Text) && !inputValid(AttemptedHits.Text) && !inputValid(Hits.Text) && !inputValid(Runs.Text) && !inputValid(Strikes.Text))
            {
                inputFromGUI();
                game = await GVM.GetGameIDAsync(Game.GameId, Game.PlayerIDs);
                updateGame(game);

                dataSaved = await GVM.AddGameAsync(Game, gameexist);

                if (dataSaved)
                {
                    updateNotification("Data Saved", 3);                    
                }
                else
                {
                    updateNotification("Data Failed", 2);                   
                }

                GVM.LoadItemsCommand.Execute(null);
            }
            else
            {
                updateNotification("Enter Valid Entry", 2);
                inputNULLGUI();
            }
               
        }
        async public void goBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Players(teamID, sessionID)));
        }
        #endregion

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (GVM.GameDB.Count == 0)
            {

                GVM.LoadItemsCommand.Execute(null);
            }

        }
    }
}