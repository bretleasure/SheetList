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

namespace SheetList
{
	public abstract class Tools
    {
		private static readonly string TableAttributeSetName = "Table_id";
		private static readonly string AttributeName = "Name";
		private static readonly string TableId = "SHEET LIST";

        public static void GetSavedSettings()
		{
			if (System.IO.File.Exists(AddinGlobal.SettingsFilePath))
			{
				var settingsJson = File.ReadAllText(AddinGlobal.SettingsFilePath);
				AddinGlobal.AppSettings = JsonConvert.DeserializeObject<SheetListSettings>(settingsJson);
			}
		}

		public static void SaveSettings()
        {
			var json = JsonConvert.SerializeObject(AddinGlobal.AppSettings);
			File.WriteAllText(AddinGlobal.SettingsFilePath, json);
        }

		public static void Clear_SheetList(CustomTable table)
		{
			table.Rows.Cast<Row>()
				.ToList()
				.ForEach(row => row.Delete());
		}

		public static string GetSheetName(Sheet oSheet)
		{
			return oSheet.Name.Split(':').First();
		}

		public static string GetSheetNumber(Sheet sheet)
        {
			return sheet.Name.Split(':').Last();
        }

		public static bool TryGetExistingSheetList(out CustomTable sheetList)
        {
			sheetList = GetExistingSheetList();
			return sheetList != null;
        }

		public static double GetSheetListHeight(CustomTable sheetList)
        {
			return sheetList.Rows.Cast<Row>()
				.Sum(row => row.Height);
		}

		public static string[] GetSheetListData()
		{
			var sheetsToAdd = AddinGlobal.oDwgDoc.Sheets.Cast<Sheet>()
				.Where(sh => !sh.ExcludeFromCount);

			var eachSheetArray = sheetsToAdd
				.Select(s => new string[] { GetSheetNumber(s), GetSheetName(s) });

			var totalArray = eachSheetArray
				.SelectMany(value => value)
				.ToArray();

			return totalArray;
        }

		public static CustomTable GetExistingSheetList()
        {
			var sheet1Tables = AddinGlobal.oDwgDoc.Sheets[1].CustomTables.Cast<CustomTable>();
			return sheet1Tables.FirstOrDefault(t => t.AttributeSets.Cast<AttributeSet>()
				.Where(set => set.Name == TableAttributeSetName)
				.Any(n => n[AttributeName]?.Value.ToString() == TableId));
		}

		public static CustomTable CreateSheetList(Point2d position, string[] tableData, double[] columnWidths)
		{
			SheetListSettings settings = AddinGlobal.AppSettings;

			string[] columnNames = new string[] { settings.SheetNoColName, settings.SheetNameColName };

			CustomTable newTable = AddinGlobal.oDwgDoc.Sheets[1].CustomTables.Add(settings.Title, position, 2, tableData.Count() / columnNames.Count(), columnNames, 
				Contents: tableData, ColumnWidths: columnWidths);

			//Assign Table ID
			newTable.AttributeSets.Add(TableAttributeSetName, true).Add(AttributeName, ValueTypeEnum.kStringType, TableId);

			newTable = ApplySheetListSettings(newTable);
			
			return newTable;
		}

		public static CustomTable ApplySheetListSettings(CustomTable table)
		{
			SheetListSettings settings = AddinGlobal.AppSettings;

			table.Columns[1].Title = settings.SheetNoColName;
			table.Columns[2].Title = settings.SheetNameColName;
			table.ShowTitle = settings.ShowTitle;
			table.Title = settings.Title;
			table.TableDirection = settings.Direction;
			table.HeadingPlacement = settings.HeadingPlacement;
			table.WrapAutomatically = settings.EnableAutoWrap;
			table.WrapLeft = settings.WrapLeft;
			table.MaximumRows = settings.MaxRows;
			if (settings.NumberOfSections != 0)
				table.NumberOfSections = settings.NumberOfSections;

			return table;
		}

		public static int GetActiveSheets()
		{
			return AddinGlobal.oDwgDoc.Sheets.Cast<Sheet>()
				.Where(s => !s.ExcludeFromCount)
				.Count();
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

		private static void ApplicationEvents_OnSaveDocument(_Document DocumentObject, EventTimingEnum BeforeOrAfter, NameValueMap Context, out HandlingCodeEnum HandlingCode)
		{
			if (BeforeOrAfter == EventTimingEnum.kBefore)
			{
				if (DocumentObject.DocumentType == DocumentTypeEnum.kDrawingDocumentObject)
				{
					AddinGlobal.oDwgDoc = (DrawingDocument)AddinGlobal.InventorApp.ActiveDocument;

					if (TryGetExistingSheetList(out _))
					{
						ButtonEvents.CreateUpdate_SheetList();
					}
				}
			}

			HandlingCode = HandlingCodeEnum.kEventHandled;
		}
	}
}
