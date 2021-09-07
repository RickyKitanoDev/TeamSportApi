using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamSportApi.Models;

namespace TeamSportApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpPost("savePlayer")]
        public Player ReturnPlayer([FromBody] Player newPlayer)
        {
            var player = new Player();
            player.Id = newPlayer.Id;
            player.Name = newPlayer.Name;
            player.Position = newPlayer.Position;
            player.BirthDate = newPlayer.BirthDate;
            player.Age = newPlayer.Age;
            player.Nationality = newPlayer.Nationality;

            return player;
               
                
        }
    }
}
