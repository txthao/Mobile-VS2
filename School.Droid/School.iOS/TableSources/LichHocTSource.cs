using System;
using UIKit;
using System.Collections.Generic;
using Foundation;
using School.Core;

namespace School.iOS
{
	public class LichHocTSource : UITableViewSource
	{
		VCLichHocTuan controller;
	
		public LichHocTSource ()
		{
		}
		public List<chiTietLH> Items;
	
		NSString cellIdentifier = new NSString("TableCell");
		public int currentExpandedIndex = -1;
		public NSIndexPath lastindexPath;
		bool key=true;

		public  LichHocTSource(List<chiTietLH> list,VCLichHocTuan controller)
		{
			Items = list;
			this.controller = controller;

		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			
				return Items.Count + ((currentExpandedIndex > -1) ? 1 : 0);

		}

		public void collapseSubItemsAtIndex(UITableView tableView, int index)
		{
			tableView.DeleteRows(new[] {NSIndexPath.FromRowSection(index+1, 0)}, UITableViewRowAnimation.Fade);
		}

		void expandItemAtIndex(UITableView tableView, int index)
		{
			int insertPos = index + 1;
			key = false;

			tableView.InsertRows(new[] {NSIndexPath.FromRowSection(insertPos++, 0)}, UITableViewRowAnimation.Fade);

		}
		   
		protected bool isChild(NSIndexPath indexPath)
		{
			return currentExpandedIndex > -1 &&
				indexPath.Row > currentExpandedIndex &&
				indexPath.Row <= currentExpandedIndex + 1;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			lastindexPath = indexPath;
			if (isChild(indexPath)) {
				//Handle selection of child cell
				Console.WriteLine("You touched a child!");
				tableView.DeselectRow(indexPath, true);
				return;
			}
			tableView.BeginUpdates();
			if (currentExpandedIndex == indexPath.Row) {
				this.collapseSubItemsAtIndex(tableView, currentExpandedIndex);
				currentExpandedIndex = -1;
			} else {
				var shouldCollapse = currentExpandedIndex > -1;
				if (shouldCollapse) {
					this.collapseSubItemsAtIndex(tableView, currentExpandedIndex);
				}
				currentExpandedIndex = (shouldCollapse && indexPath.Row > currentExpandedIndex) ? indexPath.Row - 1 : indexPath.Row;
				this.expandItemAtIndex(tableView, currentExpandedIndex);
			}
			tableView.EndUpdates();

		}

		//TODO: implement this here?
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			int row = indexPath.Row;
			if (row == currentExpandedIndex+1&&row!=0) {
				key = false;

			}
			if (key) {
				if (row == Items.Count) {
					row = row - 1;
				}
				LichHocTCell cell = tableView.DequeueReusableCell (cellIdentifier) as LichHocTCell;
				if (cell == null) {
					cell = new LichHocTCell (cellIdentifier);

				}
				 else {
					cell.UpdateCell ("", "", "", "", "", "", 0, false);
				}
				LHRemindItem rmItem = BRemind.GetLHRemind (SQLite_iOS.GetConnection (), Items [row].Id, Items [row].Tuan);
				bool hasRM = false;
					if (rmItem != null) {
						hasRM = true;
					}
					string monhoc = BMonHoc.GetMH (SQLite_iOS.GetConnection (), BLichHoc.GetLH (SQLite_iOS.GetConnection (), Items [row].Id).MaMH).TenMH;
					cell.UpdateCell (monhoc, Items [row].TietBatDau, Items [row].SoTiet, Items [row].Phong
						, Items [row].Thu
						, Items [row].Tuan.Substring (3, 2) + "/" + Items [row].Tuan.Substring (0, 2)
						,	row, hasRM);
					UILongPressGestureRecognizer longPress = new UILongPressGestureRecognizer (LongPress);
					cell.AddGestureRecognizer (longPress);
					return cell;
			
			} else {
				LHExpandCell cell = tableView.DequeueReusableCell (cellIdentifier) as LHExpandCell;

				if (cell == null) {
					cell = new LHExpandCell (cellIdentifier);

				}

				var lh = BLichHoc.GetLH (SQLite_iOS.GetConnection (), Items [row-1].Id);
				MonHoc monhoc = BMonHoc.GetMH (SQLite_iOS.GetConnection (), lh.MaMH);
				cell.UpdateCell (Items [row-1],lh,monhoc.SoTC.ToString());
				key = true;
				return cell;
			}
		}
		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			// In here you could customize how you want to get the height for row. Then   
			// just return it. 

			return 100;
		}
		void LongPress(UILongPressGestureRecognizer gesture)
		{
			LichHocTCell cell = (LichHocTCell )gesture.View;

			VCHomeReminder remid = new VCHomeReminder (controller);
			remid.ct = Items [cell.num];
			LHRemindItem rmItem = BRemind.GetLHRemind (SQLite_iOS.GetConnection (), remid.ct.Id, remid.ct.Tuan);

			if (rmItem != null) {
				remid.LoadEvent (rmItem.EventID);
			} else {
				remid.lh = BLichHoc.GetLH (SQLite_iOS.GetConnection (), Items [cell.num].Id);
				remid.RemindLH ();
			}
		}
	}
}

