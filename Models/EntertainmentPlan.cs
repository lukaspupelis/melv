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

        public EntertainmentPlan(int id)
        {
            ID = id;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from entertainment_plans where id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                Title = Convert.ToString(item["title"]);
                Text = Convert.ToString(item["text"]);
                Price = Convert.ToDecimal(item["price"]);
                Removed = Convert.ToBoolean(item["removed"]);
                Administrator = new Administrator(Convert.ToInt32(item["fk_administrator"]));
            }
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