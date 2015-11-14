using System;

using Xamarin.Forms;

namespace testRestCompressed
{
	public class Page1 : ContentPage
	{

		Grid g { get; set; }

		RowDefinition menu3{ get; set; }

		RowDefinition menu2{ get; set; }

		RowDefinition menu1{ get; set; }

		BoxView boxmenu3{ get; set; }

		BoxView boxmenu2{ get; set; }

		BoxView boxmenu1{ get; set; }

		BoxView boxmenu1B{ get; set; }

		Boolean menu1IsVisible = true;
		Boolean menu2IsVisible = false;
		Boolean menu3IsVisible = false;

		public Page1 ()
		{

			this.SizeChanged += async delegate(object sender, EventArgs e) {
				MenuManager ();

				var hGrid = this.Height - 100;
				//g.HeightRequest = hGrid;
				//menu1.Height = hGrid;
				boxmenu1.HeightRequest = hGrid / 2;
				boxmenu1B.HeightRequest = hGrid / 2;
			};

			g = new Grid (){ /*VerticalOptions = LayoutOptions.FillAndExpand*/ };
			g.RowSpacing = 0;
			g.SizeChanged += async delegate(object sender, EventArgs e) {
				return;
			};
			menu3 = new RowDefinition ();
			menu3.Height = new GridLength (100, GridUnitType.Absolute);
			g.RowDefinitions.Add (menu3);

			menu2 = new RowDefinition ();
			menu2.Height = new GridLength (100, GridUnitType.Absolute);
			g.RowDefinitions.Add (menu2);

			menu1 = new RowDefinition ();
			menu1.Height = new GridLength (100, GridUnitType.Absolute);
			g.RowDefinitions.Add (menu1);

			boxmenu3 = new BoxView ();
			boxmenu3.BackgroundColor = Color.Red;
			g.Children.Add (boxmenu3);
			Grid.SetRow (boxmenu3, 0);

			boxmenu2 = new BoxView ();
			boxmenu2.BackgroundColor = Color.Blue;
			g.Children.Add (boxmenu2);
			Grid.SetRow (boxmenu2, 1);
			var tapGestureRecognizer2 = new TapGestureRecognizer ();
			//tapGestureRecognizer.SetBinding (TapGestureRecognizer.CommandProperty, "Animate");
			boxmenu2.GestureRecognizers.Add (tapGestureRecognizer2);
			tapGestureRecognizer2.Tapped += async delegate(object sender, EventArgs e) {
				await boxmenu2.ScaleTo (3, 10);
			};


			boxmenu1 = new BoxView (){ VerticalOptions = LayoutOptions.End };
			boxmenu1.BackgroundColor = Color.Yellow;
			g.Children.Add (boxmenu1);
			Grid.SetRow (boxmenu1, 2);
			var tapGestureRecognizer1 = new TapGestureRecognizer ();
			boxmenu1.GestureRecognizers.Add (tapGestureRecognizer1);
			tapGestureRecognizer1.Tapped += async delegate(object sender, EventArgs e) {
				//menu2IsVisible = true;
				//MenuManager ();
				//await boxmenu2.Animate( ();
				boxmenu1B.IsVisible = true;
				var hGrid = this.Height - 100;
				g.HeightRequest = hGrid;
				menu1.Height = hGrid;
				boxmenu1.HeightRequest = hGrid / 2;
				boxmenu1B.HeightRequest = hGrid / 2;
				await boxmenu1B.TranslateTo (0, -(hGrid / 2), 1000);
			};


			boxmenu1B = new BoxView (){ VerticalOptions = LayoutOptions.End };
			boxmenu1B.BackgroundColor = Color.Pink;
			boxmenu1B.IsVisible = false;
			g.Children.Add (boxmenu1B);
			Grid.SetRow (boxmenu1B, 2);



			Button b = new Button ();
			b.Text = "pag2";
			b.Clicked += async delegate(object sender, EventArgs e) {
				Navigation.PushAsync (new Page2 (), true);
			};
		
			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.EndAndExpand,
				Children = {
					g
					/*
					new Label { Text = "Hello ContentPage1" },
					b,
					*/

				}
			};
		}


		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			Int32 a = 1;
		}

		private void  MenuManager ()
		{
			var hPage = this.Height;



			var numberMenuVisible = 0;
			if (menu1IsVisible)
				numberMenuVisible++;
			if (menu2IsVisible)
				numberMenuVisible++;
			if (menu3IsVisible)
				numberMenuVisible++;

			var hGrid = hPage - 100;
			var hRow = hGrid / 2;
			g.HeightRequest = hRow * numberMenuVisible;

			if (menu1IsVisible)
				menu1.Height = new GridLength (hRow, GridUnitType.Absolute);
			else
				menu1.Height = 0;

			if (menu2IsVisible)
				menu2.Height = new GridLength (hRow, GridUnitType.Absolute);
			else
				menu2.Height = 0;

			if (menu3IsVisible)
				menu3.Height = new GridLength (hRow, GridUnitType.Absolute);
			else
				menu3.Height = 0;

		}
	}
}


