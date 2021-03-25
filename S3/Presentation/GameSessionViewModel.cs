using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tbQuest.Models;
using tbQuest;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows;

namespace tbQuest.Presentation
{
    public class GameSessionViewModel : ObservableObject
    {
        #region FIELDS

        private bool _lightsOn;
        private List<string> _messages;
        private DateTime _gameStartTime;
        private string _gameTimeDisplay;
        private TimeSpan _gameTime;

        private Player _player;

        private Map _gameMap;
        private Location _currentLocation;
        private Location _northLocation;
        private Location _southLocation;

        private GameItem _currentGameItem;

        #endregion FIELDS

        #region PROPERTIES

        public bool LightsOn
        {
            get { return _lightsOn; }
            set { _lightsOn = value; }
        }

        public string Timer
        {
            get { return _gameTimeDisplay; }
            set
            {
                _gameTimeDisplay = value;
                OnPropertyChanged(nameof(Timer));
            }
        }

        public string MessageDisplay
        {
            get { return FormatMessagesForViewer(); }
        }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public Map GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
            }
        }

        public Location NorthLocation
        {
            get { return _northLocation; }
            set
            {
                _northLocation = value;
                OnPropertyChanged(nameof(NorthLocation));
                OnPropertyChanged(nameof(HasNorthLocation));
            }
        }

        public Location SouthLocation
        {
            get { return _southLocation; }
            set
            {
                _southLocation = value;
                OnPropertyChanged(nameof(SouthLocation));
                OnPropertyChanged(nameof(HasSouthLocation));
            }
        }

        public GameItem CurrentGameItem
        {
            get { return _currentGameItem; }
            set { _currentGameItem = value; }
        }

        public bool HasNorthLocation { get { return NorthLocation != null; } }
        public bool HasSouthLocation { get { return SouthLocation != null; } }

        #endregion PROPERTIES

        #region CONSTRUCTORS

        public GameSessionViewModel()
        {
        }

        public GameSessionViewModel(
            Player player,
            List<string> initialMessages,
            Map gameMap,
            Location currentLocation)
        {
            _player = player;

            _messages = initialMessages;
            _gameMap = gameMap;
            _currentLocation = currentLocation;
            InitializeView();

            GameTimer();
        }

        #endregion CONSTRUCTORS

        #region METHODS

        private void InitializeView()
        {
            _gameStartTime = DateTime.Now;
            UpdateAvailableTravelPoints();
            _player.UpdateInventoryCategories();
        }

        private string FormatMessagesForViewer()
        {
            List<string> lifoMessages = new List<string>();

            for (int index = 0; index < _messages.Count; index++)
            {
                lifoMessages.Add($"<T:{GameTime().ToString(@"mm\:ss\:ms")}>" + _messages[index]);
            }

            lifoMessages.Reverse();

            return string.Join("\n\n", lifoMessages);
        }

        #region TIMER

        public void GameTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnGameTimerTick;
            timer.Start();
        }

        private void OnGameTimerTick(object sender, EventArgs e)
        {
            _gameTime = DateTime.Now - _gameStartTime;
            Timer = _gameTime.ToString(@"mm\:ss");
        }

        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }

        #endregion TIMER

        #region MOVEMENT

        private void UpdateAvailableTravelPoints()
        {
            NorthLocation = null;
            SouthLocation = null;

            if (_gameMap.NorthLocation() != null)
            {
                Location nextNorthLocation = _gameMap.NorthLocation();

                if (nextNorthLocation.Accessible == true)
                {
                    NorthLocation = nextNorthLocation;
                }
            }
            if (_gameMap.SouthLocation() != null)
            {
                Location nextSouthLocation = _gameMap.SouthLocation();

                if (nextSouthLocation.Accessible == true)
                {
                    SouthLocation = nextSouthLocation;
                }
            }
        }

        public void MoveNorth()
        {
            if (HasNorthLocation)
            {
                _gameMap.MoveNorth();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
            }
        }

        public void MoveSouth()
        {
            if (HasSouthLocation)
            {
                _gameMap.MoveSouth();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
            }
        }

        #endregion MOVEMENT

        #region ACTIONS

        public void AddItemToInventory()
        {
            if (_currentGameItem != null && _currentLocation.GameItems.Contains(_currentGameItem))
            {
                GameItem selectedGameItem = _currentGameItem as GameItem;

                _currentLocation.RemoveGameItemFromLocation(selectedGameItem);
                _player.AddGameItemToInventory(selectedGameItem);

                OnPlayerPickUp(selectedGameItem);
            }
        }

        public void RemoveItemFromInventory()
        {
            if (_currentGameItem != null)
            {
                GameItem selectedGameItem = _currentGameItem as GameItem;

                _currentLocation.AddGameItemToLocation(selectedGameItem);
                _player.RemoveGameItemFromInventory(selectedGameItem);

                OnPlayerPutDown(selectedGameItem);
            }
        }

        private void OnPlayerPickUp(GameItem gameItem)
        {
        }

        private void OnPlayerPutDown(GameItem selectedGameItem)
        {
        }

        #endregion ACTIONS

        public void UserCommands(string userCommand)
        {
            if (userCommand.Contains("plant") == true && userCommand.Contains("key") == true)
            {
                CurrentGameItem = _currentLocation.GameItems[1];
                AddItemToInventory();
            }
            else if (userCommand.Contains("lights"))
            {
                LightsOn = true;
            }
        }

        #endregion METHODS
    }
}