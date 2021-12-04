using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TeamSportApi.Models;

namespace TeamSportApi.DataBase
{
    public class CoachDataBase
    {
        string connectionString = "Server=DESKTOP-USC4FN3\\SQLEXPRESS01;DataBase=Teams;User=sa;Integrated Security=SSPI";
        public List<Coach> GetCoachsDataBase()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string coachString = "Select * from Coach";
                List<Coach> coachs = new List<Coach>();
                SqlCommand cmd = new SqlCommand(coachString, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Coach coach = new Coach();
                    coach.Id = Convert.ToInt32(rdr["Coach_Id"]);
                    coach.Name = rdr["Name"].ToString();
                    coach.Nationality = rdr["Nationality"].ToString();
                    coach.BirthDate = (DateTime)rdr["BirthDate"];
                    coach.Age = Convert.ToInt32(rdr["Age"]);

                    coachs.Add(coach);
                }
                con.Close();
                return coachs;
            }
        }
        public Coach GetCoachById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                Coach coach = new Coach();
                string coachId = $"select * from coach where Coach_Id = {id}";
                SqlCommand cmd = new SqlCommand(coachId, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    coach.Id = Convert.ToInt32(rdr["Coach_Id"]);
                    coach.Name = rdr["Name"].ToString();
                    coach.Nationality = rdr["Nationality"].ToString();
                    coach.BirthDate = (DateTime)rdr["BirthDate"];
                    coach.Age = Convert.ToInt32(rdr["Age"]);
                }
                con.Close();
                return coach;
            }
        }

        public bool VerifyCoachExists(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                Coach coach = new Coach();
                string coachData = $"select * from coach where Coach_Id = {id}";
                SqlCommand cmd = new SqlCommand(coachData, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    coach.Id = Convert.ToInt32(rdr["Coach_Id"]);
                    coach.Name = rdr["Name"].ToString();
                    coach.Nationality = rdr["Nationality"].ToString();
                    coach.BirthDate = (DateTime)rdr["BirthDate"];
                    coach.Age = Convert.ToInt32(rdr["Age"]);
                }
                con.Close();

                if (coach.Id != 0)
                    return true;
                return false;
            }
        }

        public Coach GetCoachByName(string name)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                Coach coach = new Coach();
                string coachName = $"select * from coach where Name = '{name}'";
                SqlCommand cmd = new SqlCommand(coachName, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    coach.Id = Convert.ToInt32(rdr["Coach_Id"]);
                    coach.Name = rdr["Name"].ToString();
                    coach.Nationality = rdr["Nationality"].ToString();
                    coach.BirthDate = (DateTime)rdr["BirthDate"];
                    coach.Age = Convert.ToInt32(rdr["Age"]);
                }
                con.Close();
                return coach;
            }
        }
        public bool VerifyCoachExistsName(string name)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                Coach coach = new Coach();
                string coachName = $"select * from coach where name = '{name}'";
                SqlCommand cmd = new SqlCommand(coachName, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    coach.Id = Convert.ToInt32(rdr["Coach_Id"]);
                    coach.Name = rdr["Name"].ToString();
                    coach.Nationality = rdr["Nationality"].ToString();
                    coach.BirthDate = (DateTime)rdr["BirthDate"];
                    coach.Age = Convert.ToInt32(rdr["Age"]);
                }
                con.Close();
                if (coach.Name != null)
                    return true;
                return false;
            }
        }
    }
}
