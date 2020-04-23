using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Fagschlunger_Camping.Models.db
{
    public class DBBase : IDBBase
    {
        public string _connectionString = "Server=localhost;Database=db_camping;Uid=root;Pwd=01122001SvO";
        public MySqlConnection _connection;


        public void Open()
        {
            if (this._connection == null)
            {
                this._connection = new MySqlConnection(this._connectionString);
            }
            if (this._connection.State != ConnectionState.Open)
            {
                this._connection.Open();
            }
        }

        public void Close()
        {
            if ((this._connection != null) && (this._connection.State != ConnectionState.Closed))
            {
                this._connection.Close();
            }
        }
    }
}