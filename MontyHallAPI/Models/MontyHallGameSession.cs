using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallAPI.Models
{
    public class MontyHallGameSession
    {
        public readonly int gameId;
        public readonly Player player;
        public readonly List<Door> doorList = new List<Door>();
        public readonly DateTime startTime;
        private DateTime? endTime { get;  set; }

        public MontyHallGameSession(int gameId, Player player)
        {
            this.gameId = gameId;
            this.player = player;
            
            int winningDoorId = generateWiningDoorId();

            for (int i=0; i < 3; i++)
            {
                doorList.Add(new Door(i, winningDoorId == i ? true : false));
            }

            startTime = DateTime.Now;
            endTime = null;
        }

        private int generateWiningDoorId()
        {
            Random rnd = new Random();
            return rnd.Next(0, 3);
        }

        public bool isPlayerWon()
        {         
           return getWinningdDoor().id == player.switchSelectedDoor.id;
        }

        public bool endSession(Player player)
        {
            endTime = DateTime.Now;
            return true;
        }

        public bool isValidSession(Player player)
        {
            if (!this.player.Equals(player)) 
            {
                return false;
            }

            if (endTime == null || endTime >= startTime)
            {
                return true;
            }

            return false;
        }


        public bool isValidSession(String PersonalId)
        {
            if (!this.player.PersonalId.Equals(PersonalId))
            {
                return false;
            }

            if (endTime == null || endTime >= startTime)
            {
                return true;
            }

            return false;
        }

        public Door setHostSelectedDoor() 
        {
            int winningDoorId = getWinningdDoor().id;
            foreach (var door in doorList)
            {
                if (winningDoorId != door.id)
                {
                    door.hostSelected = true;
                    return door;
                }
                 
            }
            
            return null;
        }

        public Door getHostSelectionDoor()
        {
            foreach (var door in doorList)
            {
                if (door.hostSelected)
                {
                    return door;
                }
            }

            return null;
        }

        public Door getWinningdDoor()
        {
            foreach (var door in doorList)
            {
                if (door.winning)
                {
                    return door;
                }
            }

            return null;
        }

        public Door getDoor(int doorId)
        {
            foreach (var door in doorList)
            {
                if (doorId == door.id)
                {
                    return door;
                }

            }

            return null;
        }
    }
}
