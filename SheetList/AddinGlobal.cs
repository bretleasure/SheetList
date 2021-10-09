using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using iAD.Utilities;
using Path = System.IO.Path;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace SheetList
{
	public class AddinGlobal
	{
		public static Inventor.Application InventorApp { get; set; }

		public static DrawingDocument oDwgDoc { get; set; }

		public static string SettingsFilePath { get; set; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json");

		public static string AppId { get; } = "5342673156831821071";

		public static SheetListSettings AppSettings { get; set; }

        public static ILogger Logger { get; internal set; }
    }
}
