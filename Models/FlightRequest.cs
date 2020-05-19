using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MELV_IS.Models
{
    public class FlightRequest
    {
        public int ID { get; set; }
        public bool Direction { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal Price { get; set; }
        public Flight Flight { get; set; }
        public EntertainmentPlan EntertainmentPlan { get; set; }
        public FoodPlan FoodPlan { get; set; }
        public SportPlan SportPlan{ get; set; }
        public Client Client { get; set; }
    }
}