using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class DangNhapDKController : Controller
    {
        private readonly DatabaseModel _databaseModel;

        public DangNhapDKController()
        {
            _databaseModel = new DatabaseModel();
        }
        // GET: DangNhapDK
        public ActionResult DangNhap()
        {
            return View();
        }

    }
}