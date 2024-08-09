using System.Linq;
using System.Threading.Tasks;
using Inventor;
using File = System.IO.File;
using Newtonsoft.Json;

namespace SheetList
{
	internal static class SheetListTools
    {
        public static void LoadSavedSettings()
		{
			if (System.IO.File.Exists(AddinServer.SettingsFilePath))
			{
				var settingsJson = File.ReadAllText(AddinServer.SettingsFilePath);
				AddinServer.AppSettings = JsonConvert.DeserializeObject<SheetListAddinSettings>(settingsJson);
				
				//Reset to default if no columns are set
				if (AddinServer.AppSettings.SheetListSettings.ColumnPropertyData == null || !AddinServer.AppSettings.SheetListSettings.ColumnPropertyData.Any())
				{
					AddinServer.AppSettings.SheetListSettings = SheetListSettings.Default;
				}
			}
            else
			{
				AddinServer.AppSettings = new SheetListAddinSettings
				{
					SheetListSettings = SheetListSettings.Default
				};
				SaveSettings();
            }
		}
        
        public static void SaveSettings()
        {
			var json = JsonConvert.SerializeObject(AddinServer.AppSettings);
			File.WriteAllText(AddinServer.SettingsFilePath, json);
        }

		public static void CreateEventListener()
		{
			if (AddinServer.AppSettings != null)
			{
				if (AddinServer.AppSettings.UpdateBeforeSave)
					AddinServer.InventorApp.ApplicationEvents.OnSaveDocument += ApplicationEvents_OnSaveDocument;
				else
					AddinServer.InventorApp.ApplicationEvents.OnSaveDocument -= ApplicationEvents_OnSaveDocument;
			}			
		}

		private static void ApplicationEvents_OnSaveDocument(_Document documentObject, EventTimingEnum beforeOrAfter, NameValueMap context, out HandlingCodeEnum handlingCode)
		{
			if (beforeOrAfter == EventTimingEnum.kBefore)
			{
				if (documentObject is DrawingDocument dwgDoc && AddinServer.AppSettings.UpdateBeforeSave && dwgDoc.TryGetExistingSheetList(out var existingSheetList))
				{
					_ = AddinServer.AppAutomation.UpdateSheetList(existingSheetList, AddinServer.AppSettings.SheetListSettings, dwgDoc).Result;
				}
			}

			handlingCode = HandlingCodeEnum.kEventHandled;
		}
	}
}
