using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallAPI.Repositories
{
    public class MontyHallGameRepository : IMontyHallGameRepository
    {
        bool IMontyHallGameRepository.declareWinner(int personalId)
        {
            throw new NotImplementedException();
        }

        int IMontyHallGameRepository.loginPlayer(int personalId)
        {
            throw new NotImplementedException();
        }

        bool IMontyHallGameRepository.selectFirstDoor(int personalId, int doorId)
        {
            throw new NotImplementedException();
        }

        int IMontyHallGameRepository.switchDoor(int personalId, int doorId)
        {
            throw new NotImplementedException();
        }
    }
}
