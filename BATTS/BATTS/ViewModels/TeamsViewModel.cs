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
    class TeamsViewModel : BaseViewModel
    {

        public List<TeamDataModel> teamDB;
        public List<TeamDataModel> TeamDB { get => teamDB; set => SetProperty(ref teamDB, value); }

        public TeamDataModel Team = new TeamDataModel();

        public string sessionID;
        public Command LoadItemsCommand { get; set; }

        public TeamsViewModel(string SessionID)
        {
            Title = "Browse";
            sessionID = SessionID;

            TeamDB = new List<TeamDataModel>();

            // RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            LoadItemsCommand = new Command(async () => await GetTeamAsync());
            // TeamDB.Clear();

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // TeamDB.Clear();
                var items = new List<TeamDataModel>();
                 items = await AzuCosmoDBManager.GetTeamData();
                foreach (var item in items)
                {
                   // if (item.OwnerID == sessionID) {
                        TeamDB.Add(item); //}
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
        public async Task<bool> GetTeamAsync()
        {


            try
            {
               // var items = await AzuCosmoDBManager.GetTeamData();
                TeamDB = await AzuCosmoDBManager.GetTeamDataByID(sessionID);

                //if (TeamDB.Exists(x => x.OwnerID == sessionID))
               // {
                   
                   // var User = TeamDB.Where(p => p.OwnerID == sessionID).ToList();
                    
                    // if (LoginDB.Exists(x => x.Password == password))//Need to ensure it is only for that Data Table
                    return true ;
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

        public async Task<TeamDataModel> AddTeamAsync(string location, string name)
        {
            //Temp Fix
            Team.OwnerID = sessionID;
            Team.TeamName = name;
            Team.LocationCity = location;
            Team.ActiveTeam = true;



            try
            {
                await AzuCosmoDBManager.InsertTeamData(Team);

                return Team;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }


        }

        public async Task<bool> DeleteTeamAsync(string teamid)
        {

            Team.Id = teamid;

            try
            {
                await AzuCosmoDBManager.DeleteTeamData(Team);

                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }


        }

        public async Task<string> GetTeamIDAsync(string name, string city)
        {
            // email = Email;


            try
            {
                TeamDB = await AzuCosmoDBManager.GetTeamData();

                if (TeamDB.Exists(x => x.TeamName == name))
                {
                    var Team = TeamDB.Where(p => p.TeamName == name).First();
                    if (Team.LocationCity == city)
                    {
                        return Team.Id;

                    }
                    else
                    {
                        return "NCE";
                    }
                    // if (LoginDB.Exists(x => x.Password == password))//Need to ensure it is only for that Data Table

                    //return User.Id;
                }
                else
                {
                    return "NTE";
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "";
            }


        }
    }
}
