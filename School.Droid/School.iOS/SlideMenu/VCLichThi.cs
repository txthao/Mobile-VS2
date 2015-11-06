
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
			CGRect frame = listLT.Frame;
			frame.Width = App.Current.width;
			listLT.Frame = frame;
			frame = headers.Frame;
			frame.Width = App.Current.width;
			headers.Frame = frame;
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
					var newlistlt= BLichThi.MakeDataFromXml (SQLite_iOS.GetConnection ());
					List<LichThi> newListLT= await newlistlt;
					var checkRemind=SettingsHelper.LoadSetting("Remind");
					if (checkRemind){
						VCHomeReminder remind= new VCHomeReminder(this);
						await remind.RemindAllLT(newListLT);
					}
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				}
				progress.StopAnimating ();
				list= BLichThi.GetNewestLT(SQLite_iOS.GetConnection());
				if (list.Count>0)
				{
					timeLT.Text="Học Kỳ "+list[0].HocKy+" Năm "+ list[0].NamHoc;
					listLT.Source=new LichThiSource(list,this);
					listLT.ReloadData();
				}
			}
			catch {
			}
		}
	}
}

