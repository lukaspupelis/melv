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
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public static DB DB = new DB();

        public User(int id)
        {
            ID = id;
            DB.loadQuery("select * from users where id=?id");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            DataTable dt = DB.query();
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

        public static List<User> selectUsers()
        {
            List<User> users = new List<User>();
            DB.loadQuery("select * from users");
            DataTable dt = DB.query();
            foreach (DataRow item in dt.Rows)
            {
                users.Add(new User
                (
                    Convert.ToInt32(item["id"]),
                    Convert.ToString(item["name"]),
                    Convert.ToString(item["last_name"]),
                    Convert.ToString(item["email"]),
                    Convert.ToString(item["password"]),
                    Convert.ToString(item["phone"])
                ));
            }

            return users;
        }
    }
}