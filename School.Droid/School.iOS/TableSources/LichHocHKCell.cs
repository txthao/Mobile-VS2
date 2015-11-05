using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace School.iOS
{
	public class LichHocHKCell: UITableViewCell
	{
		public UILabel monhoc, thu, tietBD,soTiet,phong;
		public int num;
		UIImageView hasRM;
		public LichHocHKCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			monhoc  = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			thu = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			tietBD= new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			soTiet = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			phong  = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			hasRM = new UIImageView ();
				
		
			ContentView.AddSubviews (new UIView[] { monhoc, thu, tietBD, soTiet, phong,hasRM});
		
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
			hasRM = new UIImageView ();
			ContentView.AddSubviews (new UIView[] { monhoc, thu, tietBD, soTiet, phong });
		}
		public void UpdateCell (string imonhoc, string ithu,string itietbd,string isoTiet,string iphong,int num,bool hasRM)
		{
			monhoc.Text = imonhoc;
			thu.Text = ithu;
			tietBD.Text = itietbd;
			soTiet.Text = isoTiet;
			phong.Text = iphong;
			this.num = num;
			if (hasRM) {
				this.hasRM.Image = UIImage.FromBundle ("menupic/Iclichhoc.png");
			}
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			monhoc.Frame = new CGRect (0,5,ContentView.Bounds.Width-150,40);
			thu.Frame=new CGRect (ContentView.Bounds.Width-150, 5, 30,20);
			tietBD.Frame=new CGRect (ContentView.Bounds.Width-120, 5, 30,20);
			soTiet.Frame=new CGRect (ContentView.Bounds.Width-90, 5, 30,20);
			phong.Frame=new CGRect (ContentView.Bounds.Width-60, 5, 40,20);
			hasRM.Frame = new CGRect (ContentView.Bounds.Width - 20, 5, 20, 10);
		}
	}
}

