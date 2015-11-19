using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace School.iOS
{
	public class LichThiCell: UITableViewCell
	{
		UILabel monthi,thoigian,phongthi,gioBD;
		public int num;
		UIImageView hasRM;
		bool key=false;
		public LichThiCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;

			monthi = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,

				Font =UIFont.SystemFontOfSize (12)
			};
			thoigian = new UILabel () {

				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				TextAlignment= UITextAlignment.Center,
				Font =UIFont.SystemFontOfSize (12)
			};
			phongthi = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				TextAlignment= UITextAlignment.Center,
				Font =UIFont.SystemFontOfSize (12)
			};
			hasRM = new UIImageView ();

			ContentView.AddSubviews (new UIView[] { monthi,thoigian,phongthi,hasRM });
		}
		public LichThiCell (NSString cellId,bool key) : base (UITableViewCellStyle.Default, cellId)
		{
			this.key = true;
			monthi = new UILabel () {
				Text="Môn Thi",
				Lines=2,
				TextAlignment= UITextAlignment.Center,
				BackgroundColor = LayoutHelper.ourDarkCyan,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			thoigian = new UILabel () {
				Text="Thời Gian",
				Lines=2,
				TextAlignment= UITextAlignment.Center,
				BackgroundColor = LayoutHelper.ourDarkCyan,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			phongthi = new UILabel () {
				Text="Phòng Thi",
				Lines=2,
				TextAlignment= UITextAlignment.Center,
				BackgroundColor = LayoutHelper.ourDarkCyan,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			hasRM = new UIImageView ();

			ContentView.AddSubviews (new UIView[] { monthi,thoigian,phongthi });
		}
		public void UpdateCell (string imonthi, string ithoigian, string iphongthi,string igioBD,int num,bool hasRM)
		{
			monthi.Text = " "+imonthi;
			thoigian.Text = ithoigian+"         Lúc:"+igioBD;
			phongthi.Text = iphongthi;
			this.num = num;
			if (hasRM) {
				this.hasRM.Image= UIImage.FromBundle ("menupic/Iclichhoc.png");
				this.hasRM.Frame = new CGRect (0, 5, 20, 10);
			}else {
				this.hasRM.Image = UIImage.FromBundle ("");
			}
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			nfloat width = (ContentView.Bounds.Width - App.Current.labelMHWidth )/ 2;
			nfloat mhwdt = App.Current.labelMHWidth;
			if (key) {
				monthi.Frame = new CGRect (0, 5, mhwdt, 40);
			} else {
				monthi.Frame = new CGRect (7, 5, mhwdt, 40);
			}
			thoigian.Frame = new CGRect (mhwdt, 5, width, 40);
			if (hasRM.Image != null) {
				
				phongthi.Frame = new CGRect (mhwdt + width, 5, width - 10, 40);
			} else {
				phongthi.Frame = new CGRect (mhwdt + width, 5, width , 40);
			}
		}
	}
}

