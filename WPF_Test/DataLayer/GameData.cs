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
                },
                Quests = new ObservableCollection<Quest>()
                {
                    QuestById(1),
                    QuestById(2),
                    QuestById(3)
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
        public static Quest QuestById(int id)
        {
            return Quests().FirstOrDefault(q => q.Id == id);
        }

        public static Location LocationById(int id)
        {
            List<Location> locations = new List<Location>();

            foreach (Location location in GameMap().MapLocations)
            {
                if (location != null)
                {
                    locations.Add(location);
                }
            }

            return locations.FirstOrDefault(x => x.Id == id);
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

            // ***********************
            // *  Floor I: Room I    *
            // ***********************
            gameMap.MapLocations[2, 0] = new Location()
            {
                // First Room
                Id = 1,
                Name = "1-1",
                Description = "The entrance",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "You find yourself in the entrance of a dungeon",
                GameItems = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GameItemByID(101), 2)
                }
            };
            // ***********************
            // *  Floor I: Room II   *
            // ***********************
            gameMap.MapLocations[2, 1] = new Location()
            {
                // Second Room
                Id = 2,
                Name = "1-2",
                Description = "The 2nd room in the dungeon where you will encounter an enemy NPC",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "You encounter an enemy minion (Room 2)",
                Npcs = new ObservableCollection<Npc> { GetNpcById(9003)}
            };
            // ***********************
            // *  Floor I: Room III  *
            // ***********************
            gameMap.MapLocations[2, 2] = new Location()
            {
                // Third Room
                Id = 3,
                Name = "1-3",
                Description = "You will have another encounter here",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "This is the third room (Room 3)",
                Npcs = new ObservableCollection<Npc> { GetNpcById(9002)}
            };

            //
            // Row 2
            //

            // ***********************
            // *  Floor II: Room III   *
            // ***********************
            gameMap.MapLocations[1, 0] = new Location()
            {
                
                Id = 4,
                Name = "2-1",
                Description = "This is a mini boss",
                Accessible = false,
                RequiredRelicId = 301,
                Message = "Welcome to the mini boss (room 6)",
                Npcs = new ObservableCollection<Npc> { GetNpcById(9004) }
            };
            // ***********************
            // *  Floor II: Room II  *
            // ***********************
            gameMap.MapLocations[1, 1] = new Location()
            {
                Id = 5,
                Name = "2-2",
                Description = "This is a vendor room before the mini boss",
                Accessible = true,
                Message = "Welcome to the Vendor Room (room 5)",
                Npcs = new ObservableCollection<Npc> { GetNpcById(8001)}
            };
            // ***********************
            // *  Floor II: Room I *
            // ***********************
            gameMap.MapLocations[1, 2] = new Location()
            {
                Id = 6,
                Name = "2-3",
                Description = "You will pick up a treasure item of your choice here",
                Accessible = true,
                Message = "Welcome to the treasure room (room 4)"
            };

            //
            // row 3
            //

            // ***********************
            // *  Floor III: Room I  *
            // ***********************
            gameMap.MapLocations[0, 0] = new Location()
            {
                Id = 7,
                Name = "3-1",
                Description = "You encounter an NPC",
                Accessible = false,
                RequiredRelicId = 301,
                Message = "Welcome to the final boss (room 7)",
                Npcs = new ObservableCollection<Npc> { GetNpcById(8002)}

            };
            // ***********************
            // * Floor III: Room II  *
            // ***********************
            gameMap.MapLocations[0, 1] = new Location()
            {
                Id = 8,
                Name = "3-2",
                Description = "Safe room before final boss, health restored to full",
                Accessible = false,
                RequiredRelicId = 302,
                Message = "Beware, the final boss is to the east (room 8)"
            };
            // ***********************
            // * Floor III: Room III *
            // ***********************
            gameMap.MapLocations[0, 2] = new Location()
            {
                Id = 9,
                Name = "3-3",
                Description = "Final Boss Room",
                RequiredRelicId = 302,
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
                new Potion(101, "Health Potion", 100, 50, 50, 0, "restores 50 HP", "You restored 50 HP"),
                new Potion(103, "Bonus Life Potion", 150, 0, 0, 1, "grants 1 life", "You gained a life!"),
                new Potion(104, "XP Potion", 100, 0, 0, 100, "grants 100 xp", "You gained 100 xp"),

                new Relic(301, "Shrubbery", 0, 100, "is not too expensive","This shrubbery looks nice!", Relic.UseActionType.OPENLOCATION ),
                new Relic(302, "Holy Hand Grenade of Antioch", 0, 100, "is a grenade consecrated by Saint Atilla", "You can now engage the Killer Rabbit of Caerbannog", Relic.UseActionType.OPENLOCATION),

                new Money(401, "Gold Coin", 0, 25, "A shiny gold coin", "You throw a gold coin to your Shrubber", Money.MoneyType.GOLD),
                new Money(402, "Gold Coin", 0, 25, "A shiny silver coin", "You throw a silver coin to your Shrubber", Money.MoneyType.SILVER)

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
                    Name = "Killer Rabbit of Caerbannog",
                    Description = "Has giant fangs",
                    Messages = new List<string>()
                    {
                        "Screeeech",
                        "Screeeeeeeeech",
                        "Screech"
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
                new Enemy()
                {
                    Id = 9004,
                    Name = "Knights of Ni",
                    Race = Character.RaceType.Human,
                    Description = "Cannot stand 'it'",
                    Messages = new List<string>()
                    {
                        "Ni!",
                        "We want... A SHRUBBERY!!!",
                        "We are the knights who say..... Ni!"
                    }
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
                    },
                    GameItems = new ObservableCollection<GameItemQuantity>()
                    {
                        new GameItemQuantity(GameItemByID(301), 1)
                    }
                },
                new Vendor()
                {
                    Id = 8002,
                    Name = "Brother Maynard",
                    Race = Character.RaceType.Human,
                    Description = "Keeper of the Holy Hand Grenade of Antioch",
                    Messages = new List<string>()
                    {
                        "And the Lord spake, saying, 'First shalt thou take out the Holy Pin'",
                        "Then, shalt thou count to three, no more, no less",
                        "Three shall be the number thou shalt count, and the number of the counting shall be three"
                    },
                    GameItems = new ObservableCollection<GameItemQuantity>()
                    {
                        new GameItemQuantity(GameItemByID(302), 1)
                    }
                }
                
            };
        }
        #endregion

        #region QUESTS

        public static List<Quest> Quests()
        {
            return new List<Quest>()
            {
                new QuestEngage()
                {
                    Id = 1,
                    GoldToGive = 100,
                    Name = "The knighs of Ni",
                    Description = "Talk to these Npcs in order to obtain gold for a shrubbery.",

                    Status = Quest.QuestStatus.Incomplete,

                    RequiredNpcs = new List<Npc>()
                    {
                        GetNpcById(9003),
                        GetNpcById(9002),
                        GetNpcById(8001)
                    },

                    ExperienceGain = 150
                },

                new QuestEngage()
                {
                    Id = 2,
                    GoldToGive = 100,
                    Name = "Holy Hand Grenade!",
                    Description = "Engage the Knights who Say Ni and talk to Brother Maynard",

                    Status = Quest.QuestStatus.Incomplete,

                    RequiredNpcs = new List<Npc>()
                    {
                        GetNpcById(9004),
                        GetNpcById(8002)
                    }
                },

                new QuestTravel()
                {
                    Id = 3,

                    Name = "Explore the Map",
                    
                    Description = "Travel to every location in the game",

                    Status = Quest.QuestStatus.Incomplete,

                    RequiredLocations = new List<Location>()
                    {
                        LocationById(2),
                        LocationById(3),
                        LocationById(4),
                        LocationById(5),
                        LocationById(6),
                        LocationById(7),
                        LocationById(8),
                        LocationById(9)
                    },

                    ExperienceGain = 250
                    
                },

                new QuestGather()
                {
                    Id = 4,

                    Name = "Quest for the Holy Grail",

                    Description = "Find the relics required to face the final boss",

                    Status = Quest.QuestStatus.Incomplete,

                    RequiredGameItemQuantities = new List<GameItemQuantity>()
                    {
                        new GameItemQuantity(GameItemByID(301), 1),
                        new GameItemQuantity(GameItemByID(302), 1)
                    }
                }

            };
        }
        #endregion
    }
}
