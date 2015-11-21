using System;

namespace School.Core
{
	public class UrlHelper
	{
		
		public static string UrlHP(string id, string pass)
		{
			return String.Format ("http://www.schoolapi.somee.com/api/hocphi/{0}/{1}", id, pass);
		}
		public static string UrlDT(string id)
		{
			return String.Format ("http://www.schoolapi.somee.com/api/diemthi/" + id);
		}
		public static string UrlLT(string id)
		{
			return String.Format ("http://www.schoolapi.somee.com/api/lichthi/" + id);
		}
		public static string UrlLH(string id)
		{
			return String.Format ("http://www.schoolapi.somee.com/api/thoikhoabieu/" + id);
		}
		public static string About()
		{
			return "'Thông Tin Đào Tạo' là ứng dụng cung cấp thông tin đạo tạo cho sinh viên Đại học Sài Gòn. Các thông tin bao gồm lịch thi, thời khóa biểu, xem điểm thi và xem học phí. Ngoài ra, ứng dụng còn cung cấp thêm tính năng nhắc lịch thi, lịch học nhằm hỗ trợ, nhắc nhở các bạn sinh viên cho việc học tập hằng tuần hoặc ôn luyện khi thi cuối kì. ";
		}

	}
}

