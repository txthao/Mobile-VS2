
using System;

using Foundation;
using UIKit;

using FlyoutNavigation;
using MonoTouch.Dialog;


namespace School.iOS
{

	public partial class RootViewController : UIViewController
	{
		public FlyoutNavigationController navigation;
		private static RootViewController instance=null; 
		public RootViewController () : base ("RootViewController", null)
		{

			navigation = new FlyoutNavigationController ();
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
			navigation = new FlyoutNavigationController {
				// Create the navigation menu
				NavigationRoot = new RootElement ("Navigation") {
					new Section ("Trang") {
						new ImageStringElement("Lịch Học",UIImage.FromBundle("menupic/Iclichhoc.png")),
						new ImageStringElement("Lịch Thi",UIImage.FromBundle("menupic/Iclichthi.png")),
						new ImageStringElement("Điểm Thi",UIImage.FromBundle("menupic/Icdiemthi.png")),
						new ImageStringElement("Học Phí",UIImage.FromBundle("menupic/Ichocphi.png")),


					},
					new Section("Ứng dụng") {
						new ImageStringElement("Cài đặt",UIImage.FromBundle("menupic/Icsettings.png")),
					}
				},
				// Supply view controllers corresponding to menu items:
				ViewControllers = new UIViewController[] {
					new VCLichHoc(),
					new VCLichThi(),
					new VCDiemThi(),
					new VCHocPhi(),
					new VCSettings(),
				},
			};
			// Show the navigation view

			navigation.ToggleMenu ();
			View.AddSubview (navigation.View);
			// Perform any additional setup after loading the view, typically from a nib.
		}
		public static RootViewController Instance
		{
			get
			{
				if (instance == null)
					instance = new RootViewController ();
				return instance;
			}
		}
	}
}

