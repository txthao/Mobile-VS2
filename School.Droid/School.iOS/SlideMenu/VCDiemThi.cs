
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
		string hocky,namhoc;
		int value;
		string lasthk="0",lastnh="0";
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
			errorLB = LayoutHelper.ErrLabel (errorLB);
			title.Font = UIFont.FromName ("AmericanTypewriter", 21f);

			headers.Source = new DiemThiHKSource ();

			listDM.Frame =  LayoutHelper.setlayoutForTB (listDM.Frame );

			headers.Frame = LayoutHelper.setlayoutForHeader (headers.Frame );

			timeDTHK.Frame = LayoutHelper.setlayoutForTimeLB(timeDTHK.Frame );;
			title.Frame = LayoutHelper.setlayoutForTimeTT (title.Frame);
			btMenu=LayoutHelper.NaviButton (btMenu, title.Frame.Y);
			btMenu.TouchUpInside+= (object sender, EventArgs e) => {
				RootViewController.Instance.navigation.ToggleMenu();
			};
			txtTB10.Frame = LayoutHelper.setlayoutForFooter (txtTB10.Frame, 0, listDM.Frame.Y);
			txtTB4.Frame = txtTB10.Frame;

			CGRect frame = txtTB4.Frame;
			frame.X = App.Current.width - 120;
			txtTB4.Frame = frame;
			txtTC.Frame = LayoutHelper.setlayoutForFooter (txtTC.Frame, 1, listDM.Frame.Y);
			txtTCTL.Frame = LayoutHelper.setlayoutForFooter (txtTCTL.Frame, 2, listDM.Frame.Y);
			txtTBTL.Frame = LayoutHelper.setlayoutForFooter (txtTBTL.Frame, 3, listDM.Frame.Y);
			txtTBTL4.Frame = txtTBTL.Frame;
			frame = txtTBTL4.Frame;
			frame.X = App.Current.width - 120;
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
			progress = LayoutHelper.progressDT (progress);
			LoadData ("0","0");
			btHKKe.TouchUpInside += btnHK_Ke_Click;
			btHKTrc.TouchUpInside += btnHK_Truoc_Click;
			frame = btHKKe.Frame;
			frame.X = App.Current.width-60-frame.Width;
			frame.Y = App.Current.height - 40;
			btHKKe.Frame = frame;
			frame = btHKTrc.Frame;
			frame.Y = App.Current.height - 40;
			frame.X = 60;
			btHKTrc.Frame = frame;
			title.BackgroundColor = UIColor.FromRGBA((float)0.9, (float)0.9, (float)0.9, (float)1);
			// Perform any additional setup after loading the view, typically from a nib.
		}
		public async void LoadData(string hocKy, string namHoc)
		{
			try
			{
				
				progress.StartAnimating ();
				bool sync = SettingsHelper.LoadSetting ("AutoUpdate"); 
				if (sync)
				{
					bool accepted =false;
					while (Reachability.InternetConnectionStatus ()==NetworkStatus.NotReachable&&!accepted)
					{
						accepted = await LayoutHelper.ShowAlert("Lỗi", "Bạn cần mở kết nối để cập nhật dữ liệu mới nhất");


					}
					if (Reachability.InternetConnectionStatus ()!=NetworkStatus.NotReachable)
					{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
				var rs=await BDiemThi.MakeDataFromXml(SQLite_iOS.GetConnection());
						if (rs==null)
						{
							UIAlertView _error = new UIAlertView ("Lỗi", "Xảy ra lỗi trong quá trình cập nhật dữ liệu từ server", null, "Ok", null);

							_error.Show ();
						}
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
					}

				}
				progress.StopAnimating ();
				List<DiemMon> listdm= new List<DiemMon>();
				DiemThi dt;
				if (hocKy == "0"||(lastnh.Equals(namHoc)&&lasthk.Equals(hocKy))) {
				 dt= BDiemThi.GetNewestDT(SQLite_iOS.GetConnection());
					btHKKe.Enabled=false;
					lastnh=dt.NamHoc;
					lasthk=dt.Hocky;
				}
				else {
					btHKKe.Enabled=true;

					dt = BDiemThi.GetDT (SQLite_iOS.GetConnection (), hocKy, namHoc);
					int t=0;
					while (dt == null&&t<3) {
						string hK;
						string nH;
						ApiHelper.calSemester (hocKy, namHoc, value, out hK, out nH);
						dt = BDiemThi.GetDT (SQLite_iOS.GetConnection (), hK, nH);
						t++;
					}
				}
				if (dt!=null)
				{
					listDM.Hidden= false;
					errorLB.Hidden=true;
					headers.Hidden=false;
					hocky=dt.Hocky;
					namhoc=dt.NamHoc;
					timeDTHK.Text="Học Kỳ "+dt.Hocky +" Năm "+dt.NamHoc;
					listdm=BDiemThi.GetDiemMons(SQLite_iOS.GetConnection(),dt.Hocky,dt.NamHoc);
					listDM.Source=new DiemThiHKSource(listdm);
					listDM.ReloadData();
					txtTB10.Text="Điểm TB HK Hệ 10: "+dt.DiemTB10;
					txtTB4.Text="Hệ 4: "+dt.DiemTB4;
					txtTC.Text="Số TC Đạt: "+dt.SoTCDat;
					txtTCTL.Text="Số TC TL: "+dt.SoTCTL;
					txtTBTL.Text="Điểm TB TL Hệ 10: "+dt.DiemTBTL10;
					txtTBTL4.Text="Hệ 4: "+dt.DiemTBTL4;
					txtDRL.Text="Điểm Rèn Luyện: "+dt.DiemRL;
					txtTC.Hidden=false;
					txtTCTL.Hidden=false;
					txtTB10.Hidden=false;
					txtTB4.Hidden=false;
					txtTBTL.Hidden=false;
					txtTBTL4.Hidden=false;
					txtDRL.Hidden=false;
				}
				else
				{
					timeDTHK.Text="";
					headers.Hidden=true;
					listDM.Hidden= true;
					errorLB.Hidden=false;
					txtTC.Hidden=true;
					txtTCTL.Hidden=true;
					txtTB10.Hidden=true;
					txtTB4.Hidden=true;
					txtTBTL.Hidden=true;
					txtTBTL4.Hidden=true;
					txtDRL.Hidden=true;
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
		void btnHK_Truoc_Click (object sender, EventArgs e)
		{
			string hK;
			string nH;
			ApiHelper.calSemester (hocky, namhoc, -1, out hK, out nH);
			value = -1;
			LoadData (hK, nH);
		}

		void btnHK_Ke_Click (object sender, EventArgs e)
		{

			string hK;
			string nH;
			ApiHelper.calSemester (hocky, namhoc, 1, out hK, out nH);
			value = 1;
			LoadData (hK, nH);

		}

	}
}

