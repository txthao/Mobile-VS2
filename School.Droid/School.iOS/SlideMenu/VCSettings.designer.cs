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
	[Register ("VCSettings")]
	partial class VCSettings
	{
		[Outlet]
		UIKit.UIButton btCNDL { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView progress { get; set; }

		[Outlet]
		UIKit.UISwitch swtCNDL { get; set; }

		[Outlet]
		UIKit.UISwitch swtNLich { get; set; }

		[Outlet]
		UIKit.UILabel txtResult { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btCNDL != null) {
				btCNDL.Dispose ();
				btCNDL = null;
			}

			if (progress != null) {
				progress.Dispose ();
				progress = null;
			}

			if (txtResult != null) {
				txtResult.Dispose ();
				txtResult = null;
			}

			if (swtCNDL != null) {
				swtCNDL.Dispose ();
				swtCNDL = null;
			}

			if (swtNLich != null) {
				swtNLich.Dispose ();
				swtNLich = null;
			}
		}
	}
}
