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
    public class EntertainmentPlan
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public decimal? Price { get; set; }

        public bool Removed { get; set; }

        public Administrator Administrator { get; set; }

        public EntertainmentPlan()
        {
        }

        public EntertainmentPlan(int id, string title, string text, decimal price, bool removed, Administrator admin)
        {
            ID = id;
            Title = title;
            Text = text;
            Price = price;
            Removed = removed;
            Administrator = admin;
        }
    }
}