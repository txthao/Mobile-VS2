
using System;

using Foundation;
using UIKit;
using BigTed;
using ObjCRuntime;
using School.Core;
using CoreGraphics;


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
			txtMatKhau.SecureTextEntry = true;
			View.BackgroundColor = UIColor.White;


			CGRect frame = new CGRect ();
			frame = appName.Frame;
			frame.Width = App.Current.width;
			frame.Y = 80;
			appName.Frame = frame;
			appName.TextColor = LayoutHelper.ourCyan;
			frame = appFooter.Frame;
			frame.Width = App.Current.width;
			frame.Y= App.Current.height - 20;
			appFooter.Frame = frame;
			frame = appLogo.Frame;
			frame.Y = appName.Frame.Y + 40;
			frame.Height = App.Current.width / 3+30;
			frame.Width = App.Current.width / 3+30;
			frame.X = App.Current.width / 3-15;
			appLogo.Frame = frame;
			frame = txtMaSV.Frame;
			frame.Width = appLogo.Frame.Width + 40;
			frame.Y = appLogo.Frame.Y +appLogo.Frame.Height+ 30;
			frame.X = App.Current.width / 3 - 35;
			txtMaSV.Frame = frame;
			frame = txtMatKhau.Frame;
			frame.Width = appLogo.Frame.Width + 40;
			frame.Y = txtMaSV.Frame.Y + txtMaSV.Frame.Height + 20; 
			frame.X = App.Current.width / 3 - 35;
			txtMatKhau.Frame = frame;
			frame = btDangNhap.Frame;
			frame.Width = appLogo.Frame.Width -10;
			frame.Y = txtMatKhau.Frame.Y + txtMatKhau.Frame.Height + 20; 
			frame.X = App.Current.width / 3-10 ;
			btDangNhap.Frame = frame;
			btDangNhap.BackgroundColor = LayoutHelper.ourCyan;
			btDangNhap.SetTitleColor (UIColor.White, UIControlState.Normal);

			txtMaSV.Layer.BorderColor=LayoutHelper.ourCyan.CGColor;
			txtMatKhau.Layer.BorderColor=LayoutHelper.ourCyan.CGColor;
			txtMaSV.Layer.BorderWidth = 2;
			txtMatKhau.Layer.BorderWidth = 2;
			appFooter.TextColor=LayoutHelper.ourCyan;
			frame = txtError.Frame;

			frame.Width = App.Current.width - 20;
			frame.Y = btDangNhap.Frame.Y + btDangNhap.Frame.Height ; 
			frame.X = 10;
			txtError.Lines = 0;
			txtError.TextAlignment = UITextAlignment.Center;
			txtError.LineBreakMode = UILineBreakMode.WordWrap;
			txtError.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtError.Frame = frame;
			txtMaSV.Text = "3111410094";
			txtMatKhau.Text = "itdaihocsg";

			// Perform any additional setup after loading the view, typically from a nib.
		}
		partial void btDangNhapClick (NSObject sender)
		{
			if (Reachability.InternetConnectionStatus ()==NetworkStatus.NotReachable)
			{
				txtError.Text = "Không Có Kết Nối Mạng";
			}
			else
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

