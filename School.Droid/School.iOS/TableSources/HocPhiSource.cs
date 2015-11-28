using System;
using UIKit;
using School.Core;
using System.Collections.Generic;
using Foundation;

namespace School.iOS
{
	public class HocPhiSource: UITableViewSource 
	{
		public List<CTHocPhi> tableItems;
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
		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			// In here you could customize how you want to get the height for row. Then   
			// just return it. 

			return 60;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell, 
			if (!isHeader) {
				HocPhiCell cell = tableView.DequeueReusableCell (cellIdentifier) as HocPhiCell;

				if (cell == null) {
					cell = new HocPhiCell (cellIdentifier);
				}
				MonHoc mh = BMonHoc.GetMH (SQLite_iOS.GetConnection (), tableItems [indexPath.Row].MaMH);
				cell.UpdateCell (mh.TenMH, tableItems [indexPath.Row].HocPhi, tableItems [indexPath.Row].MienGiam,
					tableItems [indexPath.Row].PhaiDong);

				if (indexPath.Row % 2 != 0) {
					cell.BackgroundColor = UIColor.FromRGBA((float)0.8, (float)0.8, (float)0.8, (float)1);
				}
				else {
					cell.BackgroundColor = UIColor.White;
				}
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

