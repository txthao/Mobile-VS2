using System;

namespace School.Core
{
	public class UrlHelper
	{
		
		public static string UrlHP(string id, string pass)
		{
			return String.Format ("http://www.schoolapi.somee.com/hocphi/{0}/{1}", id, pass);
		}
		public static string UrlDT(string id)
		{
			return String.Format ("http://www.schoolapi.somee.com/diemthi/" + id);
		}
		public static string UrlLT(string id)
		{
			return String.Format ("http://www.schoolapi.somee.com/lichthi/" + id);
		}
		public static string UrlLH(string id)
		{
			return String.Format ("http://www.schoolapi.somee.com/thoikhoabieu/" + id);
		}
		public static string About()
		{
			return "'Thông Tin Đào Tạo' là ứng dụng xây dựng trên nền tảng Xamarin cung cấp thông tin đạo tạo cho sinh viên Đại học Sài Gòn. Các thông tin bao gồm lịch thi, thời khóa biểu, xem điểm thi và xem học phí. Ngoài ra, ứng dụng còn cung cấp thêm tính năng nhắc lịch thi, lịch học nhằm hỗ trợ, nhắc nhở các bạn sinh viên cho việc học tập hằng tuần hoặc ôn luyện khi thi cuối kì.\n " +
				"Khoá Luận Tốt Nghiệp DCT111\n   Giảng Viên: Cao Thái Phương Thanh\n   Sinh Viên: Bùi Minh Tiến\n   Sinh Viên: Trần Xuân Thảo";
		}

	}
}

