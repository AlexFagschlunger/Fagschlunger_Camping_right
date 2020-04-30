using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Fagschlunger_Camping.Models.db
{
    public class RepositoryReservierungDB : DBBase, IRepositoryReservierung
    {
        public bool Delete(int id)
        {
            // Command erzeugen - mit Parameter (SQL-Injections verhindern)
            DbCommand cmdDel = this._connection.CreateCommand();
            cmdDel.CommandText = "DELETE FROM users WHERE id=@firstname";

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
            cmdInsert.CommandText = "Insert Into reservierung Values(null, @firstname, @lastname, @bearbeitet, @ankunftsdatum, @abreisedatum, @personen)";

            // Parameter erzeugt
            DbParameter paramFN = cmdInsert.CreateParameter();
            paramFN.ParameterName = "firstname";
            paramFN.Value = user.Firstname;
            paramFN.DbType = DbType.String;

            DbParameter paramLN = cmdInsert.CreateParameter();
            paramLN.ParameterName = "lastname";
            paramLN.Value = user.Lastname;
            paramLN.DbType = DbType.String;

            DbParameter parambe = cmdInsert.CreateParameter();
            parambe.ParameterName = "bearbeitet";
            parambe.Value = user.Bearbeitet;
            parambe.DbType = DbType.Boolean;

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
            cmdInsert.Parameters.Add(parambe);
            cmdInsert.Parameters.Add(paramAnkunft);
            cmdInsert.Parameters.Add(paramAbreise);
            cmdInsert.Parameters.Add(paramPerson);


            // ExecuteNonQuery() ... wird bein INSERT, UPDATE, und DELETE verwendet diese Methode liefert die ANzahl der betroffenen Datensätze zurück
            return cmdInsert.ExecuteNonQuery() == 1;
        }
        public List<Reservierung> GetReservierungs()
        {
            List<Reservierung>  reservierungs = new List<Reservierung>();


            DbCommand cmdSelect = this._connection.CreateCommand();
            cmdSelect.CommandText = "SELECT * FROM users";

            // ExecuteReader() wird immer bei SELECT- Abfragen benötigt mit ihm kann man sich zeileinweise durch das erhaltene Ergebnis bewegen
            using (DbDataReader reader = cmdSelect.ExecuteReader())
            {
                // mit Read() wird der nächste Datensatz (User) gelesen
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string firstname = Convert.ToString(reader["firstname"]);
                    string lastname = Convert.ToString(reader["lastname"]);
                    bool bearbeitet = Convert.ToBoolean(reader["bearbeitet"]);
                    DateTime ankunftsdatum = Convert.ToDateTime(reader["ankunftsdatum"]);
                    DateTime abreisedatu = Convert.ToDateTime(reader["abreisedatum"]);
                    int personen = Convert.ToInt32(reader["personen"]);
                    reservierungs.Add(new Reservierung
                    {
                        ID = id,
                        Firstname = firstname, 
                        Lastname = lastname,
                        Bearbeitet = bearbeitet,
                        Ankunftsdatum = ankunftsdatum,
                        Abreisedatum = abreisedatum,
                        Personen = personen
                        
                }) ;
                }
            }
            return reservierungs;
        }

        public bool UpdateUserData(int id, Reservierung newUserData)
        {
            DbCommand cmdUpdate = this._connection.CreateCommand();
            cmdUpdate.CommandText = "UPDATE users SET firstname=@firstname, lastname=@lastname, bearbeitet=@bearbeitet + " +
                "ankunftsdatum=@ankunftsdatum, abreisedatum=@abreisedatum, personen=@personen" +
                "WHERE id=@uId";

            DbParameter paramFirstname = cmdUpdate.CreateParameter();
            paramFirstname.ParameterName = "firstname";
            paramFirstname.Value = newUserData.Firstname;
            paramFirstname.DbType = DbType.String;

            DbParameter paramLastname = cmdUpdate.CreateParameter();
            paramLastname.ParameterName = "lastname";
            paramLastname.Value = newUserData.Lastname;
            paramLastname.DbType = DbType.String;

            DbParameter parambe = cmdUpdate.CreateParameter();
            parambe.ParameterName = "bearbeitet";
            parambe.Value = newUserData.Bearbeitet;
            parambe.DbType = DbType.Boolean;

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
            cmdUpdate.Parameters.Add(parambe);
            cmdUpdate.Parameters.Add(paramAnkunft);
            cmdUpdate.Parameters.Add(paramAbreise);
            cmdUpdate.Parameters.Add(paramPerson);


            return cmdUpdate.ExecuteNonQuery() == 1;
        }

    }
}
