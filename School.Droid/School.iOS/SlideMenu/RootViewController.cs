
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
			instance = this;
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
					new Section ("Lịch Học") {

						new ImageStringElement("Theo Học Kỳ",UIImage.FromBundle("menupic/Iclichhoc.png")),
						new ImageStringElement("Theo Tuần",UIImage.FromBundle("menupic/calendar.png")),
					},
					new Section ("Điểm Thi") {

						new ImageStringElement("Học Kỳ Hiện Tại",UIImage.FromBundle("menupic/Icdiemthi.png")),
						new ImageStringElement("Tất Cả ",UIImage.FromBundle("menupic/IcAdiemthi.png")),
					}
					,
					new Section ("Học Phí-Lịch Thi") {

						new ImageStringElement("Lịch Thi",UIImage.FromBundle("menupic/Iclichthi.png")),

						new ImageStringElement("Học Phí",UIImage.FromBundle("menupic/Ichocphi.png")),


					},
					new Section("Ứng Dụng") {
						new ImageStringElement("Cài đặt",UIImage.FromBundle("menupic/Icsettings.png")),
						new ImageStringElement("Đăng xuất",UIImage.FromBundle("menupic/signout.png")),
					}
				},
				// Supply view controllers corresponding to menu items:
				ViewControllers = new UIViewController[] {
					new VCLichHoc(),
					new VCLichHocTuan(),

					new VCDiemThi(),
					new VCADiemThi(),
					new VCLichThi(),
					new VCHocPhi(),
					new VCSettings(),
					new LogOut(),
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
		public void LogOut()
		{
			this.PresentViewController (new VCLogIn(), true, null);
		}

	}
}

