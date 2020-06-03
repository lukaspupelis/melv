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
    public class PaymentController : Controller
    {
        //flight id
        public static Boolean ReturnPayments(int id)
        {
            List<Payment> payments = MELV_IS.Models.Payment.SelectPayments(id);

            return MELV_IS.Models.PaymentAPI.ReturnPayments(payments);
        }

        public ActionResult PaymentForm(int id)
        {
            Flight flight = new Flight(id);
            Payment payment = new Payment(flight);
            return View(payment);
        }

        [HttpPost]
        public ActionResult PaymentForm(int id, Payment payment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    payment.Date = DateTime.Now;
                    payment.Flight = new Flight(id);
                    payment.Client = new Client(Convert.ToInt32(Session["user"]));
                }

                return View("PaymentPage", payment);
            }
            catch
            {
                return View(payment);
            }
        }

        public ActionResult PaymentPage(Payment payment)
        {
            return View(payment);
        }

        public ActionResult paymentData(int clientID, int flightID, decimal sum, string cardNumber)
        {
            Client client = new Client(clientID);
            Flight flight = new Flight(flightID);
            Payment payment = new Payment(-1, DateTime.Now, cardNumber, sum, flight, client);
            bool status = PaymentAPI.paymentData(payment);

            if (status)
            {
                Payment.insertPayment(payment);
                return RedirectToAction("SelectedFlight", "Flight", new { id = payment.Flight.ID });
            }
            else
            {
                return View(payment);
            }
        }
    }
}