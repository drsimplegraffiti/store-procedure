
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using StoreProcedureApi.Models;

namespace StoreProcedureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _connection;
        public TeamController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        
        [HttpPost]
        public string Post([FromBody] Team team)
        {
            string msg = String.Empty;
            if (team != null)
            {
                SqlCommand cmd = new SqlCommand("usp_AddTeam", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", team.FirstName);
                cmd.Parameters.AddWithValue("@LastName", team.LastName);
                cmd.Parameters.AddWithValue("@Email", team.Email);
                cmd.Parameters.AddWithValue("@Password", team.Password);
                cmd.Parameters.AddWithValue("@Age", team.Age);
                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();

                if (i > 0)
                {
                    msg = "Data inserted";
                }
                else
                {
                    msg = "Error"; 
                }
            }

            return msg;
        }

        [HttpGet]
        public List<Team> GetAllTeam()
        {
            SqlDataAdapter da = new SqlDataAdapter("usp_GetAllTeam", _connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Team> lstTeam = new List<Team>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Team tm = new Team();
                    tm.FirstName = dt.Rows[i]["FirstName"].ToString();
                    tm.LastName = dt.Rows[i]["LastName"].ToString();
                    tm.Email = dt.Rows[i]["Email"].ToString();
                    tm.Age = (int)dt.Rows[i]["Age"];
                    tm.Password = dt.Rows[i]["Password"].ToString();
                    tm.Id = Convert.ToInt32(dt.Rows[i]["Id"]); // Assuming there's an "Id" column
                    lstTeam.Add(tm);
                }
            }

            if (lstTeam.Count > 0)
            {
                return lstTeam;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public Team GetById(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("usp_GetTeamById", _connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id",id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Team tm = new Team();
            if (dt.Rows.Count > 0)
            {
                
                tm.FirstName = dt.Rows[0]["FirstName"].ToString();
                tm.LastName = dt.Rows[0]["LastName"].ToString();
                tm.Email = dt.Rows[0]["Email"].ToString();
                tm.Age = (int)dt.Rows[0]["Age"];
                tm.Password = dt.Rows[0]["Password"].ToString();
                tm.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
            }

            if (tm != null)
            {
                return tm;
            }
            else
            {
                return null;
            }
        }

        [HttpPut("{id}")]
        public string UpdateTeamById([FromBody] Team team, int id)
        {
            string msg = String.Empty;
            if (team != null)
            {
                SqlCommand cmd = new SqlCommand("usp_UpdateTeam", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FirstName", team.FirstName);
                cmd.Parameters.AddWithValue("@LastName", team.LastName);
                cmd.Parameters.AddWithValue("@Email", team.Email);
                cmd.Parameters.AddWithValue("@Password", team.Password);
                cmd.Parameters.AddWithValue("@Age", team.Age);
                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();

                if (i > 0)
                {
                    msg = "Data Updated";
                }
                else
                {
                    msg = "Error"; 
                }
            }

            return msg;
        }

        [HttpDelete("{id}")]
        public string DeleteTeamById(int id)
        {
            string msg = String.Empty;
           
                SqlCommand cmd = new SqlCommand("usp_DeleteTeam", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();

                if (i > 0)
                {
                    msg = "Data Deleted";
                }
                else
                {
                    msg = "Error"; 
                }

            return msg;
        }
      
    }
}