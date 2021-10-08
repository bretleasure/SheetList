using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using iAD.Utilities;
using Path = System.IO.Path;
using System.Reflection;

namespace SheetList
{
	public class AddinGlobal
	{
		public static Inventor.Application InventorApp;

		public static DrawingDocument oDwgDoc;

		public static string SettingsFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json");

		public static string AppId = "5342673156831821071";

		public static SheetList_Settings AppSettings;

		

	}
}
