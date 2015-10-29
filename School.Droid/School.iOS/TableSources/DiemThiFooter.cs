using System;
using UIKit;
using Foundation;
using CoreGraphics;
using School.Core;

namespace School.iOS
{
	public class DiemThiFooter:UITableViewCell
	{
		UILabel TB10,TB4,TBTL10,TBTL4,TC,TCTL,DRL;

		public DiemThiFooter (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			TB10 = new UILabel () {
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			TB4 = new UILabel () {
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			TBTL10 = new UILabel () {
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			TBTL4 = new UILabel () {
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			TC = new UILabel () {
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			TCTL = new UILabel () {
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			DRL = new UILabel () {
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};

			ContentView.AddSubviews (new UIView[]{ TB10,TB4,TC,TCTL,TBTL10,TBTL4,DRL });
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
		
			TB10.Frame= new CGRect (0, 5, ContentView.Bounds.Width, 20);
			TB4.Frame= new CGRect (0, 25, ContentView.Bounds.Width , 20);
			TC.Frame= new CGRect (0, 50, ContentView.Bounds.Width , 20);
			TCTL.Frame= new CGRect (0, 75, ContentView.Bounds.Width , 20);
			TBTL10.Frame= new CGRect (0, 100, ContentView.Bounds.Width , 20);
			TBTL4.Frame= new CGRect (0, 125, ContentView.Bounds.Width , 20);
			DRL.Frame= new CGRect (0, 150, ContentView.Bounds.Width , 20);
		}
		public void UpdateCell(DiemThi dt)
		{
			TB10.Text = "Điểm TB Hệ 10:"+dt.DiemTB10;
			TB4.Text ="Điểm TB Hệ 4:"+ dt.DiemTB4;
			TC.Text = "Số TC Đạt:" + dt.SoTCDat;
			TCTL.Text = "Số TCTL:" + dt.SoTCTL;
			TBTL10.Text = "Điểm TB TL Hệ 10:" + dt.DiemTBTL10;
			TBTL4.Text = "Điểm TB TL Hệ 4:" + dt.DiemTBTL4;
			DRL.Text = "Điểm Rèn Luyện:" + dt.DiemRL;
		}
	}
}

