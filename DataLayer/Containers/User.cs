using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Containers
{
    // Classe représentant un classeur
    public class User
    {
        // User id
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        // Username
        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        // Email
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        // Password
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        // TokenUser
        private string _tokenUser;

        public string TokenUser
        {
            get { return _tokenUser; }
            set { _tokenUser = value; }
        }

        public User()
        {
            _id = 0;
            _username = null;
        }

        public User(string username, string email, string password)
        {
            _username = username;
            _email = email;
            _password = password;
        }

        public User(int id, string username, string email, string password, string tokenUser)
        {
            _id = id;
            _username = username;
            _email = email;
            _password = password;
            _tokenUser = tokenUser;
        }

        public override string ToString()
        {
            return _id + " " +
                   _username + " " +
                   _email + " " +
                   _password + " " +
                   _tokenUser
            ;
        }
    }
}
