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
                     Payment.insertPayment(payment);
                }

                return RedirectToAction("PaymentPage");
            }
            catch
            {
                return View(payment);
            }
        }
    }
}