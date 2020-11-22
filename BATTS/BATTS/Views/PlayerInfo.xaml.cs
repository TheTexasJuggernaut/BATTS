using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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

        public static string ConvertStringtoMD5(string strword)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(strword);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        async public void DoRegister(object sender, EventArgs e)
        {
            bool check = true;
            if (check == (String.IsNullOrWhiteSpace(firstname.Text) || String.IsNullOrWhiteSpace(lastname.Text) || String.IsNullOrWhiteSpace(Position.Text)))
            {
                notify.Text = "Please fill in all data entries";
                notify.TextColor = Color.Red;
                firstname.Text = null;
                lastname.Text = null;
                Position.Text = null;
            }
            else
            {
                try
                {

                    PlayerRegister.FirstName = firstname.Text.ToString();
                    PlayerRegister.LastName = lastname.Text.ToString();
                    PlayerRegister.Role = Position.Text.ToString();
                    PlayerRegister.ActiveUser = true;
                    PlayerRegister.Id = ConvertStringtoMD5(PlayerRegister.FirstName + PlayerRegister.LastName);



                    string test = await PVM.GetPlayerIDAsync(PlayerRegister.Id);



                    if (test == "NPI")
                    {
                        PlayerRegister.Id = ConvertStringtoMD5(PlayerRegister.FirstName + PlayerRegister.LastName + DateTime.Now.ToString());
                    }
                }
                catch
                {

                }

                bool playercreated = await PVM.CreateNewPlayerAsync(PlayerRegister, true);
                if (playercreated)
                {

                    notify.TextColor = Color.Green;
                    notify.Text = "Player has been created. PlayerID:" + PlayerRegister.Id;

                }
                else
                {
                    notify.TextColor = Color.Red;
                    notify.Text = "This player is already created";

                }
            }




        }

        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Menu(SessionID)));

        }
    }
}