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
using System.Diagnostics;

namespace BATTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerInfo : ContentPage
    {
        #region Declarations
        public List<PlayerDataModel> playerDBRegister = new List<PlayerDataModel>();
        public PlayerDataModel playerRegister = new PlayerDataModel();

        string sessionID, playerExist;
        bool playerCreated;
        private static byte[] inputBytes, hash;

        PlayersViewModel PVM;
        private static StringBuilder sb = new StringBuilder();
        private static MD5 md5 = MD5.Create();
        #endregion

        public PlayerInfo(string sessionids)
        {
            InitializeComponent();
            sessionID = sessionids;
            BindingContext = PVM = new PlayersViewModel(sessionids);
            PVM.Title = "Create Player Page";
        }

        #region Functions
        /// <summary>
        /// Creates a unique hash ID for any string provided
        /// </summary>
        /// <returns></returns>
        public static string ConvertStringtoMD5(string strword)
        {
            inputBytes = System.Text.Encoding.ASCII.GetBytes(strword);
            hash = md5.ComputeHash(inputBytes);

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

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
        /// If a player does not exist in Database it will create a unique player ID
        /// </summary>
        /// <returns></returns>
        public void setPlayerID(string inputFlag)
        {
            if (inputFlag == "NPI")
            {
                playerRegister.Id = ConvertStringtoMD5(playerRegister.FirstName + playerRegister.LastName + DateTime.Now.ToString());
            }
            else
            {
                Debug.WriteLine("GetPlayerID returned False");
            }
        }

        /// <summary>
        /// Pull data from GUI
        /// </summary>
        /// <returns></returns>
        public void inputFromGUI()
        {
            try
            {
                playerRegister.FirstName = firstname.Text.ToString();
                playerRegister.LastName = lastname.Text.ToString();
                playerRegister.Role = Position.Text.ToString();
                playerRegister.ActiveUser = true;
                playerRegister.Id = ConvertStringtoMD5(playerRegister.FirstName + playerRegister.LastName);
            }
            catch
            {
                Debug.WriteLine("Pull Conversion from GUI Failed");
            }
        }

        #endregion

        #region Event Trigger Functions
        async public void doRegister(object sender, EventArgs e)
        {
            if (!inputValid(firstname.Text) && !inputValid(lastname.Text) && !inputValid(Position.Text))
            {
                try
                {
                    inputFromGUI();

                    playerExist = await PVM.GetPlayerIDAsync(playerRegister.Id);

                    setPlayerID(playerExist);                   

                    playerCreated = await PVM.CreateNewPlayerAsync(playerRegister, true);


                    if (playerCreated)
                    {
                        updateNotification("Player has been created. PlayerID:" + playerRegister.Id, 3);

                    }
                    else
                    {
                        updateNotification("This player is already created", 2);
                    }

                }
                catch
                {
                    Debug.WriteLine("Register Failed");
                }
            }
            else
            {
                updateNotification("Please enter a valid entry", 2);
                firstname.Text = null;
                lastname.Text = null;
                Position.Text = null;
            }

        }

        async public void goBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Menu(sessionID)));

        }
        #endregion

    }
}