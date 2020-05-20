using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace MELV_IS.Models
{
    public class FoodPlan
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Pavadinimas")]
        [Required(ErrorMessage = "Šis laukas privalomas")]
        public string Title { get; set; }

        [DisplayName("Aprašymas")]
        [Required(ErrorMessage = "Šis laukas privalomas")]
        public string Text { get; set; }

        [DisplayName("Kaina")]
        [Required(ErrorMessage = "Šis laukas privalomas")]
        public decimal? Price { get; set; }

        [DisplayName("Ištrintas?")]
        [Required]
        public bool Removed { get; set; }

        [DisplayName("Kas sukūrė")]
        [Required]
        public Administrator Administrator { get; set; }

        public static DB DB = new DB();

        public FoodPlan()
        {
            Administrator = new Administrator(2);
        }

        public FoodPlan(int id)
        {
            ID = id;
            DB.loadQuery("select * from food_plans where id=?id");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            DataTable dt = DB.query();
            foreach (DataRow item in dt.Rows)
            {
                Title = Convert.ToString(item["title"]);
                Text = Convert.ToString(item["text"]);
                Price = Convert.ToDecimal(item["price"]);
                Removed = Convert.ToBoolean(item["removed"]);
                Administrator = new Administrator(Convert.ToInt32(item["fk_administrator"]));
            }
        }

        public FoodPlan(int id, string title, string text, decimal price, bool removed, Administrator admin)
        {
            ID = id;
            Title = title;
            Text = text;
            Price = price;
            Removed = removed;
            Administrator = admin;
        }


        public static List<FoodPlan> selectFoodPlans()
        {
            List<FoodPlan> foodPlans = new List<FoodPlan>();
            DB.loadQuery("select * from food_plans where removed = 0");
            DataTable dt = DB.query();
            foreach (DataRow item in dt.Rows)
            {
                foodPlans.Add(new FoodPlan
                (
                    Convert.ToInt32(item["id"]),
                    Convert.ToString(item["title"]),
                    Convert.ToString(item["text"]),
                    Convert.ToDecimal(item["price"]),
                    Convert.ToBoolean(item["removed"]),
                    new Administrator(Convert.ToInt32(item["fk_administrator"]))
                ));
            }

            return foodPlans;
        }

        public static bool insertFoodPlan(FoodPlan foodPlan)
        {
            try
            {
                DB.loadQuery(@"INSERT INTO food_plans(title,text,price,removed,fk_administrator)VALUES(?title,?text,?price,?removed,?fk_admin);");
                DB.Command.Parameters.Add("?title", MySqlDbType.VarChar).Value = foodPlan.Title;
                DB.Command.Parameters.Add("?text", MySqlDbType.VarChar).Value = foodPlan.Text;
                DB.Command.Parameters.Add("?price", MySqlDbType.Decimal).Value = foodPlan.Price;
                DB.Command.Parameters.Add("?removed", MySqlDbType.Bit).Value = 0;
                DB.Command.Parameters.Add("?fk_admin", MySqlDbType.Int32).Value = 2;
                DB.execute();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool updateFoodPlan(FoodPlan foodPlan)
        {
            try
            {
                DB.loadQuery(@"UPDATE food_plans SET title=?title, text=?text, price=?price, fk_administrator=?fk_admin WHERE id=?id");
                DB.Command.Parameters.Add("?title", MySqlDbType.VarChar).Value = foodPlan.Title;
                DB.Command.Parameters.Add("?text", MySqlDbType.VarChar).Value = foodPlan.Text;
                DB.Command.Parameters.Add("?price", MySqlDbType.VarChar).Value = foodPlan.Price;
                DB.Command.Parameters.Add("?fk_admin", MySqlDbType.VarChar).Value = 2;
                DB.Command.Parameters.Add("?id", MySqlDbType.VarChar).Value = foodPlan.ID;
                DB.execute();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static FoodPlan selectFoodPlan(int id)
        {
            FoodPlan foodPlan = new FoodPlan();
            DB.loadQuery("select * from food_plans where id=?id");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            DataTable dt = DB.query();

            foreach (DataRow item in dt.Rows)
            {
                foodPlan.ID = Convert.ToInt32(item["id"]);
                foodPlan.Title = Convert.ToString(item["title"]);
                foodPlan.Text = Convert.ToString(item["text"]);
                foodPlan.Price = Convert.ToDecimal(item["price"]);
                foodPlan.Removed = Convert.ToBoolean(item["removed"]);
                foodPlan.Administrator = new Administrator(Convert.ToInt32(item["fk_administrator"]));
            }

            return foodPlan;
        }

        public static void deleteFoodPlan(int id)
        {
            try
            {
                DB.loadQuery(@"UPDATE food_plans SET removed=1 WHERE id=?id");
                DB.Command.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
                DB.execute();
            }
            catch (Exception)
            {
            }
        }
    }
}