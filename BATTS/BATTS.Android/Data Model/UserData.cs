using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace BATTS.Droid.DataModel
{

    class UserData
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string name;
        public string phonenumber;

        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public UserData()
        {

        }

        public UserData (string Name, string PhoneNumber)
        {
            Name = name;
            PhoneNumber = phonenumber;
        }

        public override string ToString()
        {
            return Name + "" + PhoneNumber;
        }
    }
}