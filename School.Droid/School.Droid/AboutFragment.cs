using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Xml;
using School.Core;

namespace School.Droid
{
	public class AboutFragment : Fragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreate (null);
			var rootView = inflater.Inflate(Resource.Layout.About, container, false);

			string d1 = "- 'Thông tin đào tạo' là ứng dụng cung cấp thông tin đạo tạo cho sinh viên Đại học Sài Gòn. Các thông tin bao gồm lịch thi, thời khóa biểu, xem điểm thi và xem học phí. Ngoài ra, ứng dụng còn cung cấp thêm tính năng nhắc lịch thi, lịch học nhằm hỗ trợ, nhắc nhở các bạn sinh viên cho việc học tập hằng tuần hoặc ôn luyện khi thi cuối kì. ";
			string d2 = "- Với giao diện trực quan, dễ sử dụng, ứng dụng 'Thông tin đào tạo' muốn mang đến nhiều tiện lợi trong quá trình học tập của các bạn sinh viên trường Đại học Sài Gòn. ";
			string d3 = "- Nguồn dữ liệu của ứng dụng được lấy từ website cung cấp thông tin đào tạo chính thức của trường Đại học Sài Gòn là http://thongtindaotao.sgu.edu.vn";

			rootView.FindViewById<TextView> (Resource.Id.txt_d1).Text = d1;
			rootView.FindViewById<TextView>(Resource.Id.txt_d2).Text = d2;
			rootView.FindViewById<TextView>(Resource.Id.txt_d3).Text = d3;
			return rootView;
		}
	}
}

