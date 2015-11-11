
using System;

using Foundation;
using UIKit;
using School.Core;
using System.Collections.Generic;

namespace School.iOS
{
	public partial class VCReminder : UIViewController
	{

		public LichHoc lh;
		public string DateForCTLH;
		public chiTietLH ctlh;
		public string content;
		public int MinutesRemind;
		private bool isSaveId;
		public LichThi lt;
		List<string> listTimeLH;
		public VCReminder () : base ("VCReminder", null)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

