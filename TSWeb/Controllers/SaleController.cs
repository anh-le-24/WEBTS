using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSWeb.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Discount()
        {
            return View();
        }
    }
}