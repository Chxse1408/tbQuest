using System;
using System.Collections.Generic;
using tbQuest.Models;

namespace tbQuest.Presentation
{
    public class GameSessionViewModel : ObservableObject
    {
        #region FIELDS

        private List<string> _messages;
        private DateTime _gameStartTime;

        private Player _player;

        private Map _gameMap;
        private Location _currentLocation;

        #endregion FIELDS

        #region PROPERTIES

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
        }

        #endregion CONSTRUCTORS

        #region METHODS

        /// <summary>
        /// initial setup of the game session view
        /// </summary>
        private void InitializeView()
        {
            _gameStartTime = DateTime.Now;
        }

        /// <summary>
        /// generates a sting of mission messages with time stamp with most current first
        /// </summary>
        /// <returns>string of formated mission messages</returns>
        private string FormatMessagesForViewer()
        {
            List<string> lifoMessages = new List<string>();

            for (int index = 0; index < _messages.Count; index++)
            {
                lifoMessages.Add($"<T:{GameTime().ToString(@"hh\:mm\:ss")}>" + _messages[index]);
            }

            lifoMessages.Reverse();

            return string.Join("\n\n", lifoMessages);
        }

        /// <summary>
        /// running time of game
        /// </summary>
        /// <returns></returns>
        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }

        //movement
        public void MoveForward()
        {
            _gameMap.MoveForward();
            CurrentLocation = _gameMap.CurrentLocation;
        }

        public void MoveBackward()
        {
            _gameMap.MoveBackward();
            CurrentLocation = _gameMap.CurrentLocation;
        }

        #endregion METHODS
    }
}