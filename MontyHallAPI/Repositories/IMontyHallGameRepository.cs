using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallAPI.Repositories
{
    public interface IMontyHallGameRepository
    {
        int loginPlayer(int personalId);

        bool selectFirstDoor(int personalId, int doorId);

        int switchDoor(int personalId, int doorId);

        bool declareWinner(int personalId);
    }
}
