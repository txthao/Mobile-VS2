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
	public class HocPhiAdapter: BaseAdapter<CTHocPhi>
	{

		public List<CTHocPhi> list;
		private Context context;

		public HocPhiAdapter (Context context, List<CTHocPhi> list)
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

		public override CTHocPhi this [int position] {
			get {
				return list [position];
			}
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{

			View view = convertView;
			if (view == null) {
				view = LayoutInflater.From (context).Inflate (Resource.Layout.HocPhiRow, null, false);

			}
			object t =BMonHoc.GetMH (SQLite_Android.GetConnection (), list [position].MaMH).TenMH;
			view.FindViewById<TextView> (Resource.Id.txtMonHocHP).Text = BMonHoc.GetMH (SQLite_Android.GetConnection (), list [position].MaMH).TenMH;
			view.FindViewById<TextView> (Resource.Id.txtHocPhi).Text = list [position].HocPhi;
			view.FindViewById<TextView> (Resource.Id.txtMienGiam).Text = list [position].MienGiam;
			view.FindViewById<TextView> (Resource.Id.txtPhaiDong).Text = list [position].PhaiDong;

			return view;
		}


	}
}

