using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace School.iOS
{
	public class LichHocHKCell: UITableViewCell
	{
		UILabel monhoc, thu, tietBD,soTiet,phong;
		public LichHocHKCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			monhoc  = new UILabel () {

				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			thu = new UILabel () {

				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			tietBD= new UILabel () {

				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			soTiet = new UILabel () {

				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			phong  = new UILabel () {

				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			ContentView.AddSubviews (new UIView[] { monhoc, thu, tietBD, soTiet, phong });
		}
		public LichHocHKCell (NSString cellId,bool key) : base (UITableViewCellStyle.Default, cellId)
		{
			monhoc  = new UILabel () {
				Text="Môn Học",
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			thu = new UILabel () {
				Text="Thứ",
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			tietBD= new UILabel () {
				Text="Tiết BD",
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			soTiet = new UILabel () {
				Text="Số Tiết",
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			phong  = new UILabel () {
				Text="Phòng",
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			ContentView.AddSubviews (new UIView[] { monhoc, thu, tietBD, soTiet, phong });
		}
		public void UpdateCell (string imonhoc, string ithu,string itietbd,string isoTiet,string iphong)
		{
			monhoc.Text = imonhoc;
			thu.Text = ithu;
			tietBD.Text = itietbd;
			soTiet.Text = isoTiet;
			phong.Text = iphong;
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			monhoc.Frame = new CGRect (0,5,ContentView.Bounds.Width-130,20);
			thu.Frame=new CGRect (ContentView.Bounds.Width-130, 5, 30,20);
			tietBD.Frame=new CGRect (ContentView.Bounds.Width-100, 5, 30,20);
			soTiet.Frame=new CGRect (ContentView.Bounds.Width-70, 5, 30,20);
			phong.Frame=new CGRect (ContentView.Bounds.Width-40, 5, 40,20);
		}
	}
}

