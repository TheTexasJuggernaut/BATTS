using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BATTS.Models
{
    public class GameModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region GameDataModel
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

        string _gameid;
        [JsonProperty("gameid")]
        public string GameId
        {
            get => _gameid;
            set
            {
                if (_gameid == value)
                    return;

                _gameid = value;

                HandlePropertyChanged();
            }
        }
        string _winloss;
        [JsonProperty("winloss")]
        public string WinLoss
        {
            get => _winloss;
            set
            {
                if (_winloss == value)
                    return;

                _winloss = value;

                HandlePropertyChanged();
            }
        }
        
        string _playerids;
        [JsonProperty("playerids")]
        public string PlayerIDs
        {
            get => _playerids;
            set
            {
                if (_playerids == value)
                    return;

                _playerids = value;

                HandlePropertyChanged();
            }
        }
        
        int _hits;
        [JsonProperty("hits")]
        public int Hits
        {
            get => _hits;
            set
            {
                if (_hits == value)
                    return;

                _hits = value;

                HandlePropertyChanged();
            }
        }

        int _attemptedhits;
        [JsonProperty("attemptedhits")]
        public int AttemptedHits
        {
            get => _attemptedhits;
            set
            {
                if (_attemptedhits == value)
                    return;

                _attemptedhits = value;

                HandlePropertyChanged();
            }
        }

        int _strikes;
        [JsonProperty("strikes")]
        public int Strikes
        {
            get => _strikes;
            set
            {
                if (_strikes == value)
                    return;

                _strikes = value;

                HandlePropertyChanged();
            }
        }

        int _runs;
        [JsonProperty("runs")]
        public int Runs
        {
            get => _runs;
            set
            {
                if (_runs == value)
                    return;

                _runs = value;

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
