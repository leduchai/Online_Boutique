using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Models.MyModels;
using PagedList;
using PagedList.Mvc;

namespace Online_Boutique.Views.MyProducts
{
    public class MySearchProductsController : Controller
    {
        Online_BoutiqueEntities db = new Online_BoutiqueEntities();
        // GET: TimKiemSP
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KetQuaTimKiem(int? page, FormCollection f, int pagesize = 2)
        {
            string chuoitk = f["txttimkiem"].ToString();
            List<SanPham> listkqtk = db.SanPhams.Where(x => x.tensp.Contains(chuoitk)).ToList();

            int pagenumber = (page ?? 1);
            ViewBag.tukhoa = chuoitk;

            if (listkqtk.Count() == 0)
            {
                ViewBag.thongbao = "Không tìm thấy sản phẩm nào phù hợp với \"" + chuoitk + "\"";
                return View(listkqtk.ToPagedList(pagenumber, pagesize));
            }
            else
            {

                ViewBag.thongbao = "Tìm thấy " + listkqtk.Count() + " sản phẩm phù hợp với \"" + chuoitk + "\"";
                return View(listkqtk.OrderBy(x => x.tensp).ToPagedList(pagenumber, pagesize));
            }

        }

        [HttpGet]
        public ActionResult KetQuaTimKiem(int? page, string keyword, int pagesize = 1)
        {

            List<SanPham> listkqtk = db.SanPhams.Where(x => x.tensp.Contains(keyword)).ToList();

            ViewBag.tukhoa = keyword;   // bởi vì khi chuyển sang action khác thì cái ViewBag.tukhoa trên kia bị mất đi

            int pagenumber = (page ?? 1);

            if (page <= 0)    //bat loi neu thang user sửa đường dẫn page<=0
                pagenumber = 1;



            if (listkqtk.Count() == 0)
            {
                ViewBag.thongbao = "Không tìm thấy sản phẩm nào phù hợp với \"" + keyword + "\"";
                return View(listkqtk.ToPagedList(pagenumber, pagesize));
            }
            else
            {

                //bắt lỗi nếu thằng user sửa url page > @Model.PageCount

                int sumsp = 0;
                for (int i = 1; i <= page; i++)
                {
                    sumsp += pagesize;
                    if (sumsp >= listkqtk.Count())
                    {
                        pagenumber = i;
                        break;
                    }

                }

                ViewBag.thongbao = "Tìm thấy " + listkqtk.Count() + " sản phẩm phù hợp với \"" + keyword + "\"";
                return View(listkqtk.OrderBy(x => x.tensp).ToPagedList(pagenumber, pagesize));


            }
        }
    }
}