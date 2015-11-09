using System;
using UIKit;
using CoreGraphics;

namespace School.iOS
{
	public class LayoutHelper
	{
		public static UIColor ourCyan = UIColor.FromRGBA((float)0, (float)0.8, (float)1, (float)1);
		public static UIColor ourDarkCyan = UIColor.FromRGBA((float)0.5, (float)0.8, (float)0.8, (float)1);
		public static CGRect setlayoutForTB(CGRect frame)
		{
			CGRect iFrame = frame;
			iFrame.Width = App.Current.width;
			iFrame.Y = App.Current.height / 4+15;
			if (App.Current.height <= 568) {
				
				iFrame.Height = App.Current.height / 2 - 60;
			} else {
				iFrame.Height = App.Current.height / 2-100;
			}
			return iFrame;
		}
		public static CGRect setlayoutForHeader(CGRect frame)
		{
			CGRect iFrame = frame;
			iFrame.Width = App.Current.width;
			iFrame.Height = 45;
			iFrame.Y = App.Current.height / 4-30;
			return iFrame;
		}
		public static CGRect setlayoutForTimeLB(CGRect frame)
		{
			CGRect iFrame = frame;
			iFrame.Width = App.Current.width;
			iFrame.Y = (App.Current.height <= 568) ? App.Current.height / 4 - 60 : App.Current.height / 4 - 90;
			return iFrame;
		}
		public static CGRect setlayoutForTimeTT(CGRect frame)
		{
			CGRect iFrame = frame;
			iFrame.Width = App.Current.width;
			iFrame.Y =30;
			return iFrame;
		}
		public static CGRect setlayoutForFooter(CGRect frame, int num,nfloat Y)
		{
			CGRect iFrame = frame;
			iFrame.Width = App.Current.width;
			nfloat tableHeight = (App.Current.height <= 568) ? App.Current.height / 2 - 60 : App.Current.height / 2 - 100;
			iFrame.Y = 10 + Y + tableHeight;
			iFrame.Height = (App.Current.height - iFrame.Y) / 6;
			iFrame.Y = num*iFrame.Height+iFrame.Y+5;
			return iFrame;
		}
	}
}

