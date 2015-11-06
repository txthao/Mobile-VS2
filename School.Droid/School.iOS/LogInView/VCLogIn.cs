
using System;

using Foundation;
using UIKit;
using BigTed;
using ObjCRuntime;
using School.Core;


namespace School.iOS
{
	public partial class VCLogIn : UIViewController
	{
		public VCLogIn () : base ("VCLogIn", null)
		{
		}
		private static VCLogIn instance=null; 
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			if (BUser.IsLogined (SQLite_iOS.GetConnection ()) == true) {	
				this.PresentViewController(new RootViewController(),true,null);
			}
			txtMaSV.Text = "3111410094";
			txtMatKhau.Text = "itdaihocsg";

			// Perform any additional setup after loading the view, typically from a nib.
		}
		partial void btDangNhapClick (NSObject sender)
		{
			if (checkInfo())
			{
				string tk=txtMaSV.Text.Trim();
				string pass=txtMatKhau.Text.Trim();
				BTProgressHUD.Show ("Đăng nhập...");

				 ApiHelper.Login(tk,pass,"1",error=>{
					if (error==null)
					{
						BTProgressHUD.Dismiss();
						InvokeOnMainThread (()=>{
						txtMaSV.Text=string.Empty;

						txtMatKhau.Text=string.Empty;
						BTProgressHUD.Show ("Đang tải dữ liệu lần đầu...");
						
						BTProgressHUD.Dismiss();
							this.PresentViewController(new RootViewController(),true,null);
						});
					}
					else
					{
						InvokeOnMainThread(()=>BTProgressHUD.Dismiss());
						txtError.Text= error.Message;
					}	
					
				});


			}

		}
		protected bool checkInfo()
		{
			if (String.IsNullOrEmpty (txtMaSV.Text.Trim()) || String.IsNullOrEmpty (txtMatKhau.Text.Trim()))
			{
				txtError.Text="Vui lòng nhập đầy đủ thông tin đăng nhập";
				return false;
			}
			return true;
		}
		public static VCLogIn Instance
		{
			get
			{
				if (instance == null)
					instance = new VCLogIn ();
				return instance;
			}
		}
	}
}

