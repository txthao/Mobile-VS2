using System;
using SQLite;

namespace School.Core
{
	public class LTRemindItem
	{
		public LTRemindItem ()
		{
		}
		string eventID,mamh,namhoc,hocky,mess;
		int minute ;
		[PrimaryKey, Column("EventID")]
		public string EventID
		{
			get{return eventID;}
			set{ eventID = value; }
		}
		public string MaMH
		{
			get{return mamh;}
			set{ mamh = value; }
		}
		public string NamHoc
		{
			get{return namhoc;}
			set{ namhoc = value; }
		}
		public string HocKy
		{
			get{return hocky;}
			set{ hocky = value; }
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

