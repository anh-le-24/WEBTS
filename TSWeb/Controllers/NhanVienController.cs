﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSWeb.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: NhanVien
        public ActionResult TrangDangNhapNhanVien()
        {
            return View();
        }
        public ActionResult HomeNV()
        {
            return View(); 
        }
    }
}