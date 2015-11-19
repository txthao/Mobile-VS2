
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using School.Core;
using Android.Views.InputMethods;
using Android.Content.PM;

namespace School.Droid
{
	[Activity (Label = "Đăng nhập", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class LoginActivity : Activity
	{
		Button btnLogin;
		EditText username;
		EditText password;
		TextView errormsg;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
				
					
			SetContentView (Resource.Layout.LogIn);

					// Create your application here
			password = FindViewById<EditText> (Resource.Id.txtmk);
			username = FindViewById<EditText> (Resource.Id.txtmsv);
			username.FocusChange+= delegate {
				hideKeyboard(username);
			};
			password.FocusChange+= delegate {
				hideKeyboard(password);
			};



			username.NextFocusDownId = Resource.Id.txtmk;


			btnLogin = FindViewById<Button> (Resource.Id.btDangNhap);

			btnLogin.Click += new EventHandler (LogInProcess);
				
			TextView txtVersion = FindViewById<TextView> (Resource.Id.txtVersion_Log);
			txtVersion.Click += TxtVersion_Click;
		
		
		}

		void TxtVersion_Click (object sender, EventArgs e)
		{
			Dialog dialog = new Dialog(this);
			dialog.SetContentView(Resource.Layout.CustomDialog);
			dialog.SetTitle("Giới thiệu");

			string d1 = "'Thông tin đào tạo' là ứng dụng cung cấp thông tin đạo tạo cho sinh viên Đại học Sài Gòn. Các thông tin bao gồm lịch thi, thời khóa biểu, xem điểm thi và xem học phí. Ngoài ra, ứng dụng còn cung cấp thêm tính năng nhắc lịch thi, lịch học nhằm hỗ trợ, nhắc nhở các bạn sinh viên cho việc học tập hằng tuần hoặc ôn luyện khi thi cuối kì. ";


			dialog.FindViewById<TextView> (Resource.Id.txt_d1).Text = d1;

			dialog.Show();
		}


		async void  LogInProcess(object sender, EventArgs e)
		{
			errormsg = FindViewById<TextView> (Resource.Id.errorMSG);
			if (Common.checkNWConnection (this) == true) {
				
				Exception error = null;

				if (!String.IsNullOrEmpty (username.Text) && !String.IsNullOrEmpty (password.Text)) {
					ProgressDialog dialog = new ProgressDialog (this);
					dialog.SetMessage ("Đăng nhập...");
					dialog.Indeterminate = false;
					dialog.SetCancelable (false);
					
					dialog.Show ();
					error=await BUser.CheckAuth(username.Text,password.Text,SQLite_Android.GetConnection());
					if (error==null) {
						Intent myintent = new Intent (this, typeof(DrawerActivity));
						myintent.PutExtra ("FirstLoad", true);
						dialog.SetMessage ("Đang tải dữ liệu....");
						await Common.LoadDataFromSV (this);
						StartActivity (myintent);

						this.Finish ();
					} else {
						dialog.Dismiss ();
						errormsg.Text = error.Message;

					}
				}
				else
				{
					errormsg.Text = "Vui lòng nhập đầy đủ thông tin đăng nhập";
				}
			} else {
				errormsg.Text = "Không có kết nối mạng, vui lòng thử lại sau";
			}
		}
		public void hideKeyboard(View view) {
			InputMethodManager inputMethodManager =(InputMethodManager)GetSystemService(Activity.InputMethodService);
			inputMethodManager.HideSoftInputFromWindow(view.WindowToken, 0);
		}



	}
}

