using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace BATTS.Models
{
    public class UserDataModel
    {
       
        public event PropertyChangedEventHandler PropertyChanged;

        #region UserDataModel
        string _id;
        [JsonProperty("id")]
         public string Id
        {
            get => _id;
            set
            {
                if (_id == value)
                    return;

                _id = value;

                HandlePropertyChanged();
            }
        }

        string _firstname;
        [JsonProperty("firstname")]
        public string FirstName
        {
            get => _firstname;
            set
            {
                if (_firstname == value)
                    return;

                _firstname = value;

                HandlePropertyChanged();
            }
        }

        string _lastname;
        [JsonProperty("lastname")]
        public string LastName
        {
            get => _lastname;
            set
            {
                if (_lastname == value)
                    return;

                _lastname = value;

                HandlePropertyChanged();
            }
        }

        string _email;
        [JsonProperty("email")]
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value)
                    return;

                _email = value;

                HandlePropertyChanged();
            }
        }

        string _role;
        [JsonProperty("role")]
        public string Role
        {
            get => _role;
            set
            {
                if (_role == value)
                    return;

                _role = value;

                HandlePropertyChanged();
            }
        }

        string _password;
        [JsonProperty("password")]
        public string Password
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;

                _password = value;

                HandlePropertyChanged();
            }
        }

        bool _activeuser;
        [JsonProperty("activeuser")]
        public bool ActiveUser
        {
            get => _activeuser;
            set
            {
                if (_activeuser == value)
                    return;

                _activeuser = value;

                HandlePropertyChanged();
            }
        }
        #endregion
        //Other Fields unused
        #region unused
        //string _category;
        //[JsonProperty("category")]
        //public string Category
        //{
        //    get => _category;
        //    set
        //    {
        //        if (_category == value)
        //            return;

        //        _category = value;

        //        HandlePropertyChanged();
        //    }
        //}

        //string _description;
        //[JsonProperty("description")]
        //public string Description
        //{
        //    get => _description;
        //    set
        //    {
        //        if (_description == value)
        //            return;

        //        _description = value;

        //        HandlePropertyChanged();
        //    }
        //}

        //bool _completed;
        //[JsonProperty("isComplete")]
        //public bool Completed
        //{
        //    get => _completed;
        //    set
        //    {
        //        if (_completed == value)
        //            return;

        //        _completed = value;

        //        HandlePropertyChanged();
        //    }
        //}
        #endregion
        void HandlePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var eventArgs = new PropertyChangedEventArgs(propertyName);

            PropertyChanged?.Invoke(this, eventArgs);
        }
    }
}