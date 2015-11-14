using System;
using Xamarin.Forms;

namespace testRestCompressed
{
	public class imagoLabelRender:Label
	{
		public string FontName { get; set; }
	}

	public class BaseUrlWebView : WebView
	{
		public Boolean IsCollapsed { get; set; }


		public Int32 ContentHeight { get; set; }

		public Action<Int32> callBackItemSelected { get; set; }

		public void setH (Int32 dip)
		{
			AbsoluteLayout.SetLayoutBounds (this, new Rectangle (0, 10, App.k_screenW - 25, dip));
		}
	}
}

