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
    public class DB
    {
        public MySqlConnection Connection { get; }
        public MySqlCommand Command { get; set; }

        public DB()
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            Connection = new MySqlConnection(conn);
        }

        public void loadQuery(string sqlquery)
        {
            Command = new MySqlCommand(sqlquery, Connection);
        }

        public DataTable query()
        {
            Connection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(Command);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            Connection.Close();
            return dt;
        }

        public void execute()
        {
            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}