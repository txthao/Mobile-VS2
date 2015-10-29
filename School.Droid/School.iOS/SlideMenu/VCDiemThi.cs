
using System;

using Foundation;
using UIKit;
using School.Core;
using System.Collections.Generic;

namespace School.iOS
{
	public partial class VCDiemThi : UIViewController
	{
		public VCDiemThi () : base ("VCDiemThi", null)
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
			headers.Source = new DiemThiHKSource ();
			LoadData ();
			// Perform any additional setup after loading the view, typically from a nib.
		}
		private async void LoadData()
		{
			try
			{
				await BDiemThi.MakeDataFromXml(SQLite_iOS.GetConnection());
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
	}
}

