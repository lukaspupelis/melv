using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MELV_IS.Controllers
{
    public class PaymentController : Controller
    {
        //flight id
        public static Boolean ReturnPayments(int id)
        {
            MELV_IS.Models.Payment.SelectPayments(id);



            return true;
        }

        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }
    }
}