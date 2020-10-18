using System;
using System.Collections.Generic;
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
        public Menu ()
		{
			InitializeComponent ();

        }
        async public void ViewTeams(object sender, EventArgs e)
        {

            // var buttonAddTeam = FindViewById<Button>(Resource.Id.AddTeam);

            // var buttonTeam = FindViewById<Button>(Resource.Id.btviewteams1);
            await Navigation.PushModalAsync(new NavigationPage(new Teams()));

        }
        async public void DoTutorial(object sender, EventArgs e)
        {
            // Toast.MakeText(this, "Please View the manual for a a tutorial on the BATSS App", ToastLength.Long).Show();
            //SetContentView(Resource.Layout.Tutorial);
            await Navigation.PushModalAsync(new NavigationPage(new TutorialPage()));
        }
        async public void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }
    }
    
}