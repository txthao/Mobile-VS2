using System;
using System.Globalization;

namespace School.Droid
{
	public class Common
	{
		public static string checkSpaceValue (string text)
		{
			
			if (text.Equals ("&nbsp;")) {
				return "";
			}
			return text;
		}

		public static string calPercent (int tiLe)
		{


			return (100 - tiLe).ToString () + "/" + tiLe;
		}


		public static void calSemester (string hk, string nh, int number, out string outHK, out string outNH)
		{
			if ("1".Equals (hk) && (-1).Equals (number)) {
				outHK = "3";
				outNH = (int.Parse (nh) + number).ToString ();
			} else if ("3".Equals (hk) && 1.Equals (number)) {
				outHK = "1";
				outNH = (int.Parse (nh) + number).ToString ();
			} else {
				outHK = (int.Parse (hk) + number).ToString ();
				outNH = nh;
			}
		}

		public static string setDiemTK (string diem10, string diemChu)
		{
			if (!diem10.Equals ("&nbsp;") && !diemChu.Equals ("&nbsp;")) {
				return diem10 + "(" + diemChu + ")";		
			}
			return "";
		}



	}
}

