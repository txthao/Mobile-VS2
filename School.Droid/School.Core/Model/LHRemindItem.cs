using System;
using SQLite;

namespace School.Core
{
	public class LHRemindItem
	{
		public LHRemindItem ()
		{
		}
		string eventID,idLH, date;
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
	}
}

