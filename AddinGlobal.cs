using Path = System.IO.Path;
using System.Reflection;

namespace SheetList
{
	internal class AddinGlobal
	{
		public static Inventor.Application InventorApp { get; set; }
		public static string SettingsFilePath { get; set; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json");
		public static AddinSettings AppSettings { get; set; }
		public static SheetListAutomation Automation { get; set; }
    }
}
