using System;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Views;
using Android.Graphics;

namespace School.Droid
{
	public class CustomDrawerAdapter: BaseAdapter<DrawerItem>
	{
		Context context;
		List<DrawerItem> listitem;

		public CustomDrawerAdapter(Context context,List<DrawerItem> listItems):base()
		{
			
			this.context = context;
			this.listitem = listItems;
		

		}

		public override long GetItemId(int position)
		{
			return position;
		}
		public override DrawerItem this[int position]
		{
			get { return listitem[position]; }
		}
		public override int Count {
			get { return listitem.Count; }


		}

		override public View GetView(int position, View convertView, ViewGroup parent) {
			// TODO Auto-generated method stub

				var item = listitem[position];
				View view = convertView;
				if (view == null) // no view to re-use, create new
				view = LayoutInflater.From (context).Inflate (Resource.Layout.drawer_item, null, false);
				
			view.FindViewById<TextView> (Resource.Id.text_drawer).Text = item.getItemName ();
			view.FindViewById<ImageView>(Resource.Id.icon_drawer).SetImageResource(item.getImgResID());
			if (item.Separator() == true) {
				view.FindViewById<LinearLayout> (Resource.Id.separator).Visibility = ViewStates.Visible;
			}
			if (item.IsHeader () == true) {
				view.FindViewById<LinearLayout> (Resource.Id.linear_drawerItem).SetBackgroundColor (Color.ParseColor ("#e6e6e6"));
				view.FindViewById<ImageView>(Resource.Id.icon_drawer).Visibility = ViewStates.Gone;
				view.Clickable = true;
			}
				

			return view;
		}

		private  class DrawerItemHolder {
			public TextView ItemName;
			public ImageView icon;
		}

	}
}

