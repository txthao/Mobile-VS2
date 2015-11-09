
using System;

using Foundation;
using UIKit;
using School.Core;
using System.Collections.Generic;
using CoreGraphics;

namespace School.iOS
{
	public partial class VCDiemThi : UIViewController
	{
		public static VCDiemThi instance;
		public VCDiemThi () : base ("VCDiemThi", null)
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
			headers.Source = new DiemThiHKSource ();

			listDM.Frame =  LayoutHelper.setlayoutForTB (listDM.Frame );

			headers.Frame = LayoutHelper.setlayoutForHeader (headers.Frame );

			timeDTHK.Frame = LayoutHelper.setlayoutForTimeLB(timeDTHK.Frame );;
			title.Frame = LayoutHelper.setlayoutForTimeTT (title.Frame);
			txtTB10.Frame = LayoutHelper.setlayoutForFooter (txtTB10.Frame, 0, listDM.Frame.Y);
			txtTB4.Frame = txtTB10.Frame;

			CGRect frame = txtTB4.Frame;
			frame.X = App.Current.width - 150;
			txtTB4.Frame = frame;
			txtTC.Frame = LayoutHelper.setlayoutForFooter (txtTC.Frame, 1, listDM.Frame.Y);
			txtTCTL.Frame = LayoutHelper.setlayoutForFooter (txtTCTL.Frame, 2, listDM.Frame.Y);
			txtTBTL.Frame = LayoutHelper.setlayoutForFooter (txtTBTL.Frame, 3, listDM.Frame.Y);
			txtTBTL4.Frame = txtTBTL.Frame;
			frame = txtTBTL4.Frame;
			frame.X = App.Current.width - 150;
			txtTBTL4.Frame = frame;
			txtDRL.Frame = LayoutHelper.setlayoutForFooter (txtTB10.Frame, 4, listDM.Frame.Y);

			txtTB10.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtTB4.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtTC.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtTCTL.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtTBTL.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtTBTL4.Font = UIFont.SystemFontOfSize (App.Current.textSize);
			txtDRL.Font = UIFont.SystemFontOfSize (App.Current.textSize);

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
				await BDiemThi.MakeDataFromXml(SQLite_iOS.GetConnection());
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				}
				progress.StopAnimating ();
				List<DiemMon> listdm= new List<DiemMon>();
				DiemThi dt= BDiemThi.GetNewestDT(SQLite_iOS.GetConnection());
				if (dt!=null)
				{
					timeDTHK.Text="Học Kỳ "+dt.Hocky +" Năm "+dt.NamHoc;
					listdm=BDiemThi.GetDiemMons(SQLite_iOS.GetConnection(),dt.Hocky,dt.NamHoc);
					listDM.Source=new DiemThiHKSource(listdm);
					listDM.ReloadData();
					txtTB10.Text+=dt.DiemTB10;
					txtTB4.Text+=dt.DiemTB4;
					txtTC.Text+=dt.SoTCDat;
					txtTCTL.Text+=dt.SoTCTL;
					txtTBTL.Text+=dt.DiemTBTL10;
					txtTBTL4.Text+=dt.DiemTBTL4;
					txtDRL.Text+=dt.DiemRL;
				}
			}
			catch {
			}
		}
		public static VCDiemThi Instance
		{
			get
			{
				if (instance == null)
					instance = new VCDiemThi ();
				return instance;
			}
		}
	}
}

