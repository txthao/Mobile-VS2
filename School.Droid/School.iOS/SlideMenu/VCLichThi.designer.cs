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
	[Register ("VCLichThi")]
	partial class VCLichThi
	{
		[Outlet]
		UIKit.UITableView headers { get; set; }

		[Outlet]
		UIKit.UITableView listLT { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView progress { get; set; }

		[Outlet]
		UIKit.UILabel timeLT { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (headers != null) {
				headers.Dispose ();
				headers = null;
			}

			if (listLT != null) {
				listLT.Dispose ();
				listLT = null;
			}

			if (progress != null) {
				progress.Dispose ();
				progress = null;
			}

			if (timeLT != null) {
				timeLT.Dispose ();
				timeLT = null;
			}
		}
	}
}
