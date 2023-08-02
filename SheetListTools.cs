using Inventor;
using File = System.IO.File;
using Newtonsoft.Json;
using SheetList.Extensions;

namespace SheetList
{
	internal static class SheetListTools
    {
        public static void LoadSavedSettings()
		{
			if (System.IO.File.Exists(AddinGlobal.SettingsFilePath))
			{
				var settingsJson = File.ReadAllText(AddinGlobal.SettingsFilePath);
				AddinGlobal.AppSettings = JsonConvert.DeserializeObject<AddinSettings>(settingsJson);
			}
            else
			{
				AddinGlobal.AppSettings = new AddinSettings
				{
					SheetListSettings = SheetListSettings.Default
				};
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
				if (documentObject is DrawingDocument dwgDoc && AddinGlobal.AppSettings.UpdateBeforeSave && dwgDoc.GetExistingSheetList() != null)
                {
                    AddinGlobal.Automation.UpdateSheetList(AddinGlobal.AppSettings.SheetListSettings, dwgDoc);
                }
			}

			handlingCode = HandlingCodeEnum.kEventHandled;
		}
	}
}
