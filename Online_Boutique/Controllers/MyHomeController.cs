using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Models.MyModels;

namespace Online_Boutique.Controllers
{
    
    public class MyHomeController : Controller
    {
        Online_BoutiqueEntities db = new Online_BoutiqueEntities();
        // GET: MyHome
        public ActionResult Index()
        {
            var listnewsp = db.SanPhams.OrderBy(x=>x.ngaynhapsp).Take(12).ToList();
            return View(listnewsp);
        }

        public PartialViewResult HeaderPartial(string gt)
        {
            object listrecord;

           // if (gt == "nu")
            
                listrecord = db.LoaiSanPhams.Where(x => x.gioitinh == "nữ").OrderBy(x => x.tenloaisp).ToList();
          //  else
            
           //      listrecord = db.LoaiSanPhams.Where(x => x.gioitinh == "nam").OrderBy(x => x.tenloaisp).ToList();

            return PartialView(listrecord);
               
            
        }

        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
        public PartialViewResult ContainerPartial()
        {
            return PartialView(db.SanPhams.Take(8).ToList());
        }

    }
}