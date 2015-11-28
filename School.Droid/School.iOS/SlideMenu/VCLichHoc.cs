
using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using School.Core;
using CoreGraphics;

namespace School.iOS
{
	public partial class VCLichHoc : UIViewController
	{
		public static VCLichHoc instance;
		public VCLichHoc () : base ("VCLichHoc", null)
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

			headers.Source = new LichHocHKSource ();

			timeLH.Frame = LayoutHelper.setlayoutForTimeLB(timeLH.Frame);
	
			listLH.Frame = LayoutHelper.setlayoutForTB (listLH.Frame );
			CGRect frame = listLH.Frame;

			frame.Height = App.Current.height - frame.Y-20;
			listLH.Frame = frame;
			headers.Frame = LayoutHelper.setlayoutForHeader (headers.Frame );

			title.Frame = LayoutHelper.setlayoutForTimeTT (title.Frame);

			timeLH.Text = "";

			progress.Hidden = true;
			progress = LayoutHelper.progressDT (progress);
			btMenu=LayoutHelper.NaviButton (btMenu, title.Frame.Y);
			btMenu.TouchUpInside+= (object sender, EventArgs e) => {
				RootViewController.Instance.navigation.ToggleMenu();
			};
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

				listLH.Hidden= false;
				errorLB.Hidden=true;
				headers.Hidden=false;
				bool sync = SettingsHelper.LoadSetting ("AutoUpdate"); 
				if (sync)
				{
					bool accepted =false;
					while (Reachability.InternetConnectionStatus ()==NetworkStatus.NotReachable&&!accepted)
					{
						accepted = await LayoutHelper.ShowAlert("Lỗi", "Bạn cần mở kết nối để cập nhật dữ liệu mới nhất");


					}
					if (Reachability.InternetConnectionStatus ()!=NetworkStatus.NotReachable)
					{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
					var newlistlh= BLichHoc.MakeDataFromXml (SQLite_iOS.GetConnection ());
					List<LichHoc> newListLH= await newlistlh;var checkRemind=SettingsHelper.LoadSetting("Remind");
						if (newListLH==null)
						{
							UIAlertView _error = new UIAlertView ("Lỗi", "Xảy ra lỗi trong quá trình cập nhật dữ liệu từ server", null, "Ok", null);

							_error.Show ();
						}
						else
						{
					if (checkRemind){
						VCHomeReminder remind= new VCHomeReminder(this);
						await remind.RemindALLLH(newListLH,"");
					}
						UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
					}
					}

				}
				progress.StopAnimating ();
				List<LichHoc> newlistLH= BLichHoc.GetNewestLH(SQLite_iOS.GetConnection());
				if (newlistLH.Count>0)
				{
					var listCT = new List<chiTietLH> ();
					foreach (LichHoc item in newlistLH) {
						listCT.AddRange (BLichHoc.GetCTLH (SQLite_iOS.GetConnection (),item.Id ));
					}
					timeLH.TextAlignment= UITextAlignment.Center;
					timeLH.Text="Học Kỳ " + newlistLH[0].HocKy+ " Năm "+ newlistLH[0].NamHoc;
					listLH.Source=new LichHocHKSource(listCT,this);
					listLH.ReloadData();
				}
				else
				{
					headers.Hidden=true;
					listLH.Hidden= true;
					errorLB.Hidden=false;
				}
			}
			catch {
			}
		}
		public static VCLichHoc Instance
		{
			get
			{
				if (instance == null)
					instance = new VCLichHoc ();
				return instance;
			}
		}
	}
}

