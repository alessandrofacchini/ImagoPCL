using System;

using Xamarin.Forms;
using xUtilityPCL;
using System.Threading.Tasks;

using System.Collections.Generic;

using System.Collections.ObjectModel;
using System.Linq;

namespace testRestCompressed
{
	
	public class PosDetail : ContentPage
	{
		public POIs drCurrent { get; set; }


		public ScrollView svmainScrollView { get; set; }

		public StackLayout slTitleAddress { get; set; }

		public CustomLabel lblTitle { get; set; }

		public CustomLabel lblAddress { get; set; }

		public Image PosDetailImage { get; set; }

		//public XLabs.Forms.Controls.ImageGallery mainImages { get; set; }

		public CustomImageGallery PosDetailGallery { get; set; }

		public CustomLabel lblPosDetailGallery { get; set; }

		public StackLayout slDescription { get; set; }

		public AbsoluteLayout slDescriptionInner { get; set; }

		public BaseUrlWebView webViewDescription { get; set; }

		public Image lblDecriptionCollapseImage { get; set; }

		public StackLayout slInfo { get; set; }

		public AbsoluteLayout slInfoInner { get; set; }

		public StackLayout slInfoInner2 { get; set; }

		public Image lblInfoCollapseImage { get; set; }

		public string title { get; set; }

		public StackLayout slBottomBar { get; set; }

		public Int32 idCategory { get; set; }

		public Map map { get; set; }

		public NavigationPage np{ get; set; }


		public PosDetail (POIs _drCorrente, string _title, Int32 _idCategory)
		{
			//http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/webview/
			//http://forums.xamarin.com/discussion/33404/set-custom-font-in-webview
			//https://forums.xamarin.com/discussion/3467/loading-dynamic-html-and-javascript-from-assets-in-a-webview
			//http://stackoverflow.com/questions/3900658/how-to-change-font-face-of-webview-in-android/5417710#5417710
			this.title = _title;
			this.BindingContext = _drCorrente;
			this.drCurrent = _drCorrente;
			this.idCategory = _idCategory;
		

			lblTitle = new CustomLabel () {
				FontName = "AvenirLTStd-Heavy.ttf",
				FontSize = 16,
				HeightRequest = 36,
			};

			lblTitle.Text = drCurrent.NameOfThePOI;


			lblAddress = new CustomLabel () {
				FontName = "AvenirLTStd-Book.ttf",
				FontSize = 16,
				TextColor = Color.FromRgb (170, 170, 170),
				HeightRequest = 18.5,
			};

			lblAddress.Text = drCurrent.Address + " - " + drCurrent.Town;



			if (drCurrent.IsEvents == "True") {
				lblTitle.Text = drCurrent.HowToArrive; //"name of event";
				if (drCurrent.FurtherInfo != null) {
					lblAddress.Text = drCurrent.FurtherInfo;
				}
				/*
				if (drCurrent.ClosingDate.HasValue) {
					lblAddress.Text = "Fino al " + drCurrent.ClosingDate.Value.ToString ("dd MMMM yyyy");
				} else {
					if (drCurrent.OpeningDate.HasValue) {
						lblAddress.Text = "Dal " + drCurrent.OpeningDate.Value.ToString ("dd MMMM yyyy");
					}
				}
				*/
			}


			this.slTitleAddress = new StackLayout () {
				Padding = new Thickness (10, 10, 10, 30),
				Spacing = 0,
				Children = { this.lblTitle, this.lblAddress },
				//BackgroundColor = Color.Aqua,
			};



			//htmlview (start)
			webViewDescription = new  BaseUrlWebView ();


			HtmlWebViewSource v = new HtmlWebViewSource ();
			//v.BaseUrl = "file:///Assets/";
			string fontFamily = "AvenirLTStd"; //"FuturaStd";//"AvenirLTStd"; //"Roboto";// "Serif";
			string fontName = "AvenirLTStd-Roman.ttf"; //"FuturaStd-Medium.ttf";

			/*
			string htmlBody = "<style type='text/css'>font { font-family: " + fontNameHtml + ";}body{font-family:"
			                  + fontNameHtml + ";text-align:justify;text-justify:inter-word;font-size:14px}"
			                  + "</style>";
*/

			string htmlBody = "";
			if (Device.OS == TargetPlatform.Android) {
				htmlBody = "<style type='text/css'>@font-face {font-family: " + fontFamily + ";src: url('file:///android_asset/" + fontName + "')}" +
				"body{font-family:"
				+ fontFamily + ";text-align:justify;text-justify:inter-word;font-size:14px}"
				+ "</style>";
			}
			if (Device.OS == TargetPlatform.iOS) {

				string fontNameHtml = "AvenirLTStd-Roman"; // "AvenirLTStd-Heavy";// "AvenirLTStd-Roman";
				//fontNameHtml = "Avenir LT Std";
				//fontNameHtml = "FuturaStd-Book";
				/*
				htmlBody = "<style type='text/css'>font { font-family: " + fontNameHtml + "; }body{font-family:"
				+ fontNameHtml + ";text-align:justify;text-justify:inter-word;font-size:12px}"
				+ "</style>";
				*/
				htmlBody = "<style type='text/css'>body{font-family:"
				+ fontNameHtml + ";text-align:justify;text-justify:inter-word;font-size:14px;}"
				+ "</style>";

			}

			string msgstr = "<html><head>" + htmlBody + "</head><body>" + this.drCurrent.Description + "</body></html>";
			//string msgstr = "<html><head>" + htmlBody + "</head><body>" + "<div style='overflow:hidden;height:100%'>" + this.drCurrent.Description + "</div>" + "</body></html>";
			v.Html = @msgstr;

			//style='overflow:hidden'
			


			webViewDescription.Source = v;
			//webViewDescription.BackgroundColor = Color.Yellow;
			webViewDescription.IsCollapsed = true;
			//webViewDescription.HeightRequest = 40;
			//webViewDescription.WidthRequest = App.k_screenW-25;
			//htmlview (end)

			webViewDescription.SizeChanged += async delegate(object sender, EventArgs e) {
				Int32 aa = 1;
				if (webViewDescription.ContentHeight == 0)
					return;
				//AbsoluteLayout.SetLayoutBounds (webViewDescription, new Rectangle (0, 10, App.k_screenW - 25, webViewDescription.ContentHeight));
			};

			lblDecriptionCollapseImage = new Image () {
				Source = ImageSource.FromResource ("testRestCompressed.Resources.arrowexpand.png"),
				//HeightRequest = 25,
				//WidthRequest = 25,
			};
			TapGestureRecognizer g = new TapGestureRecognizer ();
			lblDecriptionCollapseImage.GestureRecognizers.Add (g);
			g.Tapped += async delegate(object sender, EventArgs e) {

				var r = AbsoluteLayout.GetLayoutBounds (webViewDescription);
				Int32 viewHeight = 0;
				Int32 rotationAngle = 0;
				if (r.Height == 40) {
					var currentPlataform = DependencyService.Get<platformSpecific> ();
					if (Device.OS == TargetPlatform.Android) {
						viewHeight = currentPlataform.getWebViewContentHeight (0);
					}
					if (Device.OS == TargetPlatform.iOS) {
						viewHeight = webViewDescription.ContentHeight;
					}
					rotationAngle = 180;
					webViewDescription.IsCollapsed = false;
					//await this.svmainScrollView.ScrollToAsync (0, 150, true);
					AbsoluteLayout.SetLayoutBounds (webViewDescription, new Rectangle (0, 10, App.k_screenW - 25, viewHeight));
					await this.svmainScrollView.ScrollToAsync (slDescription, ScrollToPosition.Start, true);
				} else {
					viewHeight = 40;
					rotationAngle = 0;
					webViewDescription.IsCollapsed = true;
					AbsoluteLayout.SetLayoutBounds (webViewDescription, new Rectangle (0, 10, App.k_screenW - 25, viewHeight));
				}

				lblDecriptionCollapseImage.RotateTo (rotationAngle, 0, Easing.Linear);
				//AbsoluteLayout.SetLayoutBounds (webViewDescription, new Rectangle (0, 10, App.k_screenW - 25, viewHeight));
			
			};

			webViewDescription.callBackItemSelected += async delegate(Int32 dip) {
				//AbsoluteLayout.SetLayoutBounds (webViewDescription, new Rectangle (0, 10, App.k_screenW - 25, dip));
			};


			this.slDescriptionInner = new AbsoluteLayout (){ Children = { webViewDescription, lblDecriptionCollapseImage } };
			AbsoluteLayout.SetLayoutFlags (webViewDescription, AbsoluteLayoutFlags.XProportional);
			//AbsoluteLayout.SetLayoutBounds (webViewDescription, new Rectangle (0, 10, App.k_screenW - 25, 1));
			AbsoluteLayout.SetLayoutBounds (webViewDescription, new Rectangle (0, 10, App.k_screenW - 25, 40));
			AbsoluteLayout.SetLayoutFlags (lblDecriptionCollapseImage, AbsoluteLayoutFlags.XProportional);
			AbsoluteLayout.SetLayoutBounds (lblDecriptionCollapseImage, new Rectangle (1.0, 0, 25, 25));


			slDescription = new StackLayout () { 
				Children = { this.slDescriptionInner },
			};
		

			Boolean isGallery = false;
			var gal = Global.K_POIsPictures.FirstOrDefault (x => x.IDPOI == drCurrent.IDPOI);
			if (gal == null)
				isGallery = false;
			else
				isGallery = true;

			this.lblPosDetailGallery = new CustomLabel () {
				FontName = "AvenirLTStd-Book.ttf",
				FontSize = 16,
				TextColor = Color.FromRgb (170, 170, 170),
				HeightRequest = 18,
				XAlign = TextAlignment.End,
				//Text = "1/" + (this.PosDetailGallery.ItemsSource as ObservableCollection<String>).Count,
			};

			string imageurl = "http://facchini-ts.it/imagobe/UPLOADPD/" + this.drCurrent.PresentationPicture;
			//posetail
			if (isGallery == false || (isGallery == true && Utility.isInternetAvailable () == false)) {
				lblPosDetailGallery.IsVisible = false;
				this.PosDetailImage = new Image () {
					Aspect = Aspect.AspectFill,
					WidthRequest = App.k_screenW,
					HeightRequest = App.k_screenW
				};
		
			
				if (this.drCurrent.PresentationPicture != null && Utility.isInternetAvailable () == true) {
					PosDetailImage.Source = new UriImageSource {
						Uri = new Uri (imageurl),
						CachingEnabled = true,
						CacheValidity = new TimeSpan (1000, 0, 0, 0),
					};
				} else {
					this.PosDetailImage.Source = ImageSource.FromResource ("testRestCompressed.Resources.posdetail_default.jpg");
				}
			}
			//////
			/// 
			if (isGallery == true) {
				lblPosDetailGallery.IsVisible = true;
				this.PosDetailGallery = new CustomImageGallery ();

				this.PosDetailGallery.callBackItemSelected += async delegate(int index) {
					System.Diagnostics.Debug.WriteLine (index.ToString ());
					this.lblPosDetailGallery.Text = (index + 1).ToString () + "/" + (this.PosDetailGallery.ItemsSource as ObservableCollection<String>).Count;
				};
				ObservableCollection<String> ll = new  ObservableCollection <string> ();
				ll.Add (imageurl);
				foreach (POIsPictures pp in Global.K_POIsPictures.Where (x => x.IDPOI == drCurrent.IDPOI).ToList()) {
					string galleryurl = "http://facchini-ts.it/imagobe/UPLOADG/" + pp.Picture;
					ll.Add (galleryurl);
				}

				/*
				ll.Add (imageurl);
				string imageurl2 = "http://facchini-ts.it/imagobe/UPLOADPD/" + "Fattorie_22.jpg";
				ll.Add (imageurl2);
				string imageurl3 = "http://facchini-ts.it/imagobe/UPLOADPD/" + "Fattorie_50.jpg";
				ll.Add (imageurl3);
				*/
				this.PosDetailGallery.ItemsSource = ll;




			}
			////////////////////////
			/// 
			Int32 progressive = 0;
			Int32 totInfoH = 0;
			CustomLabel lblInfo = new CustomLabel () {
				FontName = "FuturaStd-Medium.ttf",
				FontSize = 14,
				TextColor = Color.FromRgb (234, 102, 30),
				HeightRequest = 16 + (14 * 2),
				YAlign = TextAlignment.Start,
				Text = "INFO",
			};
			progressive += 44;

			CustomLabel oNAME = new CustomLabel () {
				FontName = "AvenirLTStd-Heavy.ttf",
				FontSize = 14,
				HeightRequest = 16 + 16,
				Text = drCurrent.NameOfThePOI,
				LineBreakMode = LineBreakMode.TailTruncation,
			};
			progressive += 32;
			CustomLabel oADDRESS = new CustomLabel () {
				FontName = "AvenirLTStd-Roman.ttf",
				FontSize = 14,
				HeightRequest = 16,
				Text = drCurrent.Address,
				LineBreakMode = LineBreakMode.TailTruncation,
			};
			progressive += 16;
			CustomLabel oADDRESS2 = new CustomLabel () {
				FontName = "AvenirLTStd-Roman.ttf",
				FontSize = 14,
				HeightRequest = 16,
				Text = "",
				LineBreakMode = LineBreakMode.TailTruncation,
			};
			progressive += 16;

			string town = String.Empty;
			town += drCurrent.CAP + " " + drCurrent.Town + " ";
			if (drCurrent.Province.ToString () != String.Empty) {
				town += " (" + drCurrent.Province + ")";
			}

			CustomLabel oTOWN = new CustomLabel () {
				FontName = "AvenirLTStd-Roman.ttf",
				FontSize = 14,
				//HeightRequest = 16 + 16,
				Text = town,
				LineBreakMode = LineBreakMode.TailTruncation,
			};
			oTOWN.SizeChanged += async delegate(object sender, EventArgs e) {
				if (oTOWN.Height > 20)
					totInfoH += 16;
			};
			progressive += 16;
			//Nota: la città può potenzialmente andare su 2 righe....non setto l'heightrequest; 
			//      nell'evento SizeChanged verifico l'effettiva altezza e correggo l'altezza dello stacklayout principale: 
			//      l'heightrequest sarà invece automaticamente settata su 16 o 32 

			bool openingHoursAvailable = true;
			Image imgOPENING = null;
			CustomLabel oOPENING = null;
			StackLayout slOPENING = null;
			Image imgCLOSING = null;
			CustomLabel oCLOSING = null;
			StackLayout slCLOSING = null;
			if (String.IsNullOrEmpty (drCurrent.OpeningHours))
				openingHoursAvailable = false;
			else {
				bool OpeningHoursOnTWOLines = true;
				string[] arrOpeningHours = drCurrent.OpeningHours.Split (Environment.NewLine.ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
				if (arrOpeningHours.Length <= 1) {
					OpeningHoursOnTWOLines = false;
				}

				imgOPENING = new Image () {
					Source = ImageSource.FromResource ("testRestCompressed.Resources.orari_aperto.png"),
					WidthRequest = 14,
					HeightRequest = 14
				};

				oOPENING = new CustomLabel () {
					FontName = "AvenirLTStd-Roman.ttf",
					FontSize = 14,
					HeightRequest = 16,
					Text = arrOpeningHours [0],
					LineBreakMode = LineBreakMode.TailTruncation,
				};
				slOPENING = new StackLayout () {
					Orientation = StackOrientation.Horizontal,
					Children = { imgOPENING, oOPENING },
				};
				progressive += 16;

				if (OpeningHoursOnTWOLines) {
					imgCLOSING = new Image () {
						Source = ImageSource.FromResource ("testRestCompressed.Resources.orari_chiuso.png"),
						WidthRequest = 14,
						HeightRequest = 14
					};

					oCLOSING = new CustomLabel () {
						FontName = "AvenirLTStd-Roman.ttf",
						FontSize = 14,
						HeightRequest = 16,
						Text = arrOpeningHours [1],
						LineBreakMode = LineBreakMode.TailTruncation,
					};
					slCLOSING = new StackLayout () {
						Orientation = StackOrientation.Horizontal,
						Children = { imgCLOSING, oCLOSING },
					};
					progressive += 16;
				}
			}


			this.slInfoInner2 = new StackLayout () {
				Children = { lblInfo, oNAME, oADDRESS, oADDRESS2, oTOWN },
				Spacing = 0, //14
			};
			if (openingHoursAvailable)
				this.slInfoInner2.Children.Add (slOPENING);
			if (slCLOSING != null) {
				this.slInfoInner2.Children.Add (slCLOSING);
			}



			Image imgPRICE = null;
			CustomLabel oPRICE = null;
			StackLayout slPRICE = null;
			CustomLabel oEMPTYPRICE = null;
			if (!string.IsNullOrEmpty (drCurrent.EntrancePrice)) {
				oEMPTYPRICE = new CustomLabel () {
					FontName = "AvenirLTStd-Roman.ttf",
					FontSize = 14,
					HeightRequest = 16,
					Text = "",
					LineBreakMode = LineBreakMode.TailTruncation,
				};

				imgPRICE = new Image () {
					Source = ImageSource.FromResource ("testRestCompressed.Resources.euro.png"),
					WidthRequest = 14,
					HeightRequest = 14
				};

				oPRICE = new CustomLabel () {
					FontName = "AvenirLTStd-Roman.ttf",
					FontSize = 14,
					HeightRequest = 16,
					Text = drCurrent.EntrancePrice,
					LineBreakMode = LineBreakMode.TailTruncation,
				};
				slPRICE = new StackLayout () {
					Orientation = StackOrientation.Horizontal,
					Children = { imgPRICE, oPRICE },
				};
				this.slInfoInner2.Children.Add (oEMPTYPRICE);
				this.slInfoInner2.Children.Add (slPRICE);
				progressive += 32;
			}


			CustomLabel oWEBSITE = null;
			CustomLabel oEMPTYWEBSITE = null;
			if (!string.IsNullOrEmpty (drCurrent.Website)) {
				oEMPTYWEBSITE = new CustomLabel () {
					FontName = "AvenirLTStd-Roman.ttf",
					FontSize = 14,
					HeightRequest = 16,
					Text = "",
					LineBreakMode = LineBreakMode.TailTruncation,
				};

				oWEBSITE = new CustomLabel () {
					FontName = "AvenirLTStd-Roman.ttf",
					FontSize = 14,
					HeightRequest = 16,
					Text = drCurrent.Website,
					LineBreakMode = LineBreakMode.TailTruncation,
				};
				oWEBSITE.GestureRecognizers.Add (new TapGestureRecognizer (async delegate(View obj) {
					//await this.DisplayAlert ("Messaggio", "maps", "OK");
					if (!string.IsNullOrEmpty (drCurrent.Website)) {
						string url = drCurrent.Website;
						if (drCurrent.Website.ToLower ().StartsWith ("www"))
							url = "http://" + url;
						var web = new  UICustomWebView (url);//drCurrent.Website);
						if (Device.OS == TargetPlatform.iOS)
							NavigationPage.SetHasBackButton (web, false);
						await Navigation.PushAsync (web);
					}

				}));


				this.slInfoInner2.Children.Add (oEMPTYWEBSITE);
				this.slInfoInner2.Children.Add (oWEBSITE);
				progressive += 32;
			}

			CustomLabel oEMAIL = null;
			CustomLabel oEMPTYEMAIL = null;
			if (!string.IsNullOrEmpty (drCurrent.email)) {
				oEMPTYEMAIL = new CustomLabel () {
					FontName = "AvenirLTStd-Roman.ttf",
					FontSize = 14,
					HeightRequest = 16,
					Text = "",
					LineBreakMode = LineBreakMode.TailTruncation,
				};
			
				oEMAIL = new CustomLabel () {
					FontName = "AvenirLTStd-Roman.ttf",
					FontSize = 14,
					HeightRequest = 16,
					Text = drCurrent.email,
					LineBreakMode = LineBreakMode.TailTruncation,
				};
				if (!string.IsNullOrEmpty (drCurrent.email)) {
					ListTemplate1 l = new ListTemplate1 (ListTemplate1.TipoFormEnum.Lista);
					oEMAIL.GestureRecognizers.Add (l.GetTapGestureRecognizerForEmail (oEMAIL)); 
				}

				this.slInfoInner2.Children.Add (oEMPTYEMAIL);
				this.slInfoInner2.Children.Add (oEMAIL);
				progressive += 32;
			}

			CustomLabel oPHONE = null;
			CustomLabel oEMPTYPHONE = null;
			if (!string.IsNullOrEmpty (drCurrent.Phone)) {
				oEMPTYPHONE = new CustomLabel () {
					FontName = "AvenirLTStd-Roman.ttf",
					FontSize = 14,
					HeightRequest = 16,
					Text = "",
					LineBreakMode = LineBreakMode.TailTruncation,
				};
			
				oPHONE = new CustomLabel () {
					FontName = "AvenirLTStd-Roman.ttf",
					FontSize = 14,
					HeightRequest = 16,
					Text = drCurrent.Phone,
					LineBreakMode = LineBreakMode.TailTruncation,
				};
				if (!string.IsNullOrEmpty (drCurrent.Phone)) {
					ListTemplate1 l = new ListTemplate1 (ListTemplate1.TipoFormEnum.Lista);
					var arrtel = drCurrent.Phone.Replace ("+xxxxx39", "").Replace ("-", ";").Split (Convert.ToChar (";"));
					string telFormattato = /* "tel:" +*/ arrtel [0].Trim ().Replace (" ", "");
					Label lblphoneTmp = new Label (){ Text = telFormattato };
					//oPHONE.GestureRecognizers.Add (l.GetTapGestureRecognizerForPhone (lblphoneTmp)); 
					oPHONE.GestureRecognizers.Add (new TapGestureRecognizer (async delegate(View obj) {
						try {
							var phone = DependencyService.Get<Acr.XamForms.Mobile.IPhoneService> ();
							phone.Call ("", telFormattato);
						} catch (Exception ex) {
							await DisplayAlert ("Messaggio", "Funzionalità di telefonia non supportata", "OK");

						}



					}));

				}
				this.slInfoInner2.Children.Add (oEMPTYPHONE);
				this.slInfoInner2.Children.Add (oPHONE);
				progressive += 32;
			}

			totInfoH = progressive;

			lblInfoCollapseImage = new Image () {
				Source = ImageSource.FromResource ("testRestCompressed.Resources.arrowexpand.png"),
			};
			TapGestureRecognizer g2 = new TapGestureRecognizer ();
			lblInfoCollapseImage.GestureRecognizers.Add (g2);
			g2.Tapped += async delegate(object sender, EventArgs e) {

				var r = AbsoluteLayout.GetLayoutBounds (slInfoInner2);
				Int32 viewHeight = 0;
				Int32 rotationAngle = 0;
				if (r.Height == 20) {
					SetVisibility (true);
					viewHeight = totInfoH;
					rotationAngle = 180;
					AbsoluteLayout.SetLayoutBounds (slInfoInner2, new Rectangle (0, 10, App.k_screenW - 25, viewHeight));
					await this.svmainScrollView.ScrollToAsync (slInfoInner, ScrollToPosition.Start, true);
				} else {
					SetVisibility (false);
					viewHeight = 20;
					rotationAngle = 0;
					AbsoluteLayout.SetLayoutBounds (slInfoInner2, new Rectangle (0, 10, App.k_screenW - 25, viewHeight));
				}
				lblInfoCollapseImage.RotateTo (rotationAngle, 0, Easing.Linear);
			};

			SetVisibility (true);//20150710

		
			this.slInfoInner = new AbsoluteLayout (){ Children = { this.slInfoInner2, lblInfoCollapseImage } };
			AbsoluteLayout.SetLayoutFlags (slInfoInner2, AbsoluteLayoutFlags.XProportional);
			AbsoluteLayout.SetLayoutBounds (slInfoInner2, new Rectangle (0, 10, App.k_screenW - 25, totInfoH)); //20150710 era 20
			AbsoluteLayout.SetLayoutFlags (lblInfoCollapseImage, AbsoluteLayoutFlags.XProportional);
			AbsoluteLayout.SetLayoutBounds (lblInfoCollapseImage, new Rectangle (1.0, 0, 25, 25));





			this.slInfo = new StackLayout () { Padding = new Thickness (10, 15, 0, 0), Children = { this.slInfoInner }
			};



			BoxView bx = new BoxView (){ HeightRequest = 1, Color = Color.FromRgb (242, 242, 242) };
			Label lbl = new Label (){ Text = "abc", BackgroundColor = Color.Red };
			Image b1 = new Image () {
				Source = ImageSource.FromResource ("testRestCompressed.Resources.calcolapercorso.png"),
				HeightRequest = 30,
				//VerticalOptions = LayoutOptions.FillAndExpand
			};
			if (drCurrent.Latitude.HasValue == true && drCurrent.Latitude != 0) {
				b1.GestureRecognizers.Add (new TapGestureRecognizer (async delegate(View obj) {
					if (Global.MapsInstalled == false) {
						await this.DisplayAlert ("Messaggio", "Maps non installata", "OK");
						return;
					}

					//await Task.Run (() => {
					//	googlecall ();
					//});
					var currentPlataform = DependencyService.Get<platformSpecific> ();
					currentPlataform.doDrive (Convert.ToDouble (drCurrent.Latitude.Value), Convert.ToDouble (drCurrent.Longitude), true);
				}));
			}

			Image b2 = new Image () {
				Source = ImageSource.FromResource ("testRestCompressed.Resources.vicino.png"),
				HeightRequest = 30,
				//VerticalOptions = LayoutOptions.FillAndExpand
			};
			b2.GestureRecognizers.Add (new TapGestureRecognizer (async delegate(View obj) {
				await loadPoisAroundMe ();

			
				/*
				var currentPlatform = DependencyService.Get<platformSpecific> ();
				var ulrShare = "http://google.com/";
				var txtShareNEW = " - Clicca qui per scaricare Imnago per iOS e Android!";
				var appTitle = "pos name" + DateTime.Now.Ticks.ToString ();
				var appName = "xyz";
				var appImage = "http://178.239.178.170/imagobe/uploadp/rosa_canina_150.jpg";//"https://raw.githubusercontent.com/fbsamples/ios-3.x-howtos/master/Images/iossdk_logo.png";
				var description = "myDescription";
				if (Device.OS == TargetPlatform.iOS)
					appImage = "settings.png";
				currentPlatform.shareFB (txtShareNEW, ulrShare, appImage, appTitle, appName, description);
				*/
			}));

			Image b3 = new Image () {
				Source = ImageSource.FromResource ("testRestCompressed.Resources.mappa_piccola.png"),
				HeightRequest = 30,
				//VerticalOptions = LayoutOptions.FillAndExpand
			};
			b3.GestureRecognizers.Add (new TapGestureRecognizer (async delegate(View obj) {
				if (drCurrent.Latitude == 0) {
					await DisplayAlert ("Messaggio", "Mappa non disponibile", "OK");
				} else {
					//if (Device.OS == TargetPlatform.iOS)
					//	await Global.getGPSPositionForIOS ();
					map = new  Map (drCurrent, this.idCategory);
					/*
					np = new NavigationPage (map);
					np.Popped += async delegate(object sender, NavigationEventArgs e) {
						Int32 aa = 1;
					};
					await Navigation.PushModalAsync (np);
					*/
					if (Device.OS == TargetPlatform.iOS)
						NavigationPage.SetHasBackButton (map, false);
					await Navigation.PushAsync (map, true);
				}
			}));
			Image b4 = new Image () {
				Source = ImageSource.FromResource ("testRestCompressed.Resources.texttospeech.png"),
				HeightRequest = 30,
				//VerticalOptions = LayoutOptions.FillAndExpand
			};
			b4.GestureRecognizers.Add (new TapGestureRecognizer (async delegate(View obj) {
				
				var currentPlataform = DependencyService.Get<iTextToSpeech> ();
				currentPlataform.Speak (xUtilityPCL.HtmlRemoval.StripTagsRegex (drCurrent.Description).Replace (System.Environment.NewLine, " ")); 
			}));

			StackLayout slBottomBarInner = new StackLayout () {
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (10, 10, 10, 10),
				Spacing = 20,
				Children = { b1, b2, b3, b4 },
				//HeightRequest = 50,

					
			};

			this.slBottomBar = new StackLayout () { Padding = new Thickness (0, 30, 0, 0), BackgroundColor = Color.White,
				Children = { bx, slBottomBarInner },

			};

			View PosDetailImageGeneric;
			if (isGallery) {
				PosDetailImageGeneric = PosDetailGallery;

			} else {
				PosDetailImageGeneric = PosDetailImage;

			}
				
			
			svmainScrollView = new ScrollView () {
				Content = new StackLayout () {
					Spacing = 0,
					BackgroundColor = Color.White,
					Children = {
						slTitleAddress,
						PosDetailImageGeneric,
						lblPosDetailGallery,
						slDescription,
						slInfo,
						slBottomBar
					}
				}
			};

			Content = svmainScrollView;

		}


		private void SetVisibility (Boolean value)
		{

			Int32 i = 1;
			foreach (View vvv in this.slInfoInner2.Children) {
				if (i > 1)
					vvv.IsVisible = value;
				i += 1;
			}
		}


		private async Task loadPoisAroundMe ()
		{
			PosList d = new PosList ("PUNTI VICINO A ME", 0, this.drCurrent.IDPOI, true);
			if (Device.OS == TargetPlatform.iOS)
				NavigationPage.SetHasBackButton (d, false);
			await Navigation.PushAsync (d, true);
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			var currentPlataform = DependencyService.Get<iTextToSpeech> ();
			currentPlataform.StopSpeak ();
			GC.Collect (1);
		}

		protected override bool OnBackButtonPressed ()
		{
			return base.OnBackButtonPressed ();
		}




	}
}


