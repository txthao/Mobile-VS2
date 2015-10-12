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

namespace School.Droid
{
	public class DiemThiApdater: BaseExpandableListAdapter
	{


		public List<DiemThi> listDT;
		private Context context;

		public DiemThiApdater (Context context, List<DiemThi> listDT)
		{
			this.context = context;
			this.listDT = listDT;
		}



		public override View GetGroupView (int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			
			View header = convertView;
			if (header == null) {
				header = LayoutInflater.From (context).Inflate (Resource.Layout.DTHeader, null);
			}
			TextView title = header.FindViewById<TextView> (Resource.Id.txtHeaderDT);
			title.Text = " HỌC KỲ " + listDT [groupPosition].Hocky + " NĂM HỌC " + listDT [groupPosition].NamHoc;
			ExpandableListView mExpandableListView = (ExpandableListView)parent;
			mExpandableListView.ExpandGroup (groupPosition);

			return header;
		}

		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{

			View row = convertView;

			if (childPosition < GetChildrenCount (groupPosition) - 1) {	
				row = LayoutInflater.From (context).Inflate (Resource.Layout.DTRow, null);
				DiemMon diemMon = GetChildViewHelper (groupPosition, childPosition);
				MonHoc mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), diemMon.MaMH);
				row.FindViewById<TextView> (Resource.Id.txtMonHocDT).Text = mh.TenMH;
				row.FindViewById<TextView> (Resource.Id.txtTiLe).Text = Common.calPercent (mh.TiLeThi);
				row.FindViewById<TextView> (Resource.Id.txtDKT).Text = Common.checkSpaceValue (diemMon.DiemKT);
				row.FindViewById<TextView> (Resource.Id.txtDT).Text = Common.checkSpaceValue (diemMon.DiemThi);
				row.FindViewById<TextView> (Resource.Id.txtDTK).Text = Common.setDiemTK (diemMon.DiemTK10, diemMon.DiemChu);
				if (childPosition % 2 == 0) {
					row.FindViewById<LinearLayout> (Resource.Id.linear45).SetBackgroundColor(Color.ParseColor("#FFFFFF"));
				} else {
					row.FindViewById<LinearLayout> (Resource.Id.linear45).SetBackgroundColor(Color.ParseColor("#C9C9C9"));
				}
			}
			else if(childPosition == GetChildrenCount(groupPosition)-1 && listDT [groupPosition].Hocky != "3")
			{
				row = LayoutInflater.From(context).Inflate(Resource.Layout.DTFooter, null);
				row.FindViewById<TextView>(Resource.Id.txtTB10).Text = "ĐTB Học kỳ hệ 10: " + listDT [groupPosition].DiemTB10;
				row.FindViewById<TextView>(Resource.Id.txtTB4).Text = "ĐTB Học kỳ hệ 4: " + listDT [groupPosition].DiemTB4;
				row.FindViewById<TextView>(Resource.Id.txtTBTL10).Text = "ĐTB Tích lũy hệ 10: " + listDT [groupPosition].DiemTBTL10;
				row.FindViewById<TextView>(Resource.Id.txtTBTL4).Text = "ĐTB Tích lũy hệ 4: " + listDT [groupPosition].DiemTBTL4;
				row.FindViewById<TextView>(Resource.Id.txtDRL).Text =  "Điểm rèn luyện: " + listDT [groupPosition].DiemRL;
			}



			return row;
		}
			
		public override int GetChildrenCount (int groupPosition)
		{
			
			List<DiemMon> listDM = BDiemThi.GetDiemMons (SQLite_Android.GetConnection (), listDT [groupPosition].Hocky, listDT [groupPosition].NamHoc);
			return listDM.Count + 1;
		}

		public override int GroupCount {
			get {
				return listDT.Count;
			}
		}

		private DiemMon GetChildViewHelper (int groupPosition, int childPosition)
		{
			
			List<DiemMon> listDM = BDiemThi.GetDiemMons (SQLite_Android.GetConnection (), listDT [groupPosition].Hocky, listDT [groupPosition].NamHoc);
			return listDM [childPosition];

		}

		#region implemented abstract members of BaseExpandableListAdapter

		public override Java.Lang.Object GetChild (int groupPosition, int childPosition)
		{
			throw new NotImplementedException ();
		}

		public override long GetChildId (int groupPosition, int childPosition)
		{
			return childPosition;
		}

		public override Java.Lang.Object GetGroup (int groupPosition)
		{
			throw new NotImplementedException ();
		}

		public override long GetGroupId (int groupPosition)
		{
			return groupPosition;
		}

		public override bool IsChildSelectable (int groupPosition, int childPosition)
		{
			return false;
		}

		public override bool HasStableIds {
			get {
				return true;
			}
		}

		#endregion
	}
}