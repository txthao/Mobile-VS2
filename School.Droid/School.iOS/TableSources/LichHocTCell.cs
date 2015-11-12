using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace School.iOS
{
	public class LichHocTCell: UITableViewCell
	{
		UILabel thuT,ngayT,monhoc,tietBD,soTiet,phong;

		public int num;
		public LichHocTCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;

			monhoc = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font =UIFont.SystemFontOfSize (12)
			};
			tietBD= new UILabel () {
				Text="Tiết Bắt Đầu ",
				BackgroundColor = UIColor.Clear,

				Font =UIFont.SystemFontOfSize (12)
			};
			soTiet=  new UILabel () {
				Text="Số Tiết ",
				BackgroundColor = UIColor.Clear,
		
				Font =UIFont.SystemFontOfSize (12)
			};
			phong= new UILabel () {
				Text="Phòng ",
				BackgroundColor = UIColor.Clear,
			
				Font =UIFont.SystemFontOfSize (12)
			};
			thuT= new UILabel () {
				
				BackgroundColor = UIColor.Clear,
				TextAlignment=UITextAlignment.Center,
				Font =UIFont.SystemFontOfSize (15)
			};
			ngayT= new UILabel () {
				

				TextAlignment=UITextAlignment.Center,
				Font =UIFont.SystemFontOfSize (15)
			};

			ContentView.AddSubviews (new UIView[] { thuT, ngayT, monhoc, tietBD, soTiet, phong }); 
		}
		public void UpdateCell (string monhoc,string tietBD,string soTiet,string phong,string thuT,string ngayT,int num,bool hasRM)
		{
			this.monhoc.Text = monhoc;
			this.tietBD.Text = "Tiết Bắt Đầu:" +tietBD;
			this.phong.Text= "Phòng:"+phong;
			this.thuT.Text = getDay(thuT);
			this.ngayT.Text = ngayT;
			this.soTiet.Text = "Số Tiết:"+ soTiet;
			this.num=num;
			if (hasRM) {
				this.thuT.BackgroundColor = LayoutHelper.ourCyan;
			
			} else {
				this.thuT.BackgroundColor = LayoutHelper.ourDarkCyan;
			}
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			ngayT.Layer.BorderColor = UIColor.Brown.CGColor;
			ngayT.Layer.BorderWidth = new nfloat(0.5);
			monhoc.Frame = new CGRect (150, 5, ContentView.Bounds.Width - 150, 40);
			tietBD.Frame=  new CGRect (150, 45, 100, 20);
			phong.Frame= new CGRect (150, 70, ContentView.Bounds.Width - 150, 20);
			thuT.Frame= new CGRect (30, 15, 80, 30);
			ngayT.Frame= new CGRect (30, 45,  80, 50);
			soTiet.Frame= new CGRect (250, 45, ContentView.Bounds.Width-250, 20);


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

