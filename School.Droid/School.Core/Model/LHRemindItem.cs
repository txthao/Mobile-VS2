using System;

namespace School.Core
{
	public class LHRemindItem
	{
		public LHRemindItem ()
		{
		}
		string eventID,idLH, date;
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

