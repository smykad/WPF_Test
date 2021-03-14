﻿using System;
using System.Collections.Generic;
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
                LocationId = 0
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

        public static Map GameMap()
        {
            int rows = 3;
            int columns = 3;

            Map gameMap = new Map(rows, columns);

            //
            // row 1
            //
            gameMap.MapLocations[2, 0] = new Location()
            {
                Id = 1,
                Name = "1-1",
                Description = "You will get to choose your weapon here",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "Welcome to my Dungeon, you have been tasked with defeating a monster at the end of this dungeon.  " +
                "Choose a weapon of your choice (Room 1)"
            };
            gameMap.MapLocations[2, 1] = new Location()
            {
                Id = 2,
                Name = "1-2",
                Description = "The 2nd room in the dungeon where you will encounter an enemy NPC",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "You encounter an enemy minion (Room 2)"
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
                Message = "Welcome to the mini boss (room 6)"
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
                Accessible = true,
                Message = "Welcome to the Boss (room 9)"
            };

            return gameMap;
        }
    }
}
