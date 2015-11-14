using System;

using Xamarin.Forms;

namespace testRestCompressed
{
	public class MapBarCell :  ViewCell
	{
		Image image { get; set; }

		public MapBarCell ()
		{
			image = new Image {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};
			image.SetBinding (Image.SourceProperty, new Binding ("MapImageFullPath"));
			image.WidthRequest = image.HeightRequest = 35;
			var viewLayout = new StackLayout () {
				//WidthRequest = 40,
				//HeightRequest = 40,
				//HorizontalOptions = LayoutOptions.CenterAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness (2, 0, 0, 3),
				Children = { image },
				//BackgroundColor = Color.Red,
			};
			View = viewLayout;
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			Categories c = BindingContext as Categories;
			if (c.MapImageFullPath == "testRestCompressed.Resources.reset_mappa_60.png") {
				image.Source = ImageSource.FromResource ("testRestCompressed.Resources.reset_mappa_60.png");
			}
		}

	}
}


