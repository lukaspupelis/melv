using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MELV_IS.Models;
using MySql.Data.MySqlClient;

namespace MELV_IS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MainPage()
        {
            List<User> users = MELV_IS.Models.User.selectUsers();

            if (Session["admin"] == null)
            {
                Session["admin"] = false;
            }
            if (Session["user"] == null)
            {
                Session["user"] = 1;
                List<Flight> flights = MELV_IS.Models.Flight.selectClientFlights(1);
                List<Flight> userFlights = flights.Where(x => x.Direction == false).ToList();
                List<Flight> userFlights1 = flights.Where(x => x.Direction == true).ToList();

                if (userFlights.Count > 0 && userFlights[0].ID != 0)
                {
                    Session["flight_mars"] = userFlights[0].ID;
                    Session["flight_mars_ddate"] = "";
                }
                else
                {
                    Session["flight_mars"] = -1;
                    Session["flight_mars_ddate"] = userFlights[0].DepartureDate;
                }
                if (userFlights1.Count > 0 && userFlights1[0].ID != 0)
                {
                    Session["flight_earth"] = userFlights1[0].ID;
                    Session["flight_earth_ddate"] = userFlights1[0].DepartureDate;
                }
                else
                {
                    Session["flight_earth"] = -1;
                    Session["flight_earth_ddate"] = "";
                }
            }

            return View(users);
        }

        [HttpPost]
        public ActionResult MainPage(string user)
        {
            Session["user"] = user;

            if (MELV_IS.Models.Administrator.isAdmin(user))
            {
                Session["admin"] = true;
            }
            else
            {
                Session["admin"] = false;
            }

            if (!(bool)Session["admin"])
            {
                List<Flight> flights = MELV_IS.Models.Flight.selectClientFlights(Convert.ToInt32(Session["user"]));
                List<Flight> userFlights = flights.Where(x => x.Direction == false).ToList();
                List<Flight> userFlights1 = flights.Where(x => x.Direction == true).ToList();

                if (userFlights.Count > 0 && userFlights[0].ID != 0)
                {
                    Session["flight_mars"] = userFlights[0].ID;
                    Session["flight_mars_ddate"] = userFlights[0].DepartureDate;
                }
                else
                {
                    Session["flight_mars"] = -1;
                    Session["flight_mars_ddate"] = "";
                }
                if (userFlights1.Count > 0 && userFlights1[0].ID != 0)
                {
                    Session["flight_earth"] = userFlights1[0].ID;
                    Session["flight_earth_ddate"] = userFlights1[0].DepartureDate;
                }
                else
                {
                    Session["flight_earth"] = -1;
                    Session["flight_earth_ddate"] = "";
                }
            }

            return RedirectToAction("MainPage");
        }
    }
}