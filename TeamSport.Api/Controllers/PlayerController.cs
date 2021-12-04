using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TeamSportApi.DataBase;
using TeamSportApi.Models;

namespace TeamSportApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {   
        private static readonly PlayerDataBase dataBase = new PlayerDataBase();
   
        //MÉTODO PARA RETORNAR TODOS OS JOGADORES.
        [HttpGet("getplayers")]
        public ActionResult<List<Player>> GetPlayers()
        {
            var players = dataBase.GetPlayerDatabase();
            if (players.Count == 0)
                return BadRequest(new { result = new List<Player>(), Message = "Não foi encontrado nenhum jogador", temJogadores = false });
            return Ok(new { result = players, message = $"Jogadores encontrados = {players.Count}", temJogadores = true });
        }

        //MÉTODO PARA RETORNAR UM JOGADOR, BUSCANDO PELO ID.
        [HttpGet("{id}")]
        public ActionResult GetPlayerId(int id)
        
        {
            var playerId = dataBase.ReturnPlayerById(id);
            bool playerExist = dataBase.VerifyPlayerExists(id);
            if (playerExist == true)            
                return Ok(playerId);           
            return BadRequest(new { message = "Favor informar um Id de um jogador existente" });
        }

        //MÉTODO PARA BUSCAR JOGADOR PELO NOME
        [HttpGet("getname/{name}")]
        public ActionResult GetPlayerName(string name)
        {
            Player playerName = dataBase.ReturnPlayerByName(name);
            bool playerExistName = dataBase.VerifyPlayerExistsByName(name);
            if (playerExistName == true)             
                return Ok(playerName);          
            return BadRequest(new { message = "Favor Informar um Nome de um jogador existente" });
        }

        //MÉTODO PARA BUSCAR JOGADOR USANDO PARTE DE STRING
        [HttpGet("getstring/{search}")]
        public ActionResult GetPlayerString(string search)
        {
            List<Player> playerString = dataBase.ReturnPlayerByString(search);
            bool playerExistString = dataBase.VerifyPlayerExistsByString(search);
            if(playerExistString == true)               
              return Ok(new { result = playerString, message = $"Jogadores encontrados = {playerString.Count}"});
            return BadRequest(new { message = "Favor verificar o dado inserido na pesquisa, pois não consta no banco de dados" });                
        }

        //MÉTODO PARA ADICIONAR UM JOGADOR.
        [HttpPost("addplayer")]
        public ActionResult<Player> AddPlayer([FromBody] Player player)
        {
            dataBase.AddPlayerDatabase(player);
            if (player.Name == "" || player.Position == "" || player.BirthDate == null || player.Age == 0 || player.Nationality == "")
                return BadRequest(new { message = "Favor preencher todos os dados corretamente." });
            return Ok(player);
        }

        //MÉTODO PARA ALTERAR AS INFORMAÇÕES DO JOGADOR, PASSANDO O ID.
        [HttpPut("{id}")]

        public ActionResult UpdatePlayer(int id, [FromBody] Player player)
        {
            if (id != player.Id)
                return BadRequest(new { message = $"Ids informados estão divergentes, Id informado na Url = {id}, Id informado na requisição = {player.Id} " });
            bool playerExist = dataBase.VerifyPlayerExists(id);
            if (playerExist == true)
            {
                dataBase.UpdatePlayerDataBase(player);
                return Ok(player);
            }
            return BadRequest(new { message = "Favor informar um Id de um jogador existente" });
        }

        //MÉTODO PARA DELETAR UM JOGADOR, PASSANDO O ID.
        [HttpDelete("{id}")]
        public ActionResult DeletePlayer(int id)
        {
            bool PlayerIdExist = dataBase.VerifyPlayerExists(id);
            if (PlayerIdExist == true)
            {
                dataBase.ReturnPlayerById(id);
                var playerReturn = dataBase.ReturnPlayerById(id);
                dataBase.DeletePlayerDataBase(id);
                return Ok(playerReturn);
            }
            return BadRequest(new { message = "Favor informar um Id de um jogador existente" });
        }
    }
}
