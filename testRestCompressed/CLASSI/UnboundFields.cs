using System;

namespace testRestCompressed
{
	public partial class POIs
	{
		private double _Distance;

		public double Distance { 
			get { return _Distance; } 
			set {
				if (value != _Distance) {
					_Distance = value;
					OnPropertyChanged ("Distance");
				}
			} 
		}

		private string _DistanceLabel;

		public string DistanceLabel { 
			get { return _DistanceLabel; } 
			set {
				if (value != _DistanceLabel) {
					_DistanceLabel = value;
					OnPropertyChanged ("DistanceLabel");
				}
			} 
		}



		private String _isEvents;

		public String IsEvents { 
			get { return _isEvents; } 
			set {
				if (this.IDPOI == 96) {
					Int32 www = 1;
				}
				if (value != _isEvents) {
					_isEvents = value;
					OnPropertyChanged ("IsEvents");
				}
			} 
		}

		private String _canSleep;

		public String CanSleep { 
			get { return _canSleep; } 
			set {
				if (value != _canSleep) {
					_canSleep = value;
					OnPropertyChanged ("CanSleep");
				}
			} 
		}

		private double _DistanceFromPoi;

		public double DistanceFromPoi { 
			get { return _DistanceFromPoi; } 
			set {
				if (value != _DistanceFromPoi) {
					_DistanceFromPoi = value;
					OnPropertyChanged ("DistanceFromPoi");
				}
			} 
		}

	}


	public partial class Categories
	{
		private bool _isEvents;

		public bool IsEvents { 
			get { return _isEvents; } 
			set {
				if (value != _isEvents) {
					_isEvents = value;
					OnPropertyChanged ("IsEvents");
				}
			} 
		}

		private string _mapImageFullPath;

		public string MapImageFullPath { 
			get { return _mapImageFullPath; } 
			set {
				if (value != _mapImageFullPath) {
					_mapImageFullPath = value;
					OnPropertyChanged ("MapImageFullPath");
				}
			} 
		}

	}

	public partial class MacroCategories
	{
		private bool _isEvents;

		public bool IsEvents { 
			get { return _isEvents; } 
			set {
				if (value != _isEvents) {
					_isEvents = value;
					OnPropertyChanged ("IsEvents");
				}
			} 
		}
	}


}

