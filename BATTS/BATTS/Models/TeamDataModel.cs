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

        string _locationcountry;
        [JsonProperty("locationcoutry")]
        public string LocationCountry
        { 

            get => _locationcountry;
            set
            {
                if (_locationcountry == value)
                    return;

                _locationcountry = value;

                HandlePropertyChanged();
            }
        }

        string _locationstate;
        [JsonProperty("locationstate")]
        public string LocationState
        {

            get => _locationstate;
            set
            {
                if (_locationstate == value)
                    return;

                _locationstate = value;

                HandlePropertyChanged();
            }
        }

        string _locationpostal;
        [JsonProperty("locationpostal")]
        public string LocationPostal
        {

            get => _locationpostal;
            set
            {
                if (_locationpostal == value)
                    return;

                _locationpostal = value;

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


        bool _teamgender;
        [JsonProperty("teamgender")]
        public bool TeamGender
        {
            get => _teamgender;
            set
            {
                if (_teamgender == value)
                    return;

                _teamgender = value;

                HandlePropertyChanged();
            }
        }

        bool _teamtype;
        [JsonProperty("teamtype")]
        public bool TeamType
        {
            get => _teamtype;
            set
            {
                if (_teamtype == value)
                    return;

                _teamtype = value;

                HandlePropertyChanged();
            }
        }

        bool _teamagegroup;
        [JsonProperty("teamagegroup")]
        public bool TeamAgeGroup
        {
            get => _teamagegroup;
            set
            {
                if (_teamagegroup == value)
                    return;

                _teamagegroup = value;

                HandlePropertyChanged();
            }
        }


        bool _organization;
        [JsonProperty("organization")]
        public bool Organization
        {
            get => _organization;
            set
            {
                if (_organization == value)
                    return;

                _organization = value;

                HandlePropertyChanged();
            }
        }

        bool _leagueorconference;
        [JsonProperty("leagueorconference")]
        public bool LeaguOrConference
        {
            get => _leagueorconference;
            set
            {
                if (_leagueorconference == value)
                    return;

                _leagueorconference = value;

                HandlePropertyChanged();
            }
        }

        bool _division;
        [JsonProperty("division")]
        public bool Division
        {
            get => _division;
            set
            {
                if (_division == value)
                    return;

                _division = value;

                HandlePropertyChanged();
            }
        }

        bool _season;
        [JsonProperty("season")]
        public bool Season
        {
            get => _season;
            set
            {
                if (_season == value)
                    return;

                _season = value;

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
