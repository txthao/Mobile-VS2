using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using School.Core;
using Android.Graphics;
using Java.Util;
using Android.Graphics.Drawables;

namespace School.Droid
{
	public class DiemThiApdater: BaseAdapter<DiemMon>
	{

		private const int HEADER_TYPE = 1;
		private const int BODY_TYPE = 2;
		private const int FOOTER_TYPE = 3;

	//	private TreeSet mSeparatorsSet = new TreeSet();
		private List<int> listType = new List<int>();
		public List<DiemMon> list;
		private Context context;


		public DiemThiApdater (Context context, List<DiemMon> list)
		{
			this.list = list;
			this.context = context;
			this.listType = setTypeForItem (list);
		}

		public List<int> setTypeForItem(List<DiemMon> list){
			List<int> listType = new List<int>();
			foreach (var item in list) {
				if (item.MaMH == "Header") {
					listType.Add (HEADER_TYPE);
				} else if (item.MaMH == "Footer") {
					listType.Add (FOOTER_TYPE);
				} else {
					listType.Add (BODY_TYPE);
				}
			}

			return listType;
		}

		public override int Count {
			get { return list.Count ; }
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override int GetItemViewType (int position)
		{	
			return listType [position];

		}

		public override int ViewTypeCount {
			get {
				return 4;
			}
		}

		public override DiemMon this [int position] {
			get {
				return list [position];
			}
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			ViewHolderItem viewholder;
			View row = convertView;
			int type = GetItemViewType (position);
			if (row == null) {
				viewholder = new ViewHolderItem ();
				switch (type) {
				case HEADER_TYPE:
					row = LayoutInflater.From (context).Inflate (Resource.Layout.DTHeader, null);
					viewholder.txtHeaderDT = row.FindViewById<TextView> (Resource.Id.txtHeaderDT);
					break;
				case BODY_TYPE:
					row = LayoutInflater.From (context).Inflate (Resource.Layout.DTRow, null);
					viewholder.txtMonHocDT = row.FindViewById<TextView> (Resource.Id.txtMonHocDT);
					viewholder.txtTiLe = row.FindViewById<TextView> (Resource.Id.txtTiLe);
					viewholder.txtDT = row.FindViewById<TextView> (Resource.Id.txtDT);
					viewholder.txtDTK = row.FindViewById<TextView> (Resource.Id.txtDTK);
					viewholder.txtDKT = row.FindViewById<TextView> (Resource.Id.txtDKT);
					viewholder.linear = row.FindViewById<LinearLayout> (Resource.Id.linear45);
					break;
				case FOOTER_TYPE:
					row = LayoutInflater.From (context).Inflate (Resource.Layout.DTFooter, null);
					viewholder.txtTB10 = row.FindViewById<TextView> (Resource.Id.txtTB10);
					viewholder.txtTB4 = row.FindViewById<TextView> (Resource.Id.txtTB4);
					viewholder.txtTBTL10 = row.FindViewById<TextView> (Resource.Id.txtTBTL10);
					viewholder.txtTBTL4 = row.FindViewById<TextView> (Resource.Id.txtTBTL4);
					viewholder.txtDRL = row.FindViewById<TextView> (Resource.Id.txtDRL);
					viewholder.txtSoTC= row.FindViewById<TextView> (Resource.Id.txtSoTC);
					viewholder.txtTCTL=row.FindViewById<TextView> (Resource.Id.txtTCTL);
					break;
				}
				row.Tag = viewholder;
			}
			else {
				viewholder = (ViewHolderItem)row.Tag;
			}
	//		System."ostics.Debug.WriteLine ("pos: " + position + " type: " + type +" view " + row);
			switch (type) {
			case HEADER_TYPE:
				//TextView title = row.FindViewById<TextView> (Resource.Id.txtHeaderDT);
				viewholder.txtHeaderDT.Text = " HỌC KỲ " + list [position].Hocky + " NĂM HỌC " + list [position].NamHoc;
				break;
			case BODY_TYPE:
				MonHoc mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), list [position].MaMH);
				viewholder.txtMonHocDT.Text = mh.TenMH;
				viewholder.txtTiLe.Text = Common.calPercent (mh.TiLeThi);
				viewholder.txtDKT.Text = Common.checkSpaceValue (list [position].DiemKT);
				viewholder.txtDT.Text = Common.checkSpaceValue (list [position].DiemThi);
				viewholder.txtDTK.Text = Common.setDiemTK (list [position].DiemTK10, list [position].DiemChu);
				break;
			case FOOTER_TYPE:				
				DiemThi dt = BDiemThi.GetDT (SQLite_Android.GetConnection (), list [position].Hocky, list [position].NamHoc);
				viewholder.txtTB10.Text = "ĐTB Học kỳ hệ 10: " + dt.DiemTB10;
				viewholder.txtTB4.Text = "ĐTB Học kỳ hệ 4: " + dt.DiemTB4;
				viewholder.txtTBTL10.Text = "ĐTB Tích lũy hệ 10: " + dt.DiemTBTL10;
				viewholder.txtTBTL4.Text = "ĐTB Tích lũy hệ 4: " + dt.DiemTBTL4;
				viewholder.txtDRL.Text = "Điểm rèn luyện: " + dt.DiemRL;
				viewholder.txtSoTC.Text = "Số TC Đạt: " + dt.SoTCDat;
				viewholder.txtTCTL.Text = "Số TCTL: " + dt.SoTCTL;
				break;
			}
			return row;
		}

		class ViewHolderItem : Java.Lang.Object
		{
			internal TextView txtTB10;
			internal TextView txtTB4;
			internal TextView txtTBTL10;
			internal TextView txtTBTL4;
			internal TextView txtDRL;

			internal TextView txtMonHocDT;
			internal TextView txtTiLe;
			internal TextView txtDKT;
			internal TextView txtDT;
			internal TextView txtDTK;

			internal TextView txtHeaderDT;
			internal TextView txtSoTC;
			internal TextView txtTCTL;
			internal LinearLayout linear;
		}

	}
}