using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallAPI.Models
{
    public class MontyHallGameSession
    {
        public readonly Player player;
        private Door hostFirstSelectedDoor { get; set; }
        public readonly int winningDoorId;
        public readonly List<Door> doorList = new List<Door>();
        public readonly DateTime startTime;
        private DateTime endTime { get;  set; }

        public MontyHallGameSession(Player player)
        {
            this.player = player;
            
            winningDoorId = getWiningDoorId();

            for (int i=0; i < 3; i++)
            {
                doorList.Add(new Door(i, winningDoorId == i ? true : false));
            }

            startTime = new DateTime();
        }

        private int getWiningDoorId()
        {
            Random rnd = new Random();
            return rnd.Next(0, 3);
        }

        public bool playerWon()
        {
            if (player.switchSelectedDoor != null)
            {
                return winningDoorId == player.switchSelectedDoor.id;
            } else
            {
                return winningDoorId == player.firstSelectedDoor.id;
            }
        }
    }
}
