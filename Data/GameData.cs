using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tbQuest.Models;

namespace tbQuest.Data
{
    public static class GameData
    {
        public static Player PlayerData()
        {
            return new Player()
            {
                Id = 1,
                Name = "Chase",
                Age = 19,
                PlayedBFor = Character.PlayedBefore.New,
                PersonalBest = 0.0,
                Time = 0.0,
                LocationId = 0
            };
        }

        public static List<string> InitialMessages()
        {
            return new List<string>()
            {
                "\tYou wake up in a dark room",
                "\tWhat do you do?"
            };
        }
    }
}