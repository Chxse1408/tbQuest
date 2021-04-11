using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tbQuest.Models
{
    internal class DrawerKey : GameItem
    {
        public bool HasKeyChange { get; set; }

        public DrawerKey(int id, string name, string description)
            : base(id, name, description)
        {
        }

        public override string InformationString()
        {
            return $"{Name}: {Description} ";
        }
    }
}