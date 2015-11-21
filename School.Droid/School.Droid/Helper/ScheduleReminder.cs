using System;
using Android.App;
using Android.Content;
using Android.OS;
using Java.Util;
using Android.Provider;
using School.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;


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
		private bool isSaveId;
		public LichThi lt;
		List<string> listTimeLH;
		public ScheduleReminder(Context context)
		{
			ctx = context;
			DateForCTLH = "";
			ctlh = new chiTietLH ();
			MinutesRemind =60;
			content = "";
			isSaveId = false;
		}	



		public  void SetCalenDarLH()
		{
			TimeForCalendar time=new TimeForCalendar();
			TimeForCalendar timeend=new TimeForCalendar();
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
				string timeEnd=listTimeLH[int.Parse(ct.TietBatDau)+int.Parse(ct.SoTiet)];
				switch(timeEnd)
				{
				case "09g00":
					timeEnd="08g40";
					break;
				case "14g00":
					timeEnd="13g40";
					break;
				
				}
				timeend= new TimeForCalendar(DateForCTLH,timeEnd);
				
				}
			catch{
			}
			eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Nhắc Lịch Học");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "Bạn Có Lịch Học Môn "+tenmh+" Ghi chú: "+content);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(time.yr,time.month,time.day,time.hr,time.min));
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend,GetDateTimeMS(time.yr,time.month,time.day,timeend.hr,timeend.min));
			eventValues.Put(CalendarContract.Events.InterfaceConsts.AllDay,"0");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.HasAlarm, "1");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone,"GMT+7:00");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone,"GMT+7:00");
			var eventUri = ctx.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
			string eventID = eventUri.LastPathSegment;

			LHRemindItem item = new LHRemindItem ();
			item.EventID = eventID;
			item.Date = DateForCTLH;
			item.IDLH = lh.Id;
			//item.Mess = content;
			//item.Minute = MinutesRemind;
			BRemind.SaveLHRemind(SQLite_Android.GetConnection (),item);

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
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Nhắc Lịch Thi");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "Bạn Có Lịch Thi Môn "+tenmh+" Ghi chú: "+content);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(time.yr,time.month,time.day,time.hr,time.min));
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend,GetDateTimeMS(time.yr,time.month,time.day,time.hr+lt.SoPhut/60,time.min+lt.SoPhut%60));
			eventValues.Put(CalendarContract.Events.InterfaceConsts.AllDay,"0");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.HasAlarm, "1");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone,"GMT+7:00");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone,"GMT+7:00");
			var eventUri = ctx.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
			string eventID = eventUri.LastPathSegment;

			LTRemindItem item = new LTRemindItem ();
			item.EventID = eventID;
			item.HocKy = lt.HocKy;
			item.NamHoc = lt.NamHoc;
			item.MaMH = lt.MaMH;
			//item.Mess = content;
			//item.Minute = MinutesRemind;
			BRemind.SaveLTRemind (SQLite_Android.GetConnection (),item);

			ContentValues remindervalues = new ContentValues();
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes,MinutesRemind);
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.EventId,eventID);
			remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Method,(int)Android.Provider.RemindersMethod.Alert);
			var reminderURI= ctx.ContentResolver.Insert(CalendarContract.Reminders.ContentUri,remindervalues);
		}


		public Task RemindAllLH( List<LichHoc> listlh)
		{
			isSaveId = true;
			return Task.Run(()=>
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
					isSaveId = false;}
			);
		}

		public Task RemindAllLT( List<LichThi> listlt)
		{
			isSaveId = true;
			return Task.Run(()=>
				{
					foreach (LichThi lt in listlt) {
						this.lt = lt;
						SetCalenDarLT ();
					}
					isSaveId = false;
				});
		}



		public Task DeleteRemind(List<string> listEventId )
		{
			return Task.Run(()=>
				{
					
					
					for (int i=0;i<listEventId.Count;i++)
					{
						List<string> eventID = new List<string>();
						eventID.Add(listEventId[i]);
						int deleted =
							ctx.ContentResolver.
							Delete (
								CalendarContract.Events.ContentUri,
								CalendarContract.Events.InterfaceConsts.Id + " =? ",
								eventID.ToArray());
						BRemind.RemoveRemind(SQLite_Android.GetConnection (),listEventId[i]);
					}
				}
					);

		}


		public Task DeleteAlLRemind()
		{
			return Task.Run(()=>
				{

					List<LHRemindItem> listLH = BRemind.GetAllLHRemind(SQLite_Android.GetConnection ());
					List<LTRemindItem> listLT = BRemind.GetAllLTRemind(SQLite_Android.GetConnection ());

					for (int i=0;i<listLH.Count;i++)
					{
						
						List<string> eventID = new List<string>();
						eventID.Add(listLH[i].EventID);
						int deleted =
							ctx.ContentResolver.
							Delete (
								CalendarContract.Events.ContentUri,
								CalendarContract.Events.InterfaceConsts.Id + " =? ",
								eventID.ToArray());
					}
					for (int i=0;i<listLT.Count;i++){
						List<string> eventID = new List<string>();
						eventID.Add(listLH[i].EventID);
						int deleted =
							ctx.ContentResolver.
							Delete (
								CalendarContract.Events.ContentUri,
								CalendarContract.Events.InterfaceConsts.Id + " =? ",
								eventID.ToArray());
					}
						
					BRemind.RemoveAllRM(SQLite_Android.GetConnection ());
				});
		}

		//ContentUris.WithAppendedId(CalendarContract.Events.ContentUri,eventID)

		public void GetRemind(string eventId, out string minutes, out string mess){
			String[] selectionArgs = new String[] { eventId }; 
			// Submit the query and get a Cursor object back. 
			var cur = ctx.ContentResolver.Query (CalendarContract.Reminders.ContentUri,new String[] {
				CalendarContract.Reminders.InterfaceConsts.EventId,CalendarContract.Reminders.InterfaceConsts.Minutes}, CalendarContract.Reminders.InterfaceConsts.EventId + " =? ", selectionArgs, null);
			var cur2 = ctx.ContentResolver.Query (CalendarContract.Events.ContentUri,new String[] {
				CalendarContract.Events.InterfaceConsts.Id,CalendarContract.Events.InterfaceConsts.Description},CalendarContract.Events.InterfaceConsts.Id + " =? ", selectionArgs, null);
			
			if (cur2.MoveToNext ()) {
				minutes = cur2.GetInt(1).ToString();
				mess = cur2.GetString (1);

			} else {
				minutes = null;
				mess = null;
			}

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

