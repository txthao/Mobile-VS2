using System;
using EventKit;
using UIKit;
using CoreGraphics;

namespace School.iOS
{
	public class App
	{
		public static App Current
		{
			get { return current; }
		}
		private static App current;
		public CGSize tableSize;
		public nfloat width;
		public nfloat height;
		public nfloat labelMHWidth;
		/// <summary>
		/// The EKEventStore is intended to be long-lived. It's expensive to new it up
		/// and can be thought of as a database, so we create a single instance of it
		/// and reuse it throughout the app
		/// </summary>
		public EKEventStore EventStore
		{
			get { return eventStore; }
		}
		protected EKEventStore eventStore;

		static App ()
		{
			current = new App();
		}
		protected App ()
		{
			eventStore = new EKEventStore ( );
			setSize ();
		}
		public void setSize()
		{
			//its iPhone. Find out which one?

				CGSize result = UIScreen.MainScreen.Bounds.Size;
				this.width = result.Width;
				this.height = result.Height;
			tableSize.Width = result.Width;
				if(result.Height == 480)
				{
				labelMHWidth = 100;
				tableSize.Height = 200;
				}
				else if(result.Height == 568)
				{
				labelMHWidth = 120;
				tableSize.Height = 360;
				}
				else if(result.Height == 667)
				{
				labelMHWidth = 150;
				tableSize.Height = 500;
				}
				else if(result.Height == 736)
				{
				labelMHWidth = 180;
				tableSize.Height = 580;
				}

		}
	}
}

