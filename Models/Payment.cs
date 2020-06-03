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

        [DisplayName("Banko kortelės nr.")]
        [Required(ErrorMessage = "Šis laukas privalomas")]
        public string CardNumber { get; set; }

        [DisplayName("Mokėjimo suma")]
        [Required(ErrorMessage = "Šis laukas privalomas")]
        public decimal? Sum { get; set; }

        public Flight Flight { get; set; }

        public Client Client { get; set; }

        public static DB DB = new DB();

        public static List<Payment> SelectPayments(int id)
        {
            List<Payment> payments = new List<Payment>();
            DB.loadQuery("select * from payments where fk_flight=?id");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            DataTable dt = DB.query();
            foreach (DataRow item in dt.Rows)
            {
                payments.Add(new Payment
                    (
                        Convert.ToInt32(item["ID"]),
                        Convert.ToDateTime(item["Date"]),
                        Convert.ToString(item["CardNumber"]),
                        Convert.ToDecimal(item["Sum"]),
                        new Flight(Convert.ToInt32(item["fk_flight"])),
                        new Client(Convert.ToInt32(item["fk_client"]))
                    ));
            }

            return payments;
        }

        public Payment()
        {
            Sum = 0;
        }

        public Payment(Flight flight)
        {
            Flight = flight;
            Client = new Client(Convert.ToInt32(HttpContext.Current.Session["user"]));
        }

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

        public static bool insertPayment(Payment payment)
        {
            try
            {
                DB.loadQuery(@"INSERT INTO payments(date,card_number,sum,fk_flight,fk_client)VALUES(now(),?card_number,?sum,?fk_flight,?fk_client);");
                DB.Command.Parameters.Add("?card_number", MySqlDbType.VarChar).Value = payment.CardNumber;
                DB.Command.Parameters.Add("?sum", MySqlDbType.Decimal).Value = payment.Sum;
                DB.Command.Parameters.Add("?fk_flight", MySqlDbType.Int32).Value = payment.Flight.ID;
                DB.Command.Parameters.Add("?fk_client", MySqlDbType.Int32).Value = payment.Client.ID;
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