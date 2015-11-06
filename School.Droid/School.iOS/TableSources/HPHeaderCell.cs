using System;
using UIKit;
using CoreGraphics;
using Foundation;

namespace School.iOS
{
	public class HPHeaderCell: UITableViewCell
	{
		UILabel monhoc,hocphi,miengiam,phaidong;
		public HPHeaderCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			monhoc = new UILabel () {
				Text="Môn Học",
				BackgroundColor = UIColor.Blue,
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			hocphi = new UILabel () {
				Text="Học Phí",
				BackgroundColor = UIColor.Blue,
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			miengiam = new UILabel () {
				Text="Miễn Giảm",
				BackgroundColor = UIColor.Blue,
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			phaidong = new UILabel () {
				Text="Phải Đóng",
				BackgroundColor = UIColor.Blue,
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};

			ContentView.AddSubviews (new UIView[] { monhoc, hocphi, miengiam, phaidong });
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			nfloat width = (ContentView.Bounds.Width - App.Current.labelMHWidth )/ 3;
			nfloat mhwdt = App.Current.labelMHWidth;
			monhoc.Frame = new CGRect (0, 10, mhwdt,20);
			hocphi.Frame = new CGRect (mhwdt, 10,width, 20);
			miengiam.Frame= new CGRect (mhwdt+width, 10,width, 20);
			phaidong.Frame =new CGRect (mhwdt+2*width, 10, width, 20);

		}
	}
}

