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
using Android.Graphics;

namespace School.Droid
{
    public class LichThiAdapter : BaseAdapter<LichThi>
    {

        public List<LichThi> list;
        private Context context;

        public LichThiAdapter(Context context, List<LichThi> list)
        {
            this.list = list;
            this.context = context;
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override LichThi this[int position]
        {
            get
            {
                return list[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
          
            View view = convertView;
            if (view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.LTRow, null, false);

            }

            TextView txtMon = view.FindViewById<TextView>(Resource.Id.txtMonHoc);
            txtMon.Text = BMonHoc.GetMH(SQLite_Android.GetConnection(), list[position].MaMH).TenMH;
            TextView txtThoiGian = view.FindViewById<TextView>(Resource.Id.txtThoiGian);
            txtThoiGian.Text = list[position].NgayThi;
            TextView txtGioBD = view.FindViewById<TextView>(Resource.Id.txtGioBD);
            txtGioBD.Text = list[position].GioBD;
            TextView txtPhong = view.FindViewById<TextView>(Resource.Id.txtPhongThi);
            txtPhong.Text = list[position].PhongThi;
			if (position % 2 == 0) {
				view.FindViewById<LinearLayout> (Resource.Id.linear2).SetBackgroundColor(Color.ParseColor("#FFFFFF"));
			} else {
				view.FindViewById<LinearLayout> (Resource.Id.linear2).SetBackgroundColor(Color.ParseColor("#C9C9C9"));
			}
            return view;
        }
     
    }
}