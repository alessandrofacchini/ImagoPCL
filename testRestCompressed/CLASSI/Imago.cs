using System;
using xUtilityPCL;
using System.Collections.Generic;
using PropertyChanged;

namespace testRestCompressed
{
	
	public partial class Categories : BaseModel
	{
		public Categories ()
		{
			this.Categories1 = new HashSet<Categories> ();
			this.Categories_POIs = new HashSet<Categories_POIs> ();
		}

		private int _iDCategory;

		public int IDCategory { 
			get { return _iDCategory; } 
			set {
				if (value != _iDCategory) {
					_iDCategory = value;
					OnPropertyChanged ("IDCategory");
				}
			} 
		}

		private string _nameOfTheCategory;

		public string NameOfTheCategory { 
			get { return _nameOfTheCategory; } 
			set {
				if (value != _nameOfTheCategory) {
					_nameOfTheCategory = value;
					OnPropertyChanged ("NameOfTheCategory");
				}
			} 
		}

		private string _icon;

		public string Icon { 
			get { return _icon; } 
			set {
				if (value != _icon) {
					_icon = value;
					OnPropertyChanged ("Icon");
				}
			} 
		}

		private Nullable<int> _iDLanguage;

		public Nullable<int> IDLanguage { 
			get { return _iDLanguage; } 
			set {
				if (value != _iDLanguage) {
					_iDLanguage = value;
					OnPropertyChanged ("IDLanguage");
				}
			} 
		}

		private Nullable<int> _iDMacroCategory;

		public Nullable<int> IDMacroCategory { 
			get { return _iDMacroCategory; } 
			set {
				if (value != _iDMacroCategory) {
					_iDMacroCategory = value;
					OnPropertyChanged ("IDMacroCategory");
				}
			} 
		}

		/*
		private bool _isEvents;

		public bool IsEvents { 
			get { return _isEvents; } 
			set {
				if (value != _isEvents) {
					_isEvents = value;
					OnPropertyChanged ("IsEvents");
				}
			} 
		}
		*/

		private Nullable<int> _iDCategoryFather;

		public Nullable<int> IDCategoryFather { 
			get { return _iDCategoryFather; } 
			set {
				if (value != _iDCategoryFather) {
					_iDCategoryFather = value;
					OnPropertyChanged ("IDCategoryFather");
				}
			} 
		}

		private string _icon2;

		public string Icon2 { 
			get { return _icon2; } 
			set {
				if (value != _icon2) {
					_icon2 = value;
					OnPropertyChanged ("Icon2");
				}
			} 
		}

		private string _icon3;

		public string Icon3 { 
			get { return _icon3; } 
			set {
				if (value != _icon3) {
					_icon3 = value;
					OnPropertyChanged ("Icon3");
				}
			} 
		}


		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual Languages Languages { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual MacroCategories MacroCategories { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual ICollection<Categories> Categories1 { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual Categories Categories2 { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual ICollection<Categories_POIs> Categories_POIs { get; set; }
	}

	public partial class Categories_POIs : BaseModel
	{
		private int _iDCategories_POIs;

		public int IDCategories_POIs { 
			get { return _iDCategories_POIs; } 
			set {
				if (value != _iDCategories_POIs) {
					_iDCategories_POIs = value;
					OnPropertyChanged ("IDCategories_POIs");
				}
			} 
		}

		private Nullable<int> _iDCategory;

		public Nullable<int> IDCategory { 
			get { return _iDCategory; } 
			set {
				if (value != _iDCategory) {
					_iDCategory = value;
					OnPropertyChanged ("IDCategory");
				}
			} 
		}

		private Nullable<int> _iDLanguage;

		public Nullable<int> IDLanguage { 
			get { return _iDLanguage; } 
			set {
				if (value != _iDLanguage) {
					_iDLanguage = value;
					OnPropertyChanged ("IDLanguage");
				}
			} 
		}

		private Nullable<int> _iDPOI;

		public Nullable<int> IDPOI { 
			get { return _iDPOI; } 
			set {
				if (value != _iDPOI) {
					_iDPOI = value;
					OnPropertyChanged ("IDPOI");
				}
			} 
		}


		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual Categories Categories { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual Languages Languages { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual POIs POIs { get; set; }
	}

	public partial class Languages : BaseModel
	{
		public Languages ()
		{
			this.Categories = new HashSet<Categories> ();
			this.MacroCategories = new HashSet<MacroCategories> ();
			this.Categories_POIs = new HashSet<Categories_POIs> ();
			this.POIs = new HashSet<POIs> ();
			this.POIsPictures = new HashSet<POIsPictures> ();
		}

		private int _iDLanguage;

		public int IDLanguage { 
			get { return _iDLanguage; } 
			set {
				if (value != _iDLanguage) {
					_iDLanguage = value;
					OnPropertyChanged ("IDLanguage");
				}
			} 
		}

		private string _nameOfTheLanguage;

		public string NameOfTheLanguage { 
			get { return _nameOfTheLanguage; } 
			set {
				if (value != _nameOfTheLanguage) {
					_nameOfTheLanguage = value;
					OnPropertyChanged ("NameOfTheLanguage");
				}
			} 
		}


		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual ICollection<Categories> Categories { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual ICollection<MacroCategories> MacroCategories { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual ICollection<Categories_POIs> Categories_POIs { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual ICollection<POIs> POIs { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual ICollection<POIsPictures> POIsPictures { get; set; }
	}

	public partial class MacroCategories : BaseModel
	{
		public MacroCategories ()
		{
			this.Categories = new HashSet<Categories> ();
		}

		private int _iDMacroCategory;

		public int IDMacroCategory { 
			get { return _iDMacroCategory; } 
			set {
				if (value != _iDMacroCategory) {
					_iDMacroCategory = value;
					OnPropertyChanged ("IDMacroCategory");
				}
			} 
		}

		private string _nameOfTheMacroCategory;

		public string NameOfTheMacroCategory { 
			get { return _nameOfTheMacroCategory; } 
			set {
				if (value != _nameOfTheMacroCategory) {
					_nameOfTheMacroCategory = value;
					OnPropertyChanged ("NameOfTheMacroCategory");
				}
			} 
		}

		private string _abbreviationOfTheMacroCategory;

		public string AbbreviationOfTheMacroCategory { 
			get { return _abbreviationOfTheMacroCategory; } 
			set {
				if (value != _abbreviationOfTheMacroCategory) {
					_abbreviationOfTheMacroCategory = value;
					OnPropertyChanged ("AbbreviationOfTheMacroCategory");
				}
			} 
		}

		private string _icon;

		public string Icon { 
			get { return _icon; } 
			set {
				if (value != _icon) {
					_icon = value;
					OnPropertyChanged ("Icon");
				}
			} 
		}

		private Nullable<int> _iDLanguage;

		public Nullable<int> IDLanguage { 
			get { return _iDLanguage; } 
			set {
				if (value != _iDLanguage) {
					_iDLanguage = value;
					OnPropertyChanged ("IDLanguage");
				}
			} 
		}

		/*
		private bool _isEvents;

		public bool IsEvents { 
			get { return _isEvents; } 
			set {
				if (value != _isEvents) {
					_isEvents = value;
					OnPropertyChanged ("IsEvents");
				}
			} 
		}
		*/

		private string _icon2;

		public string Icon2 { 
			get { return _icon2; } 
			set {
				if (value != _icon2) {
					_icon2 = value;
					OnPropertyChanged ("Icon2");
				}
			} 
		}

		private string _icon3;

		public string Icon3 { 
			get { return _icon3; } 
			set {
				if (value != _icon3) {
					_icon3 = value;
					OnPropertyChanged ("Icon3");
				}
			} 
		}


		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual ICollection<Categories> Categories { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual Languages Languages { get; set; }
	}

	public partial class POIs : BaseModel
	{
		public POIs ()
		{
			this.Categories_POIs = new HashSet<Categories_POIs> ();
			this.POIsPictures = new HashSet<POIsPictures> ();
		}

		private int _iDPOI;

		public int IDPOI { 
			get { return _iDPOI; } 
			set {
				if (value != _iDPOI) {
					_iDPOI = value;
					OnPropertyChanged ("IDPOI");
				}
			} 
		}

		private string _nameOfThePOI;

		public string NameOfThePOI { 
			get { return _nameOfThePOI; } 
			set {
				if (value != _nameOfThePOI) {
					_nameOfThePOI = value;
					OnPropertyChanged ("NameOfThePOI");
				}
			} 
		}

		private string _icon;

		public string Icon { 
			get { return _icon; } 
			set {
				if (value != _icon) {
					_icon = value;
					OnPropertyChanged ("Icon");
				}
			} 
		}

		private string _description;

		public string Description { 
			get { return _description; } 
			set {
				if (value != _description) {
					_description = value;
					OnPropertyChanged ("Description");
				}
			} 
		}

		private string _address;

		public string Address { 
			get { return _address; } 
			set {
				if (value != _address) {
					_address = value;
					OnPropertyChanged ("Address");
				}
			} 
		}

		private string _cAP;

		public string CAP { 
			get { return _cAP; } 
			set {
				if (value != _cAP) {
					_cAP = value;
					OnPropertyChanged ("CAP");
				}
			} 
		}

		private string _town;

		public string Town { 
			get { return _town; } 
			set {
				if (value != _town) {
					_town = value;
					OnPropertyChanged ("Town");
				}
			} 
		}

		private string _province;

		public string Province { 
			get { return _province; } 
			set {
				if (value != _province) {
					_province = value;
					OnPropertyChanged ("Province");
				}
			} 
		}

		private string _country;

		public string Country { 
			get { return _country; } 
			set {
				if (value != _country) {
					_country = value;
					OnPropertyChanged ("Country");
				}
			} 
		}

		private string _vATNumber;

		public string VATNumber { 
			get { return _vATNumber; } 
			set {
				if (value != _vATNumber) {
					_vATNumber = value;
					OnPropertyChanged ("VATNumber");
				}
			} 
		}

		private string _email;

		public string email { 
			get { return _email; } 
			set {
				if (value != _email) {
					_email = value;
					OnPropertyChanged ("email");
				}
			} 
		}

		private string _website;

		public string Website { 
			get { return _website; } 
			set {
				if (value != _website) {
					_website = value;
					OnPropertyChanged ("Website");
				}
			} 
		}

		private string _referencePerson;

		public string ReferencePerson { 
			get { return _referencePerson; } 
			set {
				if (value != _referencePerson) {
					_referencePerson = value;
					OnPropertyChanged ("ReferencePerson");
				}
			} 
		}

		private string _phone;

		public string Phone { 
			get { return _phone; } 
			set {
				if (value != _phone) {
					_phone = value;
					OnPropertyChanged ("Phone");
				}
			} 
		}

		private string _services;

		public string Services { 
			get { return _services; } 
			set {
				if (value != _services) {
					_services = value;
					OnPropertyChanged ("Services");
				}
			} 
		}

		private string _socialNetwork;

		public string SocialNetwork { 
			get { return _socialNetwork; } 
			set {
				if (value != _socialNetwork) {
					_socialNetwork = value;
					OnPropertyChanged ("SocialNetwork");
				}
			} 
		}

		private string _openingHours;

		public string OpeningHours { 
			get { return _openingHours; } 
			set {
				if (value != _openingHours) {
					_openingHours = value;
					OnPropertyChanged ("OpeningHours");
				}
			} 
		}

		private string _entrancePrice;

		public string EntrancePrice { 
			get { return _entrancePrice; } 
			set {
				if (value != _entrancePrice) {
					_entrancePrice = value;
					OnPropertyChanged ("EntrancePrice");
				}
			} 
		}

		private string _furtherInfo;

		public string FurtherInfo { 
			get { return _furtherInfo; } 
			set {
				if (value != _furtherInfo) {
					_furtherInfo = value;
					OnPropertyChanged ("FurtherInfo");
				}
			} 
		}

		private Nullable<decimal> _latitude;

		public Nullable<decimal> Latitude { 
			get { return _latitude; } 
			set {
				if (value != _latitude) {
					_latitude = value;
					OnPropertyChanged ("Latitude");
				}
			} 
		}

		private Nullable<decimal> _longitude;

		public Nullable<decimal> Longitude { 
			get { return _longitude; } 
			set {
				if (value != _longitude) {
					_longitude = value;
					OnPropertyChanged ("Longitude");
				}
			} 
		}

		/*
		private bool _canSleep;

		public bool CanSleep { 
			get { return _canSleep; } 
			set {
				if (value != _canSleep) {
					_canSleep = value;
					OnPropertyChanged ("CanSleep");
				}
			} 
		}
		*/

		private Nullable<System.DateTime> _entryDate;

		public Nullable<System.DateTime> EntryDate { 
			get { return _entryDate; } 
			set {
				if (value != _entryDate) {
					_entryDate = value;
					OnPropertyChanged ("EntryDate");
				}
			} 
		}

		private Nullable<System.DateTime> _openingDate;

		public Nullable<System.DateTime> OpeningDate { 
			get { return _openingDate; } 
			set {
				if (value != _openingDate) {
					_openingDate = value;
					OnPropertyChanged ("OpeningDate");
				}
			} 
		}

		private Nullable<System.DateTime> _closingDate;

		public Nullable<System.DateTime> ClosingDate { 
			get { return _closingDate; } 
			set {
				if (value != _closingDate) {
					_closingDate = value;
					OnPropertyChanged ("ClosingDate");
				}
			} 
		}

		private string _howToArrive;

		public string HowToArrive { 
			get { return _howToArrive; } 
			set {
				if (value != _howToArrive) {
					_howToArrive = value;
					OnPropertyChanged ("HowToArrive");
				}
			} 
		}

		private Nullable<int> _iDLanguage;

		public Nullable<int> IDLanguage { 
			get { return _iDLanguage; } 
			set {
				if (value != _iDLanguage) {
					_iDLanguage = value;
					OnPropertyChanged ("IDLanguage");
				}
			} 
		}

		private string _presentationPicture;

		public string PresentationPicture { 
			get { return _presentationPicture; } 
			set {
				if (value != _presentationPicture) {
					_presentationPicture = value;
					OnPropertyChanged ("PresentationPicture");
				}
			} 
		}

		private string _presentationPictureDescription;

		public string PresentationPictureDescription { 
			get { return _presentationPictureDescription; } 
			set {
				if (value != _presentationPictureDescription) {
					_presentationPictureDescription = value;
					OnPropertyChanged ("PresentationPictureDescription");
				}
			} 
		}

		/*
		private Boolean _isEvents;

		public Boolean IsEvents { 
			get { return _isEvents; } 
			set {
				if (this.IDPOI == 96) {
					Int32 www = 1;
				}
				if (value != _isEvents) {
					_isEvents = value;
					OnPropertyChanged ("IsEvents");
				}
			} 
		}
		*/


		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual ICollection<Categories_POIs> Categories_POIs { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual Languages Languages { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual ICollection<POIsPictures> POIsPictures { get; set; }
	}

	public partial class POIsPictures : BaseModel
	{
		private int _iDPOIsPictures;

		public int IDPOIsPictures { 
			get { return _iDPOIsPictures; } 
			set {
				if (value != _iDPOIsPictures) {
					_iDPOIsPictures = value;
					OnPropertyChanged ("IDPOIsPictures");
				}
			} 
		}

		private string _picture;

		public string Picture { 
			get { return _picture; } 
			set {
				if (value != _picture) {
					_picture = value;
					OnPropertyChanged ("Picture");
				}
			} 
		}

		private string _description;

		public string Description { 
			get { return _description; } 
			set {
				if (value != _description) {
					_description = value;
					OnPropertyChanged ("Description");
				}
			} 
		}

		private Nullable<int> _iDLanguage;

		public Nullable<int> IDLanguage { 
			get { return _iDLanguage; } 
			set {
				if (value != _iDLanguage) {
					_iDLanguage = value;
					OnPropertyChanged ("IDLanguage");
				}
			} 
		}

		private Nullable<int> _iDPOI;

		public Nullable<int> IDPOI { 
			get { return _iDPOI; } 
			set {
				if (value != _iDPOI) {
					_iDPOI = value;
					OnPropertyChanged ("IDPOI");
				}
			} 
		}


		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual Languages Languages { get; set; }

		[Newtonsoft.Json.JsonIgnore, SQLite.Net.Attributes.Ignore]public virtual POIs POIs { get; set; }
	}

	public class CCF:BaseModel
	{
		private string _nameOfVillage;

		public string NameOfVillage { 
			get { return _nameOfVillage; } 
			set {
				if (value != _nameOfVillage) {
					_nameOfVillage = value;
					OnPropertyChanged ("NameOfVillage");
				}
			} 
		}

		private double _Distance;

		public double Distance { 
			get { return _Distance; } 
			set {
				if (value != _Distance) {
					_Distance = value;
					OnPropertyChanged ("Distance");
				}
			} 
		}

	}


}

