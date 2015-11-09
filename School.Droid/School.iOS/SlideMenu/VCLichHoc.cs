
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
			headers.Source = new LichHocHKSource ();


	
			listLH.Frame = LayoutHelper.setlayoutForTB (listLH.Frame );

			headers.Frame = LayoutHelper.setlayoutForHeader (headers.Frame );
			title.Frame = LayoutHelper.setlayoutForTimeTT (title.Frame);


			timeLH.Frame = LayoutHelper.setlayoutForTimeLB(timeLH.Frame);
			progress.Hidden = true;
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
				if (sync&&Reachability.InternetConnectionStatus ()!=NetworkStatus.NotReachable)
				{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
					var newlistlh= BLichHoc.MakeDataFromXml (SQLite_iOS.GetConnection ());
					List<LichHoc> newListLH= await newlistlh;var checkRemind=SettingsHelper.LoadSetting("Remind");
					if (checkRemind){
						VCHomeReminder remind= new VCHomeReminder(this);
						await remind.RemindALLLH(newListLH,"");
					}
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
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

