using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.Globalization;


namespace School.Core
{
	public class DataProvider
	{
		SQLiteConnection _connection;

		public DataProvider (SQLiteConnection connection)
		{
			//_connection = DependencyService.Get<ISQLite> ().GetConnection ();
			_connection = connection;
		
			_connection.CreateTable<MonHoc> ();
			_connection.CreateTable<LichThi> ();
			_connection.CreateTable<User> ();
			_connection.CreateTable<LichHoc> ();
			_connection.CreateTable<HocPhi> ();
			_connection.CreateTable<DiemThi> ();
			_connection.CreateTable<DiemMon> ();
			_connection.CreateTable<CTHocPhi> ();
			_connection.CreateTable<chiTietLH> ();
		}

		public void AddLT (LichThi T)
		{
			_connection.Insert (T);
			_connection.Commit ();

		}

		public List<LichThi> GetAllLT ()
		{
			var query = from c in _connection.Table<LichThi> ()
			            select c;
			return query.ToList ();
		}

		public LichThi GetLT (string mamh)
		{
			var query = from c in _connection.Table<LichThi> ()
			            where c.MaMH.Equals (mamh)
			            select c;

			return query.FirstOrDefault ();
		}

//		public LichThi GetNewestLT (string mamh)
//		{
//			var query = from c in _connection.Table<LichThi> ()
//				orderby c.NamHoc descending, c.HocKy descending
//			select c;
//			return query.FirstOrDefault ();
//		}

		public void AddLH (LichHoc lh)
		{
			_connection.Insert (lh);
			_connection.Commit ();
		}

		public List<LichHoc> GetAllLH ()
		{
			var query = from c in _connection.Table<LichHoc> ()
			            select c;
			return query.ToList ();
		}

		public List<LichHoc> GetLH_Time (string hocky, string namhoc)
		{

			var query = from c in _connection.Table<LichHoc> ()
			             where c.HocKy.Equals (hocky) && c.NamHoc.Equals (namhoc)
			             select c;
			return query.ToList ();

		}

		public LichHoc GetLastLH ()
		{
			var query = from c in _connection.Table<LichHoc> ()
					orderby c.Id descending
			            select c;

			return query.FirstOrDefault ();
		}

		public List<LichHoc>  GetNewestLH(){
			var query = from c in _connection.Table<LichHoc> ()
				orderby c.NamHoc descending, c.HocKy descending
				select c;
			LichHoc lh = query.FirstOrDefault ();
			var result = from a in _connection.Table<LichHoc> ()
					where a.NamHoc.Equals(lh.NamHoc) && a.HocKy.Equals(lh.HocKy)
			             select a;
			return result.ToList();
		}

		public LichHoc GetLH_Id (string id)
		{
			var query = from c in _connection.Table<LichHoc> ()
			            where c.Id.Equals (id)
			            select c;

			return query.FirstOrDefault ();
		}

		public LichHoc GetLH_Ma (string mamh)
		{
			var query = from c in _connection.Table<LichHoc> ()
			            where c.MaMH.Equals (mamh)
			            select c;

			return query.FirstOrDefault ();
		}

		public HocPhi GetHP (int namhoc, int  hocky)
		{
			var query = from c in _connection.Table<HocPhi> ()
			            where (c.HocKy == hocky) && (c.NamHoc == namhoc)
			            select c;
			return query.FirstOrDefault ();
		}

		public CTHocPhi GetCTHP (CTHocPhi hp)
		{
			var query = from c in _connection.Table<CTHocPhi> ()
			            where (c.HocKy == hp.HocKy) && (c.NamHoc == hp.NamHoc) && (c.MaMH.Equals (hp.MaMH))
			            select c;
			return query.FirstOrDefault ();
		}

		public List<DiemThi> GetAllDT ()
		{
			var query = from c in _connection.Table<DiemThi> ()
			            select c;
			return query.ToList ();
		}

		public List<HocPhi> GetAllHP ()
		{
			var query = from c in _connection.Table<HocPhi> ()
			            select c;
			return query.ToList ();
		}

		public HocPhi GetHP ()
		{
			var query = from c in _connection.Table<HocPhi> ()
				orderby c.NamHoc descending, c.HocKy descending
			select c;
			return query.FirstOrDefault();
		}

		public List<chiTietLH> GetCTLH (string id)
		{
			var query = from c in _connection.Table<chiTietLH> ()
			            where c.Id.Equals (id)
			            select c;
			return query.ToList ();
		}

		public chiTietLH checkCTLH (chiTietLH ct)
		{
			var query = from c in _connection.Table<chiTietLH> ()
			            where c.Id.Equals (ct.Id) && c.Thu.Equals (ct.Thu) && c.Phong.Equals (ct.Phong)
			            select c;
			return query.FirstOrDefault ();
		}

		public List<CTHocPhi> GetCTHPs (int namhoc, int hocky)
		{
			var query = from c in _connection.Table<CTHocPhi> ()
			            where (c.HocKy == hocky) && (c.NamHoc == namhoc)
			            select c;
			return query.ToList ();
		}

		public DiemThi GetNewestDT(){
			var query = from c in _connection.Table<DiemThi> ()
				orderby c.NamHoc descending, c.Hocky descending
			select c;
			return query.FirstOrDefault();
		}

		public DiemThi GetDT(string hk, string nh){
			var query = from c in _connection.Table<DiemThi> ()
					where c.NamHoc.Equals(nh) && c.Hocky.Equals(hk)
			select c;
			return query.FirstOrDefault();
		}

		public List<DiemMon> GetDMs (string hocky, string namhoc)
		{
			var query = from c in _connection.Table<DiemMon> ()
			            where (c.Hocky == hocky) && (c.NamHoc == namhoc)
			            select c;
			return query.ToList ();
		}

		public DiemMon GetDM (DiemMon dm)
		{
			var query = from c in _connection.Table<DiemMon> ()
			            where (c.Hocky == dm.Hocky) && (c.NamHoc == dm.NamHoc) && (c.MaMH.Equals (dm.MaMH))
			            select c;
			return query.FirstOrDefault ();
		}

		public MonHoc GetMonHoc (string mamh)
		{
			var query = from c in _connection.Table<MonHoc> ()
			            where c.MaMH.Equals (mamh)
			            select c;

			return query.FirstOrDefault ();
		}

		public User GetUser (string id)
		{
			var query = from c in _connection.Table<User> ()
			            where c.Id.Equals (id)
			            select c;

			return query.FirstOrDefault ();
		}

		public User GetMainUser ()
		{
			var query = from c in _connection.Table<User> ()
			            where c.Password != null
			            select c;

			return query.FirstOrDefault ();
		}



		public CTHocPhi GetCTHP (int namhoc, int hocky)
		{
			var query = from c in _connection.Table<CTHocPhi> ()
			            where (c.HocKy == hocky) && (c.NamHoc == namhoc)
			            select c;
			return query.FirstOrDefault ();
		}

		public int AddDM (DiemMon T)
		{
			int i = _connection.Insert (T);
			_connection.Commit ();
			return i;
		}

		public int AddDT (DiemThi T)
		{
			int i = _connection.Insert (T);
			_connection.Commit ();
			return i;
		}

		public int AddHP (HocPhi T)
		{
			int i = _connection.Insert (T);
			_connection.Commit ();
			return i;
		}

		public int AddUser (User T)
		{
			int i = _connection.Insert (T);
			_connection.Commit ();
			return i;
		}

		public int AddMH (MonHoc T)
		{
			int i = _connection.Insert (T);
			_connection.Commit ();
			return i;
		}

		public int AddCTLH (chiTietLH T)
		{
			int i = _connection.Insert (T);
			_connection.Commit ();
			return i;
		}

		public int AddCTHP (CTHocPhi T)
		{
			int i = _connection.Insert (T);
			_connection.Commit ();
			return i;
		}

		public int UpdateMH (MonHoc mh)
		{
			int i = _connection.Update (mh);
			_connection.Commit ();
			return i;
		}

		//check if hay any schedule 
		public List<LichThi> GetLichThiByTime(string time)
		{
			var query = from c in _connection.Table<LichThi> ()
					where c.NgayThi.Equals(time)
				select c;
			return query.ToList ();
		}
		public List<chiTietLH> GetCTLichHocByTime()
		{
			var query = from c in _connection.Table<chiTietLH> ()
					where (checkTime(c.ThoigianBD))&&(checkTime(c.ThoigianKT))
			select c;
			return query.ToList ();
		}
		private bool checkTime(string time)
		{
			
			DateTime now = DateTime.Now;
			if (now.CompareTo(convertFromStringToDate(time))<=0)
			{
				return false;

			}
			return true;
		}
		public  DateTime convertFromStringToDate (string date)
		{
			return DateTime.ParseExact (date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

		}
		public int DeleteAll ()
		{
			
			int i = _connection.DeleteAll<LichHoc> ();
			_connection.Commit ();
			i += _connection.DeleteAll<User> ();
			_connection.Commit ();
			i += _connection.DeleteAll<LichThi> ();
			_connection.Commit ();
			i += _connection.DeleteAll<DiemThi> ();
			_connection.Commit ();
			i += _connection.DeleteAll<HocPhi> ();
			_connection.Commit ();
			i += _connection.DeleteAll<CTHocPhi> ();
			_connection.Commit ();
			i += _connection.DeleteAll<chiTietLH> ();
			_connection.Commit ();
			i += _connection.DeleteAll<DiemMon> ();
			_connection.Commit ();
			i += _connection.DeleteAll<MonHoc> ();
			_connection.Commit ();
			return i;
		}


	}
}

