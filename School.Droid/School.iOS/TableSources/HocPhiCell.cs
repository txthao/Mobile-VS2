using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace School.iOS
{
	public class HocPhiCell: UITableViewCell
	{
		UILabel monhoc,hocphi,miengiam,phaidong;

		public HocPhiCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
		
			monhoc = new UILabel () {
				
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			hocphi = new UILabel () {
				
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			miengiam = new UILabel () {
				
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			phaidong = new UILabel () {
				
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};

			ContentView.AddSubviews (new UIView[] { monhoc, hocphi, miengiam, phaidong });
		}
		public void UpdateCell (string imonhoc, string ihocphi, string imiengiam, string iphaidong)
		{
			monhoc.Text = imonhoc;
			hocphi.Text = ihocphi;
			miengiam.Text = imiengiam;
			phaidong.Text = iphaidong;
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

