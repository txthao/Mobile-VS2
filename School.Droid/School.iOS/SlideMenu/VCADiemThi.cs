
using System;

using Foundation;
using UIKit;
using School.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;

namespace School.iOS
{
	public partial class VCADiemThi : UIViewController
	{
		public static VCADiemThi instance;
		public VCADiemThi () : base ("VCADiemThi", null)
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
			listContent.Frame = LayoutHelper.setlayoutForTB (listContent.Frame);
			CGRect iFrame = listContent.Frame;
			iFrame.Height = 3 * (App.Current.height / 4) - 50;
			listContent.Frame = iFrame;
			headers.Frame = LayoutHelper.setlayoutForHeader (headers.Frame );
			title.Frame = LayoutHelper.setlayoutForTimeTT (title.Frame);
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
				await  BDiemThi.MakeDataFromXml(SQLite_iOS.GetConnection());
				
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				}
				List<DiemThi> listDT = new List<DiemThi>();
				List<DiemMon> list = new List<DiemMon>();
				listDT = BDiemThi.getAll(SQLite_iOS.GetConnection ());
				if (listDT.Count>0)
				{
					await Task.Run(()=>
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
					});
					
					listContent.Source=new DiemThiSource(list);
					listContent.ReloadData();
				}
				progress.StopAnimating ();
			}
			catch {
			}
		}
		public static VCADiemThi Instance
		{
			get
			{
				if (instance == null)
					instance = new VCADiemThi ();
				return instance;
			}
		}
	}
}

