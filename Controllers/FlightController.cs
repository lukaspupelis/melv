using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MELV_IS.Models;
using MySql.Data.MySqlClient;
using MELV_IS.Controllers;

namespace MELV_IS.Controllers
{
    public class FlightController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FlightList()
        {
            List<Flight> flights = MELV_IS.Models.Flight.SelectFlights();

            return View(flights);
        }

        public ActionResult CancelFlight(int id)
        {
            bool temp = MELV_IS.Controllers.PaymentController.ReturnPayments(id);
            
            //temp = false;
            
            //if payment return succeeded
            if(temp)
            {
                MELV_IS.Models.Flight.RemoveFlight(id);
                ViewBag.removed = "Skrydis pašalintas";
            }
            else
            {
                ViewBag.removed = "Nepavyko gražinti pinigų ir skrydis nepašalintas";
                return View("FlightPage", new Flight(id));
            }
            ViewBag.removed = "Skrydis buvo pašalintas";
            return View("FlightList", MELV_IS.Models.Flight.SelectFlights());
        }

        public ActionResult SelectedFlight(int id)
        {
            Flight flight = MELV_IS.Models.Flight.SelectFlightWithData(id);

            return View("FlightPage",flight);
        }

        public ActionResult UpdateFlight(int id)
        {
            MELV_IS.Models.Flight.UpdateFlight(id);

            //Flight flight = MELV_IS.Models.Flight.SelectFlightWithData(id);
            List<Flight> flights = MELV_IS.Models.Flight.SelectFlights();
            
            ViewBag.Updated = "Skrydis patvirtintas!";
            return View("FlightList", flights);
        }

        public ActionResult submit(string date1, string date2, string direction)
        {
            DateTime firstDate = Convert.ToDateTime(date1);
            DateTime secondDate = Convert.ToDateTime(date2);
            int directionConv = Convert.ToInt32(direction);
            MELV_IS.Models.Flight.insertFlight(firstDate, secondDate, directionConv);
            return RedirectToAction("FlightCreationForm", "FlightRequest", new { area = "" });
        }
    }
}