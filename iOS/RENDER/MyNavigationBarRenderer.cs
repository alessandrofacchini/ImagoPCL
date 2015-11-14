using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Xamarin.Forms;
using testRestCompressed.iOS;
using testRestCompressed;

[assembly: ExportRenderer (typeof(myNavPage), typeof(MyNavigationBarRenderer))]
namespace testRestCompressed.iOS
{
	public class MyNavigationBarRenderer:NavigationRenderer
	{

		Boolean IsMenu = false;
		UILabel viewTitle;
		UIImageView viewCCF;
		UIImageView viewBackButton;
		float backButtonWidth = 60;

		public MyNavigationBarRenderer ()
		{
			Console.WriteLine ("...");
			//UIApplication.SharedApplication.SetStatusBarHidden (false, true);
			//UIApplication.SharedApplication.SetStatusBarStyle (UIStatusBarStyle.Default, true);
			//this.NavigationBar.Translucent = false;
		}

		private void createViewTitle ()
		{
			//viewTitle = new UILabel (new CoreGraphics.CGRect (0, 5, UIScreen.MainScreen.Bounds.Width, 30));
			nfloat w = UIScreen.MainScreen.Bounds.Width;
			viewTitle = new UILabel (new CoreGraphics.CGRect ((w / 2), 5, w / 2, 30));
			viewTitle.Hidden = true;
			viewTitle.Lines = 1;
			string fontName = "FuturaStd-Medium";
			var customFont = UIFont.FromName (fontName, 14);
			viewTitle.Font = customFont;//UIFont.SystemFontOfSize (15);
		
			//viewTitle.TextAlignment = UITextAlignment.Center;
			viewTitle.TextColor = UIColor.FromRGB (252, 97, 0);
			viewTitle.BackgroundColor = UIColor.Clear;
			viewTitle.Text = "TITLE";
			this.NavigationBar.AddSubview (viewTitle);
		}




		


		private void createViewCCF ()
		{
			this.viewCCF = new UIImageView ();
			this.viewCCF.Image = UIImage.FromFile ("Image/CCF_logo");
			this.viewCCF.Tag = 1234;
			float with = 150; 
			this.viewCCF.Frame = new CoreGraphics.CGRect (UIScreen.MainScreen.Bounds.Width - (with + 5), 0, with, 30);
			this.NavigationBar.AddSubview (this.viewCCF);
		}

		private void createViewBackButton ()
		{
			
			this.viewBackButton = new UIImageView ();
			this.viewBackButton.Image = UIImage.FromFile ("Image/backButton.png");
			this.viewBackButton.Tag = 1235;
			//float with = 60;
			this.viewBackButton.Frame = new CoreGraphics.CGRect (20, 10, backButtonWidth, 30);
			this.viewBackButton.UserInteractionEnabled = true;
			var tapRecognizer = new UITapGestureRecognizer (delegate(UITapGestureRecognizer obj) {
				//ViewController.ParentViewController.NavigationController.PopViewController (true);
				var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController.ChildViewControllers [0];
				var navcontroller = rootController as UINavigationController;
				if (navcontroller != null)
					rootController = navcontroller.PopViewController (true);
			});
			viewBackButton.AddGestureRecognizer (tapRecognizer);




			/*
			ViewController.ParentViewController.NavigationItem.SetLeftBarButtonItem (
				new UIBarButtonItem (UIImage.FromFile ("Image/backButton.png")
					, UIBarButtonItemStyle.Plain
					, (sender, args) => {
					this.NavigationController.PopViewController (true);
				})
				, true);
				*/
			this.NavigationBar.AddSubview (this.viewBackButton);

		}



		protected override System.Threading.Tasks.Task<bool> OnPushAsync (Page page, bool animated)
		{
			if (page is myimagomenu) {
				IsMenu = true;
			} else {
				IsMenu = false;
				if (page is PosList)
					viewTitle.Text = (page as PosList).title;
				if (page is PosDetail)
					viewTitle.Text = (page as PosDetail).title;
				//viewTitle.Text = "page title";
			}
			styleNav (IsMenu);

			return base.OnPushAsync (page, animated);
		}




		public override UIViewController PopViewController (bool animated)
		{
			var b = base.PopViewController (animated);
			var page = (base.Element as NavigationPage).CurrentPage;

			if (page is PosList) {
				//seconda pagina prima del menu
				if ((page as PosList).poisAroundMe)
					IsMenu = false;
				else
					IsMenu = true;
			} else
				IsMenu = false;

			styleNav (IsMenu);

			return b;

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationBar.TintColor = UIColor.White;
			this.NavigationBar.SetBackgroundImage (UIImage.FromFile ("Image/logobar"), UIBarMetrics.Default);
			//NavigationItem.SetHidesBackButton (true, true);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
			/*
			if ((e.NewElement as NavigationPage).CurrentPage is myimagomenu) {
				IsMenu = true;
			} else {
				IsMenu = false;
			}
			*/
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		
		}

		private void styleNav (bool ismenu)
		{

			//this.NavigationItem.SetHidesBackButton (true, true);
			//if (this.NavigationBar.BackItem != null)
			//this.NavigationBar.BackItem.SetHidesBackButton (true, true);


			if (viewTitle == null) {
				this.createViewTitle ();
			}
			//var viewCCF = this.NavigationBar.ViewWithTag (1234);

			if (viewCCF == null) {
				createViewCCF ();
			}

			if (this.viewBackButton == null) {
				createViewBackButton ();
			}


			if (ismenu) {
				this.NavigationBar.SetBackgroundImage (UIImage.FromFile ("Image/logobar"), UIBarMetrics.Default);
				this.viewCCF.Hidden = false;
				this.viewTitle.Hidden = true;
				this.viewBackButton.Hidden = true;

			} else {
				this.NavigationBar.SetBackgroundImage (null, UIBarMetrics.Default);
				this.viewCCF.Hidden = true;
				this.viewTitle.Hidden = false;
				this.viewBackButton.Hidden = false;
			}
		}




	}
}

