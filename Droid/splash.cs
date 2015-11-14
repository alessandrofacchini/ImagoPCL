
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using Xamarin.Forms;
using System.Threading.Tasks;
using xUtilityPCL;
using System.IO;
using SQLite.Net;
using AnagrafeCaninaMobileFORMS.DROID;

namespace testRestCompressed.Droid
{
	[Activity (MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash")]			
	public class splash : Activity
	{
		//http://forums.xamarin.com/discussion/19362/xamarin-forms-splashscreen-in-android
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			string databasePath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			string databaseFull = System.IO.Path.Combine (databasePath, Global.databaseName);

			if (File.Exists (databaseFull) == true) {
				//it's not the first time
				var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid ();
				var cn = new SQLite.Net.SQLiteConnection (plat, databaseFull);
				var m = cn.Table<MacroCategories> ().ToList ();
				Global.k_MacroCategories = new MyObservableCollection<MacroCategories> (m);
				var c = cn.Table<Categories> ().ToList ();
				Global.K_Categories = new MyObservableCollection<Categories> (c);
				var p = cn.Table<POIs> ().ToList ();
				Global.K_POIs = new MyObservableCollection<POIs> (p);
				var cp = cn.Table<Categories_POIs> ().ToList ();
				Global.K_Categories_POIs = new MyObservableCollection<Categories_POIs> (cp);
				var pg = cn.Table<POIsPictures> ().ToList ();
				Global.K_POIsPictures = new MyObservableCollection<POIsPictures> (pg);
				Global.K_CCFs = Global.createCCFs ();
				Thread.Sleep (1); // Simulate a long loading process on app startup
			} else {
				Thread.Sleep (2000); // Simulate a long loading process on app startup
			}


			var intent = new Intent (this, typeof(MainActivity));
			StartActivity (intent);

			Finish ();
			// Create your application here
		}

		protected override void OnDestroy ()
		{
			
			//SetTheme(Resource.Style.Theme_SplashActivityNoBG);
			base.OnDestroy ();
		}



	}
}

