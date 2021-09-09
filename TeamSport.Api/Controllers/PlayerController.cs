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
        string connectionString =
               "Server=DESKTOP-USC4FN3\\SQLEXPRESS01;DataBase=Teams;User=sa;Integrated Security=SSPI";

        [HttpGet("getplayer")]
        public ActionResult<List<Player>> GetPlayer()
        {
            var players = GetPlayerDatabase();
            if (players == null)
                return BadRequest(new { result = new List<Player>(), Message = "Não foi encontrado nenhum jogador", temJogadores = false });
            return Ok(new { result = players, message = "Jogadores encontrados", temJogadores = true });

        }

        [HttpPost("addplayer")]
        public ActionResult<Player> AddPlayer([FromBody] Player player)
        {
            AddPlayerDatabase(player);
            if (player.Name == "" || player.Position == "" || player.BirthDate == null || player.Age == 0 || player.Nationality == "")
                return BadRequest(new { message = "Favor preencher todos os dados corretamente." });
            return Ok(player);
        }

        public List<Player> GetPlayerDatabase()
        {
            //try { 


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
        }

        public void AddPlayerDatabase(Player player)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string comandoSql = "insert into Player(Name, Position, BirthDate, Age, Nationality) VALUES(@Name, @Position, @BirthDate, @Age, @Nationality)";
                    SqlCommand cmd = new SqlCommand(comandoSql, connection);
                    cmd.CommandType = CommandType.Text;

                    //cmd.Parameters.AddWithValue("Id", player.Id);
                    cmd.Parameters.AddWithValue("Name", player.Name);

                    cmd.Parameters.AddWithValue("Position", player.Position);
                    cmd.Parameters.AddWithValue("BirthDate", player.BirthDate);
                    cmd.Parameters.AddWithValue("Age", player.Age);
                    cmd.Parameters.AddWithValue("Nationality", player.Nationality);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
