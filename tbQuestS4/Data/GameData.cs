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
                Name = "Player",
                Age = 0,
                PlayedBFor = Character.PlayedBefore.New,
                PersonalBest = 0.0,
                Time = 0.0,
                LocationId = 0,
                Inventory = new ObservableCollection<GameItem>()
                {
                }
            };
        }

        private static GameItem GameItemById(int id)
        {
            return StandardGameItems().FirstOrDefault(i => i.Id == id);
        }

        private static Npc NpcById(int id)
        {
            return Npcs().FirstOrDefault(i => i.Id == id);
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
                    Description = " Behind you there is a desk, to your left there is a painting, in the corner of the room there is a plant",
                    Accessible = true,
                    GameItems = new ObservableCollection<GameItem>
                    {
                        GameItemById(2),
                        GameItemById(3)
                    }
                }
                );

            gameMap.Locations.Add
                (new Location()
                {
                    Id = 1,
                    Name = "Lobby",
                    Description = " straight ahead you see an elevator with a keypad beside it, to your left there is a couch and to your right there is an unconsious man sitting against the wall",
                    Accessible = false,
                    RequiredItemID = 0,
                    GameItems = new ObservableCollection<GameItem>
                    {
                        GameItemById(0)
                    },
                    Npcs = new ObservableCollection<Npc>()
                    {
                        NpcById(0)
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
                new DrawerKey(0, "Lock Box Key","rusted key simple key, looks to be for an old lock box"),
                new DoorKey(2, "Master Lock Key","Shiny key reading 'Master Lock' maybe it's for the door?"),
                new Knife(3, "Envelope Knife", "Knife for opening an envelope")
            };
        }

        public static List<Npc> Npcs()
        {
            return new List<Npc>()
            {
                new Jerry()
                {
                    Id = 0,
                    Name = "Jerry",
                    Description = "Unconscious man laying against the wall. Maybe they can be woken up.",
                    Messages = new List<string>()
                    {
                        "W-What, WHO ARE YOU?!\n" +
                        "Nevermind that, we need to get out of here.\n" +
                        "The code to that elavator is 1408."
                    }
                }
            };
        }

        public static List<string> InitialMessages()
        {
            return new List<string>()
            {
                "You wake up in a dark room",
                "You feel a lightswitch on the wall",
                "What do you do?"
            };
        }
    }
}