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
        private readonly IMontyHallGameRepository montyHallGameRepository;

        private readonly ILogger<MontyHallController> Logger;

        public MontyHallController (IMontyHallGameRepository montyHallGameRepository, ILogger<MontyHallController> logger)
        {
            this.montyHallGameRepository = montyHallGameRepository;
            Logger = logger;
        }

        // GET api/montyHall
        [HttpGet]
        [Authorize]
        //[ProducesResponseType((int)System.Net.HttpStatusCode.BadRequest)]
        //[ProducesResponseType((int)System.Net.HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<string>> Get()
        {
            var currentUser = HttpContext.User;

            Claim claim = currentUser.Claims.FirstOrDefault(c => c.Type == "personalId");
            if (claim != null)
            {
                Player player = new Player(claim.Value);
                MontyHallGameSession montyHallGameSession = new MontyHallGameSession(player);

                System.Text.StringBuilder result = new System.Text.StringBuilder();
                result.Append(String.Format("Player {0} with ", player.PersonalId));

                foreach (Door door in montyHallGameSession.doorList)
                {
                    result.Append(door.ToString());
                    result.Append(", ");
                }
                return new string[] { result.ToString() };
            } else
            {
                Logger.LogDebug("Token did not contain personalId");
                return Unauthorized();
            }
        }

        // GET api/montyHall/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/montyHall
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/montyHall/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/montyHall/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
