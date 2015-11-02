
using System;

using Foundation;
using UIKit;

namespace School.iOS
{
	public partial class VCSettings : UIViewController
	{
		
		public VCSettings () : base ("VCSettings", null)
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
			swtCNDL.On = SettingsHelper.LoadSetting ("AutoUpdate");
			swtNLich.On=SettingsHelper.LoadSetting ("Remind");
			btCNDL.Enabled = !swtCNDL.On;
			swtCNDL.ValueChanged+= SwtCNDL_ValueChanged;
			swtNLich.ValueChanged+= SwtNLich_ValueChanged;
			btCNDL.TouchUpInside+= BtCNDL_TouchUpInside;

			progress.Hidden = true;
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void SwtNLich_ValueChanged (object sender, EventArgs e)
		{
			SettingsHelper.SaveSetting ("Remind", swtNLich.On);
		}

		void SwtCNDL_ValueChanged (object sender, EventArgs e)
		{
			btCNDL.Enabled = !swtCNDL.On;
			SettingsHelper.SaveSetting ("AutoUpdate", swtCNDL.On);
		}

		async void BtCNDL_TouchUpInside (object sender, EventArgs e)
		{
			
			progress.Hidden = false;
			progress.StartAnimating ();
			try
			{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			var result= ApiHelper.LoadDataFromSV();
			txtResult.Text=await result;
			progress.StopAnimating ();
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			}
			catch {
			txtResult.Text = "Xảy Ra Lỗi Trong Quá Trình Load Dữ Liệu";
			}

		}

	}
}

