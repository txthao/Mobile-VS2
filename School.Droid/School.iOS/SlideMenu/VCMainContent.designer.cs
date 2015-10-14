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
	[Register ("VCMainContent")]
	partial class VCMainContent
	{
		[Outlet]
		UIKit.UIButton menubt { get; set; }

		[Action ("menu_click:")]
		partial void menu_click (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (menubt != null) {
				menubt.Dispose ();
				menubt = null;
			}
		}
	}
}
