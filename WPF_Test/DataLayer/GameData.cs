using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Test.Models;

namespace WPF_Test.DataLayer
{
    public static class GameData
    {
        public static Player PlayerData()
        {
            return new Player()
            {
                Id = 1,
                Name = "Fred",
                Age = 34,
                JobTitle = Player.JobTitleName.Warrior,
                Race = Character.RaceType.Human,
                Health = 100,
                Lives = 3,
                ExperiencePoints = 10,
                LocationId = 0,
                SkillLevel = 5,
                Inventory = new ObservableCollection<GameItemQuantity>()
                {
                    new GameItemQuantity(GameItemByID(101), 1)
                }
            };
        }

        public static List<string> InitialMessages()
        {
            return new List<string>()
            {
                "Welcome to my Dungeon, you have been tasked with defeating a monster at the end of this dungeon"
            };
        }

        public static GameMapCoordinates InitialGameMapLocation()
        {
            return new GameMapCoordinates() { Row = 2, Column = 0 };
        }

        #region GameMap

        public static GameItem GameItemByID(int id)
        {
            return StandardGameItems().FirstOrDefault(i => i.Id == id);
        }
        public static Npc GetNpcById(int id)
        {
            return Npcs().FirstOrDefault(i => i.Id == id);
        }
        public static Map GameMap()
        {
            int rows = 3;
            int columns = 3;

            Map gameMap = new Map(rows, columns);

            gameMap.StandardGameItems = StandardGameItems();
            //
            // row 1
            //
            gameMap.MapLocations[2, 0] = new Location()
            {
                Id = 1,
                Name = "1-1",
                Description = "The entrance",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "You find yourself in the entrance of a dungeon",
                GameItems = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GameItemByID(101), 2)
                },
                Npcs = new ObservableCollection<Npc> { GetNpcById(9001) }
            };
            gameMap.MapLocations[2, 1] = new Location()
            {
                Id = 2,
                Name = "1-2",
                Description = "The 2nd room in the dungeon where you will encounter an enemy NPC",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "You encounter an enemy minion (Room 2)",
                Npcs = new ObservableCollection<Npc> { GetNpcById(9003)}
            };

            //
            // row 2
            //
            gameMap.MapLocations[2, 2] = new Location()
            {
                Id = 3,
                Name = "1-3",
                Description = "You will have another encounter here",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "This is the third room (Room 3)"
            };
            gameMap.MapLocations[1, 0] = new Location()
            {
                Id = 4,
                Name = "2-1",
                Description = "This is a mini boss",
                Accessible = true,
                GameItems = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GameItemByID(201), 1)
                },
                Message = "Welcome to the mini boss (room 6)",
                Npcs = new ObservableCollection<Npc> { GetNpcById(9002) }
            };

            //
            // row 3
            //
            gameMap.MapLocations[1, 1] = new Location()
            {
                Id = 5,
                Name = "2-2",
                Description = "This is a vendor room before the mini boss",
                Accessible = false,
                Message = "Welcome to the Vendor Room (room 5)"
            };
            gameMap.MapLocations[1, 2] = new Location()
            {
                Id = 6,
                Name = "2-3",
                Description = "You will pick up a treasure item of your choice here",
                Accessible = true,
                Message = "Welcome to the treasure room (room 4)"
            };
            gameMap.MapLocations[0, 0] = new Location()
            {
                Id = 7,
                Name = "3-1",
                Description = "You encounter an NPC",
                Accessible = true,
                Message = "Welcome to the final boss (room 7)"
            };
            gameMap.MapLocations[0, 1] = new Location()
            {
                Id = 8,
                Name = "3-2",
                Description = "Safe room before final boss, health restored to full",
                Accessible = true,
                Message = "Beware, the final boss is to the east (room 8)"
            };
            gameMap.MapLocations[0, 2] = new Location()
            {
                Id = 9,
                Name = "3-3",
                Description = "Final Boss Room",
                RequiredItem = 201,
                Accessible = false,
                Message = "Welcome to the Boss (room 9)",
                Npcs = new ObservableCollection<Npc> { GetNpcById(9001) }
            };

            return gameMap;
        }
        #endregion

        #region Game Items

        public static List<GameItem> StandardGameItems()
        {
            return new List<GameItem>()
            {
                new Potion(101, "Health Potion", 100, 50, 0, 0, "Health Potion restores 50 HP", "You restored 50 HP"),
                new Potion(103, "Bonus Life Potion", 150, 0, 1, 0, "Bonus Life potion grants 1 life", "You gained a life!"),
                new Potion(104, "XP Potion", 100, 0, 0, 100, "Bonus XP potion grants 100 xp", "You gained 100 xp"),

                new Armor(201, "Dark Iron Armor", 20,  500, Armor.ArmorType.Plate, 0, "This armor emanates with the power of Dark Iron", "You have gained 20 xp"),
                new Armor(202, "Dark Elf Armor", 20, 500, Armor.ArmorType.Leather, 0,"This armor emanates with the power of Dark Elves", "You have gained 20 xp"),
                new Armor(203, "Arcane Armor", 20, 500, Armor.ArmorType.Cloth, 0, "This armor emanates with the power of the Arcane", "You have gained 20 xp")

            };
        }
        #endregion

        #region NPC
        public static List<Npc> Npcs()
        {
            return new List<Npc>()
            {
                new Enemy()
                {
                    Id = 9001,
                    Name = "Guy De Lombard",
                    Race = Character.RaceType.Human,
                    Description = "Already has a Holy Grail",
                    Messages = new List<string>()
                    {
                        "Severed heads never go out of fashion!",
                        "Did ye just call me short?",
                        "You can't make an omelet without killing a few people!"
                    },
                    SkillLevel = 10
                },
                new Enemy()
                { 
                    Id = 9002,
                    Name = "The Taunter",
                    Race = Character.RaceType.Human,
                    Description = "Speaks in an outrageous French Accent",
                    Messages = new List<string>()
                    {
                        "Mind your own business!",
                        "Go and boil your bottoms, son of a silly person!",
                        "Your mother was a hamster, and your father smelt of elderberries!"
                    }
                },
                new Enemy()
                {
                    Id = 9003,
                    Name = "The Black Knight",
                    Race = Character.RaceType.Human,
                    Description = "Will never be defeated, even if you cut all his limbs off... ",
                    Messages = new List<string>()
                    { 
                        "None shall pass",
                        "Come on then, you pansy!",
                        "Have at you!"
                    },
                    SkillLevel = 2
                },
                new Vendor()
                {
                    Id = 8001,
                    Name = "Roger",
                    Race = Character.RaceType.Human,
                    Description = "The Shrubber",
                    Messages = new List<string>()
                    {
                        "Oh, what sad times are these when passing ruffians can say Ni at will to old ladies.",
                        "There is a pestilence upon this land.",
                        "Nothing is sacred. Even those who arrange and design shrubberies are under considerable economic stress at this period in history."
                    }//,
                    //GameItems = new ObservableCollection<GameItemQuantity>()
                    //{
                     //   new GameItemQuantity(GameItemByID(101), 10),
                       // new GameItemQuantity(GameItemByID(103), 10),
                       // new GameItemQuantity(GameItemByID(104), 10)
                    //}
                }
            };
        }
        #endregion
    }
}
