
using System;

using Foundation;
using UIKit;
using School.Core;
using System.Collections.Generic;

namespace School.iOS
{
	public partial class VCADiemThi : UIViewController
	{
		public VCADiemThi () : base ("VCADiemThi", null)
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
				await  BDiemThi.MakeDataFromXml(SQLite_iOS.GetConnection());
				List<DiemThi> listDT = new List<DiemThi>();
				List<DiemMon> list = new List<DiemMon>();
				listDT = BDiemThi.getAll(SQLite_iOS.GetConnection ());
				if (listDT.Count>0)
				{
					foreach (var item in listDT) {

						DiemMon Header = new DiemMon ();
						Header.Hocky = item.Hocky;
						Header.NamHoc = item.NamHoc;
						Header.MaMH = "Header";
						list.Add (Header);
						list.AddRange (BDiemThi.GetDiemMons (SQLite_iOS.GetConnection (),item.Hocky, item.NamHoc));
						DiemMon Footer = new DiemMon ();
						if (item.Hocky != "3") {
							Footer.Hocky = item.Hocky;
							Footer.NamHoc = item.NamHoc;
							Footer.MaMH = "Footer";
							list.Add (Footer);
						}

					}
					listContent.Source=new DiemThiSource(list);
					listContent.ReloadData();
				}
			}
			catch {
			}
		}
	}
}

