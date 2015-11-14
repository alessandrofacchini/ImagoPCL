using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

using System.IO;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Locations;
using System.Collections.Generic;
using System.Linq;
using xUtilityANDROID2;
using AnagrafeCaninaMobileFORMS.DROID;

namespace testRestCompressed.Droid
{
	[Activity (MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	//Theme = "@style/Theme.AppCompat")]                         
	public class MainActivity : MainActivityBase, ILocationListener
	{
		//Android.Locations.Location _currentLocation;
		//Android.Locations.LocationManager _locationManager;
		//String _locationProvider;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//SupportRequestWindowFeature (WindowCompat.FeatureActionBar);

			RequestedOrientation = global::Android.Content.PM.ScreenOrientation.Portrait;

			global::Xamarin.Forms.Forms.Init (this, bundle);
			global::Xamarin.FormsMaps.Init (this, bundle);
			var hud = DependencyService.Get<Acr.XamForms.UserDialogs.IUserDialogService> ();
			var phone = DependencyService.Get<Acr.XamForms.Mobile.IPhoneService> ();
			var email = DependencyService.Get<Xamarin.Forms.Labs.Services.Email.IEmailService> ();  

			LoadApplication (new App ());
			App.k_screenW = xUtilityAndroid.Measures.getWidthDpi (this);
			App.k_screenH = xUtilityAndroid.Measures.getHeightDpi (this);
			App.k_Density = xUtilityAndroid.Measures.getDensity (this);
			InitializeLocationManager ();
			Global.MapsInstalled = CheckMapApplication ();

		}

		protected override void OnResume ()
		{
			base.OnResume ();
			try {
				//posso provenire dalla finestra settaggi
				if (_locationProvider == "" || _locationProvider == "passive") {
					InitializeLocationManager ();
				}
				if (_locationProvider == "" || _locationProvider == "passive") {
					Global.currentPosition = new Xamarin.Forms.Maps.Position (Global.defaultPosition.Latitude, Global.defaultPosition.Longitude);
					var x = new platformSpecific_Android ();
					x.userlatitude = Global.defaultPosition.Latitude;
					x.userlongitude = Global.defaultPosition.Longitude;
					Global.PositionChangedNumber += 1;
					Global.calculateDistance (false);
					Global.LocationUpdatePending = true;
					return;
				}

				_locationManager.RequestLocationUpdates (_locationProvider, 60000, 500, this); //60 sec e 500 metri
			} catch (Exception ex) {
				
			}

		}

		protected override void OnPause ()
		{
			base.OnPause ();
			_locationManager.RemoveUpdates (this);
		}

		void InitializeLocationManagerxxxx ()
		{
			_locationManager = (LocationManager)GetSystemService (LocationService);

			Criteria criteriaForLocationService = new Criteria {
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders (criteriaForLocationService, true);

			if (acceptableLocationProviders.Count == 0) {

				Criteria criteriaForLocationService2 = new Criteria {
					Accuracy = Accuracy.Medium
				};
				_locationProvider = _locationManager.GetBestProvider (criteriaForLocationService2, true);

				if (_locationProvider == "" || _locationProvider == "passive") {
					Criteria criteriaForLocationService3 = new Criteria {
						Accuracy = Accuracy.Low
					};
					_locationProvider = _locationManager.GetBestProvider (criteriaForLocationService3, true);
				}
				return;
			}

			if (acceptableLocationProviders.Any ()) {
				_locationProvider = acceptableLocationProviders.First ();
			} else {
				_locationProvider = String.Empty;
			}
		}

		public Boolean  CheckMapApplicationxxx ()
		{
			PackageManager pm = PackageManager;

			var packagesls = pm.GetInstalledPackages (PackageInfoFlags.MetaData);

			foreach (PackageInfo packageInfo in packagesls) {
				if (packageInfo.PackageName == "com.google.android.apps.maps") {
					return true;
				}
				System.Console.WriteLine (packageInfo.PackageName);
				//System.Console.WriteLine (pm.GetLaunchIntentForPackage (packageInfo.PackageName));
			}
			return false;
		}

		public void OnLocationChanged (Android.Locations.Location location)
		{
			_currentLocation = location;

			if (_currentLocation == null) {
				Global.currentPosition = new Xamarin.Forms.Maps.Position (0, 0);
				Global.PositionChangedNumber = 1;
				//_locationText.Text = "Unable to determine your location.";
			} else {
				Global.currentPosition = new Xamarin.Forms.Maps.Position (location.Latitude, location.Longitude);
				var x = new platformSpecific_Android ();
				x.userlatitude = location.Latitude;
				x.userlongitude = location.Longitude;
				Global.PositionChangedNumber += 1;
				Global.calculateDistance (false);
				Global.LocationUpdatePending = true;
				//_locationText.Text = String.Format ("{0},{1}", _currentLocation.Latitude, _currentLocation.Longitude);
			}
				

		}

		public void OnProviderDisabled (string provider)
		{
		}

		public void OnProviderEnabled (string provider)
		{
		}

		public void OnStatusChanged (string provider, Android.Locations.Availability status, Bundle extras)
		{
		}
	

	}



	public class MainActivityBasexxx:  global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		public Android.Locations.Location _currentLocation { get; set; }

		public Android.Locations.LocationManager _locationManager { get; set; }

		public String _locationProvider { get; set; }

		public void InitializeLocationManager ()
		{
			_locationManager = (LocationManager)GetSystemService (LocationService);

			Criteria criteriaForLocationService = new Criteria {
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders (criteriaForLocationService, true);

			if (acceptableLocationProviders.Count == 0) {

				Criteria criteriaForLocationService2 = new Criteria {
					Accuracy = Accuracy.Medium
				};
				_locationProvider = _locationManager.GetBestProvider (criteriaForLocationService2, true);

				if (_locationProvider == "" || _locationProvider == "passive") {
					Criteria criteriaForLocationService3 = new Criteria {
						Accuracy = Accuracy.Low
					};
					_locationProvider = _locationManager.GetBestProvider (criteriaForLocationService3, true);
				}
				return;
			}

			if (acceptableLocationProviders.Any ()) {
				_locationProvider = acceptableLocationProviders.First ();
			} else {
				_locationProvider = String.Empty;
			}
		}

		public Boolean  CheckMapApplication ()
		{
			PackageManager pm = PackageManager;

			var packagesls = pm.GetInstalledPackages (PackageInfoFlags.MetaData);

			foreach (PackageInfo packageInfo in packagesls) {
				if (packageInfo.PackageName == "com.google.android.apps.maps") {
					return true;
				}
				System.Console.WriteLine (packageInfo.PackageName);
				//System.Console.WriteLine (pm.GetLaunchIntentForPackage (packageInfo.PackageName));
			}
			return false;
		}

	}

}

