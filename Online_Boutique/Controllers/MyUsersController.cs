using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Models.MyModels;

namespace Online_Boutique.Controllers
{
    public class MyUsersController : Controller
    {
        Online_BoutiqueEntities db = new Online_BoutiqueEntities();
        // GET: MyUsers
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKi()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DangKi(KhachHang kh)
        {
            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection fc)
        {
            //FormCollection la cai deo gi
            string username = fc["txttk"].ToString();
            string password = fc["txtmk"].ToString();
            // hoac username = fc.Get("txttk").ToString();
            //      password = fc.Get("txtmk").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(x => x.tentaikhoan == username && x.matkhau == password);
            if (kh != null)
            {
                ViewBag.thongbao = "Đăng nhập thành công";
                Session["user"] = kh;
                Session["username"] = kh.tentaikhoan;

                if (kh.quyenhan == "user")

                    return RedirectToAction("Index", "MyHome");
                else
                    return RedirectToAction("DanhSachSP", "MyManageProducts");
          
                //lưu cái tai khoan vừa đăng nhập thành công lại
                
            }
            else
                ViewBag.thongbao = "Tên tài khoản hoặc mật khẩu không đúng";
            return View();
        }
    }
}