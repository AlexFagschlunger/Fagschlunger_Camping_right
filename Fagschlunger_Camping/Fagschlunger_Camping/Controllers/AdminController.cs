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
        private Reservierung newUserData;

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
        public ActionResult Update(int id)
        {
            rep = new RepositoryReservierungDB();
            rep.Open();

            if (rep.UpdateUserData(id, newUserData))
            {
                rep.Delete(id);
                rep.Close();
                return View("Message", new Message("User löschen", "Benutzer wurde erfolgreich geupdatet!"));
            }
            else
            {
                rep.Close();
                return View("Message", new Message("User löschen", "Benutzer konnte nicht geupdatet werden!"));
            }

        }
        public ActionResult RegistrieteBenutzer()
        {
            return View();
        }
    }
}