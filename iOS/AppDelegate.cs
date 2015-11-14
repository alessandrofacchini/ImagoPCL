using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using xUtilityMAC2;
using System.Threading;
using System.IO;
using xUtilityPCL;
using CoreLocation;

namespace testRestCompressed.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			UIApplication.SharedApplication.SetStatusBarHidden (false, true);
			UIApplication.SharedApplication.SetStatusBarStyle (UIStatusBarStyle.Default, true);

			UINavigationBar.Appearance.ShadowImage = new UIKit.UIImage ();
			global::Xamarin.Forms.Forms.Init ();
			xUtilityMAC2.platformSpecific_IOS.NoOp (); 
			LoadApplication (new App ());
			App.k_screenW = (float)UIScreen.MainScreen.Bounds.Width;
			App.k_screenH = (float)UIScreen.MainScreen.Bounds.Height;
			App.k_Density = 1;
			Global.MapsInstalled = true;

			var lst = UIFont.FamilyNames;

			foreach (string f in lst) {
			
				Console.WriteLine ("family:" + f);

				var fonts = UIFont.FontNamesForFamilyName (f);

				foreach (string fa in fonts) {

					Console.WriteLine ("   ---- font:" + fa);
				}
			}

			GetLocation ();
			//xUtilityMAC.Misc.GetLocation ();//20140925

			Global.iOSDeviceName = UIKit.UIDevice.CurrentDevice.Name;

			if (Global.iOSDeviceName.ToLower () == "iPad di Alessandro".ToLower () || Global.iOSDeviceName.ToLower () == "iphone simulator".ToLower ())
				Global.MaxDistanceMeters = 200000;


			string databasePath = xUtilityMAC.Tools.getDicStore ();
			string databaseFull = System.IO.Path.Combine (databasePath, Global.databaseName);

			if (File.Exists (databaseFull) == true) {
				//it's not the first time
			
				//var plat = new  SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS ();
				var cn = new SQLite.SQLiteConnection (databaseFull);  //new SQLite.Net.SQLiteConnection (plat, databaseFull);
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





			return base.FinishedLaunching (app, options);
		}

		#region GPS

		const float distanceFilter = 5;
		//meters
		//const float distanceFilter = 100;
		//const float distanceFilter = 0;
		//if the device change on this values the event will be raized


		public static xUtilityMAC.LocationManager Manager = null;

		public void GetLocation ()
		{
			Manager = new xUtilityMAC.LocationManager ();

			Manager.callbackGps += delegate(CoreLocation.CLLocation obj) {




				processNewGpsUpdated (obj);//ask to the webservice

				NSUserDefaults.StandardUserDefaults.SetBool (true, "gpsEnabled");
				NSUserDefaults.StandardUserDefaults.Synchronize ();

				//Manager = null; // to just call one
			};



			Manager.callbackGpsError += delegate(string obj) {

				//MonoTouch.TestFlight.TestFlight.PassCheckpoint ("callbackGpsError : " + obj);

				NSUserDefaults.StandardUserDefaults.SetBool (false, "gpsEnabled");
				NSUserDefaults.StandardUserDefaults.Synchronize ();

				UIAlertView alert = new UIAlertView ();
				alert.Title = "Errore";
				alert.AddButton ("OK");

				alert.Message = obj;
				alert.Show ();

			};



			Manager.distanceFilter = distanceFilter;
			Manager.forceFilter = true; // to set a range of filtering


			Manager.StartLocationUpdates ();
		}

		private void processNewGpsUpdated (CoreLocation.CLLocation location)
		{
			Global.currentPosition = new Xamarin.Forms.Maps.Position (location.Coordinate.Latitude, location.Coordinate.Longitude);
			var x = new platformSpecific_IOS ();
			x.userlatitude = location.Coordinate.Latitude;
			x.userlongitude = location.Coordinate.Longitude;
			Global.PositionChangedNumber += 1;
			Global.calculateDistance (false);
			Global.LocationUpdatePending = true;
		}

		private bool FirstTimeLoadingGps = true;
		private bool loadingOfferts = false;

		private void callNotification (double lat, double lon, bool fromtimer)
		{
			//MonoTouch.TestFlight.TestFlight.PassCheckpoint ("calling web service");

			if (loadingOfferts)
				return;

			loadingOfferts = true;

			ThreadPool.QueueUserWorkItem (state => {

				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo ("en-US");    

				System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo ("en-US");          



			});
		}



		#endregion

		/*
		public override void OnActivated (UIApplication uiApplication)
		{
			base.OnActivated (uiApplication);
			GetLocation ();
		}

		public override void WillEnterForeground (UIApplication uiApplication)
		{
			base.WillEnterForeground (uiApplication);

		}
		*/


	}
}

