using System;
using Foundation;


namespace School.iOS
{
	public class SettingsHelper
	{
		public static void SaveSetting(string key,bool value)
		{
//			NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
//			NSMutableDictionary appDefaults = new NSMutableDictionary(); 
//			appDefaults.SetValueForKey( NSObject.FromObject( value), new NSString("AutoUpdate" ) );
//			userDefaults.RegisterDefaults( appDefaults );
//			userDefaults.Synchronize();
			NSUserDefaults.StandardUserDefaults.SetValueForKey (NSObject.FromObject (value), new NSString (key));
		}
		public static bool LoadSetting (string key)
		{
			var settingValue = NSUserDefaults.StandardUserDefaults.BoolForKey(key);

			return settingValue;
		}

	}
}

