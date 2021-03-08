using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tbQuest.Models;

namespace tbQuest.Data
{
    public class GameData
    {
        private Map gameMap = new Map();

        public static Player PlayerData()
        {
            return new Player()
            {
                Id = 0,
                Name = "Chase",
                Age = 19,
                PlayedBFor = Character.PlayedBefore.New,
                PersonalBest = 0.0,
                Time = 0.0,
                LocationId = 0
            };
        }

        public static Map GameMap()
        {
            Map gameMap = new Map();

            gameMap.Locations.Add
                (new Location()
                {
                    Id = 0,
                    Name = "Office",
                    Description = " Behind you there is\n a desk, to your left\n there is a painting,\n in the corner of the\n room there is a plant",
                    Accessible = true
                }
                );

            gameMap.Locations.Add
                (new Location()
                {
                    Id = 1,
                    Name = "Lobby",
                    Description = "straight ahead you see an elevator with a keypad beside it, to your left there is a couch and to your right there is an unconsious man sitting against the wall",
                    Accessible = true
                }
            );

            gameMap.CurrentLocation = gameMap.Locations.FirstOrDefault(l => l.Id == 0);
            return gameMap;
        }

        public Location InitialGameMapLocation()
        {
            return gameMap.Locations.FirstOrDefault(l => l.Id == 0);
        }

        public static List<string> InitialMessages()
        {
            return new List<string>()
            {
                "\tYou wake up in a dark room",
                "\tYou feel a lightswitch on the wall",
                "\tWhat do you do?"
            };
        }
    }
}