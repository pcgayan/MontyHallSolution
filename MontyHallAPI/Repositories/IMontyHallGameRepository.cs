using MontyHallAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallAPI.Repositories
{
    public interface IMontyHallGameRepository
    {

        bool firstDorrSelection(Player player, int doorId);

        bool switchDoorSelection(Player player, int doorId);

        bool declareWinner(Player player);

        int getOngoingSessionStage(Player player);

        MontyHallGameSession getOngoingSession(Player player);

        int getHostDoorSelection(Player player);

        MontyHallGameSession getOngoingSession(string playerId);

        public string getStageName(int stage);
    }
}
