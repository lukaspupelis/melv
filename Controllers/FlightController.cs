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
    public class FlightController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //----------------------------------------- Edvinas----------
        public ActionResult SelectAllFlights()
        {
            List<Flight> flights = MELV_IS.Models.Flight.SelectFlights();

            return View(flights);
        }

        //-------------------------------------------
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