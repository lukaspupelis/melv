﻿using System;
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
    public class FlightRequest
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Kryptis")]
        public bool Direction { get; set; }

        [DisplayName("Išvykimo data")]
        public DateTime DepartureDate { get; set; }

        [DisplayName("Atvykimo data")]
        public DateTime ArrivalDate { get; set; }
        public decimal Price { get; set; }
        public Flight Flight { get; set; }
        public EntertainmentPlan EntertainmentPlan { get; set; }
        public FoodPlan FoodPlan { get; set; }
        public SportPlan SportPlan{ get; set; }
        public Client Client { get; set; }

        public FlightRequest()
        {

        }

        public FlightRequest(int id, bool direction, DateTime departuredate, DateTime arrivaldate, decimal price, 
            Flight flight, EntertainmentPlan entertainmentPlan, FoodPlan foodPlan, SportPlan sportPlan, Client client)
        {
            ID = id;
            Direction = direction;
            DepartureDate = departuredate;
            ArrivalDate = arrivaldate;
            Price = price;
            Flight = flight;
            EntertainmentPlan = entertainmentPlan;
            FoodPlan = foodPlan;
            SportPlan = sportPlan;
            Client = client;
        }

        public static List<FlightRequest> selectFlightRequestsWithPlanData()
        {
            List<FlightRequest> flightRequests = new List<FlightRequest>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from flight_requests"; // imti tik einancio menesio
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                flightRequests.Add(new FlightRequest
                (
                    Convert.ToInt32(item["id"]),
                    Convert.ToBoolean(item["direction"]),
                    Convert.ToDateTime(item["departure_date"]),
                    Convert.ToDateTime(item["arrival_date"]),
                    Convert.ToDecimal(item["price"]),
                    (item["fk_flight"] == System.DBNull.Value ? new Flight() : new Flight(Convert.ToInt32(item["fk_flight"]))),
                    new EntertainmentPlan(Convert.ToInt32(item["fk_entertainment_plan"])),
                    new FoodPlan(Convert.ToInt32(item["fk_food_plan"])),
                    new SportPlan(Convert.ToInt32(item["fk_sport_plan"])),
                    new Client(Convert.ToInt32(item["fk_client"]))
                ));
            }

            return flightRequests;
        }
    }
}