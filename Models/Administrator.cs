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
    public class Administrator : User
    {
        public Administrator(int id) : base(id)
        {

        }

        public static bool isAdmin(string id)
        {
            DB.loadQuery("select * from administrators where fk_user=?id");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            DataTable dt = DB.query();
            if (dt.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}