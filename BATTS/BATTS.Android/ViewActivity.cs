using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace BATTS.Droid
{
    [Activity(Label = "ViewActivity")]
    public class ViewActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewTask);

            //Trigger click event of Login Button  
            var button = FindViewById<Button>(Resource.Id.bttut);
            button.Click += DoTutorial;
            var button2 = FindViewById<Button>(Resource.Id.btviewteams);
            button2.Click += ViewTask;
        }
        public void ViewTask(object sender, EventArgs e)
        {

            SetContentView(Resource.Layout.ViewTeams);

        }
        public void DoTutorial(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Please View the manual for a a tuturoial on the BATSS App", ToastLength.Long).Show();
            SetContentView(Resource.Layout.Tutorial);
        }
    }
}