using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class SanPhamController : Controller
    {
        public ActionResult Index()
        {
            return View();  // Trang Index của sản phẩm
        }

        public ActionResult ThemSanPham()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemSPMoi(string TenSP,
                                   HttpPostedFileBase HinhAnh,
                                   float Gia,
                                   int GiamGia,
                                   DateTime NgayTao,
                                   string MoTa,
                                   int IDVC,
                                   int IDDM)
        {
            // Kiểm tra và lưu hình ảnh
            string fileName = null;
            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                fileName = Path.GetFileName(HinhAnh.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Picture"), fileName);
                HinhAnh.SaveAs(path);
            }

            // Gọi stored procedure
            DatabaseModel db = new DatabaseModel(); // Khởi tạo đối tượng DatabaseModel
            try
            {
                // Xây dựng câu lệnh SQL
                string sql = "EXEC ThemSanPham N'" + TenSP + "', " + Gia + ", N'" +
                             (fileName ?? "NULL") + "', '" +
                             NgayTao.ToString("yyyy-MM-dd") + "', N'" +
                             MoTa + "', " + GiamGia + ", " + IDVC + ", " + IDDM + ";";

                db.get(sql);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, có thể ghi log hoặc trả thông báo lỗi
                ModelState.AddModelError("", "Có lỗi xảy ra trong quá trình thêm sản phẩm: " + ex.Message);
                return View("ThemSanPham"); // Trả lại view để người dùng sửa lỗi
            }

            return RedirectToAction("QLSanPham", "SanPham");
        }

         public ActionResult QLSanPham()
        {
            DatabaseModel db = new DatabaseModel();
            var list = db.get("SELECT * FROM SANPHAM"); // Lấy sản phẩm từ cơ sở dữ liệu
            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        public ActionResult ChinhSuaSP(int IDSP, string TenSP, HttpPostedFileBase HinhAnh, float Gia, int GiamGia, DateTime NgayTao, string MoTa, int IDVC, int IDDM)
        {
            DatabaseModel db = new DatabaseModel();
            string fileName = null;

            // Kiểm tra và lưu hình ảnh mới nếu có
            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                fileName = Path.GetFileName(HinhAnh.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Picture"), fileName);
                HinhAnh.SaveAs(path);
            }
            else
            {
                // Nếu không có hình ảnh mới, giữ lại hình ảnh cũ
                fileName = Request.Form["existingImage"];
            }

            try
            {
                // Xây dựng câu lệnh SQL để cập nhật sản phẩm
                string sql = "EXEC ChinhSuaSanPham " + IDSP + ", N'" + TenSP + "', " + Gia + ", N'" +
                             (fileName ?? "NULL") + "', '" +
                             NgayTao.ToString("yyyy-MM-dd") + "', N'" +
                             MoTa + "', " + GiamGia + ", " + IDVC + ", " + IDDM + ";";
                db.get(sql);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra trong quá trình cập nhật sản phẩm: " + ex.Message);
                return View("ChinhSuaSanPham"); // Trả lại view để người dùng sửa lỗi
            }
            return RedirectToAction("QLSanPham", "SanPham");
        }
        public ActionResult ChinhSuaSanPham(int id)
        {
            DatabaseModel db = new DatabaseModel();
            var productsList = db.get("SELECT * FROM SANPHAM WHERE IDSP = " + id);

            // Chuyển đổi ArrayList sang List<dynamic>
            var productList = productsList.Cast<dynamic>().ToList();
            var product = productList.FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.IDSP = product[0]; // Giả sử cột đầu tiên là IDSP
            ViewBag.TenSP = product[1];
            ViewBag.Gia = product[2];
            ViewBag.GiamGia = product[3];
            ViewBag.MoTa = product[4];
            ViewBag.IDVC = product[5];
            ViewBag.IDDM = product[6];
            ViewBag.HinhAnh = product[7];
            ViewBag.NgayTao = product[8];

            return View();
        }

        public ActionResult ChinhSuaSanPham()
        {
            return View();
        }


        public ActionResult XoaSanPham()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Chitiet()
        {
            return View();
        }
        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult TongQuan()
        {
            return View();
        }
        public ActionResult KhachHang()
        {
            return View();
        }
        public ActionResult NhanVien()
        {
            return View();
        }
       



        public ActionResult ThanhToan()
        {
            return View();
        }
        public ActionResult UuDai()
        {
            return View();
        }
        public ActionResult ChinhSuaSP()
        {
            return View();
        }
        public ActionResult DanhSachSP()
        {
            return View();
        }
        public ActionResult Topping()
        {
            return View();
        }
       
        public ActionResult ThemTopping()
        {
            return View();
        }
        public ActionResult ChinhSuaTopping()
        {
            return View();
        }
    }
}
