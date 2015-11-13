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
	[Register ("VCLichHoc")]
	partial class VCLichHoc
	{
		[Outlet]
		UIKit.UIButton btMenu { get; set; }

		[Outlet]
		UIKit.UIButton btRefresh { get; set; }

		[Outlet]
		UIKit.UILabel errorLB { get; set; }

		[Outlet]
		UIKit.UITableView headers { get; set; }

		[Outlet]
		UIKit.UITableView listLH { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView progress { get; set; }

		[Outlet]
		UIKit.UILabel timeLH { get; set; }

		[Outlet]
		UIKit.UILabel title { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btMenu != null) {
				btMenu.Dispose ();
				btMenu = null;
			}

			if (errorLB != null) {
				errorLB.Dispose ();
				errorLB = null;
			}

			if (headers != null) {
				headers.Dispose ();
				headers = null;
			}

			if (listLH != null) {
				listLH.Dispose ();
				listLH = null;
			}

			if (progress != null) {
				progress.Dispose ();
				progress = null;
			}

			if (btRefresh != null) {
				btRefresh.Dispose ();
				btRefresh = null;
			}

			if (timeLH != null) {
				timeLH.Dispose ();
				timeLH = null;
			}

			if (title != null) {
				title.Dispose ();
				title = null;
			}
		}
	}
}
