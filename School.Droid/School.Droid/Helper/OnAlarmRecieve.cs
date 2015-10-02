using System;
using Android.Content;
using Android.App;
using System.Collections.Generic;
using School.Core;

namespace School.Droid
{
	[BroadcastReceiver]
	public class OnAlarmReceive : BroadcastReceiver
	{
		public override void OnReceive (Context context, Intent intent)
		{
			List<LichThi> list = new List<LichThi> ();
			list = BLichThi.GetLichThiByTime (SQLite_Android.GetConnection());
			if(list.Count>=1)
			{
				string nofcontent = "";
			Intent i = new Intent(context, typeof(DrawerActivity));
				if (list.Count==1){
					nofcontent=String.Format("Bạn có lịch thi vào lúc {0}",list[0].GioBD);
				}
			else
			{
				nofcontent=String.Format("Hôm nay bạn phải thi nhiều môn lắm đấy");
			}
			const int pendingIntentId = 0;
			PendingIntent pendingIntent = 
				PendingIntent.GetActivity (context, pendingIntentId, i, PendingIntentFlags.OneShot);
			Notification.Builder builder = new Notification.Builder(context)
				.SetContentIntent (pendingIntent)
				.SetContentTitle ("Nhắc Lịch")
				.SetContentText (nofcontent)
				.SetSmallIcon (Resource.Drawable.SguIcon);
			builder.SetAutoCancel (true);
			Notification notification = builder.Build();
			NotificationManager notificationManager =
				context.GetSystemService (Context.NotificationService) as NotificationManager;
			const int notificationId = 0;
			notificationManager.Notify (notificationId, notification);
		}

		}
	}
}

