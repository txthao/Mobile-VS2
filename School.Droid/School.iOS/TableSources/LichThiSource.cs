using System;
using System.Collections.Generic;
using School.Core;
using Foundation;
using UIKit;

namespace School.iOS
{
	public class LichThiSource: UITableViewSource 
	{
		List<LichThi> tableItems;
		NSString cellIdentifier = new NSString("TableCell");
		bool isHeader=false;
		public LichThiSource (List<LichThi> list)
		{
			tableItems = list;
			isHeader = false;
		}
		public LichThiSource()
		{
			tableItems = new List<LichThi> ();
			isHeader = true;
			LichThi lt = new LichThi ();
			lt.MaMH = "000";
			tableItems.Add (lt);
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell, 
			if (!isHeader) {
				LichThiCell cell = tableView.DequeueReusableCell (cellIdentifier) as LichThiCell;

				if (cell == null) {
					cell = new LichThiCell (cellIdentifier);
				}
				cell.UpdateCell (tableItems [indexPath.Row].MaMH, tableItems [indexPath.Row].NgayThi, tableItems [indexPath.Row].PhongThi
					,tableItems [indexPath.Row].GioBD);
				return cell;
				// now set the properties as normal
			} else {
				LichThiCell cell = tableView.DequeueReusableCell (cellIdentifier) as LichThiCell;
				if (cell == null) {
					cell = new LichThiCell (cellIdentifier,true);
				}
				return cell;
			}


		}
	}
}

