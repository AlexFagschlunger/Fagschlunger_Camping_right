using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fagschlunger_Camping.Models
{
    public enum Gender { male, female, notspecified }
    public enum Benutzer { Gast, Admin, registrierterBenutzer }

    public class User
    {
        public int ID { get; set; }
        public Benutzer Rolle { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Password2 { get; set; }

        public User() : this(0, Benutzer.Gast, "", "", Gender.notspecified, DateTime.MinValue, "", "", "") { }

        public User(int id, Benutzer rolle, string firstname, string lastname, Gender gender, DateTime? birthdate, string username, string password, string password2)
        {
            this.ID = id;
            this.Rolle = rolle;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Gender = gender;
            this.Birthdate = birthdate;
            this.Username = username;
            this.Password = password;
            this.Password2 = password2;
        }
    }
}