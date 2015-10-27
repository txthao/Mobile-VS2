
using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using School.Core;

namespace School.iOS
{
	public partial class VCLichHoc : UIViewController
	{
		public VCLichHoc () : base ("VCLichHoc", null)
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
			headers.Source = new LichHocHKSource ();
			LoadData ();
			// Perform any additional setup after loading the view, typically from a nib.
		}
		private async void LoadData()
		{
			try
			{
				await BLichHoc.MakeDataFromXml(SQLite_iOS.GetConnection ());
				List<LichHoc> newlistLH= BLichHoc.GetNewestLH(SQLite_iOS.GetConnection());
				if (newlistLH.Count>0)
				{
					var listCT = new List<chiTietLH> ();
					foreach (LichHoc item in newlistLH) {
						listCT.AddRange (BLichHoc.GetCTLH (SQLite_iOS.GetConnection (),item.Id ));
					}
					listLH.Source=new LichHocHKSource(listCT);
					listLH.ReloadData();
				}
			}
			catch {
			}
		}
	}
}

