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
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			hocphi = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			miengiam = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			phaidong = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
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
			nfloat width = (ContentView.Bounds.Width - App.Current.labelMHWidth )/ 3;
			nfloat mhwdt = App.Current.labelMHWidth;
			monhoc.Frame = new CGRect (0, 10, mhwdt,20);
			hocphi.Frame = new CGRect (mhwdt, 10,width, 20);
			miengiam.Frame= new CGRect (mhwdt+width, 10,width, 20);
			phaidong.Frame =new CGRect (mhwdt+2*width, 10, width, 20);

		}
	}
}

