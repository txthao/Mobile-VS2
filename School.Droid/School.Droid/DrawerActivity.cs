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
using System.Threading.Tasks;
using Android.Content.PM;

namespace School.Droid
{
	[Activity (Label = "Thông Tin Đào Tạo", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class DrawerActivity : Activity
	{
		private DrawerLayout _drawer;
		private MyActionBarDrawerToggle _drawerToggle;
		private ListView _drawerList;
		Bundle bundle;
		private string _drawerTitle;
		private string _title;
		private string[] _menuTitles;
		public int previousItemChecked;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Menu);
		
			Intent t = this.Intent;

				bool firstload=t.GetBooleanExtra("FirstLoad",false);
			
			if (firstload == true) {
				
			}

			User sv = BUser.GetMainUser (SQLite_Android.GetConnection ());
			_title = _drawerTitle = Title;
			_menuTitles = Resources.GetStringArray(Resource.Array.MenuArray);
			_drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			_drawerList = FindViewById<ListView>(Resource.Id.left_drawer);

			_drawer.SetDrawerShadow(Resource.Drawable.drawer_shadow_dark, (int)GravityFlags.Start);
			View header = this.LayoutInflater.Inflate (Resource.Layout.DrawerHeader, null);
			header.FindViewById<TextView> (Resource.Id.txtTenSV).Text=sv.Hoten;
			header.FindViewById<TextView> (Resource.Id.txtmasv).Text=sv.Id;
			_drawerList.AddHeaderView (header);
			List<DrawerItem> listItems = new List<DrawerItem> ();
		//	listItems.Add(new DrawerItem(namesv,Resource.Drawable.user,true));
			listItems.Add(new DrawerItem("Lịch Học",0,false,true));
			listItems.Add(new DrawerItem("Theo Học Kỳ",Resource.Drawable.Iclichhoc,true,false));
			listItems.Add(new DrawerItem("Theo Tuần",Resource.Drawable.calendar,false,false));

			listItems.Add(new DrawerItem("Điểm Thi",0,false,true));
			listItems.Add(new DrawerItem("Theo Học Kỳ",Resource.Drawable.Icdiemthi,true,false));
			listItems.Add(new DrawerItem("Tất Cả",Resource.Drawable.IcAdiemthi,false,false));

			listItems.Add(new DrawerItem("Học Phí-Lịch Thi",0,false,true));
			listItems.Add(new DrawerItem("Lịch Thi",Resource.Drawable.Iclichthi,true,false));
			listItems.Add(new DrawerItem("Học Phí",Resource.Drawable.Ichocphi,false,false));

			listItems.Add(new DrawerItem("Ứng Dụng",0,false,true));
			listItems.Add(new DrawerItem("Cài đặt",Resource.Drawable.Icsettings,true,false));

			listItems.Add (new DrawerItem ("Đăng xuất", Resource.Drawable.signout,false,false));
			_drawerList.Adapter = new CustomDrawerAdapter (this, listItems);
			_drawerList.ItemClick += (sender, args) => SelectItem(args.Position);


			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);
			ActionBar.SetIcon(new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));

			//DrawerToggle is the animation that happens with the indicator next to the
			//ActionBar icon. You can choose not to use this.
			_drawerToggle = new MyActionBarDrawerToggle(this, _drawer,
				Resource.Drawable.ic_menu_white_18dp,
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
				SelectItem (2);
				previousItemChecked = 2;
			}

		}

		public void SelectItem(int position)
		{
			bundle=Common.LoadSettings ();
			var fragment=new Fragment();
            switch (position) {
			case 0:
				break;
            case 1:
                break;
            case 2:
                fragment = new LichHocHKFragment ();
                break;
            case 3:
                fragment = new LichHocTuanFragment ();
                break;
            case 4:
                break;
			case 5:
				fragment = new DiemThiHKFragment ();
				break;
			case 6:
				fragment = new DiemThiFragment ();
				break;
			case 7:
				
				break;
			case 8:
				fragment = new LichThiFragment ();
				break;
			case 9:
				fragment = new HocPhiFragment ();
				break;
			case 10:
				
				break;
			case 11:
				fragment = new SettingsFragment ();
				break;
			
			case 12:
				AlertDialog.Builder builder = new AlertDialog.Builder (
					                      this);

				// Setting Dialog Title

				builder.SetTitle (Resource.String.Alertitle); 
				builder.SetIcon (Android.Resource.Drawable.IcDialogAlert); 
				builder.SetMessage ("Bạn có muốn đăng xuất khỏi ứng dụng?"); 
				builder.SetPositiveButton ("Có", (sender, e) => {
					BUser.LogOut (SQLite_Android.GetConnection ());
					var prefs = Application.Context.GetSharedPreferences ("SGU APP", FileCreationMode.Private);
					prefs.Edit ().Clear ().Commit ();
					Intent myintent = new Intent (this, typeof(LoginActivity));
					StartActivity (myintent);
					this.Finish ();
				});
				builder.SetNegativeButton ("Không", (sender, e) => {
				});
				builder.Show(); 


				// Setting Dialog Message
			

				break;
           
            }

			if (position != 0&&position!=1&&position!=4&&position!=7&&position!=10&&position!=12) {
				
					fragment.Arguments = bundle;
					FragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, fragment).AddToBackStack ("" + previousItemChecked)
				.Commit ();
				if (position == previousItemChecked && FragmentManager.BackStackEntryCount > 1) {
					FragmentManager.PopBackStackImmediate ();

				}
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

		override public void OnBackPressed()
		{


				if (FragmentManager.BackStackEntryCount > 1) {
					String title = FragmentManager.GetBackStackEntryAt (FragmentManager.BackStackEntryCount - 1).Name;
					FragmentManager.PopBackStackImmediate ();
					int pos = int.Parse (title);
					
					while (pos > 12) {
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
