
using System;

using Foundation;
using UIKit;
using SidebarNavigation;

namespace School.iOS
{
	public partial class VCMainContent : UIViewController
	{
		RootViewController root;
		public VCMainContent (RootViewController vroot) : base ("VCMainContent", null)
		{
			root = vroot;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
		

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			menubt.TouchUpInside+= (sender, e) => {

				root.sidebarController.ToggleMenu();
			};
			// Perform any additional setup after loading the view, typically from a nib.
		}

	}
}

