using System;
using UIKit;
using School.Core;
using System.Collections.Generic;
using Foundation;

namespace School.iOS
{
	public class HocPhiSource: UITableViewSource 
	{
		List<CTHocPhi> tableItems;
		NSString cellIdentifier = new NSString("TableCell");
		bool isHeader=false;
		public HocPhiSource (List<CTHocPhi> list)
		{
			tableItems = list;
			isHeader = false;
		}
		public HocPhiSource()
		{
			tableItems = new List<CTHocPhi> ();
			isHeader = true;
			CTHocPhi ct = new CTHocPhi ();
			ct.MaMH="Môn Học";
			tableItems.Add (ct);
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell, 
			if (!isHeader) {
				HocPhiCell cell = tableView.DequeueReusableCell (cellIdentifier) as HocPhiCell;

				if (cell == null) {
					cell = new HocPhiCell (cellIdentifier);
				}
				cell.UpdateCell (tableItems [indexPath.Row].MaMH, tableItems [indexPath.Row].HocPhi, tableItems [indexPath.Row].MienGiam,
					tableItems [indexPath.Row].PhaiDong);
				return cell;
				// now set the properties as normal
			} else {
				HPHeaderCell cell = tableView.DequeueReusableCell (cellIdentifier) as HPHeaderCell;
				if (cell == null) {
					cell = new  HPHeaderCell (cellIdentifier);
				}
				return cell;
			}
			

		}

	}
}

