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
	[Register ("VCADiemThi")]
	partial class VCADiemThi
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
		UIKit.UITableView listContent { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView progress { get; set; }

		[Outlet]
		UIKit.UILabel timeDT { get; set; }

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

			if (listContent != null) {
				listContent.Dispose ();
				listContent = null;
			}

			if (btRefresh != null) {
				btRefresh.Dispose ();
				btRefresh = null;
			}

			if (progress != null) {
				progress.Dispose ();
				progress = null;
			}

			if (timeDT != null) {
				timeDT.Dispose ();
				timeDT = null;
			}

			if (title != null) {
				title.Dispose ();
				title = null;
			}
		}
	}
}
