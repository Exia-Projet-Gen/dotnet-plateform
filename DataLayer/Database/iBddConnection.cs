using System;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer.DataBase
{
    // Interface pour les classes de connection à une base de donnée
    public interface iBddConnection : IDisposable
    {
        // Requête 'SELECT'
        DataTable SelectRows(SqlCommand command);

        // Requête 'INSERT', 'UPDATE', etc...
        int ActionsOnRows(SqlCommand command);
    }
}
