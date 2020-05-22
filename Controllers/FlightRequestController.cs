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
            ViewData["recc_day1"] = null;
            ViewData["recc_day2"] = null;

            if (distinctClients < 10)
            {
                ViewData["FlightCreationForm_Error"] = "Užsiregistravo per mažai žmonių!";
                return View(flightRequests);
            }

            int totalDays = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
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
            double maxVal2 = 0;
            int maxDay = 0;
            int maxDay2 = 0;

            for(int i = 0; i < dayRatings.Length; i++)
            {
                if (maxVal <= dayRatings[i])
                {
                    maxVal2 = maxVal;
                    maxDay2 = maxDay;
                    maxVal = dayRatings[i];
                    maxDay = i;
                }
            }

            if (maxVal < 10)
            {
                ViewData["FlightCreationForm_Error"] = "Užsiregistravo per mažai žmonių!";
                return View(flightRequests);
            }

            if (maxVal >= 10)
            {
                ViewData["recc_day1"] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, maxDay);
            }

            if (maxVal2 >= 10)
            {
                ViewData["recc_day1"] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, maxDay2);
                ViewData["recc_day2"] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, maxDay);
            }

            return View(flightRequests);
        }

        [HttpPost]
        public DateTime calculateSecondDate(string firstDate)
        {
            DateTime date = Convert.ToDateTime(firstDate);
            return date.AddMonths(1);
        }
    }
}