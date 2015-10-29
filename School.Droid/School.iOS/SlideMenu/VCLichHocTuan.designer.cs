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
		UIKit.UITableView listContent { get; set; }

		[Outlet]
		UIKit.UILabel timeLHTuan { get; set; }

		[Outlet]
		UIKit.UILabel txtngayLHTuan { get; set; }

		[Action ("btTuanKeClick:")]
		partial void btTuanKeClick (Foundation.NSObject sender);

		[Action ("btTuanTRCClick:")]
		partial void btTuanTRCClick (Foundation.NSObject sender);

		[Action ("btTuanTRClick:")]
		partial void btTuanTRClick (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (listContent != null) {
				listContent.Dispose ();
				listContent = null;
			}

			if (timeLHTuan != null) {
				timeLHTuan.Dispose ();
				timeLHTuan = null;
			}

			if (txtngayLHTuan != null) {
				txtngayLHTuan.Dispose ();
				txtngayLHTuan = null;
			}
		}
	}
}
