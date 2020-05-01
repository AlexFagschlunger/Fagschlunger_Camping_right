using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fagschlunger_Camping.Models
{
    public class Reservierung
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool Bearbeitet { get; set; }
        public DateTime Ankunftsdatum { get; set; }
        public DateTime Abreisedatum { get; set; }
        public int Personen { get; set; }

        public Reservierung() : this(0, "", "", false, DateTime.MinValue, DateTime.MinValue, 0) { }

        public Reservierung(int id, string firstname, string lastname, bool bearbeitet, DateTime ankunftsdatum, DateTime abreisedatum, int personen)
        {
            this.ID = id;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Bearbeitet = bearbeitet;
            this.Ankunftsdatum = ankunftsdatum;
            this.Abreisedatum = abreisedatum;
            this.Personen = personen;
            
        }

        internal static void Add(Reservierung reservierung)
        {
            throw new NotImplementedException();
        }

        //ToString()
    }
}
