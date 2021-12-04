using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TeamSportApi.Models;

namespace TeamSportApi.DataBase
{
    public class PlayerDataBase
    {
        //STRING DE CONEXÃO COM O BANCO DE DADOS
        string connectionString =
               "Server=DESKTOP-USC4FN3\\SQLEXPRESS01;DataBase=Teams;User=sa;Integrated Security=SSPI";
        public List<Player> GetPlayerDatabase()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from Player";
                List<Player> players = new List<Player>();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
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
                con.Close();
                return players;
            }
        }

        public Player ReturnPlayerById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    Player player = new Player();
                    string returnById = $"select * from player where  id = {id}";
                    SqlCommand cmd = new SqlCommand(returnById, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        player.Id = Convert.ToInt32(rdr["Id"]);
                        player.Name = rdr["Name"].ToString();
                        player.Position = rdr["Position"].ToString();
                        player.BirthDate = (DateTime)rdr["BirthDate"];
                        player.Age = Convert.ToInt32(rdr["Age"]);
                        player.Nationality = rdr["Nationality"].ToString();
                    }
                    con.Close();
                    return player;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Player ReturnPlayerByName(string name)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    Player player = new Player();
                    string returnByName = $"select * from Player where Name = '{name}'";
                    SqlCommand cmd = new SqlCommand(returnByName, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        player.Id = Convert.ToInt32(rdr["Id"]);
                        player.Name = rdr["Name"].ToString();
                        player.Position = rdr["Position"].ToString();
                        player.BirthDate = (DateTime)rdr["BirthDate"];
                        player.Age = Convert.ToInt32(rdr["Age"]);
                        player.Nationality = rdr["Nationality"].ToString();
                    }
                    con.Close();

                    return player;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Player> ReturnPlayerByString(string search)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<Player> players = new List<Player>();
                string returnString = $"Select * from player where name like'%{search}%'";
                SqlCommand cmd = new SqlCommand(returnString, con);
                cmd.CommandType = CommandType.Text;
                con.Open();

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
                con.Close();

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


        public void UpdatePlayerDataBase(Player player)

        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string ComandoSql = $"Update Player set Name = @Name, Position = @Position, BirthDate = @BirthDate, Age = @Age, Nationality = @Nationality where id = {player.Id}";
                    SqlCommand cmd = new SqlCommand(ComandoSql, connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", player.Name);
                    cmd.Parameters.AddWithValue("@Position", player.Position);
                    cmd.Parameters.AddWithValue("@BirthDate", player.BirthDate);
                    cmd.Parameters.AddWithValue("@Age", player.Age);
                    cmd.Parameters.AddWithValue("@Nationality", player.Nationality);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void DeletePlayerDataBase(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string DeleteId = $"Delete  from Player where Id = {id}";
                SqlCommand cmd = new SqlCommand(DeleteId, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                con.Close();
            }
        }
        //MÉTODO PARA VERIFICAR SE O PLAYER EXISTE, PASSANDO ID.
        public bool VerifyPlayerExists(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string queryId = $"Select Id,Name,Position,BirthDate,Age,Nationality from Player where id = {id} ";
                    SqlCommand cmd = new SqlCommand(queryId, con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    Player player = new Player();

                    while (rdr.Read())
                    {
                        player.Id = Convert.ToInt32(rdr["Id"]);
                        player.Name = rdr["Name"].ToString();
                        player.Position = rdr["Position"].ToString();
                        player.BirthDate = (DateTime)rdr["BirthDate"];
                        player.Age = Convert.ToInt32(rdr["Age"]);
                        player.Nationality = rdr["Nationality"].ToString();
                    }
                    if (player.Id != 0)
                        return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerifyPlayerExistsByName(string name)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string queryName = $"Select * from player where name = '{name}' ";
                SqlCommand cmd = new SqlCommand(queryName, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                Player player = new Player();

                while (rdr.Read())
                {
                    player.Id = Convert.ToInt32(rdr["Id"]);
                    player.Name = rdr["Name"].ToString();
                    player.Position = rdr["Position"].ToString();
                    player.BirthDate = (DateTime)rdr["BirthDate"];
                    player.Age = Convert.ToInt32(rdr["Age"]);
                    player.Nationality = rdr["Nationality"].ToString();
                }
                if (player.Name != null)
                    return true;
                return false;
            }
        }
        public bool VerifyPlayerExistsByString(string search)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string returnString = $"Select * from Player where name like '%{search}%'";
                SqlCommand cmd = new SqlCommand(returnString, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                Player player = new Player();

                while (rdr.Read())
                {
                    player.Id = Convert.ToInt32(rdr["Id"]);
                    player.Name = rdr["Name"].ToString();
                    player.Position = rdr["Position"].ToString();
                    player.BirthDate = (DateTime)rdr["BirthDate"];
                    player.Age = Convert.ToInt32(rdr["Age"]);
                    player.Nationality = rdr["Nationality"].ToString();
                }
                if (player.Name != null)
                    return true;
                return false;
            }
        }
    }
}
