using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MELV_IS.Models;

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

        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }
    }
}