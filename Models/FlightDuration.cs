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
    public class FlightDuration
    {
        [Key]
        public int ID { get; set; }
        public DateTime DateMars { get; set; }
        public DateTime DateEarth { get; set; }
        public bool Direction { get; set; }
        public double Price { get; set; }

        public static DB DB = new DB();

        public FlightDuration(int id)
        {
            ID = id;
            DB.loadQuery("select * from flight_durations where id=?id");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            DataTable dt = DB.query();
            foreach (DataRow item in dt.Rows)
            {
                DateMars = Convert.ToDateTime(item["date_mars"]);
                DateEarth = Convert.ToDateTime(item["date_earth"]);
                Direction = Convert.ToBoolean(item["direction"]);
                Price = Convert.ToDouble(item["price"]);
            }
        }

        public FlightDuration(int iD, DateTime dateMars, DateTime dateEarth, bool direction, double price)
        {
            ID = iD;
            DateMars = dateMars;
            DateEarth = dateEarth;
            Direction = direction;
            Price = price;
        }

        public static FlightDuration findOrFail(DateTime startDate, bool direction)
        {
            string query = direction ? 
                "SELECT * FROM flight_durations WHERE date_mars=?date AND direction=1" :
                "SELECT * FROM flight_durations WHERE date_earth=?date AND direction=0";

            DB.loadQuery(query);
            DB.Command.Parameters.Add("?date", MySqlDbType.Date).Value = startDate;
            DataTable dt = DB.query();
            
            FlightDuration flightDur = null;

            foreach (DataRow item in dt.Rows)
            {
                int iD = Convert.ToInt32(item["id"]);
                DateTime dateMars = Convert.ToDateTime(item["date_mars"]);
                DateTime dateEarth = Convert.ToDateTime(item["date_earth"]);
                bool dir = Convert.ToBoolean(item["direction"]);
                double price = Convert.ToDouble(item["price"]);

                flightDur = new FlightDuration(iD, dateMars, dateEarth, dir, price);
            }

            return flightDur;
        }

        public static bool insertSecondDate(FlightDuration flightDuration)
        {
            try
            {
                DB.loadQuery(@"INSERT INTO flight_durations (id, date_mars, date_earth, direction, price) VALUES(NULL, ?dateMars, ?dateEarth, ?direction, ?price);");
                DB.Command.Parameters.Add("?dateMars", MySqlDbType.Date).Value = flightDuration.DateMars;
                DB.Command.Parameters.Add("?dateEarth", MySqlDbType.Date).Value = flightDuration.DateEarth;
                DB.Command.Parameters.Add("?direction", MySqlDbType.Bit).Value = flightDuration.Direction;
                DB.Command.Parameters.Add("?price", MySqlDbType.Double).Value = flightDuration.Price;

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