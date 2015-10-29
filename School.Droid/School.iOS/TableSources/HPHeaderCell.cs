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
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			hocphi = new UILabel () {
				Text="Học Phí",
				BackgroundColor = UIColor.Blue,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			miengiam = new UILabel () {
				Text="Miễn Giảm",
				BackgroundColor = UIColor.Blue,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			phaidong = new UILabel () {
				Text="Phải Đóng",
				BackgroundColor = UIColor.Blue,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};

			ContentView.AddSubviews (new UIView[] { monhoc, hocphi, miengiam, phaidong });
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			monhoc.Frame = new CGRect (0, 10, 80,20);
			hocphi.Frame = new CGRect (80, 10,80, 20);
			miengiam.Frame= new CGRect (160, 10,40, 20);
			phaidong.Frame =new CGRect (200, 10, 80, 20);

		}
	}
}

