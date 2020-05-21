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
    public class FlightRequestController : Controller
    {
        public ActionResult FlightCreationForm()
        {
            List<FlightRequest> flightRequests = MELV_IS.Models.FlightRequest.selectFlightRequestsWithPlanData();

            int distinctClients = flightRequests.Select(x => x.Client.ID).Distinct().Count();

            ViewData["FlightCreationForm_Error"] = "";
            if (distinctClients < 10)
            {
                ViewData["FlightCreationForm_Error"] = "Užsiregistravo per mažai žmonių!";
                return View(flightRequests);
            }



            double[] dayRatings = new double[31];


            return View(flightRequests);
        }
    }
}