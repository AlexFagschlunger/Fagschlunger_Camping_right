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
        IRepositoryUser rep1;

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
        public ActionResult Update(int idreservation)
        {

            if (Session["loggedinUser"] == null)
            {
                return RedirectToAction("login", "admin");
            }

            rep = new RepositoryReservierungDB();

            rep.Open();
            rep.Update(idreservation, true);
            rep.Close();
            return View();

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
            List<User> users;
            // DB-Instanz erzeugen
            rep1 = new RepositoryUserDB();
            // DB-Verbindung öffnen
            rep1.Open();
            // alle Benutzer aus der DB-Tabelle ermitteln
            users = rep1.GetRegisteredUser();
            // DB-Verbindung trennen
            rep1.Close();

            // Jetzt wird an die View eine Liste mit den Benutzern aus der DB übergeben
            return View(users);
        }

        public ActionResult DeleteUser(int id)
        {
            rep1 = new RepositoryUserDB();
            rep1.Open();

            if (rep1.Delete(id))
            {
                rep1.Close();
                return RedirectToAction("RegistrieteBenutzer");
            }
            else
            {
                rep1.Close();
                return View("Message", new Message("User löschen", "User konnte nicht gelöscht werden!"));
            }
        }
    }
}