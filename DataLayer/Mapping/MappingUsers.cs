using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataBase;
using DataLayer.Containers;
using DataLayer.Database;

namespace DataLayer.Mapping
{
    // User mapping
    public class MappingUsers
    {
        private iBddConnection _bdd;

        public MappingUsers()
        {
            _bdd = SQLConnection.Instance();
        }

        // SELECT all
        public List<User> SelectAll()
        {
            string query = "SELECT * FROM Users";

            return Select(new SqlCommand(query));
        }

        // SELECT by username
        public List<User> SelectByUsername(string username)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE username = @USERNAME");
            command.Parameters.AddWithValue("USERNAME", username);

            return Select(command);
        }

        // Check existance
        public bool Exist(int id)
        {
            string query = "SELECT * FROM Users WHERE id = @ID";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("ID", id);

            if (_bdd.SelectRows(command).Rows.Count > 0) return true;
            else return false;
        }

        // Check if username exist
        public bool UsernameExist(string username)
        {
            string query = "SELECT * FROM Users WHERE username = @USERNAME";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("USERNAME", username);

            if (_bdd.SelectRows(command).Rows.Count > 0) return true;
            else return false;
        }

        // Check if email exist
        public bool EmailExist(string email)
        {
            string query = "SELECT * FROM Users WHERE email = @EMAIL";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("EMAIL", email);

            if (_bdd.SelectRows(command).Rows.Count > 0) return true;
            else return false;
        }

        public string getUserHashedPassword(string username)
        {
            string query = "SELECT password FROM Users WHERE username = @USERNAME";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("USERNAME", username);

            DataRowCollection rows = _bdd.SelectRows(command).Rows;

            try
            {
                return rows[0]["password"].ToString();
            }
            catch (InvalidOperationException)
            {
                InvalidOperationException ex = new InvalidOperationException("Can't find user");
                throw ex;
            }
        }

        // Handle SELECT request
        private List<User> Select(SqlCommand command)
        {
            List<User> users = new List<User>();
            User user;

            // Mappage de la DataTable récupérée dans une liste d'objet Plan
            foreach (DataRow row in _bdd.SelectRows(command).Rows)
            {
                user = new User(int.Parse(row["id"].ToString()),
                                row["username"].ToString(),
                                row["email"].ToString(),
                                row["password"].ToString(),
                                row["tokenUser"].ToString()
                );

                users.Add(user);
            }

            return users;
        }

        // INSERT INTO
        public int Insert(User user)
        {
            string query = "INSERT INTO Users (username, email, password) VALUES (@USERNAME, @EMAIL, @PASSWORD)";
            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("USERNAME", user.Username);
            command.Parameters.AddWithValue("EMAIL", user.Email);
            command.Parameters.AddWithValue("PASSWORD", user.Password);

            try
            {
                return _bdd.ActionsOnRows(command);
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }

        // Update tokenUser
        public int UpdateTokenUser(int id, string tokenUser)
        {
            string query = "UPDATE Users SET tokenUser = @TOKEN WHERE id = @ID";
            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("TOKEN", tokenUser);
            command.Parameters.AddWithValue("ID",id);

            try
            {
                return _bdd.ActionsOnRows(command);
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }
    }
}