
using System;

using Foundation;
using UIKit;
using School.Core;
using System.Collections.Generic;

namespace School.iOS
{
	public partial class VCLichThi : UIViewController
	{
		public VCLichThi () : base ("VCLichThi", null)
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
			headers.Source = new LichThiSource ();
			progress.Hidden = true;
			LoadData ();

			// Perform any additional setup after loading the view, typically from a nib.
		}
		private async void LoadData()
		{
			try
			{
				progress.Hidden = false;
				progress.StartAnimating ();
				List<LichThi> list= new List<LichThi>();
				bool sync = SettingsHelper.LoadSetting ("AutoUpdate"); 
				if (sync)
				{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
				await BLichThi.MakeDataFromXml(SQLite_iOS.GetConnection());
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				}
				progress.StopAnimating ();
				list= BLichThi.GetNewestLT(SQLite_iOS.GetConnection());
				if (list.Count>0)
				{
					timeLT.Text="HỌc Kỳ "+list[0].HocKy+"Năm "+ list[0].NamHoc;
					listLT.Source=new LichThiSource(list);
					listLT.ReloadData();
				}
			}
			catch {
			}
		}
	}
}

