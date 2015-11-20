using System;
using UIKit;
using Foundation;
using School.Core;
using CoreGraphics;

namespace School.iOS
{
	public class LHExpandCell: UITableViewCell
	{
		UILabel gv,mamh,sotc,nhommh,malop,thoigian;
		public LHExpandCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			gv=new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				Text="Giáo Viên:",
				BackgroundColor = UIColor.Clear,
				Font =UIFont.SystemFontOfSize (12)
			};
			mamh=new UILabel () {
				Text="Mã Môn Học: ",
				BackgroundColor = UIColor.Clear,
				Font =UIFont.SystemFontOfSize (12)
			};
			sotc=new UILabel () {
				Text="Số TC: ",
				BackgroundColor = UIColor.Clear,
				Font =UIFont.SystemFontOfSize (12)
			};
			nhommh=new UILabel () {
				Text="Nhóm Môn Học:",
				BackgroundColor = UIColor.Clear,
				Font =UIFont.SystemFontOfSize (12)
			};
			malop= new UILabel () {
				Text="Nhóm Lớp:",
				BackgroundColor = UIColor.Clear,
				Font =UIFont.SystemFontOfSize (12)
			};
			thoigian= new UILabel () {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines=0,
				Text="Thời Gian:",
				BackgroundColor = UIColor.Clear,
				Font =UIFont.SystemFontOfSize (12)
			};
			ContentView.AddSubviews (new UIView[] { gv, thoigian, malop, nhommh, sotc, mamh });
		}
		public void UpdateCell (chiTietLH ct,LichHoc lh,string soTC)
		{
			gv.Text ="Giáo Viên:"+ ct.CBGD;
			thoigian.Text = "Thời Gian:"+ct.ThoigianBD + "-" + ct.ThoigianKT;
			malop.Text = "Mã Lớp:"+lh.MaLop;
			nhommh.Text = "Nhóm Môn:"+ lh.NhomMH;
			sotc.Text= "Số TC:"+soTC;
			mamh.Text= "Mã MH:"+ lh.MaMH;

		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			gv.Frame = new CGRect (80, 5, ContentView.Bounds.Width - 180, 20);
			thoigian.Frame= new CGRect (80, 75, ContentView.Bounds.Width - 80, 20);
			malop.Frame= new CGRect (80, 25, 100, 20);
			nhommh.Frame= new CGRect (190, 25, ContentView.Bounds.Width - 190, 20);
			sotc.Frame= new CGRect (80, 50, 100, 20);
			mamh.Frame= new CGRect (180, 50, ContentView.Bounds.Width - 180, 20);
		}
	}
}

