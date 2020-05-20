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

        public Flight()
        {

        }

        public Flight(int id)
        {
            ID = id;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from flights where id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
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
    }
}