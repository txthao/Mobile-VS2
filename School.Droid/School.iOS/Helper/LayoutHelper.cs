using System;
using UIKit;
using CoreGraphics;

namespace School.iOS
{
	public class LayoutHelper
	{
		public static UIColor ourCyan = UIColor.FromRGBA((float)0.7, (float)0.8, (float)1, (float)1);
		public static UIColor ourDarkCyan = UIColor.FromRGBA((float)0.3, (float)0.8, (float)1, (float)1);
		public static CGRect setlayoutForTB(CGRect frame)
		{
			CGRect iFrame = frame;
			iFrame.Width = App.Current.width;
			iFrame.Y = App.Current.height / 4+15;
			if (App.Current.height <= 568) {
				
				iFrame.Height = App.Current.height / 2 - 70;
			} else {
				iFrame.Height = App.Current.height / 2-110;
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
			nfloat tableHeight = (App.Current.height <= 568) ? App.Current.height / 2 - 70 : App.Current.height / 2 - 110;
			iFrame.Y =  Y + tableHeight;
			iFrame.Height = (App.Current.height - iFrame.Y) / 6-5;
			iFrame.Y = num*iFrame.Height+iFrame.Y+5;
			return iFrame;
		}
		public static UIButton NaviButton(UIButton bt,nfloat Y)
		{
			UIButton mbt = bt;
			
			mbt.SetImage(UIImage.FromBundle("menupic/options27.png"),UIControlState.Normal);
			CGRect iFrame = mbt.Frame;
			iFrame.Width = 30;
			iFrame.Height = 30;
			iFrame.X = 15;
			iFrame.Y = Y;
			mbt.Frame = iFrame;
			return mbt;
		}
		public static UILabel ErrLabel(UILabel lb)
		{
			UILabel mlb = lb;
			CGRect iFrame = mlb.Frame;
			iFrame.Width = App.Current.width;
			iFrame.Height = 40;
			iFrame.X = 0;
			iFrame.Y = App.Current.height / 2-20;
			mlb.Frame = iFrame;
			mlb.Text = "Hiện Tại Chưa Có Dữ Liệu, Vui Lòng Thử Lại Sau";
			mlb.Font = UIFont.ItalicSystemFontOfSize (16);
			mlb.TextAlignment = UITextAlignment.Center;
			mlb.Lines = 0;
			mlb.LineBreakMode = UILineBreakMode.WordWrap;
			mlb.Hidden = true;
			return mlb;
		}
	}
}

