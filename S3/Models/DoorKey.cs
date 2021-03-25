using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tbQuest.Models
{
    public class DoorKey : GameItem
    {
        public bool HasKeyChange { get; set; }

        public DoorKey(int id, string name, string description)
            : base(id, name, description)
        {
        }

        public override string InformationString()
        {
            return $"{Name}: {Description}";
        }
    }
}