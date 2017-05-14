using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Models.MyModels;
using PagedList;
using PagedList.Mvc;


namespace Online_Boutique.Controllers
{
    public class MyProductsController : Controller
    {
        Online_BoutiqueEntities db = new Online_BoutiqueEntities();
        // GET: MyProducts
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult LoaiSP(int? page, int maloaisp = 0, int pagesize = 2)
        {
            //tao bien so trang
            //???????? mấy dấu ? là thế nào?
            //nếu ko đủ số sp trên 1 trang thì pagenumber=1 ???
            int pagenumber = (page ?? 2);

            if (page <= 0)    //bat loi neu thang user sửa đường dẫn page<=0
                pagenumber = 2;



            LoaiSanPham lsp = db.LoaiSanPhams.SingleOrDefault(x => x.maloaisp == maloaisp);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }   
            else
            {
                ViewBag.mlsp = lsp.maloaisp;

                //bắt lỗi nếu thằng user sửa url page > @Model.PageCount
                List<SanPham> listsp = db.SanPhams.Where(x => x.maloaisp == lsp.maloaisp).ToList();
                int sumsp=0;
                for(int  i=1;i<=page;i++)
                {
                    sumsp += pagesize;
                    if(sumsp>= listsp.Count())
                    {
                        pagenumber = i;
                        break;
                    }
                    
                }
                //////////////////////////////

                return View(db.SanPhams.Where(x => x.maloaisp == lsp.maloaisp).OrderBy(x => x.ngaynhapsp).ToPagedList(pagenumber, pagesize));
             
            }
        }

        public ViewResult ChiTietSP(int masp)
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
 
    }
}