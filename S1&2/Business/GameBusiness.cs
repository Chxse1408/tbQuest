using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tbQuest.Presentation;
using tbQuest.Data;
using tbQuest.Models;

namespace tbQuest.Business
{
    public class GameBusiness
    {
        private GameSessionViewModel _gameSessionViewModel;

        //private bool _newPlayer = true; // player data coming from GameData class
        private Player _player = new Player();

        private Map _gameMap;
        private Location _currentLocation;

        //private PlayerSetupView _playerSetupView = null;
        private List<string> _messages;

        public GameBusiness()
        {
            //SetupPlayer();
            InitializeDataSet();
            InstantiateAndShowView();
        }

        /// <summary>
        /// setup new or existing player
        /// </summary>
        //private void SetupPlayer()
        //{
        //    if (_newPlayer)
        //    {
        //        _playerSetupView = new PlayerSetupView(_player);
        //        _playerSetupView.ShowDialog();

        //        //
        //        // setup up game based player properties
        //        //
        //        _player.PersonalBest = 0.0;
        //        _player.Time = 0.0;
        //    }
        //    else
        //    {
        //        _player = GameData.PlayerData();
        //    }
        //}

        /// <summary>
        /// initialize data set
        /// </summary>
        private void InitializeDataSet()
        {
            _player = GameData.PlayerData();
            _messages = GameData.InitialMessages();
            _gameMap = GameData.GameMap();
            _currentLocation = GameData.GameMap()._currentLocation;
        }

        /// <summary>
        /// create view model with data set
        /// </summary>
        private void InstantiateAndShowView()
        {
            //
            // instantiate the view model and initialize the data set
            //
            _gameSessionViewModel = new GameSessionViewModel(_player, _messages, _gameMap, _currentLocation);
            GameSessionView gameSessionView = new GameSessionView(_gameSessionViewModel);

            gameSessionView.DataContext = _gameSessionViewModel;

            gameSessionView.Show();

            //
            // dialog window is initially hidden to mitigate issue with
            // main window closing after dialog window closes
            //
            // commented out because the player setup window is disabled
            //
            //_playerSetupView.Close();
        }
    }
}