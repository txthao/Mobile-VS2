using System;

namespace School.Droid
{
	public class DrawerItem
	{
		String ItemName;
		int imgResID;
		bool haveSeparator;
		bool isHeader;
		public DrawerItem(String itemName, int imgResID, bool haveSeparator, bool isHeader) {
			
			ItemName = itemName;
			this.imgResID = imgResID;
			this.haveSeparator = haveSeparator;
			this.isHeader = isHeader;
		}

		public String getItemName() {
			return ItemName;
		}
		public void setItemName(String itemName) {
			ItemName = itemName;
		}
		public int getImgResID() {
			return imgResID;
		}
		public bool HaveSeparator()
		{
			return haveSeparator;
		}
		public void setImgResID(int imgResID) {
			this.imgResID = imgResID;
		}
		public bool IsHeader()
		{
			return isHeader;
		}

	}
}

