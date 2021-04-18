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
                JobTitle = Player.JobTitleName.Patsy,
                Race = Character.RaceType.Blue,
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
                    QuestById(3),
                    QuestById(4)
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
                Name = "Medieval England",
                Description = "It sure rains a lot here...",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "You find yourself in Medieval England on a quest for the Holy Grail, you stumble across two coconuts",
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
                Name = "Castle",
                Description = "A French Castle",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "You find yoursself in front of a castle and upon the ramparts is a vile guard with a pointy helmet...",
                Npcs = new ObservableCollection<Npc> { GetNpcById(9002)}
            };
            // ***********************
            // *  Floor I: Room III  *
            // ***********************
            gameMap.MapLocations[2, 2] = new Location()
            {
                // Third Room
                Id = 3,
                Name = "'Bridge'",
                Description = "It's really just a plank of wood over a small stream, you could probably just step over it...",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "You encounter the Black Knight, guarding a pla.. bridge",
                Npcs = new ObservableCollection<Npc> { GetNpcById(9003)}
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
                Name = "Woods",
                Description = "These woods are dangerous...",
                Accessible = false,
                RequiredRelicId = 301,
                Message = "You walk into the a dangerous forest and encounter the Knights who say.... 'Ni'!",
                Npcs = new ObservableCollection<Npc> { GetNpcById(9004) }
            };
            // ***********************
            // *  Floor II: Room II  *
            // ***********************
            gameMap.MapLocations[1, 1] = new Location()
            {
                Id = 5,
                Name ="Dirt Road",
                Description = "A dirt road outside a small village",
                Accessible = true,
                Message = "You encounter a peasant named Roger, he looks like he might be a shrubber",
                Npcs = new ObservableCollection<Npc> { GetNpcById(8001)}
            };
            // ***********************
            // *  Floor II: Room I *
            // ***********************
            gameMap.MapLocations[1, 2] = new Location()
            {
                Id = 6,
                Name = "Coconut Road",
                Description = "A road with coconuts on the ground",
                Accessible = true,
                Message = "You come upon several coconuts laying on the ground, they must have been carried here by sparrows... but what kind? African or European?",
                GameItems = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GameItemByID(102), 2)
                }
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
                Name = "Camp",
                Description = "A makeshift camp where Brother Maynard is staying",
                Accessible = false,
                RequiredRelicId = 301,
                Message = "You come upon a camp and encounter Brother Maynard... Should probably talk to him",
                Npcs = new ObservableCollection<Npc> { GetNpcById(8002)}

            };
            // ***********************
            // * Floor III: Room II  *
            // ***********************
            gameMap.MapLocations[0, 1] = new Location()
            {
                Id = 8,
                Name = "Hill",
                Description = "Just past this hill is the cave of Carbannog",
                Accessible = false,
                RequiredRelicId = 302,
                Message = "As you approcach the hill you see a sign that reads 'Beware!'\nYou notice some coconuts laying on the ground",
                GameItems = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GameItemByID(103), 2)
                }
            };
            // ***********************
            // * Floor III: Room III *
            // ***********************
            gameMap.MapLocations[0, 2] = new Location()
            {
                Id = 9,
                Name = "Cave of Caerbannog",
                Description = "Look at the fangs on that monster!",
                RequiredRelicId = 302,
                Accessible = false,
                Message = "You bravely walk into the cave of the killer rabbit, Holy Hand Grenade of Antioch in hand",
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
                new Potion(101, "Healing Coconut", 50, 5, 50, 0, 0, "Grants 50 Health", "You restored 50 HP by banging two coconuts together!"),
                new Potion(102, "Skill Coconut", 50, 5, 0, 0, 1, "Grants 1 skill level", "You gained a skill level by banging two coconuts together!"),
                new Potion(103, "Life Coconut", 50, 5, 0, 1, 0, "Grants 1 life", "You gained a life by banging two coconuts together!"),

                new Relic(301, "Shrubbery", 0, 100, "is not too expensive","This shrubbery looks nice!", Relic.UseActionType.OPENLOCATION ),
                new Relic(302, "Holy Hand Grenade of Antioch", 0, 100, "is a grenade consecrated by Saint Atilla", "You can now engage the Killer Rabbit of Caerbannog", Relic.UseActionType.OPENLOCATION),
                new Relic(303, "Holy Grail", 500, 500, "IT'S THE HOLY GRAIL!!!", "Congratulations you have completed the Ultimate Quest!", Relic.UseActionType.KILLRABBIT),

                new Money(401, "Gold Coin", 0, 25, "A shiny gold coin", "You throw a gold coin to your Shrubber", Money.MoneyType.GOLD)

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
                    Race = Character.RaceType.Blue,
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
                    Race = Character.RaceType.Red,
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
                    Race = Character.RaceType.Yellow,
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
                    Race = Character.RaceType.Blue,
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
                    Race = Character.RaceType.Yellow,
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
                    Name = "The Knighs of Ni",
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

                    Description = "Find the relics required to face the final boss, then defeat him!",

                    Status = Quest.QuestStatus.Incomplete,

                    RequiredGameItemQuantities = new List<GameItemQuantity>()
                    {
                        new GameItemQuantity(GameItemByID(301), 1),
                        new GameItemQuantity(GameItemByID(302), 1),
                        new GameItemQuantity(GameItemByID(303), 1)
                    }
                }

            };
        }
        #endregion
    }
}
