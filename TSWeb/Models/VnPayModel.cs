using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSWeb.Models
{
    public class VNPayModel
    {
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public string OrderInfo { get; set; }
        public string Status { get; set; }
    }

}