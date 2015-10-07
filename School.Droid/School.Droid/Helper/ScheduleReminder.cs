using System;
using Android.App;
using Android.Content;
using Android.OS;
using Java.Util;
using Android.Provider;
using School.Core;


namespace School.Droid
{
	public class ScheduleReminder
	{
		string[] listTimeLH= new string[] {"7g00","7g50","8g40","9g00","9g50","10g40","11g30","12g00","12g50","13g40","14g00","14g50","15g40","16g30","17g00","17g50"
			,"18g40"};
//		public static void setupAlarm(Context context) {
//			AlarmManager alarmManager = (AlarmManager)context.GetSystemService ("alarm");
//			Intent intent = new Intent(context, typeof(OnAlarmReceive));
//
//			PendingIntent pendingIntent = PendingIntent.GetBroadcast(
//				context, 0, intent,
//				PendingIntentFlags.UpdateCurrent);
//			long t = GetMilisecondsUntilNextCheck (15,38);
//			//	alarmManager.Set(AlarmType.ElapsedRealtimeWakeup,, pendingIntent);
//			alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime ()+t,30*1000, pendingIntent);
//
//			// Finish the currently running activity
//
//		}
//		public static void stopAlarm(Context context)
//		{
//			AlarmManager alarmManager = (AlarmManager)context.GetSystemService ("alarm");
//			Intent intent = new Intent(context, typeof(OnAlarmReceive));
//			PendingIntent pendingIntent = PendingIntent.GetBroadcast(
//				context, 0, intent,
//				PendingIntentFlags.UpdateCurrent);
//		
//			alarmManager.Cancel (pendingIntent);
//		}
//		public static long GetMilisecondsUntilNextCheck(int hour,int minute)
//		{
//			DateTime now = DateTime.Now;
//			DateTime todayAtTime = now.Date.AddHours(hour).AddMinutes(minute);
//			DateTime nextInstance = now <= todayAtTime ? todayAtTime : todayAtTime.AddDays(1);
//			TimeSpan span = nextInstance - now;
//			using (var cal = Java.Util.Calendar.GetInstance(Java.Util.TimeZone.Default))
//			{
//				long t = (long)span.TotalMilliseconds;
//
//				return t;
//			}
//		}
		static void SetCalenDarLH(Context ctx,  LichHoc lh ,string Date )
		{
			TimeForCalendar time;

				
			chiTietLH ct;


			ContentValues eventValues = new ContentValues();
			string tenmh = "";
			int min;
			try{
				tenmh = BMonHoc.GetMH (lh.MaMH).TenMH;
				min=int.Parse(ct.SoTiet)*50;
				time= new TimeForCalendar(Date,listTimeLH[ct.TietBatDau]);
				}
			catch{
			}
			eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Nhắc Lịch Học");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "Bạn Có Lịch Học Môn "+tenmh);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(time.yr,time.month,time.day,time.hr,time.min));
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend,GetDateTimeMS(time.yr,time.month,time.day,time.hr+min/60,time.min+min%60));
			eventValues.Put(CalendarContract.Events.InterfaceConsts.AllDay,"0");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.HasAlarm, "1");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone,"GMT+7:00");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone,"GMT+7:00");
			var eventUri = ctx.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
			long eventID = long.Parse(eventUri.LastPathSegment);

			ContentValues remindervalues = new ContentValues();
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes,60);
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.EventId,eventID);
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Method,(int)Android.Provider.RemindersMethod.Alert);
			var reminderURI= ctx.ContentResolver.Insert(CalendarContract.Reminders.ContentUri,remindervalues);
		}


		static void SetCalenDarLT(Context ctx,  LichThi lt  )
		{
			TimeForCalendar time;
			ContentValues eventValues = new ContentValues();
			string tenmh = "";
			try{
				tenmh = BMonHoc.GetMH (lt.MaMH).TenMH;
				time= new TimeForCalendar(lt.NgayThi,lt.GioBD);

			}
			catch{
			}
			eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Lịch Thi");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "Bạn Có Lịch Thi Môn "+tenmh);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(time.yr,time.month,time.day,time.hr,time.min));
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend,GetDateTimeMS(time.yr,time.month,time.day,time.hr+lt.SoPhut/60,endtime.min+lt.SoPhut%60));
			eventValues.Put(CalendarContract.Events.InterfaceConsts.AllDay,"0");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.HasAlarm, "1");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone,"GMT+7:00");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone,"GMT+7:00");
			var eventUri = ctx.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
			long eventID = long.Parse(eventUri.LastPathSegment);

			ContentValues remindervalues = new ContentValues();
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes,60);
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.EventId,eventID);
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Method,(int)Android.Provider.RemindersMethod.Alert);
			var reminderURI= ctx.ContentResolver.Insert(CalendarContract.Reminders.ContentUri,remindervalues);
		}











		long GetDateTimeMS (int yr, int month, int day, int hr, int min)
		{
			Calendar c = Calendar.GetInstance (Java.Util.TimeZone.Default);

			c.Set (CalendarField.DayOfMonth, day);
			c.Set (CalendarField.HourOfDay, hr);
			c.Set (CalendarField.Minute, min);
			c.Set (CalendarField.Month,month-1);
			c.Set (CalendarField.Year, yr);

			return c.TimeInMillis;
		}
		public class TimeForCalendar
		{
			public int yr,  month,  day,  hr,  min;
			public TimeForCalendar(int year,int month,int day,int hour,int min )
			{
				this.yr=year;
				this.month=month;
				this.day=day;
				this.hr=hour;
				this.min=min;
			}
			public TimeForCalendar(string time,string GioBD)
			{
				yr=int.Parse(time.Substring(6,4));
				month=int.Parse(time.Substring(3,2));
				day=int.Parse(time.Substring(0,2));
				//20/22/2012
				hr=int.Parse(GioBD.Substring(0,2));
				min=int.Parse(GioBD.Substring(3,2));
			}	
		}

	}
}

