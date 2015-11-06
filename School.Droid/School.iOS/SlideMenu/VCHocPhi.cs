
using System;

using Foundation;
using UIKit;
using School.Core;
using System.Collections.Generic;
using CoreGraphics;

namespace School.iOS
{
	public partial class VCHocPhi : UIViewController
	{
		public VCHocPhi () : base ("VCHocPhi", null)
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
			headers.Source = new HocPhiSource ();
			CGRect frame = listHP.Frame;
			frame.Width = App.Current.width;
			listHP.Frame = frame;
			frame = headers.Frame;
			frame.Width = App.Current.width;
			headers.Frame = frame;
			progress.Hidden = true;
			LoadData ();

		}
		private async void LoadData()
		{
			try{
				progress.Hidden = false;
				progress.StartAnimating ();	
			List<CTHocPhi> list = new List<CTHocPhi> ();
			bool sync = SettingsHelper.LoadSetting ("AutoUpdate"); 
			if (sync)
			{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			await BHocPhi.MakeDataFromXml (SQLite_iOS.GetConnection ());
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			}
			progress.StopAnimating ();
			HocPhi hp = BHocPhi.GetHP(SQLite_iOS.GetConnection ());
			if (hp!=null)
			{
			timeHP.Text="Học Kỳ "+hp.HocKy+" Năm "+hp.NamHoc;
			list= BHocPhi.GetCTHP(SQLite_iOS.GetConnection (),hp.NamHoc,hp.HocKy);
			listHP.Source = new HocPhiSource (list);
			listHP.ReloadData();
			txtToSoTC.Text+=hp.TongSoTC;
			txtTongTienHP.Text+=hp.TongSoTien;
			txtTTLD.Text+=hp.TienDongTTLD;
			txtTongTIenDD.Text+=hp.TienDaDong;
			txtTienConNo.Text+=hp.TienConNo;
			}
			}
			catch {
			}
		}
	}
}

