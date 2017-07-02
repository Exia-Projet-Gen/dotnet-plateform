using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Containers
{
    // Classe représentant un classeur
    public class File
    {
        // User id
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        // Filename
        private string _filename;

        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        // Username
        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        // State
        private string _state;

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        // Text
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        // Key
        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        // Percent
        private double _percent;

        public double Percent
        {
            get { return _percent; }
            set { _percent = value; }
        }

        // Email
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public File()
        {
            _id = 0;
        }

        public File(string filename, string state)
        {
            _filename = filename;
            _state = state;
        }


        public File(string filename, string username, string state)
        {
            _filename = filename;
            _username = username;
            _state = state;
        }

        public File(int id, string filename, string username, string state)
        {
            _id = id;
            _filename = filename;
            _username = username;
            _state = state;
        }

        public File(int id, string filename, string username, string state, string text, string key, double percent, string email)
        {
            _id = id;
            _filename = filename;
            _username = username;
            _state = state;
            _text = text;
            _key = key;
            _percent = percent;
            _email = email;
        }

        public File(string filename, string username, string state, string text, string key, double percent, string email)
        {
            _filename = filename;
            _username = username;
            _state = state;
            _text = text;
            _key = key;
            _percent = percent;
            _email = email;
        }

        public override string ToString()
        {
            return _id + " " +
                   _filename + " " +
                   _username + " " +
                   _state + " " +
                   _text + " " +
                   _key + " " +
                   _percent + " " +
                   _email
            ;
        }
    }
}
