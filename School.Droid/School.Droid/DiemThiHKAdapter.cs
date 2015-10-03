using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using School.Core;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace School.Droid
{
		
	public class DiemThiHKAdapter : BaseAdapter<DiemMon>
	{

		public List<DiemMon> list;
		private Context context;

		public DiemThiHKAdapter (Context context, List<DiemMon> list)
		{
			this.list = list;
			this.context = context;
		}

		public override int Count {
			get { return list.Count + 1; }
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override DiemMon this [int position] {
			get {
				return list [position];
			}
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{

			View row = convertView;
			if (position < Count - 1) {	
				row = LayoutInflater.From (context).Inflate (Resource.Layout.DTRow, null);
				MonHoc mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), list [position].MaMH);
				row.FindViewById<TextView> (Resource.Id.txtMonHocDT).Text = mh.TenMH;
				row.FindViewById<TextView> (Resource.Id.txtTiLe).Text = Common.calPercent (mh.TiLeThi);
				row.FindViewById<TextView> (Resource.Id.txtDKT).Text = Common.checkSpaceValue (list [position].DiemKT);
				row.FindViewById<TextView> (Resource.Id.txtDT).Text = Common.checkSpaceValue (list [position].DiemThi);
				row.FindViewById<TextView> (Resource.Id.txtDTK).Text = Common.setDiemTK (list [position].DiemTK10, list [position].DiemChu);
			} else if(position == Count - 1) {
				row = LayoutInflater.From(context).Inflate(Resource.Layout.DTFooter, null);
				DiemThi dt = BDiemThi.GetDT (SQLite_Android.GetConnection (), list [0].Hocky, list [0].NamHoc);
				row.FindViewById<TextView>(Resource.Id.txtTB10).Text = "ĐTB Học kỳ hệ 10: " + dt.DiemTB10;
				row.FindViewById<TextView>(Resource.Id.txtTB4).Text = "ĐTB Học kỳ hệ 4: " + dt.DiemTB4;
				row.FindViewById<TextView>(Resource.Id.txtTBTL10).Text = "ĐTB Tích lũy hệ 10: " + dt.DiemTBTL10;
				row.FindViewById<TextView>(Resource.Id.txtTBTL4).Text = "ĐTB Tích lũy hệ 4: " + dt.DiemTBTL4;
				row.FindViewById<TextView>(Resource.Id.txtDRL).Text =  "Điểm rèn luyện: " + dt.DiemRL;
			}

			


			return row;
		}


	}
}

