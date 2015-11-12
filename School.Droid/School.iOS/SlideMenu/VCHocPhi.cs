﻿
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
		public static VCHocPhi instance;
		public VCHocPhi () : base ("VCHocPhi", null)
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
			btMenu=LayoutHelper.NaviButton (btMenu, title.Frame.Y);
			btMenu.TouchUpInside+= (object sender, EventArgs e) => {
				RootViewController.Instance.navigation.ToggleMenu();
			};
			headers.Source = new HocPhiSource ();


			title.Frame = LayoutHelper.setlayoutForTimeTT (title.Frame);
			timeHP.Frame = LayoutHelper.setlayoutForTimeLB(timeHP.Frame);;
			txtToSoTC.Frame = LayoutHelper.setlayoutForFooter (txtToSoTC.Frame, 0, listHP.Frame.Y);
			txtTongTienHP.Frame = LayoutHelper.setlayoutForFooter (txtTongTienHP.Frame, 1, listHP.Frame.Y);
			txtTTLD.Frame = LayoutHelper.setlayoutForFooter (txtTTLD.Frame, 2, listHP.Frame.Y);
			txtTongTIenDD.Frame = LayoutHelper.setlayoutForFooter (txtTongTIenDD.Frame, 3, listHP.Frame.Y);
			txtTienConNo.Frame = LayoutHelper.setlayoutForFooter (txtTienConNo.Frame, 4, listHP.Frame.Y);

			txtToSoTC.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtTongTienHP.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtTTLD.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtTongTIenDD.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtTienConNo.Font = UIFont.SystemFontOfSize (App.Current.textSize);

			listHP.Frame = LayoutHelper.setlayoutForTB (listHP.Frame );

			headers.Frame = LayoutHelper.setlayoutForHeader (headers.Frame );

			progress.Hidden = true;
			LoadData ();

		}
		public async void LoadData()
		{
			try{
				progress.Hidden = false;
				progress.StartAnimating ();	
			List<CTHocPhi> list = new List<CTHocPhi> ();
			bool sync = SettingsHelper.LoadSetting ("AutoUpdate"); 
				if (sync&&Reachability.InternetConnectionStatus ()!=NetworkStatus.NotReachable)
			{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			await BHocPhi.MakeDataFromXml (SQLite_iOS.GetConnection ());
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			}
			progress.StopAnimating ();
			HocPhi hp = BHocPhi.GetHP(SQLite_iOS.GetConnection ());
			if (hp!=null)
			{
					
				listHP.Hidden= false;
				errorLB.Hidden=true;
				headers.Hidden=false;
				txtToSoTC.Hidden=false;
				txtTongTIenDD.Hidden=false;
				txtTTLD.Hidden=false;
				txtTienConNo.Hidden=false;
				txtTongTienHP.Hidden=false;
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
				else
				{
					timeHP.Text="";
					headers.Hidden=true;
					listHP.Hidden= true;
					errorLB.Hidden=false;
					txtToSoTC.Hidden=true;
					txtTongTIenDD.Hidden=true;
					txtTTLD.Hidden=true;
					txtTienConNo.Hidden=true;
					txtTongTienHP.Hidden=true;
				}
			}
			catch {
			}
		}
		public static VCHocPhi Instance
		{
			get
			{
				if (instance == null)
					instance = new VCHocPhi ();
				return instance;
			}
		}
	}
}

