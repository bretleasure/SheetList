using System.Collections.Generic;
using Path = System.IO.Path;
using System.Reflection;
using Inventor;
using SheetList.Buttons;

namespace SheetList
{
	internal class AddinGlobal
	{
		public static Inventor.Application InventorApp { get; set; }
		public static string SettingsFilePath { get; set; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json");
		public static SheetListAddinSettings AppSettings { get; set; }
		public static SheetListAutomation Automation { get; set; }
		public static Theme ActiveTheme => InventorApp.ThemeManager.ActiveTheme;
		public static List<InventorButton> Buttons { get; set; }
    }
}
