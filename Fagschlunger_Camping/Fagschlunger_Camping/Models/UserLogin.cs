using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fagschlunger_Camping.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserLogin() : this("", "") { }

        public UserLogin(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}