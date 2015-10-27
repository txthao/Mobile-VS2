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
		public HocPhiSource (List<CTHocPhi> list)
		{
			tableItems = list;

		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell, 
			HocPhiCell cell = tableView.DequeueReusableCell (cellIdentifier) as HocPhiCell;

			if (cell == null) {
				cell = new HocPhiCell ( cellIdentifier);
			}
			cell.UpdateCell (tableItems [indexPath.Row].MaMH, tableItems [indexPath.Row].HocPhi, tableItems [indexPath.Row].MienGiam,
				tableItems [indexPath.Row].PhaiDong);
			// now set the properties as normal
			
			
			return cell;
		}

	}
}

