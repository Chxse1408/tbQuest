namespace tbQuest.Models
{
    public class Location
    {
        #region Fields

        private int _id;
        private string _name;
        private string _description;
        private bool _accessible;

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

        #endregion Properties

        #region Methods

        //public bool IsAccessibleByKey(bool playerHasKey)
        //{
        //    return playerHasKey = _requiredKey ? true : false;
        //}

        #endregion Methods
    }
}