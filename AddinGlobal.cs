﻿using Path = System.IO.Path;
using System.Reflection;

namespace SheetList
{
	public class AddinGlobal
	{
		public static Inventor.Application InventorApp { get; set; }

		public static string SettingsFilePath { get; set; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json");

		public static string AppId { get; } = "5342673156831821071";

		public static SheetListSettings AppSettings { get; set; }

		public static SheetListAutomation Automation { get; set; }
    }
}