using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using iAD.Utilities;
using System.IO;
using File = System.IO.File;
using Newtonsoft.Json;
using SheetList.Extensions;

namespace SheetList
{
	public abstract class SheetListTools
    {
        public static void LoadSavedSettings()
		{
			if (System.IO.File.Exists(AddinGlobal.SettingsFilePath))
			{
				var settingsJson = File.ReadAllText(AddinGlobal.SettingsFilePath);
				AddinGlobal.AppSettings = JsonConvert.DeserializeObject<SheetListSettings>(settingsJson);
			}
            else
            {
				AddinGlobal.AppSettings = SheetListSettings.Default;
				SaveSettings();
            }
		}

		public static void SaveSettings()
        {
			var json = JsonConvert.SerializeObject(AddinGlobal.AppSettings);
			File.WriteAllText(AddinGlobal.SettingsFilePath, json);
        }

		public static void CreateEventListener()
		{
			if (AddinGlobal.AppSettings != null)
			{
				if (AddinGlobal.AppSettings.UpdateBeforeSave)
					AddinGlobal.InventorApp.ApplicationEvents.OnSaveDocument += ApplicationEvents_OnSaveDocument;
				else
					AddinGlobal.InventorApp.ApplicationEvents.OnSaveDocument -= ApplicationEvents_OnSaveDocument;
			}			
		}

		private static void ApplicationEvents_OnSaveDocument(_Document documentObject, EventTimingEnum beforeOrAfter, NameValueMap context, out HandlingCodeEnum handlingCode)
		{
			if (beforeOrAfter == EventTimingEnum.kBefore)
			{
				if (documentObject is DrawingDocument dwgDoc && dwgDoc.GetExistingSheetList() != null)
				{
					AddinGlobal.Automation.CreateSheetList(AddinGlobal.AppSettings);
				}
			}

			handlingCode = HandlingCodeEnum.kEventHandled;
		}
	}
}
