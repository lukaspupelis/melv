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
    public class Flight
    {
        [Key]
        public int ID { get; set; }
        public bool Direction { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal Price { get; set; }
        public bool Confirmed { get; set; }
        public Administrator Administrator { get; set; }

        public static DB DB = new DB();

        public Flight()
        {

        }

        public Flight(int id)
        {
            ID = id;
            DB.loadQuery("select * from flights where id=?id");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            DataTable dt = DB.query();
            foreach (DataRow item in dt.Rows)
            {
                Direction = Convert.ToBoolean(item["direction"]);
                DepartureDate = Convert.ToDateTime(item["departure_date"]);
                ArrivalDate = Convert.ToDateTime(item["arrival_date"]);
                Price = Convert.ToDecimal(item["price"]);
                Confirmed = Convert.ToBoolean(item["confirmed"]);
                Administrator = new Administrator(Convert.ToInt32(item["fk_administrator"]));
            }
        }

        public static bool insertFlight(DateTime date1, DateTime date2)
        {
            try
            {
                DB.loadQuery(@"INSERT INTO flights(direction,departure_date,arrival_date,price,confirmed,fk_administrator)VALUES(?direction,?departure_date,?arrival_date,?price,?confirmed,?fk_admin);");
                DB.Command.Parameters.Add("?direction", MySqlDbType.Bit).Value = 0;
                DB.Command.Parameters.Add("?departure_date", MySqlDbType.DateTime).Value = date1;
                DB.Command.Parameters.Add("?arrival_date", MySqlDbType.DateTime).Value = date2;
                DB.Command.Parameters.Add("?price", MySqlDbType.Decimal).Value = 1000000;
                DB.Command.Parameters.Add("?confirmed", MySqlDbType.Bit).Value = 0;
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