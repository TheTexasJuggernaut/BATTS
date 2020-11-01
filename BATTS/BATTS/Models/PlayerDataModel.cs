using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BATTS.Models
{
    public class PlayerDataModel
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

        string _teamID;
        [JsonProperty("teamid")]
        public string TeamID
        {
            get => _teamID;
            set
            {
                if (_teamID == value)
                    return;

                _teamID = value;

                HandlePropertyChanged();
            }
        }
        string _coachID;
        [JsonProperty("coachID")]
        public string CoachID
        {
            get => _coachID;
            set
            {
                if (_coachID == value)
                    return;

                _coachID = value;

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

        string[] _gameIDs;
        [JsonProperty("gameIDs")]
        public string[] GameIDs
        {
            get => _gameIDs;
            set
            {
                if (_gameIDs == value)
                    return;

                _gameIDs = value;

                HandlePropertyChanged();
            }
        }
        #endregion
        void HandlePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var eventArgs = new PropertyChangedEventArgs(propertyName);

            PropertyChanged?.Invoke(this, eventArgs);
        }
    }
}
