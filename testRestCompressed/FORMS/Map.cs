using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Collections.Generic;
using System.Diagnostics;
using xUtilityPCL;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace testRestCompressed
{
	public class Map : ContentPage
	{
		Boolean iscollapsed { get; set; }

		AbsoluteLayout mainPanel { get; set; }

		StackLayout leftPanel { get; set; }

		AbsoluteLayout rightPanel { get; set; }

		Image img { get; set; }

		Double rightPanelWDip { get; set; }

		//https://github.com/raechten/BindableMapTest/blob/master/Android/Renderers/ExtendedMapRenderer.cs
	
		POIs drCurrent { get; set; }

		List<POIs> mapPOIs { get; set; }



		List<Categories> mapCategories { get; set; }

		List<ExtendedPin> mypins { get; set; }

		public Int32 idCategory { get; set; }

		public ExtendedMap map { get; set; }

		ListView l{ get; set; }

		public Boolean isGeneralMap { get; set; }

		public Boolean isFirstLoading { get; set; }

		public Map (POIs selectedPOI, Int32 _idCategory)
		{
			this.isGeneralMap = false;
			this.drCurrent = selectedPOI;
			this.idCategory = _idCategory;
			this.mapPOIs = new List<POIs> ();
			this.isFirstLoading = true;


			var currentPlatform = DependencyService.Get<platformSpecific> ();
			foreach (POIs p in Global.K_POIs.Where(x=>x.IDLanguage==Global.currentLanguage).ToList()) {
				Double myDistance = currentPlatform.GetDistance (Convert.ToDouble (drCurrent.Latitude), Convert.ToDouble (drCurrent.Longitude), 
					                    Convert.ToDouble (p.Latitude), Convert.ToDouble (p.Longitude));
				if (myDistance <= Global.MaxDistanceMeters) {
					this.mapPOIs.Add (p);
				}
			}
			myLoad ();
		}

	

		public Map ()
		{
			//general map
			this.mapPOIs = new List<POIs> ();
			this.isGeneralMap = true;
			var currentPlatform = DependencyService.Get<platformSpecific> ();
			foreach (POIs p in Global.K_POIs.Where(x=>x.IDLanguage==Global.currentLanguage).ToList()) {
				Double myDistance = currentPlatform.GetDistance (Convert.ToDouble (Global.currentPosition.Latitude), 
					                    Convert.ToDouble (Global.currentPosition.Longitude), 
					                    Convert.ToDouble (p.Latitude), Convert.ToDouble (p.Longitude));
			
				/*
				if (Device.OS == TargetPlatform.iOS) {
					Global.currentPosition = new Position (45.88, 13.31);
					currentPlatform.userlatitude = 45.88;
					currentPlatform.userlongitude = 13.31;
				}
				*/
			
				if (myDistance <= Global.MaxDistanceMeters) {
					this.mapPOIs.Add (p);
				}
			}
			myLoad ();
			this.isFirstLoading = true;
		}

		private void myLoad ()
		{
			


			this.rightPanelWDip = App.k_screenW * .11;

			//var span = MapSpan.FromCenterAndRadius (new Position (44.7201029, 11.1439078), Distance.FromMiles (0.4));
			MapSpan span = null;
			if (this.isGeneralMap == false) {
				span = MapSpan.FromCenterAndRadius (new Position (Convert.ToDouble (drCurrent.Latitude.Value), Convert.ToDouble (drCurrent.Longitude.Value)), 
					Distance.FromMiles (3.2));
				//0.4  3.2
			} else {
				span = MapSpan.FromCenterAndRadius (new Position (Convert.ToDouble (Global.currentPosition.Latitude), 
					Convert.ToDouble (Global.currentPosition.Longitude)), 
					Distance.FromMiles (3.2));
			}
			map = new ExtendedMap (span) {
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			map.IsShowingUser = true;

			/*
			Pin item = new Pin ();
			item.Position = new Position (44.73, 11.15);

			item.Clicked += delegate(object sender, EventArgs e) {
				Debug.WriteLine ("..");	
			};
			item.Type = PinType.Place;

			item.Label = "some place";
			map.Pins.Add (item);// = items;
			*/

			/*
			List<ExtendedPin> mypins = new List<ExtendedPin> ();
			mypins.Add (new ExtendedPin () {
				Name = "Baboon",
				Location = new Location { Latitude = 44.73, Longitude = 11.15 },
				Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
				ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
			});

			mypins.Add (new ExtendedPin () {
				Name = "Capuchin Monkey",
				Location = new Location { Latitude = 44.73, Longitude = 11.25 },
				Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
				ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
			});

			map.UpdatePins (mypins);
			*/
			mypins = new List<ExtendedPin> ();
			this.mapCategories = new List<Categories> ();
			Categories closeMap = new Categories () {
				IDCategory = 0,
				IDMacroCategory = 0,
				MapImageFullPath = "testRestCompressed.Resources.reset_mappa_60.png"
			}; 
			this.mapCategories.Add (closeMap);

			if (this.isGeneralMap) {
				bindMap (0, null);
			} else {
				bindMap (0, null);

			}
			/*
			foreach (POIs myPoi in this.mapPOIs) {
				var currentPlatform = DependencyService.Get<platformSpecific> ();
				var path = Path.Combine (currentPlatform.getLocalDatabasePath (), "C");
				//Categories c = Global.K_Categories.First (x => x.IDCategory == this.idCategory);
				List<Categories_POIs> cp = Global.K_Categories_POIs.Where (x => x.IDPOI == myPoi.IDPOI).ToList ();
				Categories c = Global.K_Categories.First (x => x.IDCategory == cp [0].IDCategory); //take the first category
				path = Path.Combine (path, c.Icon3);
				c.MapImageFullPath = path;
				if (this.mapCategories.Contains (c) == false)
					this.mapCategories.Add (c);
				
				mypins.Add (new ExtendedPin () {
					Name = myPoi.NameOfThePOI,
					Location = new Location () {
						Latitude = Convert.ToDouble (myPoi.Latitude.Value),
						Longitude = Convert.ToDouble (myPoi.Longitude.Value)
					},
					Details = "",
					ImageUrl = path
				});
			}
			map.UpdatePins (mypins);
*/

			map.ShowDetailCommand = new Command (m => ShowDetail ((IMapModel)m));

			if (Device.OS == TargetPlatform.iOS) {

				map.callBackItemSelected += delegate(IMapModel obj) {
					ShowDetail (obj);

				};
			}

			//

			var cell = new DataTemplate (typeof(MapBarCell));

			//cell.SetBinding (TextCell.TextProperty, "Name");
			//cell.SetBinding (TextCell.DetailProperty, new Binding ("Position", stringFormat: "{0}"));
			//cell.SetBinding (ImageCell.ImageSourceProperty, "MapImageFullPath");


			l = new ListView {
				ItemsSource = this.mapCategories.OrderBy (x => x.IDMacroCategory),
				ItemTemplate = cell,
				RowHeight = 45,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				BackgroundColor = Color.Black,
				SeparatorVisibility = SeparatorVisibility.None,
			};
			l.ItemTapped += async delegate(object sender, ItemTappedEventArgs e) {
				l.SelectedItem = null;
				Int32 www = 0;
				Int32 catID = (e.Item as Categories).IDCategory;
				if (catID == 0) {
					if (this.isGeneralMap)
						catID = 0;
					else
						catID = -1;
				}
				bindMap (catID, this.drCurrent);
				Debug.WriteLine ("Catid: " + catID);
			};




			leftPanel = new StackLayout () {
				//BackgroundColor = Color.Red,
				HeightRequest = App.k_screenHMinusNavigationBarBottomBar,
				WidthRequest = Device.OnPlatform (App.k_screenW /*- ((App.k_screenW / 100) * 11)*/, App.k_screenW, App.k_screenW),
				Children = { map }
			};



			img = new Image () {
				Source = ImageSource.FromResource ("testRestCompressed.Resources.barraMappa.png"),
				//WidthRequest = 20,
				//HeightRequest = 20
				//BackgroundColor = Color.Yellow,
			};

			var mytap = new TapGestureRecognizer ();
			img.GestureRecognizers.Add (mytap);

			mytap.Tapped += async delegate {
				if (!iscollapsed) {
					if (Device.OS == TargetPlatform.Android) {
						await rightPanel.TranslateTo (this.rightPanelWDip, 0, 250, Easing.Linear);
						iscollapsed = true;
					}
					if (Device.OS == TargetPlatform.iOS) {
						rightPanel.TranslateTo (this.rightPanelWDip, 0, 250, Easing.Linear);
						img.TranslateTo (this.rightPanelWDip, 0, 250, Easing.Linear);
						iscollapsed = true;
					}
				} else {
					if (Device.OS == TargetPlatform.Android) {
						await rightPanel.TranslateTo (0, 0, 250, Easing.Linear);
						iscollapsed = false;
					}
					if (Device.OS == TargetPlatform.iOS) {
						rightPanel.TranslateTo (0, 0, 250, Easing.Linear);
						img.TranslateTo (0, 0, 250, Easing.Linear);
						iscollapsed = false;

					}
						
				}
			};


			/*
			ListView l = new ListView ();
			l.ItemsSource = new string[] {"uno", "due", "uno", "due", "uno", "due", "uno", "due", "uno", "due", "uno", "due", "uno", "due", "uno", "due", "uno", "due", "uno"
				, "due"
			};
			l.BackgroundColor = Color.Yellow;
			*/

			rightPanel = new AbsoluteLayout () {
				//BackgroundColor = Color.Red,
//				WidthRequest = App.k_screenW,
				WidthRequest = Device.OnPlatform ((App.k_screenW / 100) * 11, App.k_screenW, App.k_screenW),
				HeightRequest = App.k_screenHMinusNavigationBarBottomBar,
//				Children =  { img, l }
			};
			if (Device.OS == TargetPlatform.Android) {
				rightPanel.Children.Add (img);
				rightPanel.Children.Add (l);
			}

			if (Device.OS == TargetPlatform.iOS) {
				rightPanel.Children.Add (l, new Point (0, 0));
				AbsoluteLayout.SetLayoutFlags (l, AbsoluteLayoutFlags.All);
				AbsoluteLayout.SetLayoutBounds (l, new Rectangle (0, 0, 1.0, 1.0));
			}

			if (Device.OS == TargetPlatform.Android) {
				AbsoluteLayout.SetLayoutFlags (l, AbsoluteLayoutFlags.All);
				AbsoluteLayout.SetLayoutBounds (l, new Rectangle (1.0, 0, 0.11, 1.0));

				AbsoluteLayout.SetLayoutFlags (img, AbsoluteLayoutFlags.All);
				AbsoluteLayout.SetLayoutBounds (img, new Rectangle (0.879, 0.1, 0.1, 0.1));
			}

			//0.879 = .89-(.1-0.089) ....0.089 is 89% of 0.1 hat is the image width
			this.mainPanel = new AbsoluteLayout ();
			mainPanel.Children.Add (leftPanel, new Point (0, 0));
			if (Device.OS == TargetPlatform.Android) {
				mainPanel.Children.Add (rightPanel, new Point (0, 0));
			}
			if (Device.OS == TargetPlatform.iOS) {
				mainPanel.Children.Add (rightPanel);

				AbsoluteLayout.SetLayoutFlags (rightPanel, AbsoluteLayoutFlags.All);
				AbsoluteLayout.SetLayoutBounds (rightPanel, new Rectangle (1.0, 0, 0.11, 1.0));
			
				mainPanel.Children.Add (img);
				AbsoluteLayout.SetLayoutFlags (img, AbsoluteLayoutFlags.All);
				AbsoluteLayout.SetLayoutBounds (img, new Rectangle (0.879, 0.1, 0.1, 0.1));

			}
			if (Device.OS == TargetPlatform.Android) {
				Content = this.mainPanel;
			}
			if (Device.OS == TargetPlatform.iOS) {
				/*
				rightPanel.BackgroundColor = Color.Transparent;
				this.mainPanel.RaiseChild (leftPanel);
				this.leftPanel.WidthRequest -= 70;
				*/
				//this.mainPanel.RaiseChild (leftPanel);
				rightPanel.BackgroundColor = Color.Red;
				Content = this.mainPanel;
				img.IsVisible = false;
			}
			this.leftPanel.IsVisible = false;
			this.rightPanel.IsVisible = false;
		}


		private async Task ShowDetail (IMapModel selectedItem)
		{
			POIs p = Global.K_POIs.First (x => x.NameOfThePOI == selectedItem.Name);
			List<Categories_POIs> cp = Global.K_Categories_POIs.Where (x => x.IDPOI == p.IDPOI).ToList ();
			Categories c = Global.K_Categories.First (x => x.IDCategory == cp [0].IDCategory); //take the first category
			Int32 kkkk;
			if (Int32.TryParse (c.NameOfTheCategory.Substring (0, 2), out kkkk))
				c.NameOfTheCategory = c.NameOfTheCategory.Substring (3).ToUpper ();
			else
				c.NameOfTheCategory = c.NameOfTheCategory.ToUpper ();
			
			PosDetail d = new PosDetail (p, c.NameOfTheCategory, c.IDCategory);
			await Navigation.PushAsync (d, true);


			Debug.WriteLine ("...");
		}


		protected override async  void OnAppearing ()
		{
			base.OnAppearing ();

		
			if (this.isFirstLoading) {
				if (this.isGeneralMap == false) {
					this.map.CreateMap ();
					bindMap (-1, drCurrent);
				} else {
					this.map.CreateMap ();
				}
				this.isFirstLoading = false;
			}


			this.leftPanel.IsVisible = true;
			await Task.Delay (1000);
			this.rightPanel.IsVisible = true;
			if (Device.OS == TargetPlatform.iOS) {
				img.IsVisible = true;
			}

			if (Device.OS == TargetPlatform.Android) {
				Device.StartTimer (TimeSpan.FromSeconds (8), () => { 
					rightPanel.TranslateTo (this.rightPanelWDip, 0, 250, Easing.Linear);
					iscollapsed = true;
					return false;
				}); 
			}
			if (Device.OS == TargetPlatform.iOS) {
				Device.StartTimer (TimeSpan.FromSeconds (8), () => { 
					rightPanel.TranslateTo (this.rightPanelWDip, 0, 250, Easing.Linear);
					img.TranslateTo (this.rightPanelWDip, 0, 250, Easing.Linear);
					iscollapsed = true;
					return false;
				}); 
			}

		}


		protected override void OnDisappearing ()
		{
			
			base.OnDisappearing ();
			//this.map = null;
			GC.Collect (1);
			return;
			this.mapPOIs.Clear ();
			this.mapPOIs = null;
			this.mapCategories.Clear ();
			this.mapCategories = null;
			this.mypins.Clear ();
			this.mypins = null;
			this.l.ItemsSource = null;
			this.l = null;
			map.Pins.Clear ();
			map = null;


		}


		private void bindMap (Int32 catID, POIs drCurrent)
		{
			mypins.Clear ();
			//add current poi
			foreach (POIs myPoi in this.mapPOIs) {
				var currentPlatform = DependencyService.Get<platformSpecific> ();
				var path = Path.Combine (currentPlatform.getLocalDatabasePath (), "C");
				//Categories c = Global.K_Categories.First (x => x.IDCategory == this.idCategory);
				List<Categories_POIs> cp = Global.K_Categories_POIs.Where (x => x.IDPOI == myPoi.IDPOI).ToList ();
				Categories c = Global.K_Categories.First (x => x.IDCategory == cp [0].IDCategory); //take the first category
				path = Path.Combine (path, c.Icon3);
				c.MapImageFullPath = path;
				if (this.mapCategories.Contains (c) == false)
					this.mapCategories.Add (c);
				Boolean lAdd = false;
				if (catID == 0)
					lAdd = true;
				else {
					if (c.IDCategory != catID)
						lAdd = false;
					else
						lAdd = true;
				}
				if (drCurrent != null && myPoi.IDPOI == drCurrent.IDPOI) {
					lAdd = true;
					//always add current poi apart from general map
				}
				if (lAdd == false)
					continue;	
				mypins.Add (new ExtendedPin () {
					Name = myPoi.NameOfThePOI,
					Location = new Location () {
						Latitude = Convert.ToDouble (myPoi.Latitude.Value),
						Longitude = Convert.ToDouble (myPoi.Longitude.Value)
					},
					Details = "",
					ImageUrl = path
				});
			}
			map.UpdatePins (mypins);
		}


	}
}


