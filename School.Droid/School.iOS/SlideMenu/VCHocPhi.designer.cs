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
		UIKit.UITableView listHP { get; set; }

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
			if (txtTongTienHP != null) {
				txtTongTienHP.Dispose ();
				txtTongTienHP = null;
			}

			if (txtTTLD != null) {
				txtTTLD.Dispose ();
				txtTTLD = null;
			}

			if (txtTongTIenDD != null) {
				txtTongTIenDD.Dispose ();
				txtTongTIenDD = null;
			}

			if (txtTienConNo != null) {
				txtTienConNo.Dispose ();
				txtTienConNo = null;
			}

			if (listHP != null) {
				listHP.Dispose ();
				listHP = null;
			}

			if (txtToSoTC != null) {
				txtToSoTC.Dispose ();
				txtToSoTC = null;
			}
		}
	}
}
