using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Boutique.Models.MyModels
{
    public class GioHang
    {

        Online_BoutiqueEntities db = new Online_BoutiqueEntities();
        public int masp { get; set; }
        public string tensp { get; set; }
        public string anhsp { get; set; }
        public Nullable<int> giabansp { get; set; }
        public int slmuasp { get; set; }
        public Nullable<int> thanhtien {
            get { return slmuasp * giabansp; }
        }

        
        //hàm tạo cho giỏ hàng
        public GioHang()
        {

        }
        public GioHang(int msp)
        {
            masp = msp;
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.masp == masp);
            tensp = sp.tensp;
            anhsp = sp.anhsp;
            giabansp = sp.giabansp;
            slmuasp = 1;
            
        }
    }
}