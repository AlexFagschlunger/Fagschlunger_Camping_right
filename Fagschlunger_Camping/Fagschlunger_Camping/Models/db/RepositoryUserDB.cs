using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Fagschlunger_Camping.Models.db
{
    public class RepositoryUserDB : DBBase, IRepositoryUser
    {
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

        public List<User> GetAllUser()
        {
            List<User> users = new List<User>();


            DbCommand cmdSelect = this._connection.CreateCommand();
            cmdSelect.CommandText = "SELECT * FROM users";

            // ExecuteReader() wird immer bei SELECT- Abfragen benötigt mit ihm kann man sich zeileinweise durch das erhaltene Ergebnis bewegen
            using (DbDataReader reader = cmdSelect.ExecuteReader())
            {
                // mit Read() wird der nächste Datensatz (User) gelesen
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        // Id ... so lautet das feld in der Klasse User
                        // "id" ... so lautet der Spaltenname in der Datenbanktabelle users

                        ID = Convert.ToInt32(reader["id"]),
                        Rolle = (Benutzer)Convert.ToInt32(reader["rolle"]),
                        Firstname = Convert.ToString(reader["firstname"]),
                        Lastname = Convert.ToString(reader["lastname"]),
                        Gender = (Gender)Convert.ToInt32(reader["gender"]),
                        Birthdate = Convert.ToDateTime(reader["birthdate"]),
                        Username = Convert.ToString(reader["username"]),
                        Password = ""
                    });
                }
            }
            return users;
        }

        public User GetUser(int id)
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
                return new User
                {
                    // ID ... so lautet das feld in der Klasse User
                    // "id" ... so lautet der Spaltenname in der Datenbanktabelle users

                    ID = Convert.ToInt32(reader["id"]),
                    Rolle = (Benutzer)Convert.ToInt32(reader["rolle"]),
                    Firstname = Convert.ToString(reader["firstname"]),
                    Lastname = Convert.ToString(reader["lastname"]),
                    Gender = (Gender)Convert.ToInt32(reader["gender"]),
                    Birthdate = Convert.ToDateTime(reader["birthdate"]),
                    Username = Convert.ToString(reader["username"]),
                    Password = ""
                };
            }
        }

        public bool Insert(User user)
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
            cmdInsert.CommandText = "Insert Into users Values(null, @rolle, @firstname, @lastname, @gender, @birthdate, @username, sha2(@password, 512))";

            // Parameter erzeugt
            DbParameter paramRolle = cmdInsert.CreateParameter();
            paramRolle.ParameterName = "rolle";
            paramRolle.Value = user.Rolle;
            paramRolle.DbType = DbType.Int32;

            DbParameter paramFN = cmdInsert.CreateParameter();
            paramFN.ParameterName = "firstname";
            paramFN.Value = user.Firstname;
            paramFN.DbType = DbType.String;

            DbParameter paramLN = cmdInsert.CreateParameter();
            paramLN.ParameterName = "lastname";
            paramLN.Value = user.Lastname;
            paramLN.DbType = DbType.String;

            DbParameter paramGender = cmdInsert.CreateParameter();
            paramGender.ParameterName = "gender";
            paramGender.Value = user.Gender;
            paramGender.DbType = DbType.Int32;

            DbParameter paramBDate = cmdInsert.CreateParameter();
            paramBDate.ParameterName = "birthdate";
            paramBDate.Value = user.Birthdate;
            paramBDate.DbType = DbType.Date;

            DbParameter paramUsername = cmdInsert.CreateParameter();
            paramUsername.ParameterName = "username";
            paramUsername.Value = user.Username;
            paramUsername.DbType = DbType.String;

            DbParameter paramPwd = cmdInsert.CreateParameter();
            paramPwd.ParameterName = "password";
            paramPwd.Value = user.Password;
            paramPwd.DbType = DbType.String;

            // Parameteer mit dem Comamnd verbinden
            cmdInsert.Parameters.Add(paramRolle);
            cmdInsert.Parameters.Add(paramFN);
            cmdInsert.Parameters.Add(paramLN);
            cmdInsert.Parameters.Add(paramGender);
            cmdInsert.Parameters.Add(paramBDate);
            cmdInsert.Parameters.Add(paramUsername);
            cmdInsert.Parameters.Add(paramPwd);

            // ExecuteNonQuery() ... wird bein INSERT, UPDATE, und DELETE verwendet diese Methode liefert die ANzahl der betroffenen Datensätze zurück
            return cmdInsert.ExecuteNonQuery() == 1;




        }

        public bool UpdateUserData(int id, User newUserData)
        {
            DbCommand cmdUpdate = this._connection.CreateCommand();
            cmdUpdate.CommandText = "UPDATE users SET rolle=@rolle, firstname=@firstname, lastname =@lastname, + " +
                "gender=@gender, birthdate=@birthdate, username=@username, password=sha2(@password, 512)" +
                "WHERE id=@uId";

            DbParameter paramRolle = cmdUpdate.CreateParameter();
            paramRolle.ParameterName = "rolle";
            paramRolle.Value = newUserData.Rolle;
            paramRolle.DbType = DbType.Int32;

            DbParameter paramFirstname = cmdUpdate.CreateParameter();
            paramFirstname.ParameterName = "firstname";
            paramFirstname.Value = newUserData.Firstname;
            paramFirstname.DbType = DbType.String;

            DbParameter paramLastname = cmdUpdate.CreateParameter();
            paramLastname.ParameterName = "lastname";
            paramLastname.Value = newUserData.Lastname;
            paramLastname.DbType = DbType.String;


            DbParameter paramGender = cmdUpdate.CreateParameter();
            paramGender.ParameterName = "gender";
            paramGender.Value = newUserData.Gender;
            paramGender.DbType = DbType.Int32;

            DbParameter paramBDate = cmdUpdate.CreateParameter();
            paramBDate.ParameterName = "birthdate";
            paramBDate.Value = newUserData.Birthdate;
            paramBDate.DbType = DbType.Date;

            DbParameter paramUsername = cmdUpdate.CreateParameter();
            paramUsername.ParameterName = "username";
            paramUsername.Value = newUserData.Username;
            paramUsername.DbType = DbType.String;

            DbParameter paramPwd = cmdUpdate.CreateParameter();
            paramPwd.ParameterName = "password";
            paramPwd.Value = newUserData.Password;
            paramPwd.DbType = DbType.String;

            DbParameter paramID = cmdUpdate.CreateParameter();
            paramID.ParameterName = "id";
            paramID.Value = newUserData.ID;
            paramID.DbType = DbType.String;


            cmdUpdate.Parameters.Add(paramRolle);
            cmdUpdate.Parameters.Add(paramFirstname);
            cmdUpdate.Parameters.Add(paramLastname);
            cmdUpdate.Parameters.Add(paramGender);
            cmdUpdate.Parameters.Add(paramBDate);
            cmdUpdate.Parameters.Add(paramUsername);
            cmdUpdate.Parameters.Add(paramPwd);
            cmdUpdate.Parameters.Add(paramID);


            return cmdUpdate.ExecuteNonQuery() == 1;
        }

        public User Login(UserLogin user)
        {
            DbCommand cmdLogin = this._connection.CreateCommand();
            cmdLogin.CommandText = "SELECT * FROM users WHERE username=@username AND password=sha2(@password, 512)";


            DbParameter paramUsername = cmdLogin.CreateParameter();
            paramUsername.ParameterName = "username";
            paramUsername.Value = user.Username;
            paramUsername.DbType = DbType.String;

            DbParameter paramPwd = cmdLogin.CreateParameter();
            paramPwd.ParameterName = "password";
            paramPwd.Value = user.Password;
            paramPwd.DbType = DbType.String;

            cmdLogin.Parameters.Add(paramUsername);
            cmdLogin.Parameters.Add(paramPwd);

            using (DbDataReader reader = cmdLogin.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return null;
                }
                reader.Read();
                return new User
                {
                    ID = Convert.ToInt32(reader["id"]),
                    Rolle = (Benutzer)Convert.ToInt32(reader["rolle"]),
                    Firstname = Convert.ToString(reader["firstname"]),
                    Lastname = Convert.ToString(reader["lastname"]),
                    Gender = (Gender)Convert.ToInt32(reader["gender"]),
                    Birthdate = Convert.ToDateTime(reader["birthdate"]),
                    Username = Convert.ToString(reader["username"]),
                    Password = ""
                };
            }
        }
    }
}