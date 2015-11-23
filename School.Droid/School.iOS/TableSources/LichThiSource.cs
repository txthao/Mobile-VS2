using System;
using System.Collections.Generic;
using School.Core;
using Foundation;
using UIKit;

namespace School.iOS
{
	public class LichThiSource: UITableViewSource 
	{
		UIViewController controller;
		List<LichThi> tableItems;
		NSString cellIdentifier = new NSString("TableCell");
		bool isHeader=false;
		public LichThiSource (List<LichThi> list,UIViewController controller )
		{
			tableItems = list;
			isHeader = false;
			this.controller = controller;
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
				MonHoc mh = BMonHoc.GetMH (SQLite_iOS.GetConnection (), tableItems [indexPath.Row].MaMH);
				LTRemindItem rmItem = BRemind.GetLTRemind (SQLite_iOS.GetConnection (), tableItems [indexPath.Row].MaMH, 
					tableItems [indexPath.Row].NamHoc,
					tableItems [indexPath.Row].HocKy);
				bool hasRM = false;
				if (rmItem != null) {
					hasRM = true;
				}
				cell.UpdateCell (mh.TenMH, tableItems [indexPath.Row].NgayThi, tableItems [indexPath.Row].PhongThi
					,tableItems [indexPath.Row].GioBD,indexPath.Row,hasRM);
				UILongPressGestureRecognizer longPress = new UILongPressGestureRecognizer (LongPress);
				cell.AddGestureRecognizer (longPress);
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
		void LongPress(UILongPressGestureRecognizer gesture)
		{
			LichThiCell cell = (LichThiCell )gesture.View;

			VCHomeReminder remid = new VCHomeReminder (controller);
			remid.lt = tableItems [cell.num];
			LTRemindItem rmItem = BRemind.GetLTRemind (SQLite_iOS.GetConnection (), remid.lt.MaMH, 
				remid.lt.NamHoc,
				remid.lt.HocKy);

			if (rmItem != null) {
				remid.LoadEvent (rmItem.EventID,null);
			}
				else
				{
			remid.RemindLT ();
				}
		}
		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			// In here you could customize how you want to get the height for row. Then   
			// just return it. 

			return 60;
		}
	}
}

