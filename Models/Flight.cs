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

        [DisplayName("Išvykimo data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DepartureDate { get; set; }

        [DisplayName("Atvykimo data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ArrivalDate { get; set; }
        public bool Confirmed { get; set; }
        public Administrator Administrator { get; set; }

        public static DB DB = new DB();

        public Flight()
        {

        }

        public Flight(int id, bool direction, DateTime departureDate, DateTime arrivalDate, bool confirmed, Administrator admin)
        {
            ID = id;
            Direction = direction;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            Confirmed = confirmed;
            Administrator = admin;
        }

        public static List<Flight> SelectFlights()
        {
            List<Flight> flights = new List<Flight>();
            DB.loadQuery("select * from flights");
            DataTable dt = DB.query();
            foreach(DataRow item in dt.Rows)
            {
                flights.Add(new Flight
                    (
                        Convert.ToInt32(item[0]),
                        Convert.ToBoolean(item[1]),
                        Convert.ToDateTime(item[2]),
                        Convert.ToDateTime(item[3]),
                        Convert.ToBoolean(item[4]),
                        new Administrator(Convert.ToInt32(item[5]))
                    ));
            }
            return flights;
        }

        public static Flight SelectFlightWithData(int id)
        {
            Flight flight = new Flight();
            DB.loadQuery("select * from flights where id=?id");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            DataTable dt = DB.query();
            foreach (DataRow item in dt.Rows)
            {
                flight.ID = Convert.ToInt32(item["ID"]);
                flight.Direction = Convert.ToBoolean(item["direction"]);
                flight.DepartureDate = Convert.ToDateTime(item["departure_date"]);
                flight.ArrivalDate = Convert.ToDateTime(item["arrival_date"]);
                flight.Confirmed = Convert.ToBoolean(item["confirmed"]);
                flight.Administrator = new Administrator(Convert.ToInt32(item["fk_administrator"]));
            }
            return flight;
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
                Confirmed = Convert.ToBoolean(item["confirmed"]);
                Administrator = new Administrator(Convert.ToInt32(item["fk_administrator"]));
            }
        }

        public static bool insertFlight(DateTime date1, DateTime date2, int direction)
        {
            try
            {
                DB.loadQuery(@"INSERT INTO flights(direction,departure_date,arrival_date,confirmed,fk_administrator)VALUES(?direction,?departure_date,?arrival_date,?confirmed,?fk_admin);");
                DB.Command.Parameters.Add("?direction", MySqlDbType.Bit).Value = direction;
                DB.Command.Parameters.Add("?departure_date", MySqlDbType.DateTime).Value = date1;
                DB.Command.Parameters.Add("?arrival_date", MySqlDbType.DateTime).Value = date2;
                DB.Command.Parameters.Add("?confirmed", MySqlDbType.Bit).Value = 0;
                DB.Command.Parameters.Add("?fk_admin", MySqlDbType.Int32).Value = 2;
                long id = DB.execute(true);

                DB.loadQuery(@"UPDATE flight_requests SET fk_flight = ?fk_flight WHERE departure_date = ?departure_date");
                DB.Command.Parameters.Add("?fk_flight", MySqlDbType.Int32).Value = id;
                DB.Command.Parameters.Add("?departure_date", MySqlDbType.DateTime).Value = date1;
                DB.execute();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<Flight> selectClientFlights(int clientid)
        {
            List<Flight> flights = new List<Flight>();
            DB.loadQuery("select f.id, f.direction, f.departure_date, f.arrival_date, f.confirmed, f.fk_administrator, fr.price from flight_requests fr left join flights f on fr.fk_flight=f.id where fr.fk_client=?id and f.departure_date > now()");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = clientid;
            DataTable dt = DB.query();
            Flight flight = new Flight();
            foreach (DataRow item in dt.Rows)
            {
                flight.ID = Convert.ToInt32(item["id"]);
                flight.Direction = Convert.ToBoolean(item["direction"]);
                flight.DepartureDate = Convert.ToDateTime(item["departure_date"]);
                flight.ArrivalDate = Convert.ToDateTime(item["arrival_date"]);
                flight.Confirmed = Convert.ToBoolean(item["confirmed"]);
                flight.Administrator = new Administrator(Convert.ToInt32(item["fk_administrator"]));
                HttpContext.Current.Session["user_price" + flight.ID] = Convert.ToDecimal(item["price"]);
            }
            flights.Add(flight);

            return flights;
        }
    }
}