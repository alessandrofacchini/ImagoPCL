using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using testRestCompressed;
using testRestCompressed.Droid;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics;
using xUtilityPCL;

[assembly: ExportRenderer (typeof(myNavPage), typeof(MyNavigationBarRenderer))]
namespace testRestCompressed.Droid
{


	public class MyNavigationBarRenderer: NavigationRenderer
	{

		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.NavigationPage> e)
		{

			Console.WriteLine ("...");

			base.OnElementChanged (e);
		
		}






		protected override void OnAttachedToWindow ()
		{
			base.OnAttachedToWindow ();

			return;


		}



		private String pagename = "";

		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);
			//Activity activity1 = Context as Activity;
			//activity1.ActionBar.SetDisplayShowHomeEnabled (false);

		}

	

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			//http://javatechig.com/android/actionbar-with-custom-view-example-in-android
			base.OnElementPropertyChanged (sender, e);
			pagename = "Page 1";
			if ((sender as NavigationPage).CurrentPage is Page2)
				pagename = "Page 2";

			Activity activity = Context as Activity;
			if (activity != null) {
				//////ShapeDrawable drawable = new ShapeDrawable (new RectShape ());
				//////drawable.SetShaderFactory (new NavigationShader ());
				//activity.ActionBar.SetBackgroundDrawable (drawable);
				//activity.ActionBar.SetDisplayUseLogoEnabled (true); //visualizza la proprietà logo del manifest
				activity.ActionBar.SetDisplayShowCustomEnabled (true);
				activity.ActionBar.SetDisplayShowTitleEnabled (false); //false

				//activity.ActionBar.SetIcon (Resource.Drawable.BSLogo);
				//activity.ActionBar.SetLogo (Resource.Drawable.BSLogo);

				Android.Views.View v = null;

				if ((sender as NavigationPage).CurrentPage is myimagomenu) {
					LayoutInflater inflator = (LayoutInflater)activity.GetSystemService (Context.LayoutInflaterService);

					Boolean ccf;
					ccf = Global.isCCF;
					//ccf = true;
					if (ccf) {
						v = inflator.Inflate (Resource.Layout.action_bar_Home_ADV, null);
						var logo = v.FindViewById<ImageView> (Resource.Id.myHomelogo);
						logo.Visibility = ViewStates.Visible;
						logo.SetPadding (10, 0, 10, 0);

						var logoCCF = v.FindViewById<ImageView> (Resource.Id.myHomelogo2);
						logoCCF.Visibility = ViewStates.Visible;
						logoCCF.SetPadding (0, 0, 0, 0);


					} else {
						v = inflator.Inflate (Resource.Layout.action_bar_Home, null);
						var logo = v.FindViewById<ImageView> (Resource.Id.myHomelogo);
						//logo.Visibility = ViewStates.Gone;
						logo.Visibility = ViewStates.Visible;
						logo.SetPadding (10, 0, 10, 0);
						//title.SetBackgroundColor (Android.Graphics.Color.Yellow);
						//title.SetPadding (20, 0, 0, 0);
						//android:scaleType="fitStart"
					}
				}
				if ((sender as NavigationPage).CurrentPage is PosList || (sender as NavigationPage).CurrentPage is PosDetail
				    || (sender as NavigationPage).CurrentPage is UICustomWebView || (sender as NavigationPage).CurrentPage is Map) {
					LayoutInflater inflator = (LayoutInflater)activity.GetSystemService (Context.LayoutInflaterService);
					v = inflator.Inflate (Resource.Layout.action_bar_AllPages2, null);
					var title = v.FindViewById<TextView> (Resource.Id.mymytitle);

					if ((sender as NavigationPage).CurrentPage is PosList)
						title.Text = ((sender as NavigationPage).CurrentPage as PosList).title;
					if ((sender as NavigationPage).CurrentPage is PosDetail)
						title.Text = ((sender as NavigationPage).CurrentPage as PosDetail).title;
					if ((sender as NavigationPage).CurrentPage is UICustomWebView)
						title.Text = "IMAGO WEB"; 
					if ((sender as NavigationPage).CurrentPage is Map)
						title.Text = "MAPPA"; 
					var back = v.FindViewById<ImageView> (Resource.Id.mymybackbutton);

					//centratura
					var w = xUtilityAndroid.Measures.getWidthDpi (activity);
					var density = xUtilityAndroid.Measures.getDensity (activity);
					var leftdip = (w) / 2;
					leftdip -= (back.LayoutParameters.Width / density); //immagine
					title.SetMargin (Convert.ToInt32 (leftdip * density), 0, 0, 0);
					//title.SetPadding (Convert.ToInt32 (leftdip * density), 0, 0, 0);
					Android.Graphics.Typeface tf = null;
					try {
						tf = Typeface.CreateFromAsset (activity.Assets, "FuturaStd-Medium.ttf");
					} catch (Exception es) {

					}

					if (tf != null) {
						var tfStyle = TypefaceStyle.Normal;

						if (null != title.Typeface)
							tfStyle = TypefaceStyle.Normal;
						title.SetTextColor (Android.Graphics.Color.Rgb (252, 97, 0));
						title.TextSize = 18;

						title.SetTypeface (tf, tfStyle);
					}



					var backbutton = v.FindViewById<ImageView> (Resource.Id.mymybackbutton);
					backbutton.Click += async delegate(object sender2, EventArgs e2) {
						activity.OnBackPressed ();
					};
					//title.SetBackgroundColor (Android.Graphics.Color.Yellow);
					//title.SetPadding (20, 0, 0, 0);
				}
				;



				/*
				if ((sender as NavigationPage).CurrentPage is Page1)
					title.Text = "Page 1";

				if ((sender as NavigationPage).CurrentPage is Page2)
					title.Text = "Page 2";
					*/

				/*
				if (activity.ActionBar.CustomView == null) {
					activity.ActionBar.CustomView = v;
				}
				activity.ActionBar.SetDisplayShowCustomEnabled (true);
				if ((sender as NavigationPage).CurrentPage is  myimagomenu) {
					activity.ActionBar.SetDisplayShowHomeEnabled (false);
					activity.ActionBar.SetDisplayHomeAsUpEnabled (false);
					activity.ActionBar.SetIcon (Resource.Drawable.transparent);
					activity.ActionBar.SetHomeButtonEnabled (false);
				} else {
					activity.ActionBar.SetDisplayShowHomeEnabled (true);
					activity.ActionBar.SetDisplayHomeAsUpEnabled (false);
					activity.ActionBar.SetIcon (Resource.Drawable.backButton);
					activity.ActionBar.SetHomeButtonEnabled (false);
				}

				return;
				*/


				activity.ActionBar.CustomView = v;
				
				activity.ActionBar.SetDisplayShowCustomEnabled (true);
				//LayoutParams lp = v.LayoutParameters;
				//lp.Width = LayoutParams.MatchParent;
				//v.LayoutParameters = lp;
				//activity.ActionBar.CustomView.SetPadding (-30, 0, 0, 0);
				//parent.SetContentInsetsAbsolute (0, 0);
				activity.ActionBar.CustomView.SetBackgroundColor (Android.Graphics.Color.White); // .Blue);
				//http://stackoverflow.com/questions/20172256/android-4-4-kitkat-custom-view-actionbar-not-filling-the-whole-width
				//http://stackoverflow.com/questions/16445560/align-left-edge-to-center-relativelayout

				if ((sender as NavigationPage).CurrentPage is  myimagomenu) {//|| (sender as NavigationPage).CurrentPage is  PosList) {
					//activity.ActionBar.SetDisplayShowHomeEnabled (false);
					//activity.ActionBar.SetDisplayHomeAsUpEnabled (false);
					//activity.ActionBar.SetIcon (new ColorDrawable (Android.Graphics.Color.Transparent));
					activity.ActionBar.SetIcon (null);
					//activity.ActionBar.SetHomeAsUpIndicator (null);
					//activity.ActionBar.SetLogo (null);	
					//activity.ActionBar.SetDisplayUseLogoEnabled (false);
					//activity.ActionBar.SetHomeButtonEnabled (false);
					//activity.ActionBar.SetDisplayUseLogoEnabled (false);
				} else {
					//activity.ActionBar.SetDisplayShowHomeEnabled (false);
					//activity.ActionBar.SetDisplayUseLogoEnabled (false);
					activity.ActionBar.SetDisplayHomeAsUpEnabled (false);
					activity.ActionBar.SetHomeAsUpIndicator (null);
					activity.ActionBar.SetIcon (null);
					//activity.ActionBar.SetHomeAsUpIndicator (null);
					//activity.ActionBar.SetIcon (Resource.Drawable.backButton);//.arrowleft); //replace the application icon
					//activity.ActionBar.SetDisplayUseLogoEnabled (false);
					//activity.ActionBar.SetHomeButtonEnabled (false);
					//activity.ActionBar.SetDisplayShowTitleEnabled (false);
					//activity.ActionBar.SetDisplayHomeAsUpEnabled (true);
					//activity.ActionBar.SetIcon (null);
					//activity.ActionBar.SetLogo (null);	

				}
				//activity.ActionBar.SetLogo (Resource.Drawable.BSLogo);
			
				//activity.ActionBar.SetBackgroundDrawable (new ColorDrawable (Android.Graphics.Color.Blue));
				//activity.ActionBar.SetStackedBackgroundDrawable (new ColorDrawable (Android.Graphics.Color.Blue));
				/*
				activity.ActionBar.SetHomeButtonEnabled (false);
				activity.ActionBar.SetDisplayUseLogoEnabled (false);
				activity.ActionBar.SetDisplayShowHomeEnabled (false);
				activity.ActionBar.SetDisplayHomeAsUpEnabled (false);
*/
				// Change specific items textcolor here
			}

		}

	}

}
