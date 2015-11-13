
using System;

using Foundation;
using UIKit;
using School.Core;
using System.Collections.Generic;
using CoreGraphics;

namespace School.iOS
{
	public partial class VCLichThi : UIViewController
	{
		public static VCLichThi instance;
		public VCLichThi () : base ("VCLichThi", null)
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

			headers.Source = new LichThiSource ();

			listLT.Frame =LayoutHelper.setlayoutForTB (listLT.Frame);
		
			headers.Frame =LayoutHelper.setlayoutForHeader (headers.Frame );
			timeLT.Frame =  LayoutHelper.setlayoutForTimeLB(timeLT.Frame);
			title.Frame = LayoutHelper.setlayoutForTimeTT (title.Frame);
			btMenu=LayoutHelper.NaviButton (btMenu, title.Frame.Y);
			btMenu.TouchUpInside+= (object sender, EventArgs e) => {
				RootViewController.Instance.navigation.ToggleMenu();
			};

			progress.Hidden = true;
			progress = LayoutHelper.progressDT (progress);


			CGRect frame = listLT.Frame;
			frame.Height = App.Current.height - frame.Y;
			listLT.Frame = frame;
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
				List<LichThi> list= new List<LichThi>();
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
					var newlistlt= BLichThi.MakeDataFromXml (SQLite_iOS.GetConnection ());
					List<LichThi> newListLT= await newlistlt;
					var checkRemind=SettingsHelper.LoadSetting("Remind");
					if (checkRemind){
						VCHomeReminder remind= new VCHomeReminder(this);
						await remind.RemindAllLT(newListLT);
					}
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
					}
				}
				progress.StopAnimating ();
				list= BLichThi.GetNewestLT(SQLite_iOS.GetConnection());
				if (list.Count>0)
				{
					listLT.Hidden= false;
					errorLB.Hidden=true;
					headers.Hidden=false;
					timeLT.Text="Học Kỳ "+list[0].HocKy+" Năm "+ list[0].NamHoc;
					listLT.Source=new LichThiSource(list,this);
					listLT.ReloadData();
				}
				else
				{
					timeLT.Text="";
					listLT.Hidden= true;
					errorLB.Hidden=false;
					headers.Hidden=true;
				}
			}
			catch {
			}
		}
		public static VCLichThi Instance
		{
			get
			{
				if (instance == null)
					instance = new VCLichThi ();
				return instance;
			}
		}
	}
}

