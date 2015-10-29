using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace School.iOS
{
	public class LichHocTCell: UITableViewCell
	{
		UILabel thuT,ngayT,monhoc,tietBD,soTiet,phong;
		public LichHocTCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
			monhoc = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			tietBD= new UILabel () {
				Text="Tiết Bắt Đầu ",
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			soTiet=  new UILabel () {
				Text="Số Tiết ",
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			phong= new UILabel () {
				Text="Phòng ",
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			thuT= new UILabel () {

				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			ngayT= new UILabel () {

				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};

			ContentView.AddSubviews (new UIView[] { thuT, ngayT, monhoc, tietBD, soTiet, phong }); 
		}
		public void UpdateCell (string monhoc,string tietBD,string soTiet,string phong,string thuT,string ngayT)
		{
			this.monhoc.Text = monhoc;
			this.tietBD.Text = "Tiết Bắt Đầu:" +tietBD;
			this.phong.Text= "Phòng:"+phong;
			this.thuT.Text = getDay(thuT);
			this.ngayT.Text = ngayT;
			this.soTiet.Text = "Số Tiết:"+ soTiet;
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			monhoc.Frame = new CGRect (80, 5, ContentView.Bounds.Width - 200, 20);
			tietBD.Frame=  new CGRect (80, 25, 100, 20);
			phong.Frame= new CGRect (80, 50, ContentView.Bounds.Width - 200, 20);
			thuT.Frame= new CGRect (0, 5, 200, 50);
			ngayT.Frame= new CGRect (0, 55,  200, 50);
			soTiet.Frame= new CGRect (180, 25, ContentView.Bounds.Width-250, 20);
		}
		public static string getDay (string day)
		{
			switch (day) {
			case "2":
				return "Thứ Hai";
			case "3":     
				return "Thứ Ba";
			case "4":
				return "Thứ Tư";
			case "5":
				return "Thứ Năm";
			case "6":
				return "Thứ Sáu";
			case "7":
				return "Thứ Bảy";
			default:
				return "Chủ Nhật";
			}

		}
	}
}

