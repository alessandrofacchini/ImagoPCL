using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using testRestCompressed.iOS;
using testRestCompressed;
using UIKit;

[assembly: ExportRenderer (typeof(BaseUrlWebView), typeof(BaseUrlWebViewRenderer))]
namespace testRestCompressed.iOS
{
	public class BaseUrlWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
			var pclControl = (BaseUrlWebView)e.NewElement; 
			var html = (pclControl.Source as HtmlWebViewSource).Html; 

			this.Delegate = new browserDelegate (pclControl);
		

		}

		private  class browserDelegate : UIWebViewDelegate
		{
			BaseUrlWebView pclControl;

			public browserDelegate (BaseUrlWebView _pclControl)
			{
				pclControl = _pclControl;
			}

			public override void LoadingFinished (UIKit.UIWebView webView)
			{

				var HeightHtml = webView.ScrollView.ContentSize.Height;
				pclControl.ContentHeight = (int)HeightHtml;

				webView.ScrollView.ScrollEnabled = false;
				//webView.LoadHtmlString ((pclControl.Source as HtmlWebViewSource).Html, null);
			}
		}


	}
}

