
using System;

using Foundation;
using UIKit;

using SidebarNavigation;


namespace School.iOS
{
	public partial class RootViewController : UIViewController
	{
		public SidebarController sidebarController { get; private set; }
		public RootViewController () : base ("RootViewController", null)
		{
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
			// create a slideout navigation controller with the top navigation controller and the menu view controller
			sidebarController = new SidebarController(this, new VCMainContent(this), new VCMainMenu());
			sidebarController.HasShadowing = false;
			sidebarController.MenuWidth = 220;
			sidebarController.MenuLocation = SidebarController.MenuLocations.Right;
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

