using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace tbQuest.Models
{
    public class Player : Character
    {
        #region FIELDS

        private double _personalBest;
        private double _time;

        private ObservableCollection<GameItem> _inventory;
        private ObservableCollection<GameItem> _drawerKey;
        private ObservableCollection<GameItem> _doorKey;
        private ObservableCollection<GameItem> _knife;

        #endregion FIELDS

        #region PROPERTIES

        public double PersonalBest
        {
            get { return _personalBest; }
            set { _personalBest = value; }
        }

        public double Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public ObservableCollection<GameItem> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public ObservableCollection<GameItem> DrawerKey
        {
            get { return _drawerKey; }
            set { _drawerKey = value; }
        }

        public ObservableCollection<GameItem> DoorKey
        {
            get { return _doorKey; }
            set { _doorKey = value; }
        }

        public ObservableCollection<GameItem> Knife
        {
            get { return _knife; }
            set { _knife = value; }
        }

        #endregion PROPERTIES

        #region CONSTRUCTORS

        public Player()
        {
            _inventory = new ObservableCollection<GameItem>();
            _drawerKey = new ObservableCollection<GameItem>();
            _doorKey = new ObservableCollection<GameItem>();
            _knife = new ObservableCollection<GameItem>();
        }

        #endregion CONSTRUCTORS

        #region METHODS

        public void UpdateInventoryCategories()
        {
            _drawerKey.Clear();
            _doorKey.Clear();
            _knife.Clear();

            foreach (var gameItem in _inventory)
            {
                if (gameItem is DrawerKey) _drawerKey.Add(gameItem);
                if (gameItem is DoorKey) _doorKey.Add(gameItem);
                if (gameItem is Knife) _knife.Add(gameItem);
            }
        }

        public void AddGameItemToInventory(GameItem selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _inventory.Add(selectedGameItem);
            }
        }

        public void RemoveGameItemFromInventory(GameItem selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _inventory.Remove(selectedGameItem);
            }
        }

        #endregion METHODS
    }
}