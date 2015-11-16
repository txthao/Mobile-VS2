
using System;

using Foundation;
using UIKit;
using School.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;

namespace School.iOS
{
	public partial class VCADiemThi : UIViewController
	{
		public static VCADiemThi instance;
		public VCADiemThi () : base ("VCADiemThi", null)
		{
			instance = this;
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
			errorLB = LayoutHelper.ErrLabel (errorLB);
			title.Font = UIFont.FromName ("AmericanTypewriter", 21f);
			headers.Source = new DiemThiHKSource ();
			listContent.Frame = LayoutHelper.setlayoutForTB (listContent.Frame);
			CGRect iFrame = listContent.Frame;
			iFrame.Height = 3 * (App.Current.height / 4) - 50;
			listContent.Frame = iFrame;
			headers.Frame = LayoutHelper.setlayoutForHeader (headers.Frame );
			title.Frame = LayoutHelper.setlayoutForTimeTT (title.Frame);
			progress.Hidden = true;

			progress = LayoutHelper.progressDT (progress);
			btMenu=LayoutHelper.NaviButton (btMenu, title.Frame.Y);
			btMenu.TouchUpInside+= (object sender, EventArgs e) => {
				RootViewController.Instance.navigation.ToggleMenu();
			};
			timeDT.Frame = LayoutHelper.setlayoutForTimeLB(timeDT.Frame );
			title.BackgroundColor = UIColor.FromRGBA((float)0.9, (float)0.9, (float)0.9, (float)1);
			LoadData ();
			// Perform any additional setup after loading the view, typically from a nib.
		}
		public async void LoadData()
		{
			try
			{
				
				progress.Hidden = false;
				progress.StartAnimating ();
				bool sync = SettingsHelper.LoadSetting ("AutoUpdate"); 
				if (sync)
				{
					bool accepted =false;
					while (Reachability.InternetConnectionStatus ()==NetworkStatus.NotReachable&&!accepted)
					{
						 accepted = await ShowAlert("Lỗi", "Bạn cần mở kết nối để cập nhật dữ liệu mới nhất");
							
				
					}
					if(Reachability.InternetConnectionStatus ()!=NetworkStatus.NotReachable)
					{
						UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
						var rs=await  BDiemThi.MakeDataFromXml(SQLite_iOS.GetConnection());
						if (rs==null)
						{
							UIAlertView _error = new UIAlertView ("Lỗi", "Xảy ra lỗi trong quá trình cập nhật dữ liệu từ server", null, "Ok", null);

							_error.Show ();
						}
						UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
					}
				}

				List<DiemThi> listDT = new List<DiemThi>();
				List<DiemMon> list = new List<DiemMon>();
				listDT = BDiemThi.getAll(SQLite_iOS.GetConnection ());
				if (listDT.Count>0)
				{
					listContent.Hidden= false;
					errorLB.Hidden=true;
					headers.Hidden=false;
					await Task.Run(()=>
					{
					foreach (var item in listDT) {

						DiemMon Header = new DiemMon ();
						Header.Hocky = item.Hocky;
						Header.NamHoc = item.NamHoc;
						Header.MaMH = "Header";
						list.Add (Header);
						list.AddRange (BDiemThi.GetDiemMons (SQLite_iOS.GetConnection (),item.Hocky, item.NamHoc));
						DiemMon Footer = new DiemMon ();
						if (item.Hocky != "3") {
							Footer.Hocky = item.Hocky;
							Footer.NamHoc = item.NamHoc;
							Footer.MaMH = "Footer";
							list.Add (Footer);
						}

					}
					});
					
					listContent.Source=new DiemThiSource(list);
					listContent.ReloadData();
				}
				else
				{
					headers.Hidden=true;
					listContent.Hidden= true;
					errorLB.Hidden=false;
				}
				progress.StopAnimating ();
			}
			catch {
			}
		}
		public static VCADiemThi Instance
		{
			get
			{
				if (instance == null)
					instance = new VCADiemThi ();
				return instance;
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
	}
}

