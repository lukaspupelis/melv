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
    public class Client : User
    {
        public string BirthDate { get; set; }
        public string PersonalNo { get; set; }

        public Client(int id) : base(id)
        {
            DB.loadQuery("select * from clients where fk_user=?id");
            DB.Command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            DataTable dt = DB.query();
            foreach (DataRow item in dt.Rows)
            {
                BirthDate = Convert.ToString(item["birthdate"]);
                PersonalNo = Convert.ToString(item["personal_no"]);
            }
        }

        public Client(int id, string birthdate, string personalno) : base(id)
        {
            BirthDate = birthdate;
            PersonalNo = personalno;
        }
    }
}