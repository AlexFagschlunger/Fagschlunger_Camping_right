using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Fagschlunger_Camping.Models.db
{
    public class RepositoryReservierungDB : IRepositoryReservierung
    {
        private string _connectionString = "Server=localhost;Database=db_einfuehrung;Uid=root;Pwd=01122001SvO";
        private MySqlConnection _connection;


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

        public bool Delete(int id)
        {
            // Command erzeugen - mit Parameter (SQL-Injections verhindern)
            DbCommand cmdDel = this._connection.CreateCommand();
            cmdDel.CommandText = "DELETE FROM users WHERE id=@userId";

            //Parameter belegen
            DbParameter paramId = cmdDel.CreateParameter();
            paramId.ParameterName = "userId";
            paramId.Value = id;
            paramId.DbType = DbType.Int32;

            // Parameter dem Command hinzufügen
            cmdDel.Parameters.Add(paramId);

            // Command ausführen
            return cmdDel.ExecuteNonQuery() == 1; // gibt true zurück, falls genau ein User gelöscht wurde ansonsten false
        }

        public List<Reservierung> GetAllUser()
        {
            List<Reservierung> users = new List<Reservierung>();


            DbCommand cmdSelect = this._connection.CreateCommand();
            cmdSelect.CommandText = "SELECT * FROM users";

            // ExecuteReader() wird immer bei SELECT- Abfragen benötigt mit ihm kann man sich zeileinweise durch das erhaltene Ergebnis bewegen
            using (DbDataReader reader = cmdSelect.ExecuteReader())
            {
                // mit Read() wird der nächste Datensatz (User) gelesen
                while (reader.Read())
                {
                    users.Add(new Reservierung
                    {
                        // Id ... so lautet das feld in der Klasse User
                        // "id" ... so lautet der Spaltenname in der Datenbanktabelle users

                        ID = Convert.ToInt32(reader["id"]),
                        Firstname = Convert.ToString(reader["firstname"]),
                        Lastname = Convert.ToString(reader["lastname"]),
                        Ankunftsdatum = Convert.ToDateTime(reader["ankunftsdatum"]),
                        Abreisedatum = Convert.ToDateTime(reader["abreisedatum"]),
                        Personen = Convert.ToInt32(reader["personen"]),
                    });
                }
            }
            return users;
        }

        public Reservierung GetUser(int id)
        {
            DbCommand cmdGetUser = this._connection.CreateCommand();
            cmdGetUser.CommandText = "SELECT * FROM users WHERE id=@uid";

            DbParameter paramId = cmdGetUser.CreateParameter();
            paramId.ParameterName = "uid";
            paramId.Value = id;
            paramId.DbType = DbType.Int32;

            cmdGetUser.Parameters.Add(paramId);

            // bei SELECT-Abfragen müssen wir immer ExecuteReader() aufrufen
            using (DbDataReader reader = cmdGetUser.ExecuteReader())
            {
                // keine Zeile (Datensatz) vorhanden
                if (!reader.HasRows)
                {
                    // null wird zurückgeliefert
                    return null;
                }

                // dieser Aufruf ist notwendig, da damit der erste DAtensatz gelesen wird
                // Cursor zeigt zu Beginn "vor" alle Datensätze
                reader.Read();
                return new Reservierung
                {
                    // ID ... so lautet das feld in der Klasse User
                    // "id" ... so lautet der Spaltenname in der Datenbanktabelle users

                    ID = Convert.ToInt32(reader["id"]),
                    Firstname = Convert.ToString(reader["firstname"]),
                    Lastname = Convert.ToString(reader["lastname"]),
                    Ankunftsdatum = Convert.ToDateTime(reader["ankunftsdatum"]),
                    Abreisedatum = Convert.ToDateTime(reader["abreisedatum"]),
                    Personen = Convert.ToInt32(reader["personen"]),
                };
            }
        }

        public bool Insert(Reservierung user)
        {
            // 1. Parameter überprüfen
            if (user == null)
            {
                return false;
            }

            // ein leeres SQL-Comamnd erzeugen
            DbCommand cmdInsert = this._connection.CreateCommand();
            // @firstname, @lastname, ... Paramter => verhindern SQL-Injections
            // müssen immer verwendet werden wenn es sich um Daten des Benutzers handelt
            // @ firstname ... firstname kann beliebig benannt werden
            cmdInsert.CommandText = "Insert Into users Values(null, @firstname, @lastname, @ankunftsdatum, @abreisedatum, @personen)";

            // Parameter erzeugt
            DbParameter paramFN = cmdInsert.CreateParameter();
            paramFN.ParameterName = "firstname";
            paramFN.Value = user.Firstname;
            paramFN.DbType = DbType.String;

            DbParameter paramLN = cmdInsert.CreateParameter();
            paramLN.ParameterName = "lastname";
            paramLN.Value = user.Lastname;
            paramLN.DbType = DbType.String;

            DbParameter paramAnkunft = cmdInsert.CreateParameter();
            paramAnkunft.ParameterName = "ankunftsdatum";
            paramAnkunft.Value = user.Ankunftsdatum;
            paramAnkunft.DbType = DbType.Date;

            DbParameter paramAbreise = cmdInsert.CreateParameter();
            paramAbreise.ParameterName = "abreisedatum";
            paramAbreise.Value = user.Abreisedatum;
            paramAbreise.DbType = DbType.Date;

            DbParameter paramPerson = cmdInsert.CreateParameter();
            paramPerson.ParameterName = "personen";
            paramPerson.Value = user.Personen;
            paramPerson.DbType = DbType.Int32;

            // Parameteer mit dem Comamnd verbinden
            cmdInsert.Parameters.Add(paramFN);
            cmdInsert.Parameters.Add(paramLN);
            cmdInsert.Parameters.Add(paramAnkunft);
            cmdInsert.Parameters.Add(paramAbreise);
            cmdInsert.Parameters.Add(paramPerson);


            // ExecuteNonQuery() ... wird bein INSERT, UPDATE, und DELETE verwendet diese Methode liefert die ANzahl der betroffenen Datensätze zurück
            return cmdInsert.ExecuteNonQuery() == 1;




        }

        public bool UpdateUserData(int id, Reservierung newUserData)
        {
            DbCommand cmdUpdate = this._connection.CreateCommand();
            cmdUpdate.CommandText = "UPDATE users SET firstname=@firstname, lastname =@lastname, + " +
                "gender=@gender, birthdate=@birthdate, username=@username, password=sha2(@password, 512)" +
                "WHERE id=@uId";

            DbParameter paramFirstname = cmdUpdate.CreateParameter();
            paramFirstname.ParameterName = "firstname";
            paramFirstname.Value = newUserData.Firstname;
            paramFirstname.DbType = DbType.String;

            DbParameter paramLastname = cmdUpdate.CreateParameter();
            paramLastname.ParameterName = "lastname";
            paramLastname.Value = newUserData.Lastname;
            paramLastname.DbType = DbType.String;


            DbParameter paramAnkunft = cmdUpdate.CreateParameter();
            paramAnkunft.ParameterName = "ankunftsdatum";
            paramAnkunft.Value = newUserData.Ankunftsdatum;
            paramAnkunft.DbType = DbType.Date;

            DbParameter paramAbreise = cmdUpdate.CreateParameter();
            paramAbreise.ParameterName = "abreisedatum";
            paramAbreise.Value = newUserData.Abreisedatum;
            paramAbreise.DbType = DbType.Date;

            DbParameter paramPerson = cmdUpdate.CreateParameter();
            paramPerson.ParameterName = "personen";
            paramPerson.Value = newUserData.Personen;
            paramPerson.DbType = DbType.Int32;


            cmdUpdate.Parameters.Add(paramFirstname);
            cmdUpdate.Parameters.Add(paramLastname);
            cmdUpdate.Parameters.Add(paramAnkunft);
            cmdUpdate.Parameters.Add(paramAbreise);
            cmdUpdate.Parameters.Add(paramPerson);


            return cmdUpdate.ExecuteNonQuery() == 1;
        }

    }
}
