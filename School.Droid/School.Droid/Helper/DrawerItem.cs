using System;

namespace School.Droid
{
	public class DrawerItem
	{
		String ItemName;
		int imgResID;
		bool HaveSeparator;
		public DrawerItem(String itemName, int imgResID, bool haveSeparator) {
			
			ItemName = itemName;
			this.imgResID = imgResID;
			this.HaveSeparator = haveSeparator;
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
		public bool Separator()
		{
			return HaveSeparator;
		}
		public void setImgResID(int imgResID) {
			this.imgResID = imgResID;
		}
	}
}

