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
    public partial class Teams : ContentPage

    {
        TeamsViewModel TVM;
        public string sessionID;
        public int remove = 0;
        public int add = 0;
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        //public Teams() { InitializeComponent(); }
        public Teams (string SessionID)
		{
            InitializeComponent();
            sessionID = SessionID;            
            BindingContext = TVM = new TeamsViewModel(sessionID);
            TVM.Title = "Teams Page";

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as TeamDataModel;
            if (item == null)
                return;

          await Navigation.PushAsync(new Players());

            // Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Menu(sessionID)));
        }
        public async void CreateTeam(object sender, EventArgs e)
        {
            await TVM.AddTeamAsync();
            Button ClickedButton = (Button)sender;
            var position = Team.CursorPosition;
            if(Team.Text == null)
            {
                ClickedButton.Text = "No team entered";
            }
            add = 1;
        }
        private  void AddItemsToUi(object sender, EventArgs e)
        {
        // var TeamsDB = await TVM.GetTeamAsync();
         
            
            //Logic for teams from database
            var Teams = new List<string>();
            //Teams.Add(Team.Text);
            Teams.Add("New York Jags");
            Teams.Add("London Robins");
            Teams.Add("Mumbai Wolves");
            Teams.Add("Chicago Bears");

            if(remove == 0 && add == 0)
            {
                if (Team.Text == null)
                {
                    foreach (var item in Teams)
                    {

                        Button CurrentButton = new Button()
                        {
                            Text = item,
                            TextColor = Color.White,
                            StyleId = item

                        };

                        CurrentButton.Clicked += MyButton_Clicked;
                        TeamViews.Children.Add(CurrentButton);

                    }
                }
                else
                {
                    foreach (var item in Teams)
                    {

                        Button CurrentButton = new Button()
                        {
                            Text = item,
                            TextColor = Color.White,
                            StyleId = item

                        };

                        CurrentButton.Clicked += MyButton_Clicked;
                        TeamViews.Children.Add(CurrentButton);

                    }
                }
            }
            if (remove == 1 && add == 0)
            {
                if (Team.Text == null)
                {
                    foreach (var item in Teams)
                    {

                        Button CurrentButton = new Button()
                        {
                            Text = item,
                            TextColor = Color.White,
                            StyleId = item

                        };

                        CurrentButton.Clicked += MyButton_Clicked;
                        TeamViews.Children.Add(CurrentButton);

                    }
                }
                else
                {
                    foreach (var item in Teams)
                    {
                        if (Team.Text == item)
                        {
                            Teams.Remove(Team.Text);
                        }
                    }
                    foreach (var item in Teams)
                    {

                        Button CurrentButton = new Button()
                        {
                            Text = item,
                            TextColor = Color.White,
                            StyleId = item

                        };

                        CurrentButton.Clicked += MyButton_Clicked;
                        TeamViews.Children.Add(CurrentButton);

                    }
                }
            }
            if (remove == 0 && add == 1)
            {
                if (Team.Text == null)
                {
                    foreach (var item in Teams)
                    {

                        Button CurrentButton = new Button()
                        {
                            Text = item,
                            TextColor = Color.White,
                            StyleId = item

                        };

                        CurrentButton.Clicked += MyButton_Clicked;
                        TeamViews.Children.Add(CurrentButton);

                    }
                }
                else
                {
                    Teams.Add(Team.Text);
                    foreach (var item in Teams)
                    {

                        Button CurrentButton = new Button()
                        {
                            Text = item,
                            TextColor = Color.White,
                            StyleId = item

                        };

                        CurrentButton.Clicked += MyButton_Clicked;
                        TeamViews.Children.Add(CurrentButton);

                    }
                }
            }
            if(remove == 1 && add == 1)
            {
                remove = 0;
                add = 0;
            }
            
            

        }

        public void RemoveTeam(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            var position = Team.CursorPosition;
            if (Team.Text == null)
            {
                ClickedButton.Text = "No team entered";
            }
            remove = 1;
        }
        async private void MyButton_Clicked(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            ClickedButton.Text = "You clicked team:" + ClickedButton.StyleId;
            await Navigation.PushModalAsync(new NavigationPage(new Players()));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (TVM.TeamDB.Count == 0)
            {
                //TVM.RefreshCommand.Execute(null);
                TVM.LoadItemsCommand.Execute(null);
            }
        }

    }
}