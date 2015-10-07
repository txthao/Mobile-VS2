﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics.Drawables;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.Content.Res;
using School.Core;

namespace School.Droid
{
	[Activity (Label = "School App")]			
	public class DrawerActivity : Activity
	{
		private DrawerLayout _drawer;
		private MyActionBarDrawerToggle _drawerToggle;
		private ListView _drawerList;
		Bundle bundle;
		private string _drawerTitle;
		private string _title;
		private string[] _menuTitles;
		private int previousItemChecked;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Menu);
			Intent t = this.Intent;

				bool firstload=t.GetBooleanExtra("FirstLoad",false);
			
			if (firstload == true) {
				if (Common.checkNWConnection (this) == true) {
					Common.LoadDataFromSV ();
				} else {
					Toast.MakeText (this,"Không Có Kết Nối Tới Mạng Internet, Vui Lòng Thử Lại Sau", ToastLength.Long).Show();

				}
			}

			string namesv = BUser.GetMainUser (SQLite_Android.GetConnection ()).Hoten;
			_title = _drawerTitle = Title;
			_menuTitles = Resources.GetStringArray(Resource.Array.MenuArray);
			_drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			_drawerList = FindViewById<ListView>(Resource.Id.left_drawer);

			_drawer.SetDrawerShadow(Resource.Drawable.drawer_shadow_dark, (int)GravityFlags.Start);
			List<DrawerItem> listItems = new List<DrawerItem> ();
			listItems.Add(new DrawerItem(namesv,Resource.Drawable.user,true));
			listItems.Add(new DrawerItem("Lịch Học",Resource.Drawable.note,false));
			listItems.Add(new DrawerItem("Lịch Thi",Resource.Drawable.pencil,false));
			listItems.Add(new DrawerItem("Điểm Thi",Resource.Drawable.archive,false));
			listItems.Add(new DrawerItem("Học Phí",Resource.Drawable.tag,true));
			listItems.Add(new DrawerItem("Cài đặt",Resource.Drawable.configuration2,false));
			listItems.Add (new DrawerItem ("Đăng xuất", Resource.Drawable.back,false));
			_drawerList.Adapter = new CustomDrawerAdapter (this, listItems);
			_drawerList.ItemClick += (sender, args) => SelectItem(args.Position);


			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);
			ActionBar.SetIcon(new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));

			//DrawerToggle is the animation that happens with the indicator next to the
			//ActionBar icon. You can choose not to use this.
			_drawerToggle = new MyActionBarDrawerToggle(this, _drawer,
				Resource.Drawable.ic_drawer_light,
				Resource.String.DrawerOpen,
				Resource.String.DrawerClose);

			//You can alternatively use _drawer.DrawerClosed here
			_drawerToggle.DrawerClosed += delegate
			{
				ActionBar.Title = _title;
				ActionBar.SetIcon(new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));
				InvalidateOptionsMenu();
			};

			//You can alternatively use _drawer.DrawerOpened here
			_drawerToggle.DrawerOpened += delegate
			{
				ActionBar.Title = _drawerTitle;
				InvalidateOptionsMenu();
			};

			_drawer.SetDrawerListener(_drawerToggle);

			this.ActionBar.SetDisplayHomeAsUpEnabled(true);
			this.ActionBar.SetHomeButtonEnabled(true);
			if (null == savedInstanceState) {
				SelectItem (5);
				previousItemChecked = 5;
			}

		}

		private void SelectItem(int position)
		{
			LoadSettings ();
			var fragment=new Fragment();
            switch (position) {
			case 0:
				break;
            case 1:
                fragment = new LichHocTuanFragment ();
                break;
            case 2:
                fragment = new LichThiFragment ();
                break;
            case 3:
                fragment = new DiemThiHKFragment ();
                break;
            case 4:
                fragment = new HocPhiFragment ();
                break;
			case 5:
				fragment = new SettingsFragment ();

				break;
			case 6:
				BUser.LogOut (SQLite_Android.GetConnection ());
				var prefs = Application.Context.GetSharedPreferences ("SGU APP", FileCreationMode.Private);
				prefs.Edit ().Clear ().Commit ();
				Intent myintent = new Intent (this, typeof(LoginActivity));
				StartActivity (myintent);
				this.Finish ();
				break;
           
            }
			if (position != 0) {
				
				fragment.Arguments = bundle;
				FragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, fragment).AddToBackStack(""+previousItemChecked)
				.Commit ();
				_drawerList.SetItemChecked (position, true);
				previousItemChecked = _drawerList.CheckedItemPosition;

			
				ActionBar.Title = _title = _menuTitles [position];
				_drawer.CloseDrawer (_drawerList);
			}
		}

		protected override void OnPostCreate(Bundle savedInstanceState)
		{
			base.OnPostCreate(savedInstanceState);
			_drawerToggle.SyncState();
		}

		public override void OnConfigurationChanged(Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);
			_drawerToggle.OnConfigurationChanged(newConfig);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{

			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnPrepareOptionsMenu(IMenu menu)
		{
			
		
			return base.OnPrepareOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			

			if (_drawerToggle.OnOptionsItemSelected(item))
				return true;
			return base.OnOptionsItemSelected(item);
		}
		private void LoadSettings()
		{
			var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);              
			var checkRemind = prefs.GetBoolean ("Remind",false);
			var autoUpdate= prefs.GetBoolean ("AutoUpdateData",false);
			bundle = new Bundle();

			bundle.PutBoolean ("Remind", checkRemind);
			bundle.PutBoolean ("AutoUpdateData", autoUpdate);

		}
		override public void OnBackPressed()
		{
			
			if (FragmentManager.BackStackEntryCount > 1) {
				String title = FragmentManager.GetBackStackEntryAt (FragmentManager.BackStackEntryCount - 1).Name;
				FragmentManager.PopBackStackImmediate ();
				int pos = int.Parse (title);
				while (pos > 5) {
					title = FragmentManager.GetBackStackEntryAt (FragmentManager.BackStackEntryCount - 1).Name;
					FragmentManager.PopBackStackImmediate ();
					 pos = int.Parse (title);
				}
				SelectItem (pos);
				FragmentManager.PopBackStackImmediate ();
			} else {
				base.OnBackPressed ();
				this.Finish ();
			}

		}
}
}