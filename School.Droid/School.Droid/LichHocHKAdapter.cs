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
	public class LichHocHKAdapter: BaseAdapter<chiTietLH>
	{
		
		public List<chiTietLH> list;
		private Context context;

		public LichHocHKAdapter (Context context, List<chiTietLH> list)
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

		public override chiTietLH this [int position] {
			get {
				return list [position];
			}
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			ViewHolderItem viewholder;
			View view = convertView;
			if (view == null) {
				view = LayoutInflater.From (context).Inflate (Resource.Layout.LichHocRow, null, false);
				viewholder = new ViewHolderItem ();
				viewholder.txtMH = view.FindViewById<TextView> (Resource.Id.txtMonHoc_HK);
				viewholder.txtPhong = view.FindViewById<TextView> (Resource.Id.txtPhong_HK);
				viewholder.txtSoTiet = view.FindViewById<TextView> (Resource.Id.txtSoTiet_HK);
				viewholder.txtTBD = view.FindViewById<TextView> (Resource.Id.txtTietBD_HK);
				viewholder.txtThu = view.FindViewById<TextView> (Resource.Id.txtThu_HK);
				viewholder.imgBell = view.FindViewById<ImageView> (Resource.Id.imgBell_HK);
				view.Tag = viewholder;
			} else {
				viewholder = (ViewHolderItem)view.Tag;
			}
			LichHoc lh = BLichHoc.GetLH (SQLite_Android.GetConnection (), list [position].Id);
			viewholder.txtMH.Text = BMonHoc.GetMH (SQLite_Android.GetConnection (), lh.MaMH).TenMH;
			viewholder.txtPhong.Text = list [position].Phong;
			viewholder.txtSoTiet.Text = list [position].SoTiet;
			viewholder.txtTBD.Text = list [position].TietBatDau;
			viewholder.txtThu.Text = list [position].Thu;
			if (BRemind.GetLHRemind (SQLite_Android.GetConnection (), lh.Id).Count > 0) {
				List<LHRemindItem> rmList = BRemind.GetLHRemind (SQLite_Android.GetConnection (), lh.Id);
				foreach (var item in rmList) {
					if (Common.checkInWeek (list [position].Tuan, item.Date)) {
						viewholder.imgBell.Visibility = ViewStates.Visible;
					}
				}
			}
//			view.FindViewById<TextView> (Resource.Id.txtMonHoc_HK).TextView = BMonHoc.GetMH (SQLite_Android.GetConnection (), lh.MaMH).TenMH;
//			view.FindViewById<TextView> (Resource.Id.txtThu_HK).Text = list [position].Thu;
//			view.FindViewById<TextView> (Resource.Id.txtTietBD_HK).Text = list [position].TietBatDau;
//			view.FindViewById<TextView> (Resource.Id.txtSoTiet_HK).Text = list [position].SoTiet;
//			view.FindViewById<TextView> (Resource.Id.txtPhong_HK).Text = list [position].Phong;
			if (position % 2 == 0) {
				view.FindViewById<LinearLayout> (Resource.Id.linearRowLH).SetBackgroundColor(Color.ParseColor("#FFFFFF"));
			} else {
				view.FindViewById<LinearLayout> (Resource.Id.linearRowLH).SetBackgroundColor(Color.ParseColor("#C9C9C9"));
			}
			return view;
		}

		class ViewHolderItem : Java.Lang.Object
		{
			internal TextView txtMH;
			internal TextView txtThu;
			internal TextView txtTBD;
			internal TextView txtSoTiet;
			internal TextView txtPhong;
			internal ImageView imgBell;
		}

	}
}

