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
	[Register ("VCLogIn")]
	partial class VCLogIn
	{
		[Outlet]
		UIKit.UILabel appFooter { get; set; }

		[Outlet]
		UIKit.UIImageView appLogo { get; set; }

		[Outlet]
		UIKit.UILabel appName { get; set; }

		[Outlet]
		UIKit.UIButton btDangNhap { get; set; }

		[Outlet]
		UIKit.UILabel txtError { get; set; }

		[Outlet]
		UIKit.UITextField txtMaSV { get; set; }

		[Outlet]
		UIKit.UITextField txtMatKhau { get; set; }

		[Action ("btDangNhapClick:")]
		partial void btDangNhapClick (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (appFooter != null) {
				appFooter.Dispose ();
				appFooter = null;
			}

			if (appLogo != null) {
				appLogo.Dispose ();
				appLogo = null;
			}

			if (appName != null) {
				appName.Dispose ();
				appName = null;
			}

			if (btDangNhap != null) {
				btDangNhap.Dispose ();
				btDangNhap = null;
			}

			if (txtError != null) {
				txtError.Dispose ();
				txtError = null;
			}

			if (txtMaSV != null) {
				txtMaSV.Dispose ();
				txtMaSV = null;
			}

			if (txtMatKhau != null) {
				txtMatKhau.Dispose ();
				txtMatKhau = null;
			}
		}
	}
}
