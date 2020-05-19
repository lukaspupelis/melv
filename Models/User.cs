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
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        
        public User(int id)
        {
            ID = id;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from users where id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            foreach (DataRow item in dt.Rows)
            {
                Name = Convert.ToString(item["name"]);
                LastName = Convert.ToString(item["last_name"]);
                Email = Convert.ToString(item["email"]);
                Password = Convert.ToString(item["password"]);
                Phone = Convert.ToString(item["phone"]);
            }
        }

        public User(int id, string name, string lastname, string email, string password, string phone)
        {
            ID = id;
            Name = name;
            LastName = lastname;
            Email = email;
            Password = password;
            Phone = phone;
        }
    }
}