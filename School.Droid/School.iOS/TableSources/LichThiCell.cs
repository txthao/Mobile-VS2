using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace School.iOS
{
	public class LichThiCell: UITableViewCell
	{
		UILabel monthi,thoigian,phongthi,gioBD;
		public LichThiCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;

			monthi = new UILabel () {

				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			thoigian = new UILabel () {

				BackgroundColor = UIColor.Clear,
				Lines=2,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			phongthi = new UILabel () {

				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};


			ContentView.AddSubviews (new UIView[] { monthi,thoigian,phongthi });
		}
		public LichThiCell (NSString cellId,bool key) : base (UITableViewCellStyle.Default, cellId)
		{
			monthi = new UILabel () {
				Text="Môn Thi",
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			thoigian = new UILabel () {
				Text="Thời Gian",
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			phongthi = new UILabel () {
				Text="Phòng Thi",
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			ContentView.AddSubviews (new UIView[] { monthi,thoigian,phongthi });
		}
		public void UpdateCell (string imonthi, string ithoigian, string iphongthi,string igioBD)
		{
			monthi.Text = imonthi;
			thoigian.Text = ithoigian+"\n"+igioBD;
			phongthi.Text = iphongthi;
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			monthi.Frame= new CGRect (0,5,ContentView.Bounds.Width-160,20);
			thoigian.Frame= new CGRect (ContentView.Bounds.Width-160, 5, 80,20);
			phongthi.Frame= new CGRect (ContentView.Bounds.Width-80, 5, 80,20);
		}
	}
}

