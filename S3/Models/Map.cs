using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace tbQuest.Models
{
    public class Map
    {
        #region FIELDS

        private List<Location> _locations;
        public Location _currentLocation;

        private List<GameItem> _standardGameItems;

        #endregion FIELDS

        #region PROPERTIES

        public List<Location> Locations
        {
            get { return _locations; }
            set { _locations = value; }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; }
        }

        public List<GameItem> StandardGameItems
        {
            get { return _standardGameItems; }
            set { _standardGameItems = value; }
        }

        #endregion PROPERTIES

        #region Constructors

        public Map()
        {
            _locations = new List<Location>();
        }

        #endregion Constructors

        #region Methods

        #region MOVEMENT

        public void MoveNorth()
        {
            if (_currentLocation.Id == 0)
            {
                _currentLocation = Locations.Last();
            }
        }

        public void MoveSouth()
        {
            if (_currentLocation.Id == 1)
            {
                _currentLocation = Locations.First();
            }
        }

        public Location NorthLocation()
        {
            Location northLocation = null;

            if (_currentLocation.Id == 0)
            {
                Location nextNorthLocation = _locations[1];

                if (nextNorthLocation != null)
                {
                    northLocation = nextNorthLocation;
                }
            }
            return northLocation;
        }

        public Location SouthLocation()
        {
            Location southLocation = null;

            if (_currentLocation.Id == 1)
            {
                Location nextSouthLocation = _locations[0];

                if (nextSouthLocation != null)
                {
                    southLocation = nextSouthLocation;
                }
            }
            return southLocation;
        }

        #endregion MOVEMENT

        public string OpenLocationsByKey(int itemId)
        {
            string message = "skrt skrt";
            Location mapLocation = new Location();
            mapLocation = Locations[1];
            if (mapLocation.RequiredItemID == itemId)
            {
                mapLocation.Accessible = true;
            }
            message = CurrentLocation.GameItems[itemId].Description;
            return message;
        }

        #endregion Methods
    }
}