using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core
{
	public class CTHocPhi
    {
        int hocKy;

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
        private string maNhom;

        public string MaNhom
        {
            get { return maNhom; }
            set { maNhom = value; }
        }
        private string hocPhi;

        public string HocPhi
        {
            get { return hocPhi; }
            set { hocPhi = value; }
        }
        private string mienGiam;

        public string MienGiam
        {
            get { return mienGiam; }
            set { mienGiam = value; }
        }
        private string phaiDong;

        public string PhaiDong
        {
            get { return phaiDong; }
            set { phaiDong = value; }
        }
		private string maMH;
		public string MaMH
		{
			get { return maMH; }
			set { maMH = value; }
		}
    }
}
