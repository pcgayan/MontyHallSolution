using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MontyHallAPI.Models;
using MontyHallAPI.Repositories;



namespace MontyHallAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontyHallController : Controller
    {
        private const string PERSONAL_ID = "personalId";
        private readonly IMontyHallGameRepository montyHallGameRepository;

        private readonly ILogger<MontyHallController> logger;

        public MontyHallController(IMontyHallGameRepository montyHallGameRepository, ILogger<MontyHallController> logger)
        {
            this.montyHallGameRepository = montyHallGameRepository;
            this.logger = logger;
        }

        [HttpGet("GameStart")]
        [Authorize]
        //[ProducesResponseType((int)System.Net.HttpStatusCode.BadRequest)]
        //[ProducesResponseType((int)System.Net.HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<string>> GameStart()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            Claim claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == PERSONAL_ID);

            if (claim != null)
            {

                MontyHallGameSession montyHallGameSession = montyHallGameRepository.getOngoingSession(claim.Value);
                Player player = montyHallGameSession.player;
                result.Append(String.Format("Player {0} with ", player.PersonalId));

                foreach (Door door in montyHallGameSession.doorList)
                {
                    result.Append(door.ToString());
                    result.Append(", ");
                }

                int stage = montyHallGameRepository.getOngoingSessionStage(player);
                if (stage == 0 || stage == 1)
                {
                    result.Append(String.Format("Player {0}, Please select your winning door..", player.PersonalId));
                }
                else
                {
                    logger.LogDebug(String.Format("Player {0}, Player is on stage {1}", player.PersonalId, stage));
                }
            }
            else
            {
                logger.LogDebug("Token did not contain personalId");
                return Unauthorized();
            }

            return new string[] { result.ToString() };
        }

        [HttpGet("FirstDoorSelection/{firstDoorSelection}")]
        [Authorize]
        public ActionResult<string> SelectFirstDoor(int firstDoorSelection)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            Claim claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == PERSONAL_ID);

            if (claim != null)
            {
                if (firstDoorSelection < 1 || firstDoorSelection > 3)
                {
                    logger.LogDebug("First Door Selection must be 1 to 3 only.");
                    return BadRequest();
                }

                Player player = montyHallGameRepository.getOngoingSession(claim.Value).player;
                int stage = montyHallGameRepository.getOngoingSessionStage(player);
                if (stage == 1)
                {
                    bool sucess = montyHallGameRepository.firstDorrSelection(player, firstDoorSelection);
                    result.Append(String.Format("Player {0}, Selection of door number {1} was {2}", player.PersonalId, firstDoorSelection, sucess == true ? "Sucessed" : "Failed"));
                    return result.ToString();
                }
                else
                {
                    logger.LogDebug(String.Format("Player {0}, Player is on stage {1} can not not allowed to perfrom this action", player.PersonalId, stage));
                    return BadRequest();
                }
            }
            else
            {
                logger.LogDebug("Token did not contain personalId");
                return Unauthorized();
            }

        }

        [HttpGet("HostDoorSelection")]
        [Authorize]
        public ActionResult<string> HostDoorSelection()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            Claim claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == PERSONAL_ID);

            if (claim != null)
            {
                Player player = montyHallGameRepository.getOngoingSession(claim.Value).player;
                int stage = montyHallGameRepository.getOngoingSessionStage(player);
                if (stage == 2)
                {
                    int hostSelectionDoor = montyHallGameRepository.getHostDoorSelection(player);
                    result.Append(String.Format("Player {0}, Host door select was {1}.", player.PersonalId, hostSelectionDoor));
                    return result.ToString();
                }
                else
                {
                    logger.LogDebug(String.Format("Player {0}, Player is on stage {1} can not not allowed to perfrom this action", player.PersonalId, stage));
                    return BadRequest();
                }
            }
            else
            {
                logger.LogDebug("Token did not contain personalId");
                return Unauthorized();
            }

        }

        [HttpGet("SwitchDoorSelection/{switchDoorSelection}")]
        [Authorize]
        public ActionResult<string> SwitchDoorSelection(int switchDoorSelection)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            Claim claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == PERSONAL_ID);

            if (claim != null)
            {
                if (switchDoorSelection < 1 || switchDoorSelection > 3)
                {
                    logger.LogDebug("Switch Door Selection must be 1 to 3 only.");
                    return BadRequest();
                }

                Player player = montyHallGameRepository.getOngoingSession(claim.Value).player;
                int stage = montyHallGameRepository.getOngoingSessionStage(player);
                if (stage == 3)
                {
                    bool sucess = montyHallGameRepository.switchDoorSelection(player, switchDoorSelection);
                    result.Append(String.Format("Player {0}, Switching of door number {1} was {2}", player.PersonalId, switchDoorSelection, sucess == true ? "Sucessed" : "Failed"));
                    return result.ToString();
                }
                else
                {
                    logger.LogDebug(String.Format("Player {0}, Player is on stage {1} can not not allowed to perfrom this action", player.PersonalId, stage));
                    return BadRequest();
                }
            }
            else
            {
                logger.LogDebug("Token did not contain personalId");
                return Unauthorized();
            }

        }

        [HttpGet("WinnerStatus")]
        [Authorize]
        public ActionResult<string> WinnerStatus()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            Claim claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == PERSONAL_ID);

            if (claim != null)
            {
                Player player = montyHallGameRepository.getOngoingSession(claim.Value).player;
                int stage = montyHallGameRepository.getOngoingSessionStage(player);
                if (stage == 4)
                {
                    bool sucess = montyHallGameRepository.declareWinner(player);
                    result.Append(String.Format("Player {0}, you {1} !", player.PersonalId, sucess == true ? "Won" : "Lost"));
                    return result.ToString();
                }
                else
                {
                    logger.LogDebug(String.Format("Player {0}, Player is on stage {1} can not not allowed to perfrom this action", player.PersonalId, stage));
                    return BadRequest();
                }
            }
            else
            {
                logger.LogDebug("Token did not contain personalId");
                return Unauthorized();
            }

        }

    }
}
