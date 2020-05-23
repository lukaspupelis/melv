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
            List<User> users = MELV_IS.Models.User.selectUsers();

            if (Session["admin"] == null)
            {
                Session["admin"] = false;
            }
            if (Session["user"] == null)
            {
                Session["user"] = 1;
            }

            return View(users);
        }

        [HttpPost]
        public ActionResult MainPage(string user)
        {
            Session["user"] = user;

            if (MELV_IS.Models.Administrator.isAdmin(user))
            {
                Session["admin"] = true;
            }
            else
            {
                Session["admin"] = false;
            }
            
            return RedirectToAction("MainPage");
        }
    }
}