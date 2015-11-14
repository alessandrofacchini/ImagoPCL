using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;

namespace testRestCompressed
{
	public class ImagoMap: Xamarin.Forms.Maps.Map
	{
		public ImagoMap (Xamarin.Forms.Maps.MapSpan m) : base (m)
		{

		}
			
	}

	public interface ILocation
	{
		double Latitude { get; set; }

		double Longitude { get; set; }
	}

	public interface IMapModel
	{
		string Name { get; set; }

		string Details { get; set; }

		ILocation Location { get; set; }

		string ImageUrl { get; set; }
	}

	public static class MapExtensions
	{
		public static IList<Pin> ToPins<T> (this IEnumerable<T> items) where T : IMapModel
		{
			return items.Select (i => i.AsPin ()).ToList ();
		}

		public static Pin AsPin (this IMapModel item)
		{
			var location = item.Location;
			var position = location != null ? new Position (location.Latitude, location.Longitude) : Location.DefaultPosition;
			var url = item.ImageUrl != null ? item.ImageUrl : "";
			return new Pin { Label = item.Name, Address = item.Details, Position = position  };
		}
	}

	public class Location : ILocation
	{
		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public static Position DefaultPosition {
			get { return new Position (34.033897, -118.291869); }
		}
	}

	public class ExtendedPin : IMapModel
	{
		public ExtendedPin ()
		{
			
		}

		public ExtendedPin (string name, string details, double latitude, double longitude)
		{
			Name = name;
			Details = details;
			Location = new Location { Latitude = latitude, Longitude = longitude };
		}

		public string Name { get; set; }

		public string Details { get; set; }

		public string ImageUrl { get; set; }

		public ILocation Location { get; set; }
	}


	public class ExtendedMap : Xamarin.Forms.Maps.Map
	{
		private readonly ObservableCollection<IMapModel> _items = new ObservableCollection<IMapModel> ();

		public ExtendedMap (MapSpan region) : base (region)
		{
			LastMoveToRegion = region;
		}

		public static readonly BindableProperty SelectedPinProperty = BindableProperty.Create<ExtendedMap, ExtendedPin> (x => x.SelectedPin, null);

		public ExtendedPin SelectedPin {
			get{ return (ExtendedPin)base.GetValue (SelectedPinProperty); }
			set{ base.SetValue (SelectedPinProperty, value); }
		}

		public ICommand ShowDetailCommand { get; set; }

		public Action<IMapModel> callBackItemSelected { get; set; }

		private MapSpan _visibleRegion;

		public MapSpan LastMoveToRegion { get; private set; }

		public new MapSpan VisibleRegion {
			get { return _visibleRegion; }
			set {
				if (_visibleRegion == value) {
					return;
				}
				if (value == null) {
					throw new ArgumentNullException ("value");
				}

				OnPropertyChanging ("VisibleRegion");
				_visibleRegion = value;
				OnPropertyChanged ("VisibleRegion");
			}
		}

		private Boolean _mapChanged;

		public Boolean MapChanged { 
			get { return _mapChanged; } 
			set {
				OnPropertyChanged ("MapChanged");
				/*
				if (value != _mapChanged) {
					_mapChanged = value;
					OnPropertyChanged ("MapChanged");

				}*/
			} 
		}

		private Boolean _mapCreated;

		public Boolean MapCreated { 
			get { return _mapCreated; } 
			set {
				OnPropertyChanged ("MapCreated");
				/*
				if (value != _mapChanged) {
					_mapChanged = value;
					OnPropertyChanged ("MapChanged");

				}*/
			} 
		}

		public ObservableCollection<IMapModel> Items {
			get { return _items; }
		}


		public void CreateMap ()
		{
			this.MapCreated = true;
		}


		public void UpdatePins (IEnumerable<IMapModel> items)
		{
			Items.Clear ();
			//Pins.Clear ();

			foreach (var item in items) {
				Items.Add (item);
			}
			this.MapChanged = true;
			//foreach (var item in items) {
			//Pins.Add (item.AsPin ());
			//}
		}
	}
}

