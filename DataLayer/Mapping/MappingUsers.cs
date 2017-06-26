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

        // --- SELECT ---

        // SELECT all
        public List<User> SelectAll()
        {
            string query = "SELECT * FROM Users";

            return Select(new SqlCommand(query));
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

        //Handle SELECT request
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
    }
}