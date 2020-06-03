using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using MELV_IS.Models;
using MySql.Data.MySqlClient;

namespace MELV_IS.Controllers
{
    public class FlightRequestController : Controller
    {
        public ActionResult FlightCreationForm()
        {
            ViewData["FlightCreationForm_Error0"] = "";
            ViewData["FlightCreationForm_Error1"] = "";
            ViewData["recc_day0"] = null;
            ViewData["recc_day1"] = null;

            List<FlightRequest> allFlightRequests = MELV_IS.Models.FlightRequest.selectFlightRequestsWithPlanData();

            for (int iii = 0; iii < 2; iii++)
            {
                List<FlightRequest> flightRequests = allFlightRequests.Where(x => x.Direction == (iii == 0 ? false : true)).ToList();
                

                int distinctClients = flightRequests.Select(x => x.Client.ID).Distinct().Count();

                if (distinctClients < 10)
                {
                    ViewData["FlightCreationForm_Error" + iii] = "Užsiregistravo per mažai žmonių!";
                    continue;
                }

                DateTime nextMonthDate = DateTime.Today.AddMonths(1);

                int totalDays = DateTime.DaysInMonth(nextMonthDate.Year, nextMonthDate.Month);

                double[] dayRatings = new double[totalDays + 1];
                List<int> clients = new List<int>();

                for (int i = 1; i <= totalDays; i++)
                {
                    // 100% svoris, kiekvienos dienos kelioniu prasymu
                    for (int j = 0; j < flightRequests.Count; j++)
                    {
                        if (!clients.Contains(flightRequests[j].Client.ID) && flightRequests[j].DepartureDate.Day == i)
                        {
                            dayRatings[i]++;
                            clients.Add(flightRequests[j].Client.ID);
                        }
                    }

                    // 50% svoris, kiekvienos dienos 3 praejusiu ir sekanciu
                    for (int j = 0; j < flightRequests.Count; j++)
                    {
                        if (!clients.Contains(flightRequests[j].Client.ID) && flightRequests[j].DepartureDate.Day <= i + 3 && flightRequests[j].DepartureDate.Day >= i - 3)
                        {
                            dayRatings[i] += 0.5;
                            clients.Add(flightRequests[j].Client.ID);
                        }
                    }

                    // 25% svoris, kiekvienos dienos 6 praejusiu ir sekanciu
                    for (int j = 0; j < flightRequests.Count; j++)
                    {
                        if (!clients.Contains(flightRequests[j].Client.ID) && flightRequests[j].DepartureDate.Day <= i + 9 && flightRequests[j].DepartureDate.Day >= i - 9)
                        {
                            dayRatings[i] += 0.25;
                            clients.Add(flightRequests[j].Client.ID);
                        }
                    }

                    // 10% svoris, kiekvienos dienos likusiu
                    for (int j = 0; j < flightRequests.Count; j++)
                    {
                        if (!clients.Contains(flightRequests[j].Client.ID))
                        {
                            dayRatings[i] += 0.1;
                            clients.Add(flightRequests[j].Client.ID);
                        }
                    }

                    clients.Clear();
                }

                ViewData["dayRatings"] = dayRatings;

                double maxVal = 0;
                int maxDay = 0;

                for (int i = 0; i < dayRatings.Length; i++)
                {
                    if (maxVal <= dayRatings[i])
                    {
                        maxVal = dayRatings[i];
                        maxDay = i;
                    }
                }

                if (maxVal < 10)
                {
                    ViewData["FlightCreationForm_Error" + iii] = "Užsiregistravo per mažai žmonių!";
                    return View(flightRequests);
                }

                if (maxVal >= 10)
                {
                    ViewData["recc_day" + iii] = new DateTime(nextMonthDate.Year, nextMonthDate.Month, maxDay);
                }
            }

            return View(allFlightRequests);
        }

        [HttpPost]
        public JsonResult calculateSecondDate(string firstDate, string direction)
        {
            bool returning = direction == "1" ? true : false;

            DateTime fstDate = Convert.ToDateTime(firstDate);

            // # patikrinti, ar jau skaiciuota
            FlightDuration flightDur = FlightDuration.findOrFail(fstDate, returning);
            if (flightDur != null) return Json(flightDur, JsonRequestBehavior.AllowGet);// return flightDur;

            // earth 149500000, mars 227900000
            double startAvgDistance = returning ? 227900000 : 149500000; // km
            double endAvgDistance = returning ? 149500000 : 227900000; // km

            // earth 365.25, mars 686.97
            double startOrbitPeriod = returning ? 686.97 : 365.25; // days
            double endOrbitPeriod = returning ? 365.25 : 686.97; // days

            // assuming average speed of 16 km/s
            double spaceShipSpeed = 1382400; // km/d
            double startStopFuelConsumption = 100000; // litres
            double dailyFuelConsumption = 150; // litres
            double fuelLitreCost = 1.5; // euros

            // # apskaiciuot pozicijas pradines datos metu
            double startRadialPos = calculatePlanetPosition(fstDate, startOrbitPeriod);
            double endRadialPos = calculatePlanetPosition(fstDate, endOrbitPeriod);

            // # while ( not at end position)
            DateTime sndDate = new DateTime(fstDate.Ticks);
            int limit = 0; // infinite loops are never fun
            while (limit < 1500)
            {
                // #    apskaiciuot galine planeta po vienos dienos
                sndDate = sndDate.AddDays(1);
                double endPos = calculatePlanetPosition(sndDate, endOrbitPeriod);

                double spaceShipDistance = (sndDate - fstDate).Days * spaceShipSpeed;

                // straight line distance 
                double currentAngle = Math.Abs(startRadialPos - endPos);
                double distanceToPlanet = Math.Sqrt(Math.Pow(startAvgDistance, 2) + Math.Pow(endAvgDistance, 2)
                - 2 * startAvgDistance * endAvgDistance * Math.Cos(currentAngle * 2 * Math.PI));

                // #    patikrint ar trajektorija nekerta saules
                if (0.4 < currentAngle && currentAngle < 0.6)
                    distanceToPlanet *= 1.3; // modify distance to account for parabolic trajectory

                // #    patikrinti, ar pasieke planeta
                if (spaceShipDistance >= distanceToPlanet) break;
                limit++;
            }

            // # paskaiciuot kiek dienu truks
            // # paskaiciuot kura pagal dienas
            double fuelCost = (2 * startStopFuelConsumption + (sndDate - fstDate).Days * dailyFuelConsumption) * fuelLitreCost;

            // # issaugot duombazej
            flightDur = new FlightDuration(-1, returning ? fstDate : sndDate, returning ? sndDate : fstDate, returning, fuelCost);
            FlightDuration.insertSecondDate(flightDur);

            // # return
            return Json(flightDur, JsonRequestBehavior.AllowGet);
        }

        public static double calculatePlanetPosition(DateTime date, double orbitPeriod)
        {
            // date of alignment - mars, sun and earth in one line
            DateTime zeroPointDate = new DateTime(2014, 04, 8);
            return (date - zeroPointDate).Days % orbitPeriod / orbitPeriod;
        }

        public ActionResult submitFlightRequestDates(string date1, string date2, string direction, string price)
        {
            return RedirectToAction("PlansSelectionForm", "Plans", new { date1 = date1, date2 = date2, direction = direction, price = price});
        }

        public ActionResult submitFlightRequestSelectedPlans(string date1, string date2, string direction, string price, string foodPlanId, string foodPlan,
            string entertainmentPlanId, string entertainmentPlan, string sportPlanId, string sportPlan)
        {
            ViewBag.date1 = date1;
            ViewBag.date2 = date2;
            ViewBag.direction = direction;
            ViewBag.price = price;
            ViewBag.finalPrice = calculateFinalPrice(price, foodPlanId, entertainmentPlanId, sportPlanId);
            ViewBag.foodPlanId = foodPlanId;
            ViewBag.foodPlan = foodPlan;
            ViewBag.entertainmentPlanId = entertainmentPlanId;
            ViewBag.entertainmentPlan = entertainmentPlan;
            ViewBag.sportPlanId = sportPlanId;
            ViewBag.sportPlan = sportPlan;

            return View("FlightRequestSummaryForm");
        }

        public double calculateFinalPrice(string price, string foodPlanId, string entertainmentPlanId, string sportPlanId)
        {
            double sum = Convert.ToDouble(price);
            sum += (double)(new FoodPlan(Convert.ToInt32(foodPlanId)).Price);
            sum += (double)(new EntertainmentPlan(Convert.ToInt32(entertainmentPlanId)).Price);
            sum += (double)(new SportPlan(Convert.ToInt32(sportPlanId)).Price);

            return sum;
        }

        public ActionResult submitFlightRequest(string date1, string date2, string direction, string price, string foodPlanId, string entertainmentPlanId, string sportPlanId) 
        {
            FlightRequest.insertFlightRequest(direction == "1", Convert.ToDateTime(date1), Convert.ToDateTime(date2), Convert.ToDouble(price),
                Convert.ToInt32(entertainmentPlanId), Convert.ToInt32(foodPlanId), Convert.ToInt32(sportPlanId), Convert.ToInt32(Session["user"]));


            return RedirectToAction("MainPage", "Home");
        }

        public ActionResult cancelSubmission()
        {
            return RedirectToAction("MainPage", "Home");
        }
    }
}