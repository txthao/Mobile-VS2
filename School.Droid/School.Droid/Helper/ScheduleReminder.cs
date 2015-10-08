using System;
using Android.App;
using Android.Content;
using Android.OS;
using Java.Util;
using Android.Provider;
using School.Core;
using System.Collections.Generic;


namespace School.Droid
{
	public class ScheduleReminder
	{
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
		public static void SetCalenDarLH(Context ctx,  LichHoc lh ,string Date, chiTietLH ctlh,string content)
		{
			TimeForCalendar time=new TimeForCalendar();

			List<string> listTimeLH= new List<string> {"07g00","07g50","08g40","09g00","09g50","10g40","11g30","12g00","12g50","13g40","14g00","14g50","15g40","16g30","17g00","17g50"
			,"18g40"};
		
			chiTietLH ct=ctlh;


			ContentValues eventValues = new ContentValues();
			string tenmh = "";
			int min=0;
			try{
				tenmh = BMonHoc.GetMH (SQLite_Android.GetConnection(),lh.MaMH).TenMH;
				min=int.Parse(ct.SoTiet)*50;
				time= new TimeForCalendar(Date,listTimeLH[int.Parse(ct.TietBatDau-1)]);
				}
			catch{
			}
			eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Nhắc Lịch Học");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "Bạn Có Lịch Học Môn "+tenmh+" Ghi chú: "+content);
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


		public static void SetCalenDarLT(Context ctx,  LichThi lt  )
		{
			TimeForCalendar time=new TimeForCalendar();
			ContentValues eventValues = new ContentValues();
			string tenmh = "";
			try{
				tenmh = BMonHoc.GetMH (SQLite_Android.GetConnection(),lt.MaMH).TenMH;
				time= new TimeForCalendar(lt.NgayThi,lt.GioBD);

			}
			catch{
			}
			eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 2);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Lịch Thi");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "Bạn Có Lịch Thi Môn "+tenmh);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(time.yr,time.month,time.day,time.hr,time.min));
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend,GetDateTimeMS(time.yr,time.month,time.day,time.hr+lt.SoPhut/60,time.min+lt.SoPhut%60));
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

		public static void RemindAllLH(Context ctx, List<LichHoc> listlh)
		{
			foreach (LichHoc lh in listlh) {
				List<chiTietLH> cts = BLichHoc.GetCTLH (SQLite_Android.GetConnection (), lh.Id);
				foreach (chiTietLH ct in cts)
				{
					List<string> listNgayHoc = Common.strListTuanToArrayString (ct.Tuan);
					foreach (string s in listNgayHoc) {
						SetCalenDarLH (ctx, lh, s, ct, "");
					}
				}
				
			}
		}

		public static void RemindAllLT(Context ctx, List<LichThi> listlt)
		{
			foreach (LichThi lt in listlt) {
				SetCalenDarLT (ctx, lt);
			}
		}


		public static void DeleteAlLRemind(Context ctx)
		{
			List<string> list = new List<string> ();
			list.Add ("1");
			int k = 1;
			int deleted;
			do {
				list [0] = k.ToString ();	
				deleted =
				ctx.ContentResolver.
				Delete (
					CalendarContract.Events.ContentUri,
					CalendarContract.Events.InterfaceConsts.Id + " =? ",
					list.ToArray ());
				k++;
			} while (deleted != 0);

		}





		static long GetDateTimeMS (int yr, int month, int day, int hr, int min)
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
				day=int.Parse(time.Substring(3,2));
				month=int.Parse(time.Substring(0,2));
				//20/22/2012
				hr=int.Parse(GioBD.Substring(0,2));
				min=int.Parse(GioBD.Substring(3,2));
			}	
			public TimeForCalendar()
			{
			}
		}

	}
}

