using System;
using UIKit;
using School.Core;
using System.Collections.Generic;
using Foundation;

namespace School.iOS
{
	public class DiemThiSource: UITableViewSource
	{
		NSString cellIdentifier = new NSString("TableCell");
		private const int HEADER_TYPE = 1;
		private const int BODY_TYPE = 2;
		private const int FOOTER_TYPE = 3;
		private List<int> listType = new List<int>();
		public List<DiemMon> tableItems;
		public DiemThiSource (List<DiemMon> list)
		{
			tableItems = list;
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
		public int GetItemViewType (int position)
		{	
			return listType [position];

		}
		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			// In here you could customize how you want to get the height for row. Then   
			// just return it. 
			int type = GetItemViewType (indexPath.Row);
			switch (type) {
			case HEADER_TYPE: 
				return 50;
			case  BODY_TYPE:
				return 50;
			case FOOTER_TYPE:
				return 200;
			}

			return 40;
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			int type = GetItemViewType (indexPath.Row);
			DiemThi dt;
			switch (type) {
			case HEADER_TYPE: 
				DTHeader hcell = tableView.DequeueReusableCell (cellIdentifier) as DTHeader;
				if (hcell == null) {
					hcell = new DTHeader (cellIdentifier);
				}
				dt = BDiemThi.GetDT (SQLite_iOS.GetConnection (), tableItems [indexPath.Row].Hocky, tableItems [indexPath.Row].NamHoc);
				hcell.UpdateCell (dt.Hocky, dt.NamHoc);
				return hcell;
			case BODY_TYPE:
				DiemThiCell cell = tableView.DequeueReusableCell (cellIdentifier) as DiemThiCell;
				if (cell == null) {
					cell = new DiemThiCell (cellIdentifier);
				}
				MonHoc mh = BMonHoc.GetMH (SQLite_iOS.GetConnection (), tableItems [indexPath.Row].MaMH);
				cell.UpdateCell (mh.TenMH, mh.TiLeThi.ToString (), tableItems [indexPath.Row].DiemKT,
					tableItems [indexPath.Row].DiemThi, tableItems [indexPath.Row].DiemTK10, tableItems [indexPath.Row].DiemChu);
				return cell;


			case FOOTER_TYPE:
				DiemThiFooter icell = tableView.DequeueReusableCell (cellIdentifier) as DiemThiFooter;
				if (icell == null) {
					icell = new DiemThiFooter (cellIdentifier);
				}

				 dt = BDiemThi.GetDT (SQLite_iOS.GetConnection (), tableItems [indexPath.Row].Hocky, tableItems [indexPath.Row].NamHoc);
				icell.UpdateCell (dt);
				return icell;

			}
			return null;
		}
	}
}

