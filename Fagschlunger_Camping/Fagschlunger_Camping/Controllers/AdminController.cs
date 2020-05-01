using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Fagschlunger_Camping.Models;
using Fagschlunger_Camping.Models.db;

namespace Fagschlunger_Camping.Controllers
{
    public class AdminController : Controller
    {
        IRepositoryReservierung rep;

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Reservierungsanfrage()
        {
            List<Reservierung> res;

            rep = new RepositoryReservierungDB();
            rep.Open();

            res = rep.GetReservierungs();
            rep.Close();

            return View(res);
        }
        // Parametername id muss mit dem angegebenen Namen beim Aufruf Users.cshtml bei DELETE-Button übereinstimmen
        public ActionResult Update(int bearbeitet)
        {
            rep = new RepositoryReservierungDB();
            rep.Open();

            if (rep.Update(bearbeitet))
            {
                rep.Close();
                return View("Message", new Message("Reservierung updaten", "Benutzer wurde erfolgreich geupdatet!"));
            }
            else
            {
                rep.Close();
                return View("Message", new Message("Reservierung updaten", "Benutzer konnte nicht geupdatet werden!"));
            }

        }
        public ActionResult Delete(int id)
        {
            rep = new RepositoryReservierungDB();
            rep.Open();

            if (rep.Delete(id))
            {
                rep.Close();
                return View("Message", new Message("Reservierung löschen", "Reservierung wurde erfolgreich gelöscht!"));
            }
            else
            {
                rep.Close();
                return View("Message", new Message("Reservierung löschen", "Reservierung konnte nicht gelöscht werden!"));
            }

        }
        public ActionResult RegistrieteBenutzer()
        {
            return View();
        }
    }
}