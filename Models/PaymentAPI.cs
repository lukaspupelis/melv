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
    public class PaymentAPI
    {
        public static Boolean ReturnPayments(List<Payment> payments)
        {
            //Payment job
            return true;
        }

        public static bool paymentData(Payment payment)
        {
            return true;
        }
    }
}