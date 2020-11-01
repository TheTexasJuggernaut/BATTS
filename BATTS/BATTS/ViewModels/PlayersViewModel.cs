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
    class PlayersViewModel : BaseViewModel
    {
        public List<PlayerDataModel> playerDB;
        public List<PlayerDataModel> PlayerDB { get => playerDB; set => SetProperty(ref playerDB, value); }

        public string sessionID;
        public PlayerDataModel Player = new PlayerDataModel();
        
        public Command LoadItemsCommand { get; set; }

        public PlayersViewModel(string SessionID)
        {
            Title = "Browse";
            sessionID = SessionID;

            PlayerDB = new List<PlayerDataModel>();

            // RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            LoadItemsCommand = new Command(async () => await GetPlayersAsync());


           

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // TeamDB.Clear();
                var items = await AzuCosmoDBManager.GetPlayerData(sessionID);
                foreach (var item in items)
                {
                    PlayerDB.Add(item);
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
        public async Task<PlayerDataModel> GetPlayersAsync()
        {


            try
            {
                PlayerDB = await AzuCosmoDBManager.GetPlayerData(sessionID);

                if (PlayerDB.Exists(x => x.TeamID== sessionID))
                {
                    var Player = PlayerDB.Where(p => p.TeamID == sessionID).First();
                    //foreach (var item in Player)
                    //{
                    //    PlayerDB.Add(item);
                    //}
                    // if (LoginDB.Exists(x => x.Password == password))//Need to ensure it is only for that Data Table
                    return Player;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }


        }

        public async Task<PlayerDataModel> AddPlayerToTeamAsync(string teamId, string playerID)
        {
            PlayerDB = await AzuCosmoDBManager.GetPlayerDataByID(playerID);
            if (PlayerDB.Exists(x => x.Id == playerID))
            {
                var Data = PlayerDB.Where(p => p.Id == playerID).First();
                // Player.FirstName = Data.FirstName;
                // Player.TeamID = Data.TeamID;
                Data.TeamID = teamId;
                Data.Id = playerID;
               // Player.Id = Data.Id;
                try
                {
                    await AzuCosmoDBManager.UpdatePlayerData(Data);

                    return Player;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return null;
                }
                
            }
            else
            {
                return null;
            }
     
        }

        public async Task<bool> CreateNewPlayerAsync(PlayerDataModel player, bool IsNew)
        {
            Player = player;
            Player.CoachID = sessionID;
            try
            {
                if (IsNew)
                {
                    await AzuCosmoDBManager.InsertPlayerData(Player);
                }
                else
                {
                    await AzuCosmoDBManager.UpdatePlayerData(Player);
                }
                return true;
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
               
            }
        

        }

        public async Task<bool> RemovePlayerFromTeamAsync(string playerID)
        {
            PlayerDB = await AzuCosmoDBManager.GetPlayerDataByID(playerID);
            if (PlayerDB.Exists(x => x.Id == playerID))
            {
                var Data = PlayerDB.Where(p => p.Id == playerID).First();
                // Player.FirstName = Data.FirstName;
                // Player.TeamID = Data.TeamID;
                Data.TeamID = "";
                Data.Id = playerID;
                // Player.Id = Data.Id;
                try
                {
                    await AzuCosmoDBManager.UpdatePlayerData(Data);

                    return true;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }

            }
            else
            {
                return false;
            }

        }    

        public async Task<string> GetPlayerIDAsync(string playerID)
        {
            // email = Email;


            try
            {
                PlayerDB = await AzuCosmoDBManager.GetPlayerData(sessionID);

                if (PlayerDB.Exists(x => x.Id == playerID))
                {
                    var Player = PlayerDB.Where(p => p.Id == playerID).First();
                    if (Player.TeamID== playerID)
                    {
                        return Player.Id;

                    }
                    else
                    {
                        return "NPI";
                    }
                    // if (LoginDB.Exists(x => x.Password == password))//Need to ensure it is only for that Data Table

                    //return User.Id;
                }
                else
                {
                    return "NPI";
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "";
            }


        }
        // TeamDB.Clear();
    }
}
