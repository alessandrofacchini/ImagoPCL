using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using testRestCompressed.Droid;
using testRestCompressed;
using Android.Graphics;


[assembly: ExportRenderer (typeof(BaseUrlWebView), typeof(BaseUrlWebViewRenderer))]
namespace testRestCompressed.Droid
{
	public class BaseUrlWebViewRenderer : WebViewRenderer
	{

	

		protected override void OnElementChanged (ElementChangedEventArgs<WebView> e)
		{
			base.OnElementChanged (e);
			if (Control != null) { 
				var s = Control.Settings;
				//s.FixedFontFamily = "AvenirLTStd";	
				var pclControl2 = (BaseUrlWebView)e.NewElement; 
				var xxxx = (pclControl2.Source as HtmlWebViewSource).Html;

				//NON va//Control.LoadDataWithBaseURL ("file:///android_asset/", xxxx, "text/html", "UTF-8", null);
				Control.LoadDataWithBaseURL (null, xxxx, "text/html", "UTF-8", null);
				Control.Touch += async delegate(object sender, TouchEventArgs e2) {
					if (pclControl2.IsCollapsed) {
						e2.Event.Action = Android.Views.MotionEventActions.Cancel;
						e2.Handled = true;
					} else {
						e2.Handled = false;
					}

				};

				//var paramss= Control.LayoutParameters;
				//paramss.Height = 600;

				//paramss.SetMargins (0, 1, 1, 1);

				//	Control.LayoutParameters = paramss;


				//Control.setma/

				Control.LayoutChange += delegate(object sender, LayoutChangeEventArgs e3) {
					if (pclControl2.IsCollapsed) {
						Control.ScrollTo (0, 0);
					} else {
						//if (e3.Bottom != Control.ContentHeight)
						//pclControl2.setH (Control.ContentHeight);
						var aa = e3.Bottom;
						var bb = e3.OldBottom;
						var paramss = Control.LayoutParameters;
						if (Control.ContentHeight != 0)
							paramss.Height = Control.ContentHeight;
						pclControl2.callBackItemSelected (Control.ContentHeight);
						//pclControl2.ContentHeight = Control.ContentHeight;
						//AbsoluteLayout.SetLayoutBounds (pclControl2, new Rectangle (0, 10, App.k_screenW - 25, Control.ContentHeight));
					}
				};



			
			}
		}



	}


}

