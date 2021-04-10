using System.Collections.Generic;
using System.Linq;

namespace tbQuest.Models
{
    public class Map
    {
        private List<Location> _locations;
        public Location _currentLocation;

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

        #region Constructors

        public Map()
        {
            _locations = new List<Location>();
        }

        #endregion Constructors

        #region Methods

        public void MoveForward()
        {
            if (CanMoveForward() == true)
            {
                _currentLocation = Locations.Last();
            }
        }

        public void MoveBackward()
        {
            if (CanMoveBackward() == true)
            {
                _currentLocation = Locations.First();
            }
        }

        private bool CanMoveForward()
        {
            bool canMove = false;

            if (_currentLocation == Locations.First())
            {
                canMove = true;
            }
            return canMove;
        }

        private bool CanMoveBackward()
        {
            bool canMove = false;

            if (_currentLocation == Locations.Last())
            {
                canMove = true;
            }

            return canMove;
        }

        #endregion Methods
    }
}