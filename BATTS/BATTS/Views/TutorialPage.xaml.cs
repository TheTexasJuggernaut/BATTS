﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BATTS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TutorialPage : ContentPage
	{
        string sessionID;
		public TutorialPage (string SessionID)
		{
            sessionID = SessionID;
			InitializeComponent ();
		}
        async public void GoBack(object sender, EventArgs e)
        {
            // Toast.MakeText(this, "Please View the manual for a a tutorial on the BATSS App", ToastLength.Long).Show();
            //SetContentView(Resource.Layout.Tutorial);
            await Navigation.PushModalAsync(new NavigationPage(new Menu(sessionID)));
        }
    }
}