using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BATTS.Models
{
    public class TeamDataModel
    {

        public event PropertyChangedEventHandler PropertyChanged;
        #region TeamDataModel
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

        string _teamname;
        [JsonProperty("teamname")]
        public string TeamName
        {
            get => _teamname;
            set
            {
                if (_teamname == value)
                    return;

                _teamname = value;

                HandlePropertyChanged();
            }
        }

        string _locationcity;
        [JsonProperty("locationcity")]
        public string LocationCity
        {
            get => _locationcity;
            set
            {
                if (_locationcity == value)
                    return;

                _locationcity = value;

                HandlePropertyChanged();
            }
        }

        string _ownerid;
        [JsonProperty("ownerid")]
        public string OwnerID
        {
            get => _ownerid;
            set
            {
                if (_ownerid == value)
                    return;

                _ownerid = value;

                HandlePropertyChanged();
            }
        }

        bool _activeteam;
        [JsonProperty("activeteam")]
        public bool ActiveTeam
        {
            get => _activeteam;
            set
            {
                if (_activeteam == value)
                    return;

                _activeteam = value;

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
