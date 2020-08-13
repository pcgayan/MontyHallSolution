using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallAPI.Models
{
    public class Player
    {
        public readonly string PersonalId;
        public Door firstSelectedDoor { get; set; }
        public Door switchSelectedDoor { get; set; }

        public Player(string personalId)
        {
            this.PersonalId = personalId;
        }

        public override bool Equals(object obj)
        {
            return obj is Player player &&
                   PersonalId.Equals(player.PersonalId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(switchSelectedDoor);
        }
    }
}
