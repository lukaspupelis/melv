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
    public class PlansController : Controller
    {

        public ActionResult FoodPlanList()
        {
            List<FoodPlan> foodPlans = MELV_IS.Models.FoodPlan.selectFoodPlans();

            return View(foodPlans);
        }

        public ActionResult FoodPlanForm(int id)
        {
            FoodPlan foodPlan;

            ViewBag.ID = id;
            if (id == -1)
            {
                foodPlan = new FoodPlan();
            }
            else
            {
                foodPlan = MELV_IS.Models.FoodPlan.selectFoodPlan(id);
            }

            return View(foodPlan);
        }

        [HttpPost]
        public ActionResult FoodPlanForm(int id, FoodPlan foodPlan)
        {
            ViewBag.ID = id;
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == -1)
                    {
                        MELV_IS.Models.FoodPlan.insertFoodPlan(foodPlan);
                    }
                    else
                    {
                        MELV_IS.Models.FoodPlan.updateFoodPlan(foodPlan);
                    }
                    
                }

                return RedirectToAction("FoodPlanList");
            }
            catch
            {
                return View(foodPlan);
            }
        }

        public ActionResult FoodPlanPage(int id)
        {
            FoodPlan foodPlan;

            ViewBag.ID = id;
            foodPlan = MELV_IS.Models.FoodPlan.selectFoodPlan(id);

            return View(foodPlan);
        }

        public ActionResult removeFoodPlan(int id)
        {
            MELV_IS.Models.FoodPlan.deleteFoodPlan(id);
            return RedirectToAction("FoodPlanList");
        }
    }
}