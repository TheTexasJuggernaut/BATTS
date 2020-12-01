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
    public partial class Teams : ContentPage

    {
        #region Declarations
        TeamsViewModel TVM;
        public string sessionID;
        TeamDataModel Team = new TeamDataModel();
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        #endregion

        //public Teams() { InitializeComponent(); }

        public Teams(string SessionID)
        {
            InitializeComponent();
            sessionID = SessionID;
            BindingContext = TVM = new TeamsViewModel(sessionID);
            TVM.Title = "Teams Page";

        }

        #region Event Trigger Functions
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var team = args.SelectedItem as TeamDataModel;
            if (team == null)
                return;

            await Navigation.PushAsync(new Players(team.Id, sessionID));

            // Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Menu(sessionID)));
        }
        public async void CreateTeam(object sender, EventArgs e)
        {

            bool check = true;
            if (check == (String.IsNullOrWhiteSpace(TeamName.Text) || String.IsNullOrWhiteSpace(TeamCity.Text)))
            {
                Notify.Text = "Please fill in all data entries";
                Notify.TextColor = Color.Red;
                TeamName.Text = null;
                TeamCity.Text = null;
               
            }
            else
            {
                try
                {
                    Team.TeamName = TeamName.Text.ToString();
                    Team.LocationCity = TeamCity.Text.ToString();
                }
                catch
                {
                    Debug.WriteLine("Error");
                }

           
                await TVM.AddTeamAsync(TeamCity.Text, TeamName.Text);
                TVM.LoadItemsCommand.Execute(null);
                Button ClickedButton = (Button)sender;

              
            }
        }
        private void AddItemsToUi(object sender, EventArgs e)
        {           
            TVM.LoadItemsCommand.Execute(null);

            //Logic for teams from database
            var Teams = new List<string>();
           
        }
        public async void RemoveTeam(object sender, EventArgs e)
        {
            try
            {
                // TeamName.Text;
                string teamid = await TVM.GetTeamIDAsync(TeamName.Text, TeamCity.Text);
                if (teamid == "NTE")
                {
                    Notify.Text = "No Team Found";
                }
                if (teamid == "NCE")
                {
                    Notify.Text = "No City Found for Team";
                }
                bool worked = await TVM.DeleteTeamAsync(teamid);


            }
            catch
            {
                Notify.Text = "Failed to remove team, try again";
            }
            TVM.LoadItemsCommand.Execute(null);
            
        }
        #endregion
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (TVM.TeamDB.Count == 0)
            {

                TVM.LoadItemsCommand.Execute(null);
            }
            ItemsListView.HeightRequest = 10;
        }

    }
}