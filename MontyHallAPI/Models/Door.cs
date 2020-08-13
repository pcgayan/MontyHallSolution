using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallAPI.Models
{
    public class Door
    {
        public readonly int id;
        public readonly bool winning;
        public bool hostSelected;

        public Door(int id, bool winning)
        {
            this.id = id;
            this.winning = winning;
        }

        public override string ToString()
        {
            return String.Format("Door number {0} with a {1}", id + 1 , winning ? "Car" : "Goat");
        }
    }
}
