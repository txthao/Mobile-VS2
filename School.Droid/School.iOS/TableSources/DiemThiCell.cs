using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace School.iOS
{
	public class DiemThiCell: UITableViewCell
	{
		UILabel monhoc,tile,diemkt,diemthi,diemtk;
		public DiemThiCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			monhoc = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			tile = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			diemkt = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			diemthi = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			diemtk = new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("AmericanTypewriter", 12f)
			};
			ContentView.AddSubviews (new UIView[] { monhoc, tile, diemkt, diemthi,diemtk });

		}
		public DiemThiCell (NSString cellId,bool key) : base (UITableViewCellStyle.Default, cellId)
		{
			monhoc = new UILabel () {
				Text="Môn Học",
				Lines=2,
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			tile = new UILabel () {
				Text="Tỉ Lệ",
				Lines=2,
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			diemkt = new UILabel () {
				Text="Điểm KT",
				Lines=2,
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			diemthi = new UILabel () {
				Text="Điểm Thi",
				Lines=2,
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			diemtk = new UILabel () {
				Text="Điểm TK",
				Lines=2,
				BackgroundColor = UIColor.Gray,
				Font = UIFont.FromName("AmericanTypewriter", 15f)
			};
			ContentView.AddSubviews (new UIView[] { monhoc, tile, diemkt, diemthi,diemtk });

		}
		public void UpdateCell (string imonhoc,string itile,string idiemkt, string idiemthi,string idiemtk,string diemchu)
		{
			monhoc.Text = imonhoc;
			tile.Text = itile;
			diemkt.Text = idiemkt;
			diemthi.Text = idiemthi;
			diemtk.Text = idiemtk + " (" + diemchu + ")";
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			monhoc.Frame = new CGRect (0, 5, ContentView.Bounds.Width - 200, 50);
			tile.Frame= new CGRect (ContentView.Bounds.Width-200, 5, 50,50);
			diemkt.Frame= new CGRect (ContentView.Bounds.Width-150, 5, 50,50);
			diemthi.Frame = new CGRect (ContentView.Bounds.Width-100, 5, 50,50);
			diemtk.Frame= new CGRect (ContentView.Bounds.Width-50, 5, 50,50);
		}
	}
}

