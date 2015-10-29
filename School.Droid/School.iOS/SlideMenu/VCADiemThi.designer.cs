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
		UIKit.UITableView headers { get; set; }

		[Outlet]
		UIKit.UITableView listContent { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (headers != null) {
				headers.Dispose ();
				headers = null;
			}

			if (listContent != null) {
				listContent.Dispose ();
				listContent = null;
			}
		}
	}
}
