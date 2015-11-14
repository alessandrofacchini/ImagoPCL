using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using System.Linq;
using testRestCompressed;
using testRestCompressed.Droid;
using Android.Graphics.Drawables;
using System.Collections.Generic;

[assembly: ExportRenderer (typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace testRestCompressed.Droid
{
	public class ExtendedMapRenderer : MapRenderer
	{
		bool _isDrawnDone;

		Page page { get; set; }

		myNavPage navPage { get; set; }

		private List<Marker> myMarkers { get; set; }

		private void OnPagePopped (object s, NavigationEventArgs e)
		{
			return;
			if (e.Page == page) {
				var androidMapView2 = (MapView)Control;

				androidMapView2.Map.Clear ();
				androidMapView2.Map.Dispose ();

				androidMapView2 = null;
				this.Dispose (true);
				navPage.Popped -= OnPagePopped;

				page = null;
				navPage = null;



				GC.Collect (1);
			}
		}


		protected override void OnElementChanged (Xamarin.Forms.Platform.Android.ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);



			if (e.OldElement == null && e.NewElement != null) {
				//map created the first time
				page = GetContainingPage (e.NewElement);
				navPage = (page.Parent as myNavPage);
				navPage.Popped += OnPagePopped;
				//return;
			}

			

			var formsMap = (ExtendedMap)Element;
			var androidMapView = (MapView)Control;

			if (myMarkers == null)
				myMarkers = new List<Marker> ();

			if (androidMapView != null && androidMapView.Map != null) {
				androidMapView.Map.InfoWindowClick += MapOnInfoWindowClick;

			}

			//if (formsMap != null) {
			//	((ObservableCollection<Pin>)formsMap.Pins).CollectionChanged += OnCollectionChanged;
			//}
		}

		private Page GetContainingPage (Xamarin.Forms.Element element)
		{
			Element parentElement = element.ParentView;

			if (typeof(Page).IsAssignableFrom (parentElement.GetType ()))
				return (Page)parentElement;
			else
				return GetContainingPage (parentElement);
		}


	
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			if (e.PropertyName.Equals ("MapChanged")) {
				var formsMap = (ExtendedMap)Element;
				foreach (Marker m in this.myMarkers) {
					var i =	formsMap.Items.FirstOrDefault (x => x.Name == m.Title);
					if (i == null)
						m.Visible = false;
					else {
						m.Visible = true;
					}
				}
			}

			if (e.PropertyName.Equals ("VisibleRegion") && !_isDrawnDone) {
				//UpdatePins ();
				_isDrawnDone = true;
			}
			if (e.PropertyName.Equals ("MapCreated") && !_isDrawnDone) {
				UpdatePins ();
			}

		}

	
		/*
		void   OnCollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		//raised only if the original Pin collection is changed
		{
			if (e.Action.ToString ().ToLower () == "reset") {
				_isDrawnDone = false;
				foreach (Marker m in this.myMarkers) {
					m.Visible = false;
				}
				return;

			}
			if (e.Action.ToString ().ToLower () == "add") {
				
				if (_isDrawnDone == false) {
					var formsMap = (ExtendedMap)Element;
					foreach (Marker m in this.myMarkers) {
						var i =	formsMap.Items.FirstOrDefault (x => x.Name == m.Title);
						if (i == null)
							m.Visible = false;
						else {
							m.Visible = false;

						}
					}
					_isDrawnDone = true;
				}

			
			}
		}
		*/


	
		private void UpdatePins ()
		{
			var androidMapView = (MapView)Control;
			var formsMap = (ExtendedMap)Element;

			androidMapView.Map.Clear ();
			androidMapView.Map.MarkerClick += HandleMarkerClick;
			androidMapView.Map.MyLocationEnabled = formsMap.IsShowingUser;

			var items = formsMap.Items;



			foreach (var item in items) {
				var markerWithIcon = new MarkerOptions ();
				markerWithIcon.SetPosition (new LatLng (item.Location.Latitude, item.Location.Longitude));
				markerWithIcon.SetTitle (string.IsNullOrWhiteSpace (item.Name) ? "-" : item.Name);
				markerWithIcon.SetSnippet (item.Details);
			
				try {
					//markerWithIcon.InvokeIcon (BitmapDescriptorFactory.FromResource (GetPinIcon ()));
					//markerWithIcon.InvokeIcon (GetImage (testPath ()));
					markerWithIcon.InvokeIcon (GetImage (item.ImageUrl));
				} catch (Exception) {
					markerWithIcon.InvokeIcon (BitmapDescriptorFactory.DefaultMarker ());
				}

				Marker m = androidMapView.Map.AddMarker (markerWithIcon);
				this.myMarkers.Add (m);
		
			}
		}

		private string testPath ()
		{
			var path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			;
		
			string pathName = System.IO.Path.Combine (path, "C");
			pathName = System.IO.Path.Combine (pathName, "alberghi_AR_60.png");

			return pathName;
		}

		private BitmapDescriptor GetImage (string file)
		{
			
			try {

				if (!System.IO.File.Exists (file))
					return BitmapDescriptorFactory.DefaultMarker ();

				//Drawable drawable = Drawable.CreateFromPath (file);
				//return BitmapDescriptorFactory.FromBitmap ((drawable as BitmapDrawable).Bitmap);
				using (Android.Graphics.Bitmap bm = Android.Graphics.BitmapFactory.DecodeFile (file)) {
					
					using (Android.Graphics.Bitmap bm2 = Android.Graphics.Bitmap.CreateScaledBitmap (bm, 
						                                     (bm.Width / 2) * Convert.ToInt32 (App.k_Density), (bm.Height / 2) * Convert.ToInt32 (App.k_Density), false)) {
						BitmapDescriptor image = BitmapDescriptorFactory.FromBitmap (bm2);
						return image;
					}

				}
				//Android.Graphics.Bitmap bm = Android.Graphics.BitmapFactory.DecodeFile (file);
				//bm.Recycle ();
				//bm.Dispose ();//
				//bm = null;
				//bm = Android.Graphics.Bitmap.CreateScaledBitmap (bm, bm.Width * 1.5, bm.Height * 1.5, false);
				//BitmapDescriptor image = BitmapDescriptorFactory.FromBitmap (bm);



			} catch (Exception ex) {
			
				return BitmapDescriptorFactory.DefaultMarker ();
			}
		}

	


		private static int GetPinIcon ()
		{
			//BitmapDescriptorFactory.
			return Resource.Drawable.icon;
		}

		private void HandleMarkerClick (object sender, GoogleMap.MarkerClickEventArgs e)
		{
			var marker = e.Marker;
			marker.ShowInfoWindow ();

			var map = this.Element as ExtendedMap;

			var formsPin = new ExtendedPin (marker.Title, marker.Snippet, marker.Position.Latitude, marker.Position.Longitude);

			map.SelectedPin = formsPin;
		}

		private void MapOnInfoWindowClick (object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
			Marker clickedMarker = e.Marker;
			// Find the matchin item
			var formsMap = (ExtendedMap)Element;
			formsMap.ShowDetailCommand.Execute (formsMap.SelectedPin);
		}

		private bool IsItem (IMapModel item, Marker marker)
		{
			return item.Name == marker.Title &&
			item.Details == marker.Snippet &&
			item.Location.Latitude == marker.Position.Latitude &&
			item.Location.Longitude == marker.Position.Longitude;
		}

		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);

			//NOTIFY CHANGE

			if (changed) {
				_isDrawnDone = false;
			}
		}
	}
}


