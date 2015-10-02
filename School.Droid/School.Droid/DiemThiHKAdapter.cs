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
			get { return list.Count; }
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
			if (row == null) {
				row = LayoutInflater.From (context).Inflate (Resource.Layout.DTRow, null, false);

			}
			MonHoc mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), list[position].MaMH);
			row.FindViewById<TextView> (Resource.Id.txtMonHocDT).Text = mh.TenMH;
			row.FindViewById<TextView> (Resource.Id.txtTiLe).Text = Common.calPercent (mh.TiLeThi);
			row.FindViewById<TextView> (Resource.Id.txtDKT).Text = Common.checkSpaceValue (list[position].DiemKT);
			row.FindViewById<TextView> (Resource.Id.txtDT).Text = Common.checkSpaceValue (list[position].DiemThi);
			row.FindViewById<TextView> (Resource.Id.txtDTK).Text = Common.setDiemTK (list[position].DiemTK10, list[position].DiemChu);


			return row;
		}


	}
}

