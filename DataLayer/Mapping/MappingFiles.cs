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
    // File mapping
    public class MappingFiles
    {
        private iBddConnection _bdd;

        public MappingFiles()
        {
            _bdd = SQLConnection.Instance();
        }

        // SELECT all
        public List<File> SelectAll()
        {
            string query = "SELECT * FROM Files";

            return Select(new SqlCommand(query));
        }

        // SELECT by username and filename
        public List<File> SelectByUserAndFile(string filename, string username)
        {
            string query = "SELECT * FROM Files WHERE filename = @FILENAME AND username = @USERNAME";
            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("FILENAME", filename);
            command.Parameters.AddWithValue("USERNAME", username);

            return Select(command);
        }

        // SELECT all users' files
        public List<File> SelectAllFile(string username)
        {
            string query = "SELECT filename, state FROM Files WHERE username = @USERNAME";
            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("USERNAME", username);

            List<File> files = new List<File>();
            File file;

            // Mappage de la DataTable récupérée dans une liste d'objet Plan
            foreach (DataRow row in _bdd.SelectRows(command).Rows)
            {
                file = new File(row["filename"].ToString(),
                                row["state"].ToString()
                );

                files.Add(file);
            }

            return files;
        }

        // Handle SELECT request
        private List<File> Select(SqlCommand command)
        {
            List<File> files = new List<File>();
            File file;

            // Mappage de la DataTable récupérée dans une liste d'objet Plan
            foreach (DataRow row in _bdd.SelectRows(command).Rows)
            {
                file = new File(int.Parse(row["id"].ToString()),
                                row["filename"].ToString(),
                                row["username"].ToString(),
                                row["state"].ToString()
                                
                );

                if(file.State == "finish")
                {
                    file.Text = row["text"].ToString();
                    file.Key = row["bfkey"].ToString();
                    file.Percent = double.Parse(row["bfpercent"].ToString());
                    file.Email = row["email"].ToString();
                }

                files.Add(file);
            }

            return files;
        }


        // Check if file exist for user
        public bool FileExistForUser(string filename, string username)
        {
            string query = "SELECT * FROM Files WHERE filename = @FILENAME AND username = @USERNAME";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("FILENAME", filename);
            command.Parameters.AddWithValue("USERNAME", username);

            if (_bdd.SelectRows(command).Rows.Count > 0) return true;
            else return false;
        }

        // INSERT INTO
        public int Insert(File file)
        {
            string query = "INSERT INTO Files (filename, username, state) VALUES (@FILENAME, @USERNAME, @STATE)";
            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("FILENAME", file.Filename);
            command.Parameters.AddWithValue("USERNAME", file.Username);
            command.Parameters.AddWithValue("STATE", file.State);

            try
            {
                return _bdd.ActionsOnRows(command);
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }

        // Clean file
        public int CleanFile(string filename, string username, string state)
        {
            string query = "UPDATE Files SET state = @STATE, bfkey = NULL, text = NULL, bfpercent = NULL, email = NULL WHERE filename = @FILENAME AND username = @USERNAME";
            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("FILENAME", filename);
            command.Parameters.AddWithValue("USERNAME", username);
            command.Parameters.AddWithValue("STATE", state);

            try
            {
                return _bdd.ActionsOnRows(command);
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }

        // Clean file
        public int FinishBF(File file)
        {
            string query = "UPDATE Files SET state = @STATE, text = @TEXT, bfkey = @KEY, bfpercent = @PERCENT, email = @EMAIL WHERE filename = @FILENAME AND username = @USERNAME";
            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("STATE", file.State);
            command.Parameters.AddWithValue("TEXT", file.Text);
            command.Parameters.AddWithValue("KEY", file.Key);
            command.Parameters.AddWithValue("PERCENT", file.Percent.ToString().Replace(',', '.'));
            command.Parameters.AddWithValue("EMAIL", file.Email);

            command.Parameters.AddWithValue("FILENAME", file.Filename);
            command.Parameters.AddWithValue("USERNAME", file.Username);

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