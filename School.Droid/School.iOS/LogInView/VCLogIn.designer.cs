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
		UIKit.UILabel txtError { get; set; }

		[Outlet]
		UIKit.UITextField txtMaSV { get; set; }

		[Outlet]
		UIKit.UITextField txtMatKhau { get; set; }

		[Action ("btDangNhap:")]
		partial void btDangNhap (Foundation.NSObject sender);

		[Action ("btDangNhapClick:")]
		partial void btDangNhapClick (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
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
