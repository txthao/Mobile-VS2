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
				
					break;
				}
				row.Tag = viewholder;
			}
			else {
				viewholder = (ViewHolderItem)row.Tag;
			}
	//		System.Diagnostics.Debug.WriteLine ("pos: " + position + " type: " + type +" view " + row);
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

			internal LinearLayout linear;
		}

//

//		public List<DiemThi> listDT;
//		private Context context;
//
//		public DiemThiApdater (Context context, List<DiemThi> listDT)
//		{
//			this.context = context;
//			this.listDT = listDT;
//		}
//
//
//
//		public override View GetGroupView (int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
//		{
//			
//			View header = convertView;
//			if (header == null) {
//				header = LayoutInflater.From (context).Inflate (Resource.Layout.DTHeader, null);
//			}
//			TextView title = header.FindViewById<TextView> (Resource.Id.txtHeaderDT);
//			title.Text = " HỌC KỲ " + listDT [groupPosition].Hocky + " NĂM HỌC " + listDT [groupPosition].NamHoc;
//			ExpandableListView mExpandableListView = (ExpandableListView)parent;
//			mExpandableListView.ExpandGroup (groupPosition);
//
//			return header;
//		}
//
//		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
//		{
//
//			View row = convertView;
//
//			if (childPosition < GetChildrenCount (groupPosition) - 1) {	
//				row = LayoutInflater.From (context).Inflate (Resource.Layout.DTRow, null);
//				DiemMon diemMon = GetChildViewHelper (groupPosition, childPosition);
//				MonHoc mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), diemMon.MaMH);
//				row.FindViewById<TextView> (Resource.Id.txtMonHocDT).Text = mh.TenMH;
//				row.FindViewById<TextView> (Resource.Id.txtTiLe).Text = Common.calPercent (mh.TiLeThi);
//				row.FindViewById<TextView> (Resource.Id.txtDKT).Text = Common.checkSpaceValue (diemMon.DiemKT);
//				row.FindViewById<TextView> (Resource.Id.txtDT).Text = Common.checkSpaceValue (diemMon.DiemThi);
//				row.FindViewById<TextView> (Resource.Id.txtDTK).Text = Common.setDiemTK (diemMon.DiemTK10, diemMon.DiemChu);
//				if (childPosition % 2 == 0) {
//					row.FindViewById<LinearLayout> (Resource.Id.linear45).SetBackgroundColor(Color.ParseColor("#FFFFFF"));
//				} else {
//					row.FindViewById<LinearLayout> (Resource.Id.linear45).SetBackgroundColor(Color.ParseColor("#C9C9C9"));
//				}
//			}
//			else if(childPosition == GetChildrenCount(groupPosition)-1 && listDT [groupPosition].Hocky != "3")
//			{
//				row = LayoutInflater.From(context).Inflate(Resource.Layout.DTFooter, null);
//				row.FindViewById<TextView>(Resource.Id.txtTB10).Text = "ĐTB Học kỳ hệ 10: " + listDT [groupPosition].DiemTB10;
//				row.FindViewById<TextView>(Resource.Id.txtTB4).Text = "ĐTB Học kỳ hệ 4: " + listDT [groupPosition].DiemTB4;
//				row.FindViewById<TextView>(Resource.Id.txtTBTL10).Text = "ĐTB Tích lũy hệ 10: " + listDT [groupPosition].DiemTBTL10;
//				row.FindViewById<TextView>(Resource.Id.txtTBTL4).Text = "ĐTB Tích lũy hệ 4: " + listDT [groupPosition].DiemTBTL4;
//				row.FindViewById<TextView>(Resource.Id.txtDRL).Text =  "Điểm rèn luyện: " + listDT [groupPosition].DiemRL;
//			}
//
//
//
//			return row;
//		}
//			
//		public override int GetChildrenCount (int groupPosition)
//		{
//			
//			List<DiemMon> listDM = BDiemThi.GetDiemMons (SQLite_Android.GetConnection (), listDT [groupPosition].Hocky, listDT [groupPosition].NamHoc);
//			return listDM.Count + 1;
//		}
//
//		public override int GroupCount {
//			get {
//				return listDT.Count;
//			}
//		}
//
//		private DiemMon GetChildViewHelper (int groupPosition, int childPosition)
//		{
//			
//			List<DiemMon> listDM = BDiemThi.GetDiemMons (SQLite_Android.GetConnection (), listDT [groupPosition].Hocky, listDT [groupPosition].NamHoc);
//			return listDM [childPosition];
//
//		}
//
//		#region implemented abstract members of BaseExpandableListAdapter
//
//		public override Java.Lang.Object GetChild (int groupPosition, int childPosition)
//		{
//			throw new NotImplementedException ();
//		}
//
//		public override long GetChildId (int groupPosition, int childPosition)
//		{
//			return childPosition;
//		}
//
//		public override Java.Lang.Object GetGroup (int groupPosition)
//		{
//			throw new NotImplementedException ();
//		}
//
//		public override long GetGroupId (int groupPosition)
//		{
//			return groupPosition;
//		}
//
//		public override bool IsChildSelectable (int groupPosition, int childPosition)
//		{
//			return false;
//		}
//
//		public override bool HasStableIds {
//			get {
//				return true;
//			}
//		}
//
//		#endregion
	}
}