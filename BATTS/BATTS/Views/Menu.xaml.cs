using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BATTS.Views
{   // <Image Source="Baseball.jpg" Aspect="AspectFill" AbsoluteLayout.LayoutBounds="1,1,1,1" AbsoluteLayout.LayoutFlags="All" />


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        #region Declarations
        string sessionID;
        #endregion

        public Menu(string sessionIDs)
        {
            InitializeComponent();
            sessionID = sessionIDs;
        }

        #region Functions
        /// <summary>
        /// Controls the navigation and page you want to travel too and takes session ID as well
        /// </summary>
        /// <returns></returns>
        async public void navigationTo(string iDInput, string page)
        {
            if (page == "TEAMS")
            {
                await Navigation.PushModalAsync(new NavigationPage(new Teams(sessionID)));
            }
            else if (page == "TUTORIALS")
            {
                await Navigation.PushModalAsync(new NavigationPage(new TutorialPage(sessionID)));
            }
            else if (page == "PLAYERSINFO")
            {
                await Navigation.PushModalAsync(new NavigationPage(new PlayerInfo(sessionID)));
            }
            else if (page == "LOGIN")
            {
                await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
            }
            else
            {
                Debug.WriteLine("No Valid Inputs");
            }
        }
        #endregion

        #region Event Trigger Functions
        public void viewTeams(object sender, EventArgs e)
        {
            navigationTo(sessionID, "TEAMS");          

        }

        public void doTutorial(object sender, EventArgs e)
        {
            navigationTo(sessionID, "TUTORIALS");
        }

        public void createPlayer(object sender, EventArgs e)
        {

            navigationTo(sessionID, "PLAYERSINFO");
        }

        public void goBack(object sender, EventArgs e)
        {
            navigationTo(sessionID, "LOGIN");
        }
        #endregion

    }

}