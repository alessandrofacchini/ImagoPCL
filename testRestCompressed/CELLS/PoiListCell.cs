using System;
using Xamarin.Forms;
using xUtilityPCL;
using System.IO;
using PropertyChanged;

namespace testRestCompressed
{
	
	public class PoiListCell: ViewCell
	{

		Style StandardRow { get; set; }

		CustomLabel lblRow1 { get; set; }

		CustomLabel lblRow2 { get; set; }

		CustomLabel lblRow3 { get; set; }

		CustomLabel lblRow4 { get; set; }

		CustomLabel lblRow5 { get; set; }

		CustomLabel lblRow6 { get; set; }

		CustomLabel lblRow7 { get; set; }

		CustomLabel lblRow8 { get; set; }

		Image poiImage { get; set; }

		Double ScaleFactor { get; set; }

		Double slPadding  { get; set; }

		public PoiListCell ()
		{

			this.StandardRow = new Style (typeof(CustomLabel)) {
				Setters = {
					new Setter { Property = CustomLabel.FontNameProperty, Value = "AvenirLTStd-Book.ttf" },
					//new Setter { Property = CustomLabel.FontSizeProperty, Value = 13.4 },
					//new Setter { Property = CustomLabel.HeightRequestProperty, Value = 15.75 },
					new Setter { Property = CustomLabel.YAlignProperty, Value = TextAlignment.End },
					new Setter { Property = CustomLabel.BackgroundColorProperty, Value = Color.White },
				}
			};

			this.lblRow1 = new CustomLabel () {
				FontName = "AvenirLTStd-Heavy.ttf",
				//FontSize = 13.4,
				//HeightRequest = 15.75,
				YAlign = TextAlignment.Start,
				//LineBreakMode = LineBreakMode.NoWrap,
			};

			this.lblRow2 = new CustomLabel (){ Style = this.StandardRow }; 
			this.lblRow3 = new CustomLabel (){ Style = this.StandardRow }; 
			this.lblRow4 = new CustomLabel () {
				Style = this.StandardRow,
				/*HeightRequest = 31.5,*/
				YAlign = TextAlignment.Start
			}; 

			this.lblRow6 = new CustomLabel () { Style = this.StandardRow,
				/*HeightRequest = 31.5,*/
				YAlign = TextAlignment.Start
			}; 

			this.lblRow8 = new CustomLabel () {
				Style = this.StandardRow,
				HeightRequest = 15.75 + 3 + 1,
			}; 

			this.poiImage = new Image (){ HeightRequest = 125, WidthRequest = 125 };

		



			this.ScaleFactor = xUtilityPCL.Utility.GetFactor ();
			this.slPadding = 20;


			var viewLayout = new StackLayout () {
				BackgroundColor = Color.White,
				Spacing = 0,
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {new StackLayout {
						Orientation = StackOrientation.Vertical,
						HorizontalOptions = LayoutOptions.Start,
						VerticalOptions = LayoutOptions.StartAndExpand,
						Spacing = 0,
						Padding = new Thickness (0, 1, 0, 0),
						Children = { this.poiImage }
					},

					new StackLayout {
						Orientation = StackOrientation.Vertical,
						HorizontalOptions = LayoutOptions.Start,
						VerticalOptions = LayoutOptions.StartAndExpand,
						Spacing = 0,
						Padding = new Thickness (slPadding, 0, 0, 0),
						HeightRequest = 125 + 3 + 1,
						//WidthRequest=215, //20150627
						//BackgroundColor = Color.Red,
						Children = {
							lblRow1, lblRow2, lblRow3, lblRow4,
							lblRow6,  lblRow8
						}
					}
				}
			};


			//viewLayout.HeightRequest = FontSize * Global.k_leadingspace;
			//viewLayout.BackgroundColor = Global.k_coloreBackgroundPagina;// Color.Pink;
			//this.lblRow1.SetBinding (CustomLabel.TextProperty, "Icon"); 
			this.lblRow1.SetBinding (CustomLabel.TextProperty, "NameOfThePOI"); 
			this.lblRow2.SetBinding (CustomLabel.TextProperty, "Town"); 
			//this.lblRow4.SetBinding (CustomLabel.TextProperty, "Address"); manual binding
			this.lblRow8.SetBinding (CustomLabel.TextProperty, "DistanceLabel"); 

			this.lblRow8.SizeChanged += async delegate(object sender, EventArgs e) {
				var y = this.lblRow8.AnchorY;
			};


			//lblRow1.Text = "aa";
			//viewLayout.BackgroundColor = Color.Yellow;
			View = viewLayout;
			//viewLayout.BackgroundColor = Color.Pink;
			//this.Height = 40;

		}




		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			POIs p = this.BindingContext as POIs;
			var currentPlataform = DependencyService.Get<platformSpecific> ();
			var path = currentPlataform.getLocalDatabasePath ();
			path = Path.Combine (path, Global.K_subfolder_P);
			if (p.Icon != null)
				this.poiImage.Source = ImageSource.FromFile (Path.Combine (path, p.Icon));
			else {
				this.poiImage.Source = ImageSource.FromResource ("testRestCompressed.Resources.polist_default.jpg");
			}

			if (string.IsNullOrEmpty (p.Address)) {
				p.Address = "   "; //forza l'altezza standard basata sul font
			}

			if (p.IsEvents == "True") {
				this.lblRow4.FontName = "AvenirLTStd-Heavy.ttf";
				this.lblRow4.HeightRequest = 47.25; //la 4 è su una riga per cui prende il font
				this.lblRow4.Text = p.HowToArrive; //"name of event";
				if (p.FurtherInfo != null) {
					this.lblRow6.Text = p.FurtherInfo;
				}
				/*
				if (p.ClosingDate.HasValue) {
					this.lblRow4.Text = "Fino al " + p.ClosingDate.Value.ToString ("dd MMMM yyyy");
				} else {
					if (p.OpeningDate.HasValue) {
						this.lblRow4.Text = "Dal " + p.OpeningDate.Value.ToString ("dd MMMM yyyy");
					}
				}
				*/
			} else {
				this.lblRow4.Text = p.Address;
				this.lblRow4.HeightRequest = 31.5;
				if (line1IsOn2lines () && line4IsOn2lines ())
					this.lblRow6.HeightRequest = 16;
				else
					this.lblRow6.HeightRequest = 31.5;
			}

			//this.lblRow6.BackgroundColor = Color.Yellow;

		}

		private Boolean line1IsOn2lines ()
		{
			
			if (GetTextBoxLines (this.lblRow1.Text, 13.5) == 2)
				return true;

			return false;

		}

		private Boolean line4IsOn2lines ()
		{
			
			if (GetTextBoxLines (this.lblRow4.Text, 13.5) == 2)
				return true;

			return false;

		}

		Int32 GetTextBoxLines (string text, double fontsize)
		{
			var availableWidth = Convert.ToInt32 (App.k_screenW - (this.poiImage.WidthRequest + slPadding));
			var currentPlataform = DependencyService.Get<platformSpecific> ();
			return currentPlataform.GetTextViewLines (text, fontsize, availableWidth);
		}


	}


}

