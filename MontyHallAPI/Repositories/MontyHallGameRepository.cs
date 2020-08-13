using MontyHallAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace MontyHallAPI.Repositories
{
    public class MontyHallGameRepository : IMontyHallGameRepository
    {

        private List<MontyHallGameSession> montyHallGameSessions;

        public MontyHallGameRepository()
        {
            montyHallGameSessions = new List<MontyHallGameSession>();
        }

        public MontyHallGameSession getOngoingSession(Player player)
        {
           foreach (var montyHallGameSession in montyHallGameSessions)
            {
                if(montyHallGameSession.isValidSession(player))
                {
                    return montyHallGameSession;
                }
            }

           MontyHallGameSession initMontyHallGameSession = new MontyHallGameSession(player);
           montyHallGameSessions.Add(initMontyHallGameSession);

            return initMontyHallGameSession;
        }

        public MontyHallGameSession getOngoingSession(string playerId)
        {
            foreach (var montyHallGameSession in montyHallGameSessions)
            {
                if (montyHallGameSession.isValidSession(playerId))
                {
                    return montyHallGameSession;
                }
            }

            MontyHallGameSession initMontyHallGameSession = new MontyHallGameSession(new Player(playerId));
            montyHallGameSessions.Add(initMontyHallGameSession);

            return initMontyHallGameSession;
        }


        public int getOngoingSessionStage(Player player)
        {
            foreach (var montyHallGameSession in montyHallGameSessions)
            {
                if (montyHallGameSession.isValidSession(player))
                {
                    // Is it Stage 1 ?
                    Door hostSelectedDoor = montyHallGameSession.getHostSelectionDoor();
                    if (hostSelectedDoor == null)
                    {
                        if (player.firstSelectedDoor == null)
                        {
                            return 1;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                    else
                    {
                        //Is it Stage 3 ?
                        if (player.switchSelectedDoor == null)
                        {
                            return 3;
                        }
                        else
                        {
                            return 4;
                        }
                    }
                }
            }

            return 0;
        }

        bool IMontyHallGameRepository.firstDorrSelection(Player player, int doorId)
        {
            try
            {
                player.firstSelectedDoor = getOngoingSession(player).getDoor(doorId - 1);
                return true;
            } 
            catch (Exception)
            {
                return false;
            }
        }

        public int getHostDoorSelection(Player player)
        {
            try
            {
                return getOngoingSession(player).setHostSelectedDoor().id + 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        bool IMontyHallGameRepository.switchDoorSelection(Player player, int doorId)
        {
            player.switchSelectedDoor = getOngoingSession(player).getDoor(doorId - 1);
            return true;
        }

        bool IMontyHallGameRepository.declareWinner(Player player)
        {
            MontyHallGameSession montyHallGameSession = getOngoingSession(player);
            montyHallGameSession.endSession(player);
            bool winStatus = montyHallGameSession.isPlayerWon();

            montyHallGameSessions.Remove(montyHallGameSession);

            return winStatus;
        }

    }
}
