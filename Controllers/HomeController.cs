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
            if (Session["admin"] == null)
            {
                Session["admin"] = false;
            }
            return View();
        }

        public ActionResult changePermissions()
        {
            if ((bool)Session["admin"])
            {
                Session["admin"] = false;
            }
            else
            {
                Session["admin"] = true;
            }
            return RedirectToAction("MainPage");
        }
    }
}