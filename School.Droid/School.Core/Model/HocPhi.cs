using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core
{
    public class HocPhi
    {
		public int hocKy;

        public int HocKy
        {
            get { return hocKy; }
            set { hocKy = value; }
        }
        int namHoc;

        public int NamHoc
        {
            get { return namHoc; }
            set { namHoc = value; }
        }

       

        private string tongSoTC;

        public string TongSoTC
        {
            get { return tongSoTC; }
            set { tongSoTC = value; }
        }

        private string tongSoTien;

        public string TongSoTien
        {
            get { return tongSoTien; }
            set { tongSoTien = value; }
        }

        private string tienDongTTLD;

        public string TienDongTTLD
        {
            get { return tienDongTTLD; }
            set { tienDongTTLD = value; }
        }

        private string tienDaDong;

        public string TienDaDong
        {
            get { return tienDaDong; }
            set { tienDaDong = value; }
        }

        private string tienConNo;

        public string TienConNo
        {
            get { return tienConNo; }
            set { tienConNo = value; }
        }

       
    }
}
