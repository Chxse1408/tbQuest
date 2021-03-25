using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tbQuest.Models;
using System.Collections.ObjectModel;

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
                LocationId = 1,
                Inventory = new ObservableCollection<GameItem>()
                {
                }
            };
        }

        private static GameItem GameItemById(int id)
        {
            return StandardGameItems().FirstOrDefault(i => i.Id == id);
        }

        public static Map GameMap()
        {
            Map gameMap = new Map();
            gameMap.StandardGameItems = StandardGameItems();

            gameMap.Locations.Add
                (new Location()
                {
                    Id = 0,
                    Name = "Office",
                    Description = " Behind you there is\n a desk, to your left\n there is a painting,\n in the corner of the\n room there is a plant",
                    Accessible = true,
                    GameItems = new ObservableCollection<GameItem>
                    {
                        GameItemById(1),
                        GameItemById(2)
                    }
                }
                );

            gameMap.Locations.Add
                (new Location()
                {
                    Id = 1,
                    Name = "Lobby",
                    Description = "straight ahead you see an elevator with a keypad beside it, to your left there is a couch and to your right there is an unconsious man sitting against the wall",
                    Accessible = false,
                    RequiredItemID = 2,
                    GameItems = new ObservableCollection<GameItem>
                    {
                        GameItemById(3)
                    }
                }
                );

            gameMap.CurrentLocation = gameMap.Locations.FirstOrDefault(l => l.Id == 0);
            return gameMap;
        }

        public Location InitialGameMapLocation()
        {
            return gameMap.Locations.FirstOrDefault(l => l.Id == 0);
        }

        public static List<GameItem> StandardGameItems()
        {
            return new List<GameItem>()
            {
                new DrawerKey(1, "Box Key","rusted key simple key, looks to be for an old lock box"),
                new DoorKey(2,"Master Lock Key","Shiny Key reading 'Master Lock' maybe it's for the door?"),
                new Knife(3, "Envelope Knife", "Knife for opening an envelope")
            };
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