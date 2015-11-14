using System;

using Xamarin.Forms;
using RestSharp.Portable;
using System.Net.Http;
using RestSharp.Portable.Encodings;
using System.IO;
using System.Threading.Tasks;

//using xAttivitaNEW;
using Ionic.Zlib;

namespace testRestCompressed
{
	public class App : Application
	{

		public static float k_screenW{ get; set; }

		public static float k_screenH{ get; set; }

		public static float k_screenHMinusNavigationBarBottomBar{ get { return k_screenH - 40 - 25; } set { } }

		public static float k_Density { get; set; }

		public App ()
		{
			// The root page of your application

			/*
			Task.Run (async () => {
				string url = "http://appserver.anagrafecaninarer.it/xAttivitaWEBAPI/api/";//xUtilityPCL.Global.BaseURL; 
				var client = new RestClient (url);
				client.AddEncoding ("DEFLATE", new  DeflateEncoding ());
				string req = "AnagraficaClientis/getClientByID";
				var request = new RestRequest (req, HttpMethod.Get);
				Parameter p1 = new Parameter ();
				p1.Name = "idAgente";
				p1.Value = 5;
				Parameter p2 = new Parameter ();
				p2.Name = "where";
				p2.Value = "";
				Parameter p3 = new Parameter ();
				p3.Name = "maxNum";
				p3.Value = 300;
				Parameter p4 = new Parameter ();
				p4.Name = "lSincronizzaContatti";
				p4.Value = true;
				Parameter p5 = new Parameter ();
				p5.Name = "lSincronizzaRaccolta";
				p5.Value = true; 
				Parameter p6 = new Parameter ();
				p6.Name = "lSincronizzaAttivita";
				p6.Value = true; 
				request.AddParameter (p1);
				request.AddParameter (p2);
				request.AddParameter (p3);
				request.AddParameter (p4);
				request.AddParameter (p5);
				request.AddParameter (p6);
				try {
					var r = await client.Execute<RootObjectBindable> (request);
					Int32 b = 1;
				} catch (Exception ex) {
					Int32 a = 1;
				}
			});


*/

			var p = new myimagomenu ();

			myNavPage np = new myNavPage (p);


			this.MainPage = np;

			/*
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
				}
			};
			*/
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
			//20150702 inizio
			if (Device.OS == TargetPlatform.Android) {
				try {
					myimagomenu m = (this.MainPage as myNavPage).CurrentPage as myimagomenu;
					m.AlignSpringboardLabels ();
				} catch (Exception ex) {
				}
				//20150702 fine
			}
		}



	}


	public class DeflateEncoding : IEncoding
	{
		/// <summary>
		/// Decode the content
		/// </summary>
		/// <param name="data">Content to decode</param>
		/// <returns>Decoded content</returns>
		public byte[] Decode (byte[] data)
		{
			var output = new MemoryStream ();
			var input = new MemoryStream (data);
			/*
			using (var stream = new System.IO.Compression.DeflateStream (input, System.IO.Compression.CompressionMode.Decompress))
				stream.CopyTo (output);
			return output.ToArray ();
			*/


			using (var stream = new DeflateStream (input, CompressionMode.Decompress))
				stream.CopyTo (output);
			return output.ToArray ();

		}
	}

	public class myNavPage: NavigationPage
	{

		public  myNavPage (Page root) : base (root)
		{
			//appare con icona sulla dx della navigation bar
			var i = new ToolbarItem ();
			i.Icon = "mysearch.png";
			//this.ToolbarItems.Add (i);
			this.BarBackgroundColor = Color.White;
			//this.BarTextColor = Color.Red;

		}

	}
}

