using System;
using UIKit;
using System.Collections.Generic;
using Foundation;
using School.Core;

namespace School.iOS
{
	public class LichHocTSource : UITableViewSource
	{
		public LichHocTSource ()
		{
		}
		public List<chiTietLH> Items;

		NSString cellIdentifier = new NSString("TableCell");
		protected int currentExpandedIndex = -1;
		bool key=true;

		public  LichHocTSource(List<chiTietLH> list)
		{
			Items = list;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return Items.Count + ((currentExpandedIndex > -1) ? 1 : 0);
		}

		void collapseSubItemsAtIndex(UITableView tableView, int index)
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
			tableView.DeselectRow(indexPath, true);
		}

		//TODO: implement this here?
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (key) {
				LichHocTCell cell = tableView.DequeueReusableCell (cellIdentifier) as LichHocTCell;

				if (cell == null) {
					cell = new LichHocTCell (cellIdentifier);
				}
				string monhoc = BMonHoc.GetMH (SQLite_iOS.GetConnection (), BLichHoc.GetLH (SQLite_iOS.GetConnection (), Items [indexPath.Row].Id).MaMH).TenMH;
				cell.UpdateCell (monhoc, Items [indexPath.Row].TietBatDau, Items [indexPath.Row].SoTiet, Items [indexPath.Row].Phong
				, Items [indexPath.Row].Thu
				, Items [indexPath.Row].Tuan.Substring (3, 2) + "/" + Items [indexPath.Row].Tuan.Substring (0, 2)
				);
				return cell;
			} else {
				LHExpandCell cell = tableView.DequeueReusableCell (cellIdentifier) as LHExpandCell;

				if (cell == null) {
					cell = new LHExpandCell (cellIdentifier);
				}
				var lh = BLichHoc.GetLH (SQLite_iOS.GetConnection (), Items [indexPath.Row-1].Id);
				MonHoc monhoc = BMonHoc.GetMH (SQLite_iOS.GetConnection (), lh.MaMH);
				cell.UpdateCell (Items [indexPath.Row-1],lh,monhoc.SoTC.ToString());
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
	}
}

