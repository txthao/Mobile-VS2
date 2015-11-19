using System;
using SQLite;

namespace School.Core
{
	public class LHRemindItem
	{
		public LHRemindItem ()
		{
		}
		string eventID,idLH, date,mess;
		int minute ;
		[PrimaryKey, Column("EventID")]
		
		public string EventID
		{
			get{return eventID;}
			set{ eventID = value; }
		}
		public string IDLH
		{
				get{return idLH;}
			set{ idLH = value; }
		}
		public string Date
		{
				get{return date;}
			set{ date = value; }
		}
		public string Mess
		{
			get{return mess;}
			set{ mess = value; }
		}
		public int Minute
		{
			get{return minute;}
			set{ minute = value; }
		}
	}
}

