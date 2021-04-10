using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tbQuest.Models
{
    internal class Knife : GameItem
    {
        public Knife(int id, string name, string description)
            : base(id, name, description)
        {
        }

        public override string InformationString()
        {
            return $"{Name}: {Description} ";
        }
    }
}