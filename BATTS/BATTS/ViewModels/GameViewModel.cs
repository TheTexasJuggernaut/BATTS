using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Generic;
using Xamarin.Forms;
using BATTS.Models;
using BATTS.Views;
using BATTS.Services;
using System.Linq;

namespace BATTS.ViewModels
{
    class GameViewModel : BaseViewModel
    {
        public List<GameModel> gameDB;
        public List<GameModel> GameDB { get => gameDB; set => SetProperty(ref gameDB, value); }

        public GameModel Team = new GameModel();

        public string sessionID, playerID;
        public Command LoadItemsCommand { get; set; }

        public GameViewModel(string PlayerID, string SessionID)
        {
            Title = "Browse";
            sessionID = SessionID;
            playerID = PlayerID;

            GameDB = new List<GameModel>();

            // RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            LoadItemsCommand = new Command(async () => await GetGameIDByPlayerIdAsync(playerID));
            //TeamDB.Clear();

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // TeamDB.Clear();
                var items = new List<GameModel>();
                items = await AzuCosmoDBManager.GetGameDataByPlayer(playerID);
                foreach (var item in items)
                {
                    // if (item.OwnerID == sessionID) {
                    GameDB.Add(item); //}
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task<bool> GetGameAsync()
        {


            try
            {
                // var items = await AzuCosmoDBManager.GetTeamData();
                GameDB = await AzuCosmoDBManager.GetGameData(sessionID);

                //if (TeamDB.Exists(x => x.OwnerID == sessionID))
                // {

                // var User = TeamDB.Where(p => p.OwnerID == sessionID).ToList();

                // if (LoginDB.Exists(x => x.Password == password))//Need to ensure it is only for that Data Table
                return true;
                // }
                // else
                // {
                //  return false;
                //   }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }


        }

        public async Task<bool> AddGameAsync(GameModel Game, bool gameexists)
        {

            try
            {

                if (!gameexists)
                    await AzuCosmoDBManager.InsertGameData(Game);
                else
                    await AzuCosmoDBManager.UpdateGameData(Game);




                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }


        }

        public async Task<bool> DeleteGameAsync(string teamid)
        {

            Team.Id = teamid;

            try
            {
                await AzuCosmoDBManager.DeleteGameData(Team);

                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }


        }

        public async Task<string> GetGameIDAsync(string gameID, string playerID)
        {
            // email = Email;


            try
            {
                GameDB = await AzuCosmoDBManager.GetGameData(gameID);

                if (GameDB.Exists(x => x.GameId == gameID))
                {
                    var Game = GameDB.Where(p => p.PlayerIDs == playerID);
                    foreach (var s in Game)
                    {
                        if (s.GameId == gameID)
                        {
                            return s.Id;

                        }
                    }
                    return "GEPDNE";
                    //Player already exists

                    // if (LoginDB.Exists(x => x.Password == password))//Need to ensure it is only for that Data Table

                    //return User.Id;
                }
                else
                {
                    return "GDNE";
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "";
            }


        }

        public async Task<bool> GetGameIDByPlayerIdAsync(string playerID)
        {



            try
            {
                GameDB = await AzuCosmoDBManager.GetGameDataByPlayer(playerID);
                GameDB.Sort();

                //
                var teamAverageScores =
                 from player in GameDB
                    group player by player.Runs into playerGroup
                      select new
                     {
                          GameId = playerGroup.Key,
                          AverageScore = playerGroup.Average(x => x.Runs),
                     };
                //
                     return true;


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }


        }


    }
}
