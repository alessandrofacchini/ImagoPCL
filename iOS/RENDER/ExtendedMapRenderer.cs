using System;
using Xamarin.Forms;
using testRestCompressed;
using testRestCompressed.iOS;
using Xamarin.Forms.Maps.iOS;
using System.Collections.Generic;
using MapKit;
using Xamarin.Forms.Maps;
using CoreLocation;
using UIKit;
using System.Linq;
using System.Drawing;
using CoreGraphics;


[assembly: ExportRenderer (typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace testRestCompressed.iOS
{
	public class ExtendedMapRenderer :  MapRenderer
	{
		public ExtendedMapRenderer ()
		{
		}


		mapDelegate mapDel;

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var UISMapView = (MKMapView)Control;
			var formsMap = (ExtendedMap)sender;
			var formsPins = formsMap.Items;
			UISMapView.ShowsUserLocation = true;
			UISMapView.UserInteractionEnabled = true;
			UISMapView.ZoomEnabled = true;
			UISMapView.ScrollEnabled = true;
			UISMapView.PitchEnabled = true;


			mapDel = new mapDelegate ();

			UISMapView.Delegate = mapDel;


			if (e.PropertyName.Equals ("MapChanged")) {
				foreach (IMKAnnotation  mtmp in UISMapView.Annotations) {
					if (mtmp is mapAnotation) {
						var m = mtmp as mapAnotation;
						var i =	formsMap.Items.FirstOrDefault (x => x.Name == m.Title);
						var viewForAnnotation = UISMapView.ViewForAnnotation (m);
						if (viewForAnnotation != null) {
							if (i == null) {
								viewForAnnotation.Hidden = true;
							} else {
								viewForAnnotation.Hidden = false;
							}
						}
					}

				}
			}


			if (e.PropertyName.Equals ("MapCreated")) {
				int index = 0;
				foreach (var c in formsPins) {
					mapAnotation pin = new  mapAnotation (c.Name, 
						                   index,
						                   new CLLocationCoordinate2D (c.Location.Latitude, c.Location.Longitude), c.ImageUrl);
					UISMapView.AddAnnotation (pin);
					index++;
				}
			}


			//UISMapView.DidUpdateUserLocation += delegate(object senders, MKUserLocationEventArgs eUser) {
			//eUser.UserLocation.Location.Coordinate.Latitude
			//};

			mapDel.callNackPin += delegate(mapAnotation obj) {
				var p = formsMap.Items [obj.pos];

				formsMap.callBackItemSelected (p);
			};

		}

		protected override void OnElementChanged (Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);
		}

		private  class mapDelegate : MKMapViewDelegate
		{
			/// <Docs>To be added.</Docs>
			/// <param name="control">To be added.</param>
			/// <remarks>To be added.</remarks>
			/// <summary>
			/// Callouts the accessory control tapped.
			/// </summary>
			/// <param name="mapView">Map view.</param>
			/// <param name="view">View.</param>
			public override void CalloutAccessoryControlTapped (MKMapView mapView,
			                                                    MKAnnotationView view, UIControl control)
			{
				Console.WriteLine ("click!!!");
				var posSelected = view.Annotation as mapAnotation;
				if (callNackPin != null)
					callNackPin (posSelected);
			}


			public override MKAnnotationView GetViewForAnnotation
			(MKMapView mapView, IMKAnnotation annotation)
			{
				MKPinAnnotationView anView;

				if (annotation is MKUserLocation)
					return null;
				if (annotation is mapAnotation) {


					anView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation (Pid);

					if (anView == null)
						anView = new MKPinAnnotationView (annotation, Pid);

					//((MKPinAnnotationView)anView).PinColor = MKPinAnnotationColor.Green;

					anView.Image = new UIImage ((annotation as mapAnotation).imageUrl);

					anView.Frame = new RectangleF ((float)anView.Frame.X, (float)anView.Frame.Y, 30, 30);
					anView.SizeToFit ();


					anView.CanShowCallout = true;
					anView.Draggable = true;

					var btn = UIButton.FromType (UIButtonType.DetailDisclosure);
					//btn.SetTitle (">", UIControlState.Normal);
					//btn.Frame = new RectangleF ((float)anView.Frame.Width - 30, ((float)anView.Frame.Height / 2) - 15, 30, 30);
					//btn.SetTitleColor (UIColor.Red, UIControlState.Normal);
					//btn.BackgroundColor = UIColor.Yellow;
					anView.RightCalloutAccessoryView = btn;
					//anView.acce .AddSubview (btn);

					//anView.RightCalloutAccessoryView = (annotation as mapAnotation).btnSelect;

					return anView;
				} else
					return null;

			}

			string Pid = "PinAnotation";

			public Action <mapAnotation> callNackPin{ get; set; }

			public override void DidSelectAnnotationView (MKMapView mapView, MKAnnotationView view)
			{
				Console.WriteLine ("...");

				//var posSelected = view.Annotation as mapAnotation;

				//callNackPin (posSelected);


			}
		}

		private class mapAnotation:MKAnnotation
		{
			public string title { get; set; }

			public CLLocationCoordinate2D coord { get; set; }

			public UIButton btn { get; set; }

			public string imageUrl { get; set; }

			public int pos { get; set; }

			public UIButton btnSelect
			{ get { return btn; } }

			public mapAnotation (string _title, int _pos, CLLocationCoordinate2D _coord, string _imageUrl)
			{
				title = _title;
				coord = _coord;
				pos = _pos;
				imageUrl = _imageUrl;

				btn = UIButton.FromType (UIButtonType.DetailDisclosure);
			}

			public override CLLocationCoordinate2D Coordinate {
				get {
					return coord;
				}

			}

			public override string Title {
				get {
					return title;
				}

			}

		}
	}
}

