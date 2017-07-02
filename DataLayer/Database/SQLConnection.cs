using DataLayer.DataBase;
using DataLayer.DataFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    class SQLConnection : iBddConnection
    {
        // Singleton
        private static SQLConnection _instance = null;
        private static readonly object _padlock = new object();

        // IDisposable
        private bool _disposed = false;

        // Connection informations
        private string _connectionString;
        private SqlConnection _bddConnection;
        private SqlDataAdapter _bddDataAdapter;
        private DataTable _datas;

        // Constructor
        private SQLConnection()
        {
            _connectionString = @"Server=DESKTOP-DB8IQ4L;Database=Gen;Integrated Security=SSPI;";
            _bddConnection = new SqlConnection();
            Connect();
        }

        // Get unique instance
        public static SQLConnection Instance()
        {
            if (_instance == null)
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new SQLConnection();
                    }
                }
            }
            return _instance;
        }

        // Connect to the database
        public bool Connect()
        {
            if (_bddConnection.State == ConnectionState.Open)
            {
                _bddConnection.Close();
            }

            try
            {
                _bddConnection.ConnectionString = _connectionString;
                _bddConnection.Open();
            }
            catch (SqlException ex)
            {
                LogSQLExceptions("Données - Connection à la BDD SQL", ex);
                return false;
            }

            return true;
        }

        // SELECT request
        public DataTable SelectRows(SqlCommand command)
        {
            try
            {
                command.Connection = _bddConnection;
                _bddDataAdapter = new SqlDataAdapter(command);
                _datas = new DataTable();
                _bddDataAdapter.Fill(_datas);
            }
            catch (SqlException ex)
            {
                LogSQLExceptions("Données - Select sur BDD SQL", ex);
            }

            return _datas;
        }

        // 'INSERT', 'UPDATE', etc... request
        public int ActionsOnRows(SqlCommand command)
        {
            try
            {
                command.Connection = _bddConnection;

                return command.ExecuteNonQuery();
            }
            // Si un problème survient
            catch (SqlException ex)
            {
                LogSQLExceptions("Données - Actions sur BDD SQL", ex);
                return 0;
            }
        }

        // Gère une exception avec la base de donnée. L'erreur est écrite dans les logs
        private void LogSQLExceptions(string source, SqlException exception)
        {
            string errorMessages = "";
            BddLogger logger = BddLogger.Instance();

            for (int i = 0; i < exception.Errors.Count; i++)
            {
                errorMessages = "Index #" + i +
                                " / Message: " + exception.Errors[i].Message +
                                " / Source: " + exception.Errors[i].Source;

                logger.WriteLog("Source : " + source + " : " + errorMessages);
            }

            Console.WriteLine("An exception occurred. Please contact your system administrator.");
        }

        //--- Méthods IDisposable ---
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SQLConnection()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_bddConnection != null) _bddConnection.Dispose();
            }

            _disposed = true;
        }
    }
}
