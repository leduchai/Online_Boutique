using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Models.MyModels;
using System.IO;

namespace Online_Boutique.Controllers
{
    public class MyManageProductsController : Controller
    {
        Online_BoutiqueEntities db = new Online_BoutiqueEntities();
        // GET: MyManageProducts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachSP()
        {
            return View(db.SanPhams.OrderBy(x=>x.tensp).ToList());
        }

        [HttpGet]
        public ActionResult ThemSP()
        {
            ViewBag.maloaisp = new SelectList(db.LoaiSanPhams.OrderBy(x => x.tenloaisp).ToList(), "maloaisp", "tenloaisp");
            return View();
        }

        [HttpPost]
        // post du lieu len,bao gom ca anh
        public ActionResult ThemSP(SanPham sp, HttpPostedFileBase hpfb)
        {
            ViewBag.maloaisp = new SelectList(db.LoaiSanPhams.OrderBy(x => x.tenloaisp).ToList(), "maloaisp", "tenloaisp");
            if(hpfb==null)
            {
                ViewBag.thongbao = "trường này là bắt buộc, chọn hình ảnh";
            }
            else
            {
                var filename = Path.GetFileName(hpfb.FileName);

                //lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/HinhAnh/Nu"), filename);

                //kiem tra hinh anh da ton tai chua
                if (System.IO.File.Exists(path))
                {
                    ViewBag.thongbao = "hình ảnh đã tồn tại";
                }
                if (ModelState.IsValid)  //thoa man cac validation
                {
                    hpfb.SaveAs(path);  //luu vao folder o visualstudio
                    sp.anhsp = hpfb.FileName;

                    db.SanPhams.Add(sp);
                    db.SaveChanges();


                }
            }
          
            return View(); 
        }

        //chinh sua san pham
        [HttpGet]
        public ActionResult SuaSP(int masp=0)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.masp == masp);
            if (sp == null)
            {
              
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                ViewBag.maloaisp = new SelectList(db.LoaiSanPhams.OrderBy(x => x.tenloaisp).ToList(), "maloaisp", "tenloaisp",sp.maloaisp);
                return View(sp);
            }
        }

        [HttpPost]
        public ActionResult SuaSP(SanPham sp)
        {
            ViewBag.maloaisp = new SelectList(db.LoaiSanPhams.OrderBy(x => x.tenloaisp).ToList(), "maloaisp", "tenloaisp", sp.maloaisp);
            if (ModelState.IsValid)
            {

                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
                return View();
            
            //Cach2
            //    SanPham sp1 = db.SanPhams.SingleOrDefault(x => x.masp = sp.masp);
            //    sp1.cacthuoctinh = sp.cacthuoctinh tương ứng
            //    db.SaveChanges();
        }
      
        [HttpGet]
        public ActionResult XoaSP(int masp)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.masp == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
                return View(sp);
        }

        [HttpPost]
        public ActionResult XoaSP(SanPham sp)
        {

            db.Entry(sp).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges(); ;
            return RedirectToAction("DanhSachSP");
        }
    }
}