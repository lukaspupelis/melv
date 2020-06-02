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
    public class Payment
    {
        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string CardNumber { get; set; }

        public decimal Sum { get; set; }

        public Flight Flight { get; set; }

        public Client Client { get; set; }

        public static DB DB = new DB();

        public Payment(int id)
        {
            ID = id;
            DB.loadQuery("select * from payments where id=?id");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            DataTable dt = DB.query();
            foreach (DataRow item in dt.Rows)
            {
                Date = Convert.ToDateTime(item["date"]);
                CardNumber = Convert.ToString(item["card_number"]);
                Sum = Convert.ToDecimal(item["sum"]);
                Flight = new Flight(Convert.ToInt32(item["fk_flight"]));
                Client = new Client(Convert.ToInt32(item["fk_client"]));
            }
        }

        public Payment(int id, DateTime date, string card_number, decimal sum, Flight flight, Client client)
        {
            ID = id;
            Date = date;
            CardNumber = card_number;
            Sum = sum;
            Flight = flight;
            Client = client;
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
    }
}