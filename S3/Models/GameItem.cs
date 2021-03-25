using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tbQuest.Models
{
    public class GameItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual string InformationString()
        {
            return $"{Name}: {Description}";
        }

        public string Information
        {
            get
            {
                return InformationString();
            }
        }

        public GameItem(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}