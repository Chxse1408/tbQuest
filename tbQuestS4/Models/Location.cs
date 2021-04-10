using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace tbQuest.Models
{
    public class Location : ObservableObject
    {
        #region Fields

        private int _id;
        private string _name;
        private string _description;
        private bool _accessible;
        private int _requiredItemId;
        private ObservableCollection<GameItem> _gameItems;
        private ObservableCollection<Npc> _npcs;

        #endregion Fields

        #region Properties

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public bool Accessible
        {
            get { return _accessible; }
            set { _accessible = value; }
        }

        public int RequiredItemID
        {
            get { return _requiredItemId; }
            set { _requiredItemId = value; }
        }

        public ObservableCollection<GameItem> GameItems
        {
            get { return _gameItems; }
            set { _gameItems = value; }
        }

        public ObservableCollection<Npc> Npcs
        {
            get { return _npcs; }
            set { _npcs = value; }
        }

        #endregion Properties

        #region CONSTRUCTORS

        public Location()
        {
            _gameItems = new ObservableCollection<GameItem>();
        }

        #endregion CONSTRUCTORS

        #region Methods

        public void UpdateLocationGameItems()
        {
            ObservableCollection<GameItem> updatedLocationGameItems = new ObservableCollection<GameItem>();

            foreach (GameItem GameItem in _gameItems)
            {
                updatedLocationGameItems.Add(GameItem);
            }

            GameItems.Clear();

            foreach (GameItem gameItem in updatedLocationGameItems)
            {
                GameItems.Add(gameItem);
            }
        }

        public void AddGameItemToLocation(GameItem selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _gameItems.Add(selectedGameItem);
            }

            UpdateLocationGameItems();
        }

        public void RemoveGameItemFromLocation(GameItem selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _gameItems.Remove(selectedGameItem);
            }

            UpdateLocationGameItems();
        }

        #endregion Methods
    }
}