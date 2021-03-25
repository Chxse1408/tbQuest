using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tbQuest.Models
{
    public class Character : ObservableObject
    {
        #region ENUMERABLES

        public enum PlayedBefore
        {
            New,
            Played
        }

        #endregion ENUMERABLES

        #region FIELDS

        protected int _id;
        protected string _name;
        protected int _locationId;
        protected int _age;
        protected PlayedBefore _playedBefore;

        #endregion FIELDS

        #region PROPERTIES

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

        public int LocationId
        {
            get { return _locationId; }
            set
            {
                _locationId = value;
                OnPropertyChanged(nameof(LocationId));
            }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public PlayedBefore PlayedBFor
        {
            get { return _playedBefore; }
            set { _playedBefore = value; }
        }

        #endregion PROPERTIES

        #region CONSTRUCTORS

        public Character()
        {
        }

        public Character(string name, PlayedBefore playedBefore, int locationId)
        {
            _name = name;
            _playedBefore = playedBefore;
            _locationId = locationId;
        }

        #endregion CONSTRUCTORS
    }
}