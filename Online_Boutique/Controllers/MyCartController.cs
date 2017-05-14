using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Models.MyModels;

namespace Online_Boutique.Controllers
{
    public class MyCartController : Controller
    {
        Online_BoutiqueEntities db = new Online_BoutiqueEntities();
        // GET: MyCart
        public ActionResult Index()
        {
            return View();
        }

        //lay gio hang
        public List<GioHang> LayGioHang()   //lay cai session ra
        {
            List<GioHang> listgh = Session["cart"] as List<GioHang>;

            // viet như thế này nếu session nó chưa tồn tại thì ép kiểu sẽ lỗi
            //còn viết như trên kia, nếu  session chưa tồn tại thì trả về null
            //List<GioHang> listgh = (List<GioHang>)Session["cart"]
            if (listgh == null)
            {
                //neu gio hang chua ton tai thi khoi tao list gio hang (session gio hang)
                listgh = new List<GioHang>();
                Session["cart"] = listgh;
            }
            return listgh; 
        }


        //thêm giỏ hàng
        public ActionResult ThemGioHang(int masp,string url)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.masp == masp);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay ra session gio hang
            List<GioHang> listgh = LayGioHang();

            GioHang sp2 = listgh.Find(x => x.masp == masp);
            if(sp2==null)
            {
                sp2 = new GioHang(masp);
                listgh.Add(sp2);
                return Redirect(Request.UrlReferrer.ToString());
                //bản chất của return Redirect(url); vẫn giống câu lệnh trên và vẫn bị load lại trang

            }
            else
            {
                sp2.slmuasp++;
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        // cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int masp,FormCollection fc)
        {
            //cai formcollection kia la 1 control của thẻ form
            //cho người dùng điều chỉnh slmua,có thể là dropdownlist,textbox,...

            //!!??lay gio hang de biêt trong do no co nhưng sp nao

            SanPham sp = db.SanPhams.SingleOrDefault(x => x.masp == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<GioHang> listgh = LayGioHang();
            GioHang sp2 = listgh.SingleOrDefault(x => x.masp == masp);
            //dkm tai sao deo phai Find
            if (sp2 != null)
            {
                sp2.slmuasp = int.Parse(fc["ddl"].ToString());
            }
            return RedirectToAction("GioHang","MyCart");
            
        }


        //xoa gio hang
        public ActionResult XoaGioHang(int masp)
        {

            SanPham sp = db.SanPhams.SingleOrDefault(x => x.masp == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listgh = LayGioHang();
            GioHang sp2 = listgh.SingleOrDefault(x => x.masp == masp);
            //dkm tai sao deo phai Find
            if (sp2 != null)
            {
                listgh.Remove(sp2);
            }
            if(listgh.Count==0)
            {
                return RedirectToAction("Index", "MyHome");
            }
            return RedirectToAction("GioHang");

        }

        //xay dung trang gio hang
        public ActionResult GioHang()
        {
            if (Session["cart"] == null)
                return RedirectToAction("Index", "Home");

            List<GioHang> listgh = LayGioHang();
            ViewBag.tongtien = TongThanhTien();
            return View(listgh);
            
        }


        // tinh tong sl mua va thanh tien

        public int TongSLMua()
        {
            int tongslmua = 0;
            List<GioHang> listgh = Session["cart"] as List<GioHang>;
            if (listgh != null)
                tongslmua = listgh.Sum(x => x.slmuasp);
            return tongslmua;
        }

        public Nullable<int> TongThanhTien()
        {
            Nullable<int> tongthanhtien = 0;
            List<GioHang> listgh = Session["cart"] as List<GioHang>;
            if (listgh != null)
                tongthanhtien = listgh.Sum(x => x.thanhtien);
          
            return tongthanhtien;
        }

        #region Đặt Hàng
       // [HttpPost]
       // public ActionResult DatHang()
        //{
        //    //kiem tra dang nhap
        //    if (Session["user"] == null || Session["user"] == "")
        //        return RedirectToAction("DangNhap", "MyUsers");

        //    DonHang ddh = new DonHang();
        //    KhachHang kh = (KhachHang)Session["user"];
        //    List<GioHang> listgh = LayGioHang();
        //    ddh.makh = kh.makh;
        //    ddh.ngaydat = DateTime.Now;

        //    //them don hang
        //    db.DonHang.Add(ddh);
        //    db.SaveChanges();
        //    //them chi tiet don hang
        //    foreach(var item in listgh)
        //    {
        //        Chitietdonhang ctdh = new Chitietdonhang();
        //        ctdh.madh = ddh.madh;
        //        ctdh.masp = item.masp;
        //        ctdh.sl = item.slmuasp;
        //        ctdh.giasp = item.giabansp;
        //        db.Chitietdonhang.Add(ctdh);

        //    }
          
        //    db.SaveChanges();
        //    return RedirectToAction("Index", "MyHome");
        //}
        #endregion
    }
}