using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace School.iOS
{
	public class DiemThiCell: UITableViewCell
	{
		UILabel monhoc,tile,diemkt,diemthi,diemtk;
		bool key = false;
		public DiemThiCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			monhoc = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font =UIFont.SystemFontOfSize (12)
			};
			tile = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				TextAlignment= UITextAlignment.Center,
				Font =UIFont.SystemFontOfSize (12)
			};
			diemkt = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				TextAlignment= UITextAlignment.Center,
				Font =UIFont.SystemFontOfSize (12)
			};
			diemthi = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				TextAlignment= UITextAlignment.Center,
				Font =UIFont.SystemFontOfSize (12)
			};
			diemtk = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font =UIFont.SystemFontOfSize (12),
				TextAlignment= UITextAlignment.Center,
			};
			ContentView.AddSubviews (new UIView[] { monhoc, tile, diemkt, diemthi,diemtk });

		}
		public DiemThiCell (NSString cellId,bool key) : base (UITableViewCellStyle.Default, cellId)
		{
			this.key = true;
			monhoc = new UILabel () {
				Text="Môn Học",
				TextAlignment= UITextAlignment.Center,
				Lines=2,
				BackgroundColor = LayoutHelper.ourDarkCyan,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			tile = new UILabel () {
				Text="Tỉ Lệ",
				TextAlignment= UITextAlignment.Center,
				Lines=2,
				BackgroundColor = LayoutHelper.ourDarkCyan,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			diemkt = new UILabel () {
				Text="Điểm KT",
				TextAlignment= UITextAlignment.Center,
				Lines=2,
				BackgroundColor = LayoutHelper.ourDarkCyan,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			diemthi = new UILabel () {
				Text="Điểm Thi",
				TextAlignment= UITextAlignment.Center,
				Lines=2,
				BackgroundColor = LayoutHelper.ourDarkCyan,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			diemtk = new UILabel () {
				Text="Điểm TK",
				TextAlignment= UITextAlignment.Center,
				Lines=2,
				BackgroundColor = LayoutHelper.ourDarkCyan,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			ContentView.AddSubviews (new UIView[] { monhoc, tile, diemkt, diemthi,diemtk });

		}
		public void UpdateCell (string imonhoc,string itile,string idiemkt, string idiemthi,string idiemtk,string diemchu)
		{
			monhoc.Text = imonhoc;
			tile.Text = ApiHelper.calPercent (int.Parse(itile));
			diemkt.Text = ApiHelper.checkSpaceValue(idiemkt);
			diemthi.Text = ApiHelper.checkSpaceValue(idiemthi);
			diemtk.Text =  ApiHelper.setDiemTK(idiemtk, diemchu);
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			if (key) {
				monhoc.Frame = new CGRect (0, 5, ContentView.Bounds.Width - 200, 50);
			} else {
				monhoc.Frame = new CGRect (7, 5, ContentView.Bounds.Width - 200, 50);
			}
			monhoc.Frame = new CGRect (0, 5, ContentView.Bounds.Width - 200, 50);
			tile.Frame= new CGRect (ContentView.Bounds.Width-200, 5, 50,50);
			diemkt.Frame= new CGRect (ContentView.Bounds.Width-150, 5, 50,50);
			diemthi.Frame = new CGRect (ContentView.Bounds.Width-100, 5, 50,50);
			diemtk.Frame= new CGRect (ContentView.Bounds.Width-50, 5, 50,50);
		}
	}
}

