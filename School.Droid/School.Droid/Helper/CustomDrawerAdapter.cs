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
			DrawerItemHolder itemHolder;
				View view = convertView;
			if (view == null) { // no view to re-use, create new
				itemHolder = new DrawerItemHolder ();
				view = LayoutInflater.From (context).Inflate (Resource.Layout.drawer_item, null, false);
				itemHolder.icon = view.FindViewById<ImageView> (Resource.Id.icon_drawer);
				itemHolder.itemName = view.FindViewById<TextView> (Resource.Id.text_drawer);
				itemHolder.separator = view.FindViewById<LinearLayout> (Resource.Id.separator);
				itemHolder.linear_drawerItem = view.FindViewById<LinearLayout> (Resource.Id.linear_drawerItem);
				itemHolder.icon_drawer = view.FindViewById<ImageView> (Resource.Id.icon_drawer);	
				view.Tag = itemHolder;
			}else {
				itemHolder = (DrawerItemHolder)view.Tag;
			}



			itemHolder.itemName.Text = listitem[position].getItemName ();
			itemHolder.icon.SetImageResource(listitem[position].getImgResID());

			if (listitem[position].HaveSeparator () == true) {
				itemHolder.separator.Visibility = ViewStates.Visible;
			}
			if (listitem[position].IsHeader () == true) {
				itemHolder.linear_drawerItem.SetBackgroundColor (Color.ParseColor ("#e6e6e6"));
				itemHolder.icon_drawer.Visibility = ViewStates.Gone;
				view.Clickable = false;
			}
				

			return view;
		}

		private  class DrawerItemHolder : Java.Lang.Object {
			public TextView itemName;
			public ImageView icon;
			public LinearLayout separator;
			public LinearLayout linear_drawerItem;
			public ImageView icon_drawer;
		}

	}
}

