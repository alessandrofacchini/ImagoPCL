using System;

using Xamarin.Forms;
using AnagrafeCaninaMobilePCL;
using System.ComponentModel;
using System.Collections.Generic;
using xUtilityPCL;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace testRestCompressed
{
	
	public class PosList : ContentPage
	{
		public Int32 idCategory { get; set; }

		public Int32 idPOI { get; set; }

		public string title { get; set; }


		public ListView myListView { get; set; }

		public PoisWrapperItemsSourceListView myWrapper { get; set; }

		public Boolean  poisAroundMe { get; set; }

		public PosList (string _title, Int32 _idCategory, Int32 _idPOI, Boolean  _poisAroundMe)
		{
			//if (Device.OS == TargetPlatform.iOS)
//				this.Title = _title;


			this.poisAroundMe = _poisAroundMe;
			this.title = _title;
			this.idCategory = _idCategory;
			this.idPOI = _idPOI;

			//list (start)
			myListView = new ListView ();
			myListView.ItemTemplate = new DataTemplate (typeof(PoiListCell));
			myListView.RowHeight = 158;
			myListView.HasUnevenRows = false;
			myListView.SeparatorVisibility = SeparatorVisibility.None;

			myListView.ItemTapped += async delegate(object sender, ItemTappedEventArgs e) {
				myListView.SelectedItem = null;
				PosDetail d = new PosDetail (e.Item as POIs, this.title, this.idCategory);
				if (Device.OS == TargetPlatform.iOS)
					NavigationPage.SetHasBackButton (d, false);
				await Navigation.PushAsync (d, true);
				myListView.SelectedItem = null;
			};
			/*
			myListView.ItemSelected += async delegate(object sender, SelectedItemChangedEventArgs e) {
				if (e.SelectedItem == null)
					return;
				PosDetail d = new PosDetail ();
				d.BindingContext = e.SelectedItem;
				Navigation.PushAsync (d);
			};
			*/
		


			Binding myBinding = new Binding ();
			this.myWrapper = new PoisWrapperItemsSourceListView ();
			this.myWrapper.CreateList (this.idCategory, this.idPOI);
			myBinding.Source = this.myWrapper;

			myBinding.Path = "myList";

			myListView.SetBinding (ListView.ItemsSourceProperty, myBinding); 

			//myListView.ItemsSource = new ObservableCollection<POIs> (this.myWrapper.myList); //this.myWrapper.myList;
			//list (end)




			Content = new StackLayout { 
				Children = {
					myListView,

					//new Label { Text = "Hello ContentPage" },
					//new  imagoLabelRender { Text = "Hello ContentPage", FontName = "FuturaStd-Medium.ttf" },
					//new  xUtilityPCL.CustomLabel { Text = "Hello ContentPage", FontName = "FuturaStd-Medium.ttf" }//, FontSize = 11.5 },
				}
			};
			myListView.BackgroundColor = Color.White;
			//20150628 inizio
			if (Device.OS == TargetPlatform.Android) {
				if (Global.LocationUpdatePending) {
					Global.calculateDistance (); 
					Global.LocationUpdatePending = false;
				}
			}
			//20150628 fine
		}

		protected override bool OnBackButtonPressed ()
		{
			return base.OnBackButtonPressed ();
		}

	
		protected override  async void OnAppearing ()
		{
			base.OnAppearing ();

		}


		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			GC.Collect (1);
		}
	}


	public class PoisWrapperItemsSourceListView : AnagrafeCaninaMobilePCL.INotifyPropertyChanged
	{

		public PoisWrapperItemsSourceListView ()
		{
			//this.dtFilteredGrouped = new ObservableCollection<MyObservableCollection<BaseModel>> ();
			_myList = new List<POIs> ();
		}


		public void CreateList (Int32 idCategory, Int32 idPOI)
		{
			_myList.Clear ();
			if (idCategory != 0) {
				//var tq1 = sqliteHelper.sqlite_ReadFromExistingLocalTableSYNC<Categories_POIs> (Global.databaseName);
				var l1 = Global.K_Categories_POIs.Where (x => x.IDCategory == idCategory).Select (z => z.IDPOI).ToList ();

				//var tq2 = sqliteHelper.sqlite_ReadFromExistingLocalTableSYNC<POIs> (Global.databaseName);
				var l2 = Global.K_POIs.Where (x => l1.Contains (x.IDPOI)).OrderBy (x => x.Distance);
				//var l3 = l2.ToList ();
				var currentPlatform = DependencyService.Get<platformSpecific> ();
				/*
			foreach (POIs p in l2) {
				p.Distance = currentPlatform.GetDistance (Global.currentPosition.Latitude, Global.currentPosition.Longitude, 
					Convert.ToDouble (p.Latitude), Convert.ToDouble (p.Longitude));
				p.Distance = p.Distance / 1000;
				p.DistanceLabel = p.Distance.ToString ("###0.##") + " Km";
			}
			*/
				Categories c = Global.K_Categories.First (x => x.IDCategory == idCategory);
				if (c.IsEvents == false) {
					_myList = new List<POIs> (l2.OrderBy (x => x.Distance));
				} else {
					_myList = new List<POIs> (l2.OrderBy (x => x.OpeningDate).ThenBy (x => x.Distance)); //20150710
				}

			} else {
				List<POIs> lTemp = new List<POIs> ();
				POIs drCurrent = Global.K_POIs.First (x => x.IDPOI == idPOI);
				var currentPlatform = DependencyService.Get<platformSpecific> ();
				foreach (POIs p in Global.K_POIs.Where(x=>x.IDLanguage==Global.currentLanguage).OrderBy (x => x.Distance).ToList()) {
					Double myDistance = currentPlatform.GetDistance (Convert.ToDouble (drCurrent.Latitude), Convert.ToDouble (drCurrent.Longitude), 
						                    Convert.ToDouble (p.Latitude), Convert.ToDouble (p.Longitude));
					
					if (myDistance <= Global.MaxDistanceMeters) {
						p.DistanceFromPoi = myDistance;
						lTemp.Add (p);
					}
					_myList = new List<POIs> (lTemp.OrderBy (x => x.DistanceFromPoi));
				}
			}





			//POIs p1 = new POIs (){ IDPOI = 1, NameOfThePOI = "one",  Icon = "Museo_Civico_Gemona_150.jpg"  };
			//POIs p2 = new POIs (){ IDPOI = 2, NameOfThePOI = "two", Icon = "Agriturismo AlButtasella150.jpg" };
			//_myList.Add (p1);
			//_myList.Add (p2);
			/////
		}



		public event PropertyChangedEventHandler PropertyChanged;

		List<POIs> _myList;

		public List<POIs> myList {
			get{ return _myList; }
			set { 
				_myList = value;
				if (PropertyChanged != null) {
					PropertyChanged (this, 
						new PropertyChangedEventArgs ("myList"));// Throw!!
				}
			}
		}

	

	

	}



}


