using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace School.iOS
{
	public class DTHeader: UITableViewCell
	{
		UILabel header;
		public DTHeader (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			header=new UILabel(){
				
				BackgroundColor = UIColor.Brown,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			ContentView.AddSubviews (new UIView[] { header });
		}
		public void UpdateCell (string hocky,string namhoc)
		{
			header.Text = "Học Kỳ " + hocky + " Năm " + namhoc;
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			header.Frame=new CGRect (0, 5, ContentView.Bounds.Width, 40);
		}
	}
}

