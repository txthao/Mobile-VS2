using System;
using System.Collections.Generic;
using School.Core;
using Foundation;
using UIKit;

namespace School.iOS
{
	public class DiemThiHKSource: UITableViewSource
	{
		List<DiemMon> tableItems;
		NSString cellIdentifier = new NSString("TableCell");
		bool isHeader=false;
		public DiemThiHKSource ()
		{
			tableItems = new List<DiemMon> ();
			isHeader = true;
			DiemMon dm = new DiemMon ();
			dm.MaMH = "000";
			tableItems.Add (dm);
		}
		public DiemThiHKSource(List<DiemMon> list)
		{
			tableItems = list;
			isHeader = false;
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell, 
			if (!isHeader) {
				DiemThiCell cell = tableView.DequeueReusableCell (cellIdentifier) as DiemThiCell;

				if (cell == null) {
					cell = new DiemThiCell (cellIdentifier);
				}
				var monhoc = BMonHoc.GetMH (SQLite_iOS.GetConnection (), tableItems [indexPath.Row].MaMH);
				cell.UpdateCell (monhoc.TenMH, monhoc.TiLeThi.ToString(), tableItems [indexPath.Row].DiemKT,
					tableItems [indexPath.Row].DiemThi,tableItems [indexPath.Row].DiemTK10,tableItems [indexPath.Row].DiemChu);
				return cell;
				// now set the properties as normal
			} else {
				DiemThiCell cell = tableView.DequeueReusableCell (cellIdentifier) as DiemThiCell;
				if (cell == null) {
					cell = new  DiemThiCell (cellIdentifier,true);
				}
				return cell;
			}


		}
	}
}

