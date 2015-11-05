using System;
using SQLite;
using System.Collections.Generic;

namespace School.Core
{
	public class BRemind
	{

		public static void RemoveAllRM(SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			dtb.RemoveALlLHRemind ();
			dtb.RemoveALlLTRemind ();
		}
		public static void SaveLTRemind(SQLiteConnection connection,LTRemindItem item)
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.GetLTRemind (item.MaMH, item.NamHoc, item.HocKy) == null) {
				dtb.AddRemindLT (item);
			}
		}
		public static LHRemindItem GetLHRemind(SQLiteConnection connection,string id,string date)
		{
			DataProvider dtb = new DataProvider (connection);

			return dtb.GetLHRemind (id,date);
		}
		public static List<LHRemindItem> GetLHRemind(SQLiteConnection connection,string id)
		{
			DataProvider dtb = new DataProvider (connection);

			return dtb.GetLHRemind (id);
		}
		public static LTRemindItem GetLTRemind(SQLiteConnection connection,string mamh,string namhoc,string hocky)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetLTRemind(mamh,namhoc,hocky);
		}

		public static void SaveLHRemind(SQLiteConnection connection,LHRemindItem item)
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.GetLHRemind (item.IDLH,item.Date) == null) {
				dtb.AddRemindLH (item);
			}
		}
		public static List<LHRemindItem> GetAllLHRemind(SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetAllLHRemind ();
		}
		public static List<LTRemindItem> GetAllLTRemind(SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetAllLTRemind ();
		}
	}
}

