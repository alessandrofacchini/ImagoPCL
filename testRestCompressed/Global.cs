using System;
using System.Collections.Generic;
using Xamarin.Forms;
using xUtilityPCL;
using System.Threading.Tasks;

namespace testRestCompressed
{
	public class Global
	{
		public Global ()
		{
		}

		public static string databaseName = "imagolight.db";
		public static Int32 currentLanguage = 1;
		public static string K_subfolder_MC = "MC";
		public static string K_subfolder_C = "C";
		public static string K_subfolder_P = "P";
		public static  xUtilityPCL.MyObservableCollection<MacroCategories> k_MacroCategories;
		public static  xUtilityPCL.MyObservableCollection<Categories> K_Categories;
		public static  xUtilityPCL.MyObservableCollection<Categories_POIs> K_Categories_POIs;
		public static  xUtilityPCL.MyObservableCollection<POIs> K_POIs;
		public static  xUtilityPCL.MyObservableCollection<POIsPictures> K_POIsPictures;
		public static  List<CCF> K_CCFs;
		public static Boolean isCCF;
		public static Xamarin.Forms.Maps.Position currentPosition;
		public static Int32 PositionChangedNumber;
		public static Boolean MapsInstalled;
		public static Xamarin.Forms.Maps.Position defaultPosition = new Xamarin.Forms.Maps.Position (46.0649418, 13.2307247);
		//udine
		public static Boolean LocationUpdatePending = false;

		public static void calculateDistance (Boolean lUpdateLabel = true)
		{
			if (K_POIs == null)
				return;
			var currentPlatform = DependencyService.Get<platformSpecific> ();
			foreach (POIs p in K_POIs) {
				p.Distance = currentPlatform.GetDistance (Global.currentPosition.Latitude, Global.currentPosition.Longitude, 
					Convert.ToDouble (p.Latitude), Convert.ToDouble (p.Longitude));
				p.Distance = p.Distance / 1000;
				if (lUpdateLabel)
					p.DistanceLabel = p.Distance.ToString ("###0.##") + " Km";
			}

			Global.isCCF = false;
			foreach (CCF p in K_CCFs) {
				p.Distance = currentPlatform.GetDistance (Global.currentPosition.Latitude, Global.currentPosition.Longitude, 
					Convert.ToDouble (p.map_latitude), Convert.ToDouble (p.map_longitude));
				if (p.Distance <= 5500)
					Global.isCCF = true;
			}

		}

		public static List<CCF>  createCCFs ()
		{
			List<CCF> _K_CCFs = new List<CCF> ();
			CCF i = new CCF () {
				NameOfVillage = "Buja",
				map_latitude = 46.1625185,
				map_longitude = 13.1168888,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Colloredo di MonteAlbano",
				map_latitude = 46.1625185,
				map_longitude = 13.1359629,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Coseano",
				map_latitude = 46.0979814,
				map_longitude = 13.0206481,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Dignano",
				map_latitude = 46.0850185,
				map_longitude = 12.9418888,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Facagna",
				map_latitude = 46.1138085,
				map_longitude = 13.0920819,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Flaibano",
				map_latitude = 46.0597037,
				map_longitude = 12.9853333,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Forgaria nel Friuli",
				map_latitude = 46.2239628,
				map_longitude = 12.9737334,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Majano",
				map_latitude = 46.1857010,
				map_longitude = 13.0683450,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Moruzzo",
				map_latitude = 46.1225110,
				map_longitude = 13.1240051,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Osoppo",
				map_latitude = 46.2561111,
				map_longitude = 13.0828148,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Ragogna",
				map_latitude = 46.1745555,
				map_longitude = 12.9834444,
			};
			_K_CCFs.Add (i);
			//----------------
			i = new CCF () {
				NameOfVillage = "Rive d'Arcano",
				map_latitude = 46.1265534,
				map_longitude = 13.0329279,
			};
			_K_CCFs.Add (i);
			//---------------
			i = new CCF () {
				NameOfVillage = "San Daniele del Friuli",
				map_latitude = 46.1612777,
				map_longitude = 13.0111111,
			};
			_K_CCFs.Add (i);
			//---------------
			i = new CCF () {
				NameOfVillage = "San Vito di Fagagna",
				map_latitude = 46.0915740,
				map_longitude = 13.0681481,
			};
			_K_CCFs.Add (i);
			//---------------
			i = new CCF () {
				NameOfVillage = "Treppo Grande",
				map_latitude = 46.1902468,
				map_longitude = 13.1582637,
			};
			_K_CCFs.Add (i);
			//---------------
			return _K_CCFs;
		}

		public static Int32 MaxDistanceMeters = 20000;

		public static string iOSDeviceName;


		public static async Task getGPSPositionForIOSxxxxx ()
		{
			var currentPlataform = DependencyService.Get<platformSpecific> ();
			//ListTemplate1 l2 = new ListTemplate1 (ListTemplate1.TipoFormEnum.Lista);


			var gpsStatus = await currentPlataform.IsGpsEnabledAsync ();

			if (gpsStatus == false) {
				Global.currentPosition = new Xamarin.Forms.Maps.Position (Global.defaultPosition.Latitude, Global.defaultPosition.Longitude);
				currentPlataform.userlatitude = Global.defaultPosition.Latitude;
				currentPlataform.userlongitude = Global.defaultPosition.Longitude;
				Global.PositionChangedNumber += 1;
				Global.calculateDistance (false);
				return;
			}

			ListTemplate1 l = new ListTemplate1 (ListTemplate1.TipoFormEnum.Lista);
			var pos = await l.GetCurrentGPSPosition (false);

			Global.currentPosition = new Xamarin.Forms.Maps.Position (pos.Latitude, pos.Longitude);

			currentPlataform.userlatitude = pos.Latitude;
			currentPlataform.userlongitude = pos.Longitude;
			Global.PositionChangedNumber += 1;
			Global.calculateDistance (true);
		}


		/*
		public static Boolean isCCF ()
		{
			var currentPlatform = DependencyService.Get<platformSpecific> ();
			Boolean Within5500MetersFromAnyVillage = false;
			foreach (CCF p in K_CCFs) {
				p.Distance = currentPlatform.GetDistance (Global.currentPosition.Latitude, Global.currentPosition.Longitude, 
					Convert.ToDouble (p.map_latitude), Convert.ToDouble (p.map_longitude));
				//p.Distance = p.Distance / 1000;
				if (p.Distance <= 5500)
					Within5500MetersFromAnyVillage = true;
				//p.DistanceLabel = p.Distance.ToString ("###0.##") + " Km";
			}

			return Within5500MetersFromAnyVillage;
		}
		*/

	}
}

