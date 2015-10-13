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

		public Context ctx;
		public LichHoc lh;
		public string DateForCTLH;
		public chiTietLH ctlh;
		public string content;
		public int MinutesRemind;

		public LichThi lt;
		List<string> listTimeLH;
		public ScheduleReminder(Context context)
		{
			ctx = context;
			DateForCTLH = "";
			ctlh = new chiTietLH ();
			MinutesRemind =60;
			content = "";
		}	



		public  void SetCalenDarLH()
		{
			TimeForCalendar time=new TimeForCalendar();

			chiTietLH ct=ctlh;

			listTimeLH= new List<string> {"07g00","07g50","08g40","09g00","09g50","10g40","11g30","12g00","12g50","13g40","14g00","14g50","15g40","16g30","17g00","17g50"
				,"18g40"};
			

			ContentValues eventValues = new ContentValues();
			string tenmh = "";
			int min=0;
			try{
				tenmh = BMonHoc.GetMH (SQLite_Android.GetConnection(),lh.MaMH).TenMH;
				min=int.Parse(ct.SoTiet)*50;
				time= new TimeForCalendar(DateForCTLH,listTimeLH[int.Parse(ct.TietBatDau)-1]);
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
			SaveRMId (eventID);
			ContentValues remindervalues = new ContentValues();
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes,MinutesRemind);
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.EventId,eventID);
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Method,(int)Android.Provider.RemindersMethod.Alert);
			var reminderURI= ctx.ContentResolver.Insert(CalendarContract.Reminders.ContentUri,remindervalues);
		}


		public  void SetCalenDarLT()
		{
			TimeForCalendar time=new TimeForCalendar();
			ContentValues eventValues = new ContentValues();
			string tenmh = "";
			try{
				tenmh = BMonHoc.GetMH (SQLite_Android.GetConnection(),lt.MaMH).TenMH;
				time= new TimeForCalendar(lt.NgayThi,lt.GioBD);
				int tmp;
				tmp=time.month;
				time.month=time.day;
				time.day=tmp;
			}
			catch{
			}

			eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
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
			SaveRMId (eventID);
			ContentValues remindervalues = new ContentValues();
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes,MinutesRemind);
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.EventId,eventID);
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Method,(int)Android.Provider.RemindersMethod.Alert);
			var reminderURI= ctx.ContentResolver.Insert(CalendarContract.Reminders.ContentUri,remindervalues);
		}

		public  void RemindAllLH( List<LichHoc> listlh)
		{
			foreach (LichHoc lh in listlh) {
				List<chiTietLH> cts = BLichHoc.GetCTLH (SQLite_Android.GetConnection (), lh.Id);
				foreach (chiTietLH ct in cts)
				{
					List<string> listNgayHoc = Common.strListTuanToArrayString (ct.Tuan);
					foreach (string s in listNgayHoc) {
						DateForCTLH = s;
						ctlh = ct;
						this.lh = lh;
						SetCalenDarLH ();
					}
				}
				
			}
		}

		public  void RemindAllLT( List<LichThi> listlt)
		{
			foreach (LichThi lt in listlt) {
				this.lt = lt;
				SetCalenDarLT ();
			}
		}


		public  void DeleteAlLRemind(Context ctx)
		{
			List<string> list = new List<string> ();
			var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);
			string listidsaved = prefs.GetString ("ListRMId",null);
			List<string> listid = CovertListId (listidsaved);

			int k = int.Parse(listid[0]);
			list.Add (k.ToString());

			for (int i=0;i<listid.Count;i++)
			{
				list [0] =k.ToString();	
				int deleted =
					ctx.ContentResolver.
					Delete (
						CalendarContract.Events.ContentUri,
						CalendarContract.Events.InterfaceConsts.Id + " =? ",
						list.ToArray ());
				try{
					k=int.Parse(listid[i+1]);
				}
				catch{
					break;
				}

			}
			var prefEditor = prefs.Edit();
			prefEditor.PutString ("ListRMId", "");
			prefEditor.Commit();
		}
		private void SaveRMId(long id)
		{
			var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);
			string listid = prefs.GetString ("ListRMId",null);
			string strid=id.ToString();
			while (strid.Length < 5) {
				strid = "0" + strid;
			}
			listid=listid+strid;
			var prefEditor = prefs.Edit();
			prefEditor.PutString ("ListRMId", listid);
			prefEditor.Commit();
		}

		private List<string> CovertListId(string s)
		{

			int number = s.ToCharArray ().Length / 5;
			List<string> strs = new List<string> ();
			for (int i = 1; i <= number; i++) {
				string a = s.Substring ((i - 1) * 5, 5);
				strs.Add (a);
			}
			return strs;
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

