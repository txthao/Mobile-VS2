// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace School.iOS
{
	[Register ("VCLichHocTuan")]
	partial class VCLichHocTuan
	{
		[Outlet]
		UIKit.UIButton btMenu { get; set; }

		[Outlet]
		UIKit.UIButton btRefresh { get; set; }

		[Outlet]
		UIKit.UIButton btTuanKe { get; set; }

		[Outlet]
		UIKit.UIButton btTuanTrc { get; set; }

		[Outlet]
		UIKit.UILabel errorLB { get; set; }

		[Outlet]
		UIKit.UITableView listContent { get; set; }

		[Outlet]
		UIKit.UILabel mytitle { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView progress { get; set; }

		[Outlet]
		UIKit.UIButton testBT { get; set; }

		[Outlet]
		UIKit.UILabel timeLHTuan { get; set; }

		[Outlet]
		UIKit.UILabel txtngayLHTuan { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btMenu != null) {
				btMenu.Dispose ();
				btMenu = null;
			}

			if (btTuanKe != null) {
				btTuanKe.Dispose ();
				btTuanKe = null;
			}

			if (btTuanTrc != null) {
				btTuanTrc.Dispose ();
				btTuanTrc = null;
			}

			if (errorLB != null) {
				errorLB.Dispose ();
				errorLB = null;
			}

			if (listContent != null) {
				listContent.Dispose ();
				listContent = null;
			}

			if (mytitle != null) {
				mytitle.Dispose ();
				mytitle = null;
			}

			if (progress != null) {
				progress.Dispose ();
				progress = null;
			}

			if (testBT != null) {
				testBT.Dispose ();
				testBT = null;
			}

			if (timeLHTuan != null) {
				timeLHTuan.Dispose ();
				timeLHTuan = null;
			}

			if (btRefresh != null) {
				btRefresh.Dispose ();
				btRefresh = null;
			}

			if (txtngayLHTuan != null) {
				txtngayLHTuan.Dispose ();
				txtngayLHTuan = null;
			}
		}
	}
}
