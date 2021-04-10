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
        private Npc _currentNpc;

        private Random random = new Random();

        #endregion FIELDS

        #region PROPERTIES

        public bool LightsOn
        {
            get { return _lightsOn; }
            set
            {
                _lightsOn = value;
                OnPropertyChanged(nameof(LightsOn));
            }
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
            set
            {
                _player = value;
                OnPropertyChanged(nameof(Player));
            }
        }

        public Map GameMap
        {
            get { return _gameMap; }
            set
            {
                _gameMap = value;
                OnPropertyChanged(nameof(GameMap));
            }
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

        public bool HasNorthLocation { get { return NorthLocation != null; } }
        public bool HasSouthLocation { get { return SouthLocation != null; } }

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

        public Npc CurrentNpc
        {
            get { return _currentNpc; }
            set
            {
                _currentNpc = value;
                OnPropertyChanged(nameof(CurrentNpc));
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
                lifoMessages.Add(_messages[index]);
            }

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
                OnPropertyChanged(nameof(Player));
            }
        }

        public void MoveSouth()
        {
            if (HasSouthLocation)
            {
                _gameMap.MoveSouth();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPropertyChanged(nameof(Player));
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

        public void OnPlayerTalkTo()
        {
            if (CurrentNpc != null && CurrentNpc is ISpeak)
            {
                ISpeak speakingNpc = CurrentNpc as ISpeak;
                string message = speakingNpc.Speak();
                _messages.Add(message);
            }
        }

        #endregion ACTIONS

        #region USER COMMANDS

        public void UserCommands(string userCommand)
        {
            #region ITEM INTERACTION

            if (userCommand.Contains("plant") == true && userCommand.Contains("key") == true)
            {
                if (CurrentLocation.Id == 0)
                {
                    _messages.Add(_gameMap.OpenLocationsByKey(0));
                    CurrentGameItem = _currentLocation.GameItems[0];
                    AddItemToInventory();
                    UpdateAvailableTravelPoints();
                    OnPropertyChanged(nameof(MessageDisplay));
                }
                else
                {
                    _messages.Add("That is not in this room.");
                }
            }
            else if (userCommand.Contains("take") == true && userCommand.Contains("knife") == true)
            {
                if (CurrentLocation.Id == 0)
                {
                    _messages.Add(_gameMap.OpenLocationsByKey(1));
                    if (Player.Inventory.Count == 1 || Player.Inventory.Count == 2)
                    {
                        CurrentGameItem = _currentLocation.GameItems[0];
                    }
                    else { CurrentGameItem = _currentLocation.GameItems[1]; }
                    AddItemToInventory();
                    UpdateAvailableTravelPoints();
                    OnPropertyChanged(nameof(MessageDisplay));
                }
                else
                {
                    _messages.Add("That is not in this room.");
                }
            }
            else if (userCommand.Contains("key") == true && userCommand.Contains("couch") == true)
            {
                if (CurrentLocation.Id == 1)
                {
                    _messages.Add(_gameMap.OpenLocationsByKey(0));
                    CurrentGameItem = _currentLocation.GameItems[0];
                    AddItemToInventory();
                    UpdateAvailableTravelPoints();
                    OnPropertyChanged(nameof(MessageDisplay));
                }
                else
                {
                    _messages.Add("That is not in this room.");
                }
            }
            else if (userCommand.Contains("open") == true && userCommand.Contains("lockbox") == true)
            {
                if (CurrentLocation.Id == 0)
                {
                    if (GameMap.Locations[1].GameItems.Count == 0)
                    {
                        _messages.Add("Upon opening the lockbox you find a letter.");
                        OnPropertyChanged(nameof(MessageDisplay));
                    }
                    else
                    {
                        _messages.Add("It is locked.");
                    }
                }
                else
                {
                    _messages.Add("That is not in this room.");
                }
            }
            else if (userCommand.Contains("open") == true && userCommand.Contains("letter") == true)
            {
                if (CurrentLocation.Id == 0)
                {
                    if (Player.Inventory.Count == 3)
                    {
                        _messages.Add("You open the letter and it reads as follows:\n'Hey new bossman!\nI just wanted to welcome you to your new office space! I hope that you find it comfortable. If you need anything, you can call me at my extention 1408. Which also so happens to be the universal code to unlock everthing in the office.\ncheers!\nYour new secretary'");
                        OnPropertyChanged(nameof(MessageDisplay));
                    }
                    else
                    {
                        _messages.Add("The glue and paper are indestructable, must need a knife.");
                    }
                }
                else
                {
                    _messages.Add("That is not in this room.");
                }
            }
            else if (userCommand.Contains("open") == true && userCommand.Contains("elavator") == true && userCommand.Contains("1408") == true)
            {
                if (CurrentLocation.Id == 1)
                {
                    _messages.Add("The elavator opens and you leave safe and sound.");
                    _messages.Add("Your final time is: " + _gameTime.ToString(@"mm\:ss"));
                    OnPropertyChanged(nameof(MessageDisplay));
                }
                else
                {
                    _messages.Add("That is not in this room.");
                }
            }

            #endregion ITEM INTERACTION

            #region checking

            else if (userCommand.Contains("check") == true && userCommand.Contains("plant") == true)
            {
                if (CurrentLocation.Id == 0)
                {
                    _messages.Add("There seems to be a shiny key hidden in the plant.");
                    OnPropertyChanged(nameof(MessageDisplay));
                }
                else
                {
                    _messages.Add("That is not in this room.");
                }
            }
            else if (userCommand.Contains("check") == true && userCommand.Contains("desk") == true)
            {
                if (CurrentLocation.Id == 0)
                {
                    _messages.Add("The desk has a few drawers and sitting upon the desk is a knife for envelopes.");
                    OnPropertyChanged(nameof(MessageDisplay));
                }
                else
                {
                    _messages.Add("That is not in this room.");
                }
            }
            else if (userCommand.Contains("check") == true && userCommand.Contains("couch") == true)
            {
                if (CurrentLocation.Id == 1)
                {
                    _messages.Add("You check under the couch cushion and see an old rusted key.");
                    OnPropertyChanged(nameof(MessageDisplay));
                }
                else
                {
                    _messages.Add("That is not in this room.");
                }
            }
            else if (userCommand.Contains("check") == true && userCommand.Contains("drawers") == true)
            {
                if (CurrentLocation.Id == 0)
                {
                    _messages.Add("You check all of the drawers and the only one that was unlocked contained an old lock box.");
                    OnPropertyChanged(nameof(MessageDisplay));
                }
                else
                {
                    _messages.Add("That is not in this room.");
                    OnPropertyChanged(nameof(MessageDisplay));
                }
            }
            else if (userCommand.Contains("unconsious") == true || userCommand.Contains("man") == true)
            {
                if (CurrentLocation.Id == 1)
                {
                    if ((23 == DateTime.Now.Hour) == true)
                    {
                        CurrentNpc = CurrentLocation.Npcs[0];
                        OnPlayerTalkTo();
                        OnPropertyChanged(nameof(MessageDisplay));
                    }
                    else
                    {
                        _messages.Add("The man is not responding.");
                        OnPropertyChanged(nameof(MessageDisplay));
                    }
                }
                else
                {
                    _messages.Add("He is not in this room.");
                    OnPropertyChanged(nameof(MessageDisplay));
                }
            }

            #endregion checking

            #region MOVEMENT

            else if (userCommand.Contains("forward") == true)
            {
                MoveNorth();
            }
            else if (userCommand.Contains("backward") == true)
            {
                MoveSouth();
            }
            else if (userCommand.Contains("lights") == true)
            {
                LightsOn = true;
            }

            #endregion MOVEMENT

            else if (userCommand.Contains("quit") == true)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else if (userCommand.Contains("help") == true)
            {
                _messages.Add("To see a description of an item in your inventory or in the room enter 'check' + your item name.\nTo attempt an escape, enter 'open elavator' + the elavator code.");
                OnPropertyChanged(nameof(MessageDisplay));
            }
            else
            {
                _messages.Add("Try another way of phrasing your command");
                OnPropertyChanged(nameof(MessageDisplay));
            }
        }

        #endregion USER COMMANDS

        #region HELPER METHODS

        private int DieRoll(int sides)
        {
            return random.Next(1, sides + 1);
        }

        #endregion HELPER METHODS

        #endregion METHODS
    }
}