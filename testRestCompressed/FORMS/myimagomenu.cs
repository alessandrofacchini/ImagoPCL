using System;
using Xamarin.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using xUtilityPCL;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Globalization;


namespace testRestCompressed
{
	public class myimagomenu : ContentPage
	{
		StackLayout b1;
		StackLayout b2;
		StackLayout b3;

		Button btn1;
		Button btn2;
		Button btn3;

		Double b1H;
		Double b2H;
		Double b3H;

		AbsoluteLayout al;



		Acr.XamForms.UserDialogs.IUserDialogService hud;
		xUtilityPCL.platformSpecific currentPlataform;

		enum MenuKindEnum
		{
			Level1Macro,
			Level2CatFather,
			Level3CatChild}

		;

		public myimagomenu ()
		{
			//http://facchini-ts.it/imagobe/DATABASE/imagolight.txt
			//HttpWebRequest a;
			//HttpClient b = new HttpClient ();
			currentPlataform = DependencyService.Get<platformSpecific> ();

			b1 = new StackLayout {
				BackgroundColor = Color.FromRgb (40, 40, 40),
				//VerticalOptions = LayoutOptions.Center

			};
			b2 = new StackLayout {

				BackgroundColor = Color.FromRgb (60, 60, 60),
			};
			b3 = new StackLayout {

				BackgroundColor = Color.FromRgb (80, 80, 80),
			};

		


			btn1 = new Button (){ Text = "btn1" };
			//b1.Children.Add (btn1);

			btn2 = new Button (){ Text = "btn2" };
			//b2.Children.Add (btn2);


			#region "open"
			btn1.Clicked += async delegate(object sender, EventArgs e) {
				b2.IsVisible = true;
				b3.IsVisible = false;
				await b2.TranslateTo (0, -this.b1H);
			};
			btn2.Clicked += async delegate(object sender, EventArgs e) {
				b1.IsVisible = false;
				b2.IsVisible = true;
				b3.IsVisible = true;
				await b2.TranslateTo (0, 0);
				await b3.TranslateTo (0, -this.b2H);
			};

			#endregion

			#region "close"
			var mygest = new TapGestureRecognizer ();
			mygest.Tapped += async (object sender, EventArgs e) => {
				//b2 b1
				await b2.TranslateTo (0, 0);
				b2.IsVisible = false;
			};
			b1.GestureRecognizers.Add (mygest);


			var mygest2 = new TapGestureRecognizer ();
			mygest2.Tapped += async (object sender, EventArgs e) => {
				if (b1.IsVisible) {
					//b2,b1
					await b2.TranslateTo (0, 0);
				} else {
					//b3 b2
					b3.IsVisible = false;
					b1.IsVisible = true;
					await b2.TranslateTo (0, -this.b1H);
				}
			};
			b2.GestureRecognizers.Add (mygest2);

			var mygest3 = new TapGestureRecognizer ();
			mygest3.Tapped += async (object sender, EventArgs e) => {
				//b3 b2
				b3.IsVisible = false;
				b1.IsVisible = true;
				await b2.TranslateTo (0, -this.b1H);
			};
			b3.GestureRecognizers.Add (mygest3);



			#endregion

			al = new AbsoluteLayout ();

			Content = al;

			this.SizeChanged += async delegate(object sender, EventArgs e) {

				var hMenu = this.Height;
				hMenu += 1;
				//568
				var half = Convert.ToInt32 (hMenu / 2);
				var delta = hMenu - (half * 2);
				b1H = half;
				//b3H += delta;
				b2H = half - 100;
				b3H = half - 200;

				/*
				b1H = (hMenu / 2);
				b2H = (hMenu / 2) - 100;
				b3H = (hMenu / 2) - 200;
*/

				al.Children.Add (b3, new Point (0, hMenu - b3H)); 
				al.Children.Add (b2, new Point (0, hMenu - b2H)); 
				al.Children.Add (b1, new Point (0, hMenu - b1H)); 


				b1.HeightRequest = b1H;
				b2.HeightRequest = b2H;
				b3.HeightRequest = b3H;
			


				/*
				myba.Children.Add (b3, new Point (0, (hMenu / 2))); 
				myba.Children.Add (b2, new Point (0, (hMenu / 2))); 
				myba.Children.Add (b1, new Point (0, (hMenu / 2))); 

				b1.HeightRequest = (hMenu / 2);
				b2.HeightRequest = (hMenu / 2);
				b3.HeightRequest = hMenu / 2;
				b1H = (hMenu / 2) ;
				b2H = (hMenu / 2) ;
				b3H = hMenu / 2;
				*/
				b1.WidthRequest = this.Width;
				b2.WidthRequest = this.Width;
				b3.WidthRequest = this.Width;
			};


		}


		public void AlignSpringboardLabels ()
		{
			if (b1.IsVisible) {
				Grid b1_grid = this.b1.Children [0] as Grid;

				foreach (View v in b1_grid.Children) {
					SpringboardButton b = v as SpringboardButton;
					b.ManageFocus ();
					/*
				b.SpringboardLabel.XAlign = TextAlignment.Center;
				b.HorizontalOptions = LayoutOptions.Center;
				b.SpringboardLabel.BackgroundColor = Color.Yellow;
				*/
				}
			}
			if (b2.IsVisible) {
				Grid b2_grid = this.b2.Children [0] as Grid;

				foreach (View v in b2_grid.Children) {
					SpringboardButton b = v as SpringboardButton;
					b.ManageFocus ();
				}
			}
			if (b3.IsVisible) {
				Grid b3_grid = this.b3.Children [0] as Grid;

				foreach (View v in b3_grid.Children) {
					SpringboardButton b = v as SpringboardButton;
					b.ManageFocus ();
				}
			}

		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			if (b1.Children.Count == 0) {
				

				if (currentPlataform.userlatitude != 0) {
					Global.currentPosition = new Xamarin.Forms.Maps.Position (currentPlataform.userlatitude, currentPlataform.userlongitude);
				}

				hud = DependencyService.Get<Acr.XamForms.UserDialogs.IUserDialogService> ();
				var phone = DependencyService.Get<Acr.XamForms.Mobile.IPhoneService> ();

				if (Device.OS == TargetPlatform.iOS) {
					//await Global.getGPSPositionForIOS ();
				}
				if (currentPlataform.FileExists (Path.Combine (currentPlataform.getLocalDatabasePath (), Global.databaseName)) == false) {
					await Update ();
				} else {
					if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS) {
						//per test currentPlataform.EnableGpsByCode ();//togliere
						var updateAvailable = await IsThereAnUpdate ();
						if (updateAvailable) {
							var dresult = await this.DisplayAlert ("Aggiornamento", "E' disponibile un nuovo aggiornamento. Vuoi scaricare le nuove informazioni", "Si", "No");
							if (dresult) {
								await Update ();
								await DisplayAlert ("Messaggio", "Aggiornamento completato.", "OK");
							} else {
								await ApplyFilters ();
								Global.calculateDistance ();
							}
						} else {
							if (Device.OS == TargetPlatform.iOS) {
								//await testTables ();
							}
							await ApplyFilters ();
							Global.calculateDistance ();
						}
						var ret = await createMenu (b1, MenuKindEnum.Level1Macro);
					}
					/*
					if (Device.OS == TargetPlatform.iOS) {
						await testTables ();
						var ret = await createMenu (b1, MenuKindEnum.Level1Macro);
					}
					*/
					//var ret = await createMenu (b1, MenuKindEnum.Level1Macro);
				}
			}
		}


		private async  Task<Boolean> IsThereAnUpdate ()
		{
			var d = Utility.isInternetAvailable ();
			if (d == false)
				return false;
			var request = WebRequest.Create ("http://facchini-ts.it/imagobe/updateinfo.aspx") as HttpWebRequest;
			request.Method = "GET";
			DateTime dataUltimoAggiornamentoServerData;
			using (var response = await request.GetResponseAsync ()) {
				Stream st = response.GetResponseStream ();
				StreamReader reader = new StreamReader (st);
				string body = reader.ReadToEnd ();
				Int32 i = body.IndexOf ("Data ultimo aggiornamento:|");
				string dataUltimoAggiornamentoServerString = body.Substring (i + 27, 19);
				DateTime.TryParseExact (dataUltimoAggiornamentoServerString, "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out dataUltimoAggiornamentoServerData);
			}
			DateTime dataUltimoAggiornamentoClientData;
			string pathLog = currentPlataform.getLocalDatabasePath ();
			var arrbyteLog = currentPlataform.readfile (System.IO.Path.Combine (pathLog, "imagolight.log"));
			var str = System.Text.Encoding.UTF8.GetString (arrbyteLog, 0, arrbyteLog.Length);
			string dataUltimoAggiornamentoClientString = str.Split ('|') [1].Substring (0, 19);
			DateTime.TryParseExact (dataUltimoAggiornamentoClientString, "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out dataUltimoAggiornamentoClientData);
			DateTime dServer = new DateTime (dataUltimoAggiornamentoServerData.Year, dataUltimoAggiornamentoServerData.Month, dataUltimoAggiornamentoServerData.Day,
				                   dataUltimoAggiornamentoServerData.Hour, dataUltimoAggiornamentoServerData.Minute, 00);
			DateTime dClient = new DateTime (dataUltimoAggiornamentoClientData.Year, dataUltimoAggiornamentoClientData.Month, dataUltimoAggiornamentoClientData.Day,
				                   dataUltimoAggiornamentoClientData.Hour, dataUltimoAggiornamentoClientData.Minute, 00);
			if (dServer > dClient)
				return true;
			else
				return false;
		}


		private async Task Update ()
		{
			Boolean isGpsEnabled = currentPlataform.IsGpsEnabled ();

			var p = hud.Loading ("Download");
			ListTemplate1 l = new ListTemplate1 (ListTemplate1.TipoFormEnum.Dettaglio);
			await DownloadFiles ();
			await testTables ();
			await ApplyFilters ();

			//if (isGpsEnabled)
			//Global.currentPosition = await l.GetCurrentGPSPosition (true, false);
			Global.calculateDistance ();
			var ret = await createMenu (b1, MenuKindEnum.Level1Macro);

			//hud.HideLoading ();
			p.Hide ();


			if (Device.OS == TargetPlatform.Android) {
				if (isGpsEnabled == false) {
					string msg = "Imago richiede che il gps sia attivo; vuoi cambiare le impostazioni ora?";
					var dresult = await this.DisplayAlert ("Imago", msg, "Si", "No");
					if (dresult) {
						currentPlataform.EnableGpsByCode ();
					}
				}


				//string msg = "Imago richiede gps attivo, vai in impostazioni ed attiva la modalità alta precisione o solo dispositivo";
				//await DisplayAlert ("Imago", msg, "OK");
			}
				


		}


		private async Task ApplyFilters ()
		{
			List<POIs> lPoiToRemove = new List<POIs> ();
			foreach (POIs p in Global.K_POIs) {
				if (p.IsEvents == "True") {
					if (p.ClosingDate.HasValue == true && p.ClosingDate.Value < DateTime.Today)
						lPoiToRemove.Add (p);
				}
				if (p.CanSleep == "True") {
					lPoiToRemove.Add (p);
				}
			}
			foreach (POIs p in lPoiToRemove) {
				Global.K_POIs.Remove (p);
			}
			//////////
			List<Categories> lCatToRemove = new List<Categories> ();
			foreach (Categories c in Global.K_Categories) {
				if (c.IDCategory == 120 || c.IDCategory == 170) //nascondo impostazioni
					lCatToRemove.Add (c);
				if (c.IDCategory == 165 || c.IDCategory == 117) //nascondo QR
					lCatToRemove.Add (c);
			}
			foreach (Categories c in lCatToRemove) {
				Global.K_Categories.Remove (c);
			}

		}

	
		private async Task DownloadFiles ()
		{
			try {
			
				await currentPlataform.downloadZipFile ("http://facchini-ts.it/imagobe/DATABASE/imagolight.zip");
			} catch (Exception ex) {
				await currentPlataform.downloadZipFile ("http://178.239.178.170/imagobe/DATABASE/imagolight.zip");
			}
			try {
				await currentPlataform.downloadZipFile ("http://facchini-ts.it/imagobe/DATABASE/uploadMC.zip", Global.K_subfolder_MC);
			} catch (Exception ex) {
				await currentPlataform.downloadZipFile ("http://178.239.178.170/imagobe/DATABASE/uploadMC.zip", Global.K_subfolder_MC);
			}
			try {
				await currentPlataform.downloadZipFile ("http://facchini-ts.it/imagobe/DATABASE/uploadC.zip", Global.K_subfolder_C);
			} catch (Exception ex) {
				await currentPlataform.downloadZipFile ("http://178.239.178.170/imagobe/DATABASE/uploadC.zip", Global.K_subfolder_C);
			}
			try {
				await currentPlataform.downloadZipFile ("http://facchini-ts.it/imagobe/DATABASE/uploadP.zip", Global.K_subfolder_P);
			} catch (Exception ex) {
				await currentPlataform.downloadZipFile ("http://178.239.178.170/imagobe/DATABASE/uploadP.zip", Global.K_subfolder_P);
			}

		}


		public static async Task testTables ()
		{
			try {

				//var cn = await sqliteHelper.creaDataBaseORGetConnection (Global.databaseName);
				Global.K_Categories = await sqliteHelper.sqlite_ReadFromExistingLocalTable<Categories> (Global.databaseName);
				Global.k_MacroCategories = await sqliteHelper.sqlite_ReadFromExistingLocalTable<MacroCategories> (Global.databaseName);
				Global.K_Categories_POIs = await sqliteHelper.sqlite_ReadFromExistingLocalTable<Categories_POIs> (Global.databaseName);
				Global.K_POIs = await sqliteHelper.sqlite_ReadFromExistingLocalTable<POIs> (Global.databaseName);
				Global.K_POIsPictures = await sqliteHelper.sqlite_ReadFromExistingLocalTable<POIsPictures> (Global.databaseName);
				Global.K_CCFs = Global.createCCFs ();
				/*
				var l = await sqliteHelper.sqlite_ReadFromExistingLocalTable<Languages> (Global.databaseName);
				var p = await sqliteHelper.sqlite_ReadFromExistingLocalTable<POIs> (Global.databaseName);
				var cp = await sqliteHelper.sqlite_ReadFromExistingLocalTable<Categories_POIs> (Global.databaseName);
				var pp = await sqliteHelper.sqlite_ReadFromExistingLocalTable<POIsPictures> (Global.databaseName);
				*/
			} catch (Exception ex) {
				Int32 aa = 1;
			}

		

		}

		#region "macro"

		private async Task<Boolean> createMenu (StackLayout m, MenuKindEnum menukind, Int32 idParent = 0)
		{
			double padL = 10;
			double padR = 10;
			double colSpacing = 10;
			double imgW = 45;
			double imgH = 45;

			Double rowNumber = 0;
			List<MacroCategories> mm = null;
			List<Categories> cc = null;

			if (menukind == MenuKindEnum.Level1Macro) {
				//var tq = await sqliteHelper.sqlite_GetTableQueryFromExistingLocalTable<MacroCategories> (Global.databaseName);
				mm = Global.k_MacroCategories.Where (x => x.IDLanguage == Global.currentLanguage).ToList ();
				rowNumber = System.Math.Ceiling (Convert.ToDouble (mm.Count) / 3);
			}
			if (menukind == MenuKindEnum.Level2CatFather) {
				//var tq = await sqliteHelper.sqlite_GetTableQueryFromExistingLocalTable<Categories> (Global.databaseName);
				cc = Global.K_Categories.Where (x => x.IDLanguage == Global.currentLanguage && x.IDMacroCategory == idParent && x.IDCategoryFather == null).ToList ();
				rowNumber = System.Math.Ceiling (Convert.ToDouble (cc.Count) / 3);
			}
			if (menukind == MenuKindEnum.Level3CatChild) {
				//var tq = await sqliteHelper.sqlite_GetTableQueryFromExistingLocalTable<Categories> (Global.databaseName);
				cc = Global.K_Categories.Where (x => x.IDCategoryFather != null && x.IDCategoryFather == idParent && x.IDLanguage == Global.currentLanguage).ToList ();
				rowNumber = System.Math.Ceiling (Convert.ToDouble (cc.Count) / 3);
			}


			Grid g = new Grid () {
				ColumnSpacing = colSpacing,
				RowSpacing = 10,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				//BackgroundColor = Color.Yellow
			};
			g.Padding = new Thickness (padL, 0, padR, 0);
			for (int i = 1; i <= rowNumber; i++) {
				g.RowDefinitions.Add (new RowDefinition (){ Height = GridLength.Auto });
			}

			//g.RowDefinitions.Add (new RowDefinition (){ Height = GridLength.Auto });
			//g.RowDefinitions.Add (new RowDefinition (){ Height = GridLength.Auto });
			//g.RowDefinitions.Add (new RowDefinition (){ Height = GridLength.Auto });
			//per un phone sempre 3 colonne

			if (this.Width == -1) {
				Int32 aaattt = 0;
			}

			var gridW = App.k_screenW - (padL + padR) - (colSpacing * 2); //g.Width - (10 * 2);

			var colW = gridW / 3;

			g.ColumnDefinitions.Add (new ColumnDefinition (){ Width = colW });
			g.ColumnDefinitions.Add (new ColumnDefinition (){ Width = colW });
			g.ColumnDefinitions.Add (new ColumnDefinition (){ Width = colW });
			var gridOK = false;
			g.SizeChanged += async delegate(object sender, EventArgs e) {
				/*
				if (gridOK)
					return;
				var gridW = this.Width - (padL + padR) - (colSpacing * 2); //g.Width - (10 * 2);
				var colW = gridW / 3;
				g.ColumnDefinitions [2].Width = colW;
				g.ColumnDefinitions [1].Width = colW;
				g.ColumnDefinitions [0].Width = colW;
				System.Diagnostics.Debug.WriteLine ("grid sizeChanged; " + colW);
				gridOK = true;
				*/
			};
			g.Children.Clear ();
			Int32 riga = 0;
			Int32 colonna = 0;
			if (menukind == MenuKindEnum.Level1Macro) {
				foreach (MacroCategories mc in mm.OrderBy(x=>x.NameOfTheMacroCategory.Substring(0,2))) {
					var b1 = new SpringboardButton () {
						SpringboardLabelFocusColor = Color.FromRgb (252, 97, 0),
						SpringboardLabelWidth = colW
					};
					string path = Path.Combine (this.currentPlataform.getLocalDatabasePath (), Global.K_subfolder_MC);
					string file = mc.Icon;
					b1.SpringboardImage.Source = ImageSource.FromFile (Path.Combine (path, file));
					b1.SpringboardImage.WidthRequest = imgW;
					b1.SpringboardImage.HeightRequest = imgH;
					string file2 = mc.Icon2;
					if (string.IsNullOrEmpty (file2) == false) {
						b1.SpringboardImageFocus.Source = ImageSource.FromFile (Path.Combine (path, file2));
						b1.SpringboardImageFocus.WidthRequest = imgW;
						b1.SpringboardImageFocus.HeightRequest = imgH;
					}
					b1.SpringboardLabel.Text = mc.NameOfTheMacroCategory.Substring (3).ToUpper ();
					b1.Tag = mc.IDMacroCategory;
					b1.GestureRecognizers.Add (new TapGestureRecognizer {
						Command = new Command (() => { 
							EventHandlerMenuMacro (b1);
						}),

					});
					g.Children.Add (b1, colonna, riga);
					colonna++;
					if (colonna > 2) {
						colonna = 0;
						riga++;
					}
				}
			}
			if (menukind == MenuKindEnum.Level2CatFather || menukind == MenuKindEnum.Level3CatChild) {
				foreach (Categories c in cc.OrderBy(x=>x.NameOfTheCategory.Substring(0,2))) {
					var b1 = new SpringboardButton (){ SpringboardLabelFocusColor = Color.White,  SpringboardLabelWidth = colW  };
					string path = Path.Combine (this.currentPlataform.getLocalDatabasePath (), Global.K_subfolder_C);
					string file = c.Icon;
					b1.SpringboardImage.Source = ImageSource.FromFile (Path.Combine (path, file));
					b1.SpringboardImage.WidthRequest = imgW;
					b1.SpringboardImage.HeightRequest = imgH;
					string file2 = c.Icon2;
					if (string.IsNullOrEmpty (file2) == false) {
						b1.SpringboardImageFocus.Source = ImageSource.FromFile (Path.Combine (path, file2));
						b1.SpringboardImageFocus.WidthRequest = imgW;
						b1.SpringboardImageFocus.HeightRequest = imgH;
					}
					Int32 kkkk;
					if (Int32.TryParse (c.NameOfTheCategory.Substring (0, 2), out kkkk))
						b1.SpringboardLabel.Text = c.NameOfTheCategory.Substring (3).ToUpper ();
					else
						b1.SpringboardLabel.Text = c.NameOfTheCategory.ToUpper ();
					
					b1.Tag = c.IDCategory;
					b1.GestureRecognizers.Add (new TapGestureRecognizer {
						Command = new Command (() => { 
							EventHandlerMenuCategory (b1);
						}),

					});
					g.Children.Add (b1, colonna, riga);
					colonna++;
					if (colonna > 2) {
						colonna = 0;
						riga++;
					}
				}
			}
		
			m.Children.Clear ();
			m.Children.Add (g);

			/*
			m.Children.Add (new StackLayout () {
				BackgroundColor = Color.Red,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = { new Label (){ Text = "test" } }
			});
			*/
			//var b1 = new SpringboardButton ();
			//b1.SpringboardImage.Source = ImageSource.FromFile ("/data/data/com.testRestCompressed/files/MC/cultura_60_p_1.png");
			//b1.SpringboardLabel.Text = "Cultura";
			//g.Children.Add (b1, 0, 0);
			//m.Children.Add (g);
			return true;
			
		}

		private async Task EventHandlerMenuMacro (SpringboardButton btn)
		{
			
			//unfocus
			foreach (SpringboardButton btnTmp in  (b1.Children[0] as Grid).Children) {
				if (btnTmp.HasFocus) {
					btnTmp.HasFocus = false;
					btnTmp.ManageFocus ();
				}
			}

			//focus button
			btn.HasFocus = true;
			btn.ManageFocus ();
		
			if (Device.OS == TargetPlatform.iOS) //20150719
				await b2.RotateTo (0, 10, Easing.Linear);

			//determine the number of lines
			//var tq = await sqliteHelper.sqlite_GetTableQueryFromExistingLocalTable<MacroCategories> (Global.databaseName);
			MacroCategories m = Global.k_MacroCategories.Where (x => x.IDMacroCategory == btn.Tag).ToList () [0];
			//var tq2 = await sqliteHelper.sqlite_GetTableQueryFromExistingLocalTable<Categories> (Global.databaseName);
			var n = Global.K_Categories.Where (x => x.IDMacroCategory == btn.Tag && x.IDCategoryFather == null && x.IDLanguage == Global.currentLanguage).ToList ().Count;
			var hMenu = this.Height;
			hMenu += 1;

			if (n < 4)
				this.b2H = (hMenu / 2) - 140;
			if (n >= 4 && n <= 6)
				this.b2H = (hMenu / 2) - 70;
			if (n >= 7)
				this.b2H = (hMenu / 2);

			//b3 b2 b1
			this.al.Children.Remove (b3);
			this.al.Children.Remove (b2);
			b2.HeightRequest = b2H;
			this.al.Children.Add (b3, new Point (0, hMenu - b3H)); 
			this.al.Children.Add (b2, new Point (0, hMenu - b2H)); 
			this.al.RaiseChild (b1);
		
			if (btn.Tag == 99 || btn.Tag == 101) {
				//... 
				//string p = Global.currentPosition.Latitude.ToString () + " - " + Global.currentPosition.Longitude.ToString ();
				//await DisplayAlert ("Numero di cambi posizione", Global.PositionChangedNumber.ToString () + " - " + p, "OK");
				//return;
			}
			if (btn.Tag == 103 || btn.Tag == 94) {
				var map = new Map ();
				if (Device.OS == TargetPlatform.iOS)
					NavigationPage.SetHasBackButton (map, false);
				Navigation.PushAsync (map);
				if (b2.IsVisible) {
					b2.IsVisible = false;
					await b2.TranslateTo (0, -this.b1H);
				}
				return;
			} 
			await createMenu (b2, MenuKindEnum.Level2CatFather, btn.Tag);
			//animations
			b2.IsVisible = true;
			b3.IsVisible = false;



			await b2.TranslateTo (0, -this.b1H);

		}

		private async Task EventHandlerMenuCategory (SpringboardButton btn)
		{
			Boolean isSecondLevel = false;
			Categories c = Global.K_Categories.First (x => x.IDCategory == btn.Tag && x.IDLanguage == Global.currentLanguage);
			if (c.IDCategoryFather == null)
				isSecondLevel = true;

			//unfocus
			foreach (SpringboardButton btnTmp in  (b2.Children[0] as Grid).Children) {
				if (btnTmp.HasFocus) {
					btnTmp.HasFocus = false;
					btnTmp.ManageFocus ();
				}
			}
			if (b3.IsVisible) {
				foreach (SpringboardButton btnTmp in  (b3.Children[0] as Grid).Children) {
					if (btnTmp.HasFocus) {
						btnTmp.HasFocus = false;
						btnTmp.ManageFocus ();
					}
				}
			}

			//focus button
			btn.HasFocus = true;
			btn.ManageFocus ();

			if (Device.OS == TargetPlatform.iOS) //20150719
				await b2.RotateTo (0, 10, Easing.Linear);

			//determine the number of lines
			//var tq = await sqliteHelper.sqlite_GetTableQueryFromExistingLocalTable<Categories> (Global.databaseName);
			var n = Global.K_Categories.Where (x => x.IDCategoryFather != null && x.IDCategoryFather == btn.Tag && x.IDLanguage == Global.currentLanguage).ToList ().Count;
			if (n > 0) {
				var hMenu = this.Height;
				hMenu += 1;

				if (n < 4)
					this.b3H = (hMenu / 2) - 140;
				if (n >= 4 && n <= 6)
					this.b3H = (hMenu / 2) - 70;
				if (n >= 7)
					this.b3H = (hMenu / 2);

				//b3 b2 b1
				this.al.Children.Remove (b3);
				//this.al.Children.Remove (b2);
				b3.HeightRequest = b3H;
				this.al.Children.Add (b3, new Point (0, hMenu - b3H)); 
				//this.al.Children.Add (b2, new Point (0, hMenu - b2H)); 
				this.al.RaiseChild (b2);

				await createMenu (b3, MenuKindEnum.Level3CatChild, btn.Tag);

				//animations
				b1.IsVisible = false;
				b2.IsVisible = true;
				b3.IsVisible = true;
				await b2.TranslateTo (0, 0);
				await b3.TranslateTo (0, -this.b2H);
			} else {
				//await DisplayAlert ("Messaggio", "Open Poi list", "OK");
				if (b3.IsVisible && isSecondLevel) {
					//b3 b2
					b3.IsVisible = false;
					b1.IsVisible = true;
					await b2.TranslateTo (0, -this.b1H);
				}
				PosList myPosList = null;
				try {
					//if (Device.OS == TargetPlatform.iOS)
					//await Global.getGPSPositionForIOS ();
					myPosList = new  PosList (btn.SpringboardLabel.Text, btn.Tag, 0, false);
				} catch (Exception ex) {
					Int32 aaaaa = 1;
				}

				if (btn.Tag == 118 || btn.Tag == 166) { //20150617
					//http://facchini-ts.it/imagobe/updateinfo.aspx
					/*
					var request = WebRequest.Create ("http://facchini-ts.it/imagobe/updateinfo.aspx") as HttpWebRequest;
					request.Method = "GET";
					DateTime dataUltimoAggiornamentoServerData;
					using (var response = await request.GetResponseAsync ()) {
						Stream st = response.GetResponseStream ();
						StreamReader reader = new StreamReader (st);
						string body = reader.ReadToEnd ();
						Int32 i = body.IndexOf ("Data ultimo aggiornamento:|");
						string dataUltimoAggiornamentoServerString = body.Substring (i + 27, 19);
						DateTime.TryParseExact (dataUltimoAggiornamentoServerString, "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out dataUltimoAggiornamentoServerData);
					}
					DateTime dataUltimoAggiornamentoClientData;
					string pathLog = currentPlataform.getLocalDatabasePath ();
					var arrbyteLog = currentPlataform.readfile (System.IO.Path.Combine (pathLog, "imagolight.log"));
					var str = System.Text.Encoding.UTF8.GetString (arrbyteLog, 0, arrbyteLog.Length);
					string dataUltimoAggiornamentoClientString = str.Split ('|') [1].Substring (0, 19);
					DateTime.TryParseExact (dataUltimoAggiornamentoClientString, "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out dataUltimoAggiornamentoClientData);
					if (dataUltimoAggiornamentoServerData > dataUltimoAggiornamentoClientData) {
*/
					var updateAvailable = await IsThereAnUpdate ();
					if (updateAvailable) {
						var dresult = await this.DisplayAlert ("Aggiornamento", "E' disponibile un nuovo aggiornamento. Vuoi scaricare le nuove informazioni", "Si", "No");
						if (dresult) {
							await Update ();
							await DisplayAlert ("Messaggio", "Aggiornamento completato.", "OK");
						}
					} else {
						await DisplayAlert ("Messaggio", "Nessun aggiornamento disponibile.", "OK");
					}
					return;
				}

				if (btn.Tag == 121 || btn.Tag == 171) {
					string url = "http://178.239.178.170/imagobe/crediti/crediti.html";
					//url = "http://www.google.com";

					var web = new  UICustomWebView (url);
					if (Device.OS == TargetPlatform.iOS)
						NavigationPage.SetHasBackButton (web, false);
					await Navigation.PushAsync (web);
					return;
					//http://178.239.178.170/imagobe/crediti/crediti.html
				}
				if (btn.Tag == 119 || btn.Tag == 169) {
					string versione_build = "versione: " + this.currentPlataform.getApplicationVersion () + "; build: " + currentPlataform.getApplicationBuild () + "";
					await DisplayAlert ("Imago" + " " + Global.PositionChangedNumber.ToString (), versione_build, "OK");
				
					return;
				}
				if (Device.OS == TargetPlatform.iOS)
					NavigationPage.SetHasBackButton (myPosList, false);
				await Navigation.PushAsync (myPosList, true);
			}


		}

		#endregion
	
	}
}

