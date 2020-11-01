using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BATTS.Models;
using BATTS.Views;
using BATTS.ViewModels;
using BATTS.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BATTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerInfo : ContentPage
    {

        public List<PlayerDataModel> PlayerDBRegister = new List<PlayerDataModel>();

        public PlayerDataModel PlayerRegister = new PlayerDataModel();
        string SessionID;

        PlayersViewModel PVM;
        public PlayerInfo(string sessionid)
        {
            InitializeComponent();
            SessionID = sessionid;
            BindingContext = PVM = new PlayersViewModel(sessionid);
            PVM.Title = "Create Player Page";
        }

        async public void DoRegister(object sender, EventArgs e)
        {
                       
            if (firstname.Text != "" && firstname.Text != null && firstname.Text != "First Name")
            {

            }
            else
            {

            }
            // bool playerexist = await PVM.GetPlayerIDAsync();
            try
            {
                PlayerRegister.FirstName = firstname.Text.ToString();
                PlayerRegister.LastName = lastname.Text.ToString();
                PlayerRegister.Role = Position.Text.ToString();
                PlayerRegister.ActiveUser = true;
            }
            catch
            {

            }

            bool playercreated = await PVM.CreateNewPlayerAsync(PlayerRegister, true);
            if (playercreated)
            {

                notify.TextColor = Color.Green;
                notify.Text = "Player has been created";

            }
            else
            {
                notify.TextColor = Color.Red;
                notify.Text = "This player is already created";

            }


        }

        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Menu(SessionID)));

        }
    }
}