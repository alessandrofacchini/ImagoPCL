using System;
using Xamarin.Forms;
using testRestCompressed;
using testRestCompressed.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Graphics;

[assembly: ExportRenderer (typeof(imagoLabelRender), typeof(imageLabelAndroid))]

namespace testRestCompressed.Droid
{
	public class imageLabelAndroid:LabelRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged (e);
			var label = (TextView)Control; // for example

			var view = (imagoLabelRender)Element;

			if (!string.IsNullOrEmpty (view.FontName)) {
				Typeface font = Typeface.CreateFromAsset (Forms.Context.Assets, view.FontName);
				label.Typeface = font;

			}
		}
	}
}

