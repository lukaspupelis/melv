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

        [DisplayName("Kas sukūrė")]
        [Required]
        public int Administrator { get; set; }

        public FoodPlan()
        {
        }

        public FoodPlan(int id, string title, string text, decimal price, int admin)
        {
            ID = id;
            Title = title;
            Text = text;
            Price = price;
            Administrator = admin;
        }

        public static List<FoodPlan> selectFoodPlans()
        {
            List<FoodPlan> foodPlans = new List<FoodPlan>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from food_plans";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                foodPlans.Add(new FoodPlan
                (
                    Convert.ToInt32(item["id"]),
                    Convert.ToString(item["title"]),
                    Convert.ToString(item["text"]),
                    Convert.ToDecimal(item["price"]),
                    Convert.ToInt32(item["fk_administrator"])
                ));
            }

            return foodPlans;
        }

        public static bool insertFoodPlan(FoodPlan foodPlan)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO food_plans(title,text,price,fk_administrator)VALUES(?title,?text,?price,?fk_admin);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?title", MySqlDbType.VarChar).Value = foodPlan.Title;
                mySqlCommand.Parameters.Add("?text", MySqlDbType.VarChar).Value = foodPlan.Text;
                mySqlCommand.Parameters.Add("?price", MySqlDbType.Decimal).Value = foodPlan.Price;
                mySqlCommand.Parameters.Add("?fk_admin", MySqlDbType.Int32).Value = 2;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
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
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE food_plans SET title=?title, text=?text, price=?price, fk_administrator=?fk_admin WHERE id=?id";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?title", MySqlDbType.VarChar).Value = foodPlan.Title;
                mySqlCommand.Parameters.Add("?text", MySqlDbType.VarChar).Value = foodPlan.Text;
                mySqlCommand.Parameters.Add("?price", MySqlDbType.VarChar).Value = foodPlan.Price;
                mySqlCommand.Parameters.Add("?fk_admin", MySqlDbType.VarChar).Value = 2;
                mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = foodPlan.ID;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
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
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from food_plans where id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                foodPlan.ID = Convert.ToInt32(item["id"]);
                foodPlan.Title = Convert.ToString(item["title"]);
                foodPlan.Text = Convert.ToString(item["text"]);
                foodPlan.Price = Convert.ToDecimal(item["price"]);
                foodPlan.Administrator = Convert.ToInt32(item["fk_administrator"]);
            }

            return foodPlan;
        }

        public static void deleteFoodPlan(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM food_plans where id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}