using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

            // salvar os dados

            return player;

        }

        [HttpGet("getplayer")]
        public List<Player> GetPlayer()
        {
            try { 
                string connectionString =
               "Server=DESKTOP-USC4FN3\\SQLEXPRESS01;DataBase=Teams;User=sa;Integrated Security=SSPI";

                string query = "select * from Player";
                //string queryName = "select [Name] from Player";

                List<Player> players = new List<Player>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Player player = new Player();
                        player.Id = Convert.ToInt32(rdr["Id"]);
                        player.Name = rdr["Name"].ToString();
                        player.Position = rdr["Position"].ToString();
                        player.BirthDate = (DateTime)rdr["BirthDate"];
                        player.Age = Convert.ToInt32(rdr["Age"]);
                        player.Nationality = rdr["Nationality"].ToString();

                        players.Add(player);
                    }
                    connection.Close();

                    return players;
            }

            }catch(Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }
    }
}
