using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Fagschlunger_Camping.Models;
using Fagschlunger_Camping.Models.db;

namespace Fagschlunger_Camping.Controllers
{
    public class ReservierungController : Controller
    {
        private IRepositoryReservierung rep;
        public ActionResult Reservierung()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Anfrage()
        {
            return View(new Reservierung());
        }
        [HttpPost]
        public ActionResult Anfrage(Reservierung user)
        {
            // Parameter user:hier sind die eingegebenen Daten des Formulars enthalten

            if (user == null)
            {
                return RedirectToAction("Registration");
            }
            //2. Formulardaten überprüfen - muss immer gemacht werden
            //      auch wenn z.b das Formular mit HIlfe von JavaScript
            //      überprüft wurde
            CheckUserData(user);

            //falls Fehler vorhanden sind
            if (!ModelState.IsValid)
            {
                // wird rufen das FOrmular erneut auf
                //      das Formular wird mit dem vorhergehenden Werten befüllt
                return View(user);
            }
            else
            {
                // eine Instanz unserer DB-Klasse erzeugen
                rep = new RepositoryReservierungDB();

                // Verbindung zum BD-Server herstellen
                rep.Open();

                // versuchen die Userdaten einzutragen
                if (rep.Insert(user))
                {
                    // Verbindung schließen
                    rep.Close();
                    // Erfolgsmeldung ausgeben
                    return View("Message", new Message("Reservierung", "Ihre Daten wurde erfolgreich abgespeichert"));
                }
                else
                {
                    rep.Close();
                    // Fehlermeldung ausgeben
                    return View("Message", new Message("Reservierung", "Ihre Daten konnten nicht abgespeichert werden"));
                }
            }
        }

        [HttpPost]
        private void CheckUserData(Reservierung user)
        {
            if (user == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(user.Firstname.Trim()))
            {
                ModelState.AddModelError("Firstname", "Nachname ist ein Pflichtfeld.");
            }

            if (string.IsNullOrEmpty(user.Lastname.Trim()))
            {
                ModelState.AddModelError("Lastname", "Benutzername ist ein Pflichtfeld.");
            }
        }
    }
}