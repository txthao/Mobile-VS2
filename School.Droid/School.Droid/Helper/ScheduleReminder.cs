using System;
using Android.App;
using Android.Content;
using Android.OS;
namespace School.Droid
{
	public class ScheduleReminder
	{

		public static void setupAlarm(Context context) {
			AlarmManager alarmManager = (AlarmManager)context.GetSystemService ("alarm");
			Intent intent = new Intent(context, typeof(OnAlarmReceive));

			PendingIntent pendingIntent = PendingIntent.GetBroadcast(
				context, 0, intent,
				PendingIntentFlags.UpdateCurrent);
			long t = GetMilisecondsUntilNextCheck (15,38);
			//	alarmManager.Set(AlarmType.ElapsedRealtimeWakeup,, pendingIntent);
			alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime ()+t,30*1000, pendingIntent);

			// Finish the currently running activity

		}
		public static void stopAlarm(Context context)
		{
			AlarmManager alarmManager = (AlarmManager)context.GetSystemService ("alarm");
			Intent intent = new Intent(context, typeof(OnAlarmReceive));
			PendingIntent pendingIntent = PendingIntent.GetBroadcast(
				context, 0, intent,
				PendingIntentFlags.UpdateCurrent);
		
			alarmManager.Cancel (pendingIntent);
		}
		public static long GetMilisecondsUntilNextCheck(int hour,int minute)
		{
			DateTime now = DateTime.Now;
			DateTime todayAtTime = now.Date.AddHours(hour).AddMinutes(minute);
			DateTime nextInstance = now <= todayAtTime ? todayAtTime : todayAtTime.AddDays(1);
			TimeSpan span = nextInstance - now;
			using (var cal = Java.Util.Calendar.GetInstance(Java.Util.TimeZone.Default))
			{
				long t = (long)span.TotalMilliseconds;

				return t;
			}
		}
	}
}

