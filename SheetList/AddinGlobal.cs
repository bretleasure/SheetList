using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using iAD.Utilities;

namespace SheetList
{
	public class AddinGlobal
	{
		public static Inventor.Application InventorApp;

		public static DrawingDocument oDwgDoc;

		public static string AppFolder = Tools.GetAppFolder("SheetList");

		public static string SettingsFile = Tools.GetHexString("SheetList Settings") + ".xml";

		public static string AppId = "5342673156831821071";

		public static SheetList_Settings AppSettings;

		

	}
}
