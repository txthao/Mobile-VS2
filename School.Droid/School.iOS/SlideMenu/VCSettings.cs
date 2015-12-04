
using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using School.Core;
using System.Threading.Tasks;
using CoreGraphics;

namespace School.iOS
{
	public partial class VCSettings : UIViewController
	{
		
		public VCSettings () : base ("VCSettings", null)
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
			title.Font = UIFont.FromName ("AmericanTypewriter", 21f);
		
			SetLayout ();
			txtResult.Lines = 0;
			txtResult.TextColor = UIColor.Green;
			txtResult.LineBreakMode = UILineBreakMode.WordWrap;
			User sv = BUser.GetMainUser (SQLite_iOS.GetConnection ());
			txtMaSV.Text += "    " + sv.Id;
			txtHoTenSV.Text += "    " + sv.Hoten;
			swtCNDL.On = SettingsHelper.LoadSetting ("AutoUpdate");
			swtNLich.On=SettingsHelper.LoadSetting ("Remind");
			btCNDL.Enabled = !swtCNDL.On;
			swtCNDL.ValueChanged+= SwtCNDL_ValueChanged;
			swtNLich.ValueChanged+= SwtNLich_ValueChanged;
			btCNDL.TouchUpInside+= BtCNDL_TouchUpInside;
			btCNDL.BackgroundColor = LayoutHelper.ourDarkCyan;
			btCNDL.SetTitleColor (UIColor.Black, UIControlState.Normal);
			title.Frame = LayoutHelper.setlayoutForTimeTT (title.Frame);
			progress.Hidden = true;
			btMenu=LayoutHelper.NaviButton (btMenu, title.Frame.Y);
			btMenu.TouchUpInside+= (object sender, EventArgs e) => {
				RootViewController.Instance.navigation.ToggleMenu();
			};
			title.BackgroundColor = UIColor.FromRGBA((float)0.9, (float)0.9, (float)0.9, (float)1);

			txtResult.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			// Perform any additional setup after loading the view, typically from a nib.
		}

		async void SwtNLich_ValueChanged (object sender, EventArgs e)
		{
			SettingsHelper.SaveSetting ("Remind", swtNLich.On);
			progress1.Hidden = false;
			progress1.StartAnimating ();
			if (swtNLich.On) {
				try {
					List<LichThi> listlt = BLichThi.GetNewestLT (SQLite_iOS.GetConnection ());

					List<LichHoc> listlh = BLichHoc.GetNewestLH (SQLite_iOS.GetConnection ());
				
					VCHomeReminder reminder = new VCHomeReminder (this);
					await reminder.RemindALLLH (listlh, "");
					await reminder.RemindAllLT (listlt);
					progress1.StopAnimating ();
				} catch {

				}
			} else {
				bool accepted = await ShowAlert("Xoá Nhắc Lịch", "Bạn muốn xoá hết các nhắc lịch đã tạo");
				if (accepted) {
					
					VCHomeReminder reminder = new VCHomeReminder (this);
					await reminder.RemoveAllEvent ();
					BRemind.RemoveAllRM (SQLite_iOS.GetConnection ());
				}
				progress1.StopAnimating ();

			}
			try
			{
				VCLichHoc.Instance.LoadData();
				if (VCADiemThi.instance!=null) VCADiemThi.Instance.LoadData();
				if (VCLichHocTuan.instance!=null) VCLichHocTuan.Instance.LoadData_Tuan(DateTime.Today);
				if (VCLichThi.instance!=null) VCLichThi.Instance.LoadData();
				if (VCDiemThi.instance!=null) VCDiemThi.Instance.LoadData("0","0");
				if (VCHocPhi.instance!=null) VCHocPhi.Instance.LoadData();
			}
			catch{
			}
		}

		void SwtCNDL_ValueChanged (object sender, EventArgs e)
		{
			btCNDL.Enabled = !swtCNDL.On;
			SettingsHelper.SaveSetting ("AutoUpdate", swtCNDL.On);
			if (swtCNDL.On) {
				btCNDL.BackgroundColor = UIColor.DarkGray;
				try {
					VCLichHoc.Instance.LoadData ();
					if (VCADiemThi.instance != null)
						VCADiemThi.Instance.LoadData ();
					if (VCLichHocTuan.instance != null)
						VCLichHocTuan.Instance.LoadData_Tuan (DateTime.Today);
					if (VCLichThi.instance != null)
						VCLichThi.Instance.LoadData ();
					if (VCDiemThi.instance != null)
						VCDiemThi.Instance.LoadData ("0", "0");
					if (VCHocPhi.instance != null)
						VCHocPhi.Instance.LoadData ();
				} catch {
				}
			} else {
				btCNDL.BackgroundColor = LayoutHelper.ourDarkCyan;
			}
			
		
		}

		async void BtCNDL_TouchUpInside (object sender, EventArgs e)
		{
			

			try
			{
				if (Reachability.InternetConnectionStatus ()==NetworkStatus.NotReachable)
				{
					txtResult.Text = "Không Có Kết Nối Mạng";
				}
				else
				{	
					progress.Hidden = false;
					progress.StartAnimating ();
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			var result= ApiHelper.LoadDataFromSV(this);
			txtResult.Text=await result;
			progress.StopAnimating ();
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			VCLichHoc.Instance.LoadData();
				if (VCADiemThi.instance!=null) VCADiemThi.Instance.LoadData();
				if (VCLichHocTuan.instance!=null) VCLichHocTuan.Instance.LoadData_Tuan(DateTime.Today);
				if (VCLichThi.instance!=null) VCLichThi.Instance.LoadData();
				if (VCDiemThi.instance!=null) VCDiemThi.Instance.LoadData("0","0");
				if (VCHocPhi.instance!=null) VCHocPhi.Instance.LoadData();
				}
			}
			catch {
			txtResult.Text = "Xảy Ra Lỗi Trong Quá Trình Load Dữ Liệu";
			}

		}
		public Task<bool> ShowAlert(string title, string message) {
			var tcs = new TaskCompletionSource<bool>();

			UIApplication.SharedApplication.InvokeOnMainThread(new Action(() =>
				{
					UIAlertView alert = new UIAlertView(title, message, null, NSBundle.MainBundle.LocalizedString("Cancel", "Cancel"),
						NSBundle.MainBundle.LocalizedString("OK", "OK"));
					alert.Clicked += (sender, buttonArgs) => tcs.SetResult(buttonArgs.ButtonIndex != alert.CancelButtonIndex);
					alert.Show();
				}));

			return tcs.Task;
		}

		private void SetLayout()
		{
			title1.Font = UIFont.FromName ("AmericanTypewriter", 17f);
			title2.Font = UIFont.FromName ("AmericanTypewriter", 17f);
			title3.Font = UIFont.FromName ("AmericanTypewriter", 17f);


			CGRect frame = new CGRect ();
			frame = title1.Frame;
			frame.Y = title.Frame.Y + 50;
			frame.Width = App.Current.width-20;
			frame.Height = 20;
			frame.X = 20;
			title1.Frame = frame;
			frame = lbNL.Frame;
			frame.Y = title1.Frame.Y + 30;
			frame.X = 30;
			frame.Width = App.Current.width/2+30;
			lbNL.Frame = frame;
			frame = swtNLich.Frame;
			frame.Width = 30;
			frame.Height = 30;
			frame.X = App.Current.width-80;
			frame.Y = lbNL.Frame.Y ;
			swtNLich.Frame = frame;
			frame = progress1.Frame;
			frame.Width = 20;
			frame.Height = 20;
			frame.X = swtNLich.Frame.X+10;
			frame.Y = swtNLich.Frame.Y +35;
			progress1.Frame = frame;
			frame = sepera1.Frame;
			frame.Height = 2;
			frame.Width = App.Current.width - 20;
			frame.X = 10;
			frame.Y = progress1.Frame.Y + 40;
			sepera1.Frame = frame;
			//
			frame = title2.Frame;
			frame.Y = sepera1.Frame.Y + 12;
			frame.Width = App.Current.width-20;
			frame.Height = 20;
			frame.X = 20;
			title2.Frame = frame;
			frame = lbCNDL.Frame;
			frame.Y = title2.Frame.Y + 30;
			frame.X = 30;
			frame.Width = App.Current.width/2+30;
			lbCNDL.Frame = frame;
			frame = swtCNDL.Frame;
			frame.Width = 30;
			frame.Height = 30;
			frame.X = App.Current.width-80;
			frame.Y = lbCNDL.Frame.Y ;
			swtCNDL.Frame = frame;
			frame = btCNDL.Frame;
			frame.X = 30;
			frame.Y = lbCNDL.Frame.Y + lbCNDL.Frame.Height ;
			btCNDL.Frame = frame;
			frame = progress.Frame;
			frame.Width = 30;
			frame.Height = 30;
			frame.X = swtCNDL.Frame.X+10;
			frame.Y = btCNDL.Frame.Y;
			progress.Frame = frame;
			frame = txtResult.Frame;
			frame.Width=App.Current.width-60;
			frame.X = 30;
			frame.Y = progress.Frame.Y + 30;
			txtResult.Frame = frame;
			frame = sepera2.Frame;
			frame.Height = 2;
			frame.Width = App.Current.width - 20;
			frame.X = 10;
			frame.Y = progress.Frame.Y + 70;
			sepera2.Frame = frame;
			//
			frame = title3.Frame;
			frame.Y = 	sepera2.Frame.Y + 12;
			frame.Width = App.Current.width-20;
			frame.X = 20;
			title3.Frame = frame;
			frame = txtMaSV.Frame;
			frame.Y = title3.Frame.Y + 30;
			frame.X = 30;
			frame.Width = App.Current.width/2+30;
			txtMaSV.Frame= frame;
			frame = txtHoTenSV.Frame;
			frame.Y = txtMaSV.Frame.Y + 30;
			frame.X = 30;
			frame.Width = App.Current.width/2+50;
			txtHoTenSV.Frame= frame;
			frame = btFooter.Frame;
			frame.Width = App.Current.width;
			frame.X = 0;
			frame.Y= App.Current.height - 30;
			btFooter.Frame = frame;

			btFooter.TouchUpInside+= BtFooter_TouchUpInside;;

		}

		void BtFooter_TouchUpInside (object sender, EventArgs e)
		{
			UIAlertView _error = new UIAlertView ("About", UrlHelper.About(), null, "Ok", null);

			_error.Show ();
		}
	}
}

