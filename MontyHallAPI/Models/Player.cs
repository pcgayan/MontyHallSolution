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

    }
}
