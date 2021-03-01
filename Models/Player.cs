using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tbQuest.Models
{
    public class Player : Character
    {
        #region FIELDS

        private double _personalBest;
        private double _time;

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

        #endregion PROPERTIES
    }
}