using System;
using UIKit;
using System.Collections.Generic;
using School.Core;
using Foundation;

namespace School.iOS
{
	public class LichHocHKSource: UITableViewSource 
	{
		List<chiTietLH> tableItems;
		NSString cellIdentifier = new NSString("TableCell");
		bool isHeader=false;
		public LichHocHKSource (List<chiTietLH> list)
		{
			tableItems = list;
			isHeader = false;
		}
		public LichHocHKSource()
		{
			isHeader = true;
			tableItems = new List<chiTietLH> ();
			chiTietLH lh = new chiTietLH ();
			lh.Id = "1";
			tableItems.Add (lh);
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (!isHeader) {
				LichHocHKCell cell = tableView.DequeueReusableCell (cellIdentifier) as LichHocHKCell;

				if (cell == null) {
					cell = new LichHocHKCell (cellIdentifier);
				}
				string monhoc = BMonHoc.GetMH (SQLite_iOS.GetConnection (), BLichHoc.GetLH (SQLite_iOS.GetConnection (), tableItems [indexPath.Row].Id).MaMH).TenMH;
				cell.UpdateCell (monhoc, tableItems [indexPath.Row].Thu, tableItems [indexPath.Row].TietBatDau
					, tableItems [indexPath.Row].SoTiet,tableItems [indexPath.Row].Phong);
				return cell;
				// now set the properties as normal
			} else {
				LichHocHKCell cell = tableView.DequeueReusableCell (cellIdentifier) as LichHocHKCell;
				if (cell == null) {
					cell = new LichHocHKCell (cellIdentifier, true);
				}
				return cell;
			}
		}
	}
}

