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
	[Register ("VCHocPhi")]
	partial class VCHocPhi
	{
		[Outlet]
		UIKit.UIButton btMenu { get; set; }

		[Outlet]
		UIKit.UITableView headers { get; set; }

		[Outlet]
		UIKit.UITableView listHeaders { get; set; }

		[Outlet]
		UIKit.UITableView listHP { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView progress { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollView { get; set; }

		[Outlet]
		UIKit.UILabel timeHP { get; set; }

		[Outlet]
		UIKit.UILabel title { get; set; }

		[Outlet]
		UIKit.UILabel txtTienConNo { get; set; }

		[Outlet]
		UIKit.UILabel txtTongTIenDD { get; set; }

		[Outlet]
		UIKit.UILabel txtTongTienHP { get; set; }

		[Outlet]
		UIKit.UILabel txtToSoTC { get; set; }

		[Outlet]
		UIKit.UILabel txtTTLD { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btMenu != null) {
				btMenu.Dispose ();
				btMenu = null;
			}

			if (headers != null) {
				headers.Dispose ();
				headers = null;
			}

			if (listHeaders != null) {
				listHeaders.Dispose ();
				listHeaders = null;
			}

			if (listHP != null) {
				listHP.Dispose ();
				listHP = null;
			}

			if (progress != null) {
				progress.Dispose ();
				progress = null;
			}

			if (scrollView != null) {
				scrollView.Dispose ();
				scrollView = null;
			}

			if (timeHP != null) {
				timeHP.Dispose ();
				timeHP = null;
			}

			if (title != null) {
				title.Dispose ();
				title = null;
			}

			if (txtTienConNo != null) {
				txtTienConNo.Dispose ();
				txtTienConNo = null;
			}

			if (txtTongTIenDD != null) {
				txtTongTIenDD.Dispose ();
				txtTongTIenDD = null;
			}

			if (txtTongTienHP != null) {
				txtTongTienHP.Dispose ();
				txtTongTienHP = null;
			}

			if (txtToSoTC != null) {
				txtToSoTC.Dispose ();
				txtToSoTC = null;
			}

			if (txtTTLD != null) {
				txtTTLD.Dispose ();
				txtTTLD = null;
			}
		}
	}
}
