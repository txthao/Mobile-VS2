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
				TextAlignment=UITextAlignment.Center,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			tietBD= new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				TextAlignment=UITextAlignment.Center,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			soTiet = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				TextAlignment=UITextAlignment.Center,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			phong  = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				TextAlignment=UITextAlignment.Center,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			hasRM = new UIImageView ();
				
		
			ContentView.AddSubviews (new UIView[] { monhoc, thu, tietBD, soTiet, phong,hasRM});
		
		}
		public LichHocHKCell (NSString cellId,bool key) : base (UITableViewCellStyle.Default, cellId)
		{
			monhoc  = new UILabel () {
				Text="Môn Học",
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Gray,
				TextAlignment=UITextAlignment.Center,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			thu = new UILabel () {
				Text="Thứ",
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Gray,
				TextAlignment=UITextAlignment.Center,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			tietBD= new UILabel () {
				Text="Tiết BD",
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Gray,
				TextAlignment=UITextAlignment.Center,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			soTiet = new UILabel () {
				Text="Số Tiết",
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Gray,
				TextAlignment=UITextAlignment.Center,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			phong  = new UILabel () {
				Text="Phòng",
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Gray,
				TextAlignment=UITextAlignment.Center,
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
			nfloat width = (ContentView.Bounds.Width - App.Current.labelMHWidth )/ 4;
			nfloat mhwdt = App.Current.labelMHWidth;
			monhoc.Frame = new CGRect (0,5,App.Current.labelMHWidth,40);
			thu.Frame=new CGRect (mhwdt, 5, width-10,40);
			tietBD.Frame=new CGRect (mhwdt+width-10, 5, width-10,40);
			soTiet.Frame=new CGRect (mhwdt+2*width-20, 5, width-10,40);
			if (hasRM.Image != null) {
				phong.Frame = new CGRect (mhwdt + 3 * width-30, 5, width+20, 40);
				hasRM.Frame= new CGRect (mhwdt + 4* width-10, 5, 10, 10);
			} else {
				phong.Frame = new CGRect (mhwdt + 3 * width-30, 5, width+30, 40);
			}

		}
	}
}

