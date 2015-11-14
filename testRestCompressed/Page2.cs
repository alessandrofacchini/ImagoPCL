using System;

using Xamarin.Forms;

namespace testRestCompressed
{
	public class Page2 : ContentPage
	{
		public Page2 ()
		{
			/*
			NavigationPage.SetTitleIcon (this, "BSLogo.png");
		
			this.Title = "titolo";


			ToolbarItems.Add (new ToolbarItem (){ Icon = "BSLogo.png" });
*/

			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage2" }
				}
			};
		}
	}



}


