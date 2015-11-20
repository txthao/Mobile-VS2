using System;
using UIKit;
using System.Collections.Generic;
using School.Core;
using Foundation;
using System.Threading.Tasks;
using System.IO;
using CoreGraphics;

namespace School.iOS
{
	public class LichHocHKSource: UITableViewSource 
	{
		UIViewController controller;
		List<chiTietLH> tableItems;
		NSString cellIdentifier = new NSString("TableCell");
		bool isHeader=false;
		public LichHocHKSource (List<chiTietLH> list,UIViewController controller )
		{
			tableItems = list;
			isHeader = false;
			this.controller = controller;
		}
		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			// In here you could customize how you want to get the height for row. Then   
			// just return it. 

			return 60;
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
				List<LHRemindItem> rmItem=BRemind.GetLHRemind(SQLite_iOS.GetConnection (),tableItems [indexPath.Row].Id);
				bool hasRM=false;
				if (rmItem.Count>0)
				{
					hasRM=true;
				}
				cell.UpdateCell (monhoc, tableItems [indexPath.Row].Thu, tableItems [indexPath.Row].TietBatDau
					, tableItems [indexPath.Row].SoTiet,tableItems [indexPath.Row].Phong,indexPath.Row,hasRM);
				UILongPressGestureRecognizer longPress = new UILongPressGestureRecognizer (LongPress);

				cell.AddGestureRecognizer (longPress);

				if (cell.num % 2 != 0) {
					cell.BackgroundColor = UIColor.FromRGBA((float)0.8, (float)0.8, (float)0.8, (float)1);
				}


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
		async void LongPress(UILongPressGestureRecognizer gesture)
		{
			if (gesture.State == UIGestureRecognizerState.Ended) {
				LichHocHKCell cell = (LichHocHKCell)gesture.View;
				string monhoc = BMonHoc.GetMH (SQLite_iOS.GetConnection (), BLichHoc.GetLH (SQLite_iOS.GetConnection (), tableItems [cell.num].Id).MaMH).TenMH;
				VCHomeReminder remid = new VCHomeReminder (controller);
				List<LHRemindItem> rmItem=BRemind.GetLHRemind(SQLite_iOS.GetConnection (),tableItems [cell.num].Id);
				bool hasRM=false;
				if (rmItem.Count>0)
				{
					hasRM=true;
				}
				if (!hasRM) {

					remid.lh = BLichHoc.GetLH (SQLite_iOS.GetConnection (), tableItems [cell.num].Id);
					List<LichHoc> list = new List<LichHoc> ();
					list.Add (remid.lh);
					var mycontent = ShowAlert (monhoc);
					string content = await mycontent;
					await remid.RemindALLLH (list, content);
				}
				else
				{


					bool accepted = await ShowAlert("Xoá Nhắc Lịch", "Bạn muốn xoá hết các nhắc lịch cho môn "+monhoc);
					if (accepted) {
						remid.RemoveEvents (rmItem);

					}

				}
			}
		}

		private async Task<string> ShowAlert(string mh)
		{
			var alert = new UIAlertView();

			alert.Title="Nhắc Lịch Cho Môn "+mh;
			alert.Message = "Nội dung";
			alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			alert.AddButton ("OK");
			alert.AddButton ("Cancel");
			var tcs = new TaskCompletionSource<int>();

			alert.Clicked += (s, e) => {
				tcs.SetResult (int.Parse (e.ButtonIndex.ToString ()));

			};
			alert.Show();

			await tcs.Task;

			string text = alert.GetTextField(0).Text;
			return text;

		}
		public Task<bool> ShowAlert(string title, string message) {
			var tcs = new TaskCompletionSource<bool>();

			UIApplication.SharedApplication.InvokeOnMainThread(new Action(() =>
				{
					UIAlertView alert = new UIAlertView(title, message, null, NSBundle.MainBundle.LocalizedString("Cancel", "Cancel"),
						NSBundle.MainBundle.LocalizedString("OK", "OK"));
					alert.Clicked += (sender, buttonArgs) => tcs.SetResult(buttonArgs.ButtonIndex != alert.CancelButtonIndex);
					alert.Show();
				}));

			return tcs.Task;
		}
	}
}

