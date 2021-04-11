using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tbQuest.Models
{
    public abstract class Npc : Character
    {
        public string Description { get; set; }

        public string Information
        {
            get
            {
                return InformationText();
            }
            set
            {
            }
        }

        public Npc()
        {
        }

        public Npc(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        protected abstract string InformationText();
    }
}