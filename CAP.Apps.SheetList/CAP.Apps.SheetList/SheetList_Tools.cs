using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using CAP.Utilities;
using System.IO;

namespace CAP.Apps.SheetList
{
	public abstract class SheetList_Tools
    {
        public static void Get_SavedSettings()
		{
			string SettingsFilePath = AddinGlobal.AppFolder + AddinGlobal.SettingsFile;

			if (System.IO.File.Exists(SettingsFilePath))
			{
				SheetList_Settings oSettings = new SheetList_Settings();
				oSettings = (SheetList_Settings)XMLTools.Get_ObjectFromXML(SettingsFilePath, oSettings);

				AddinGlobal.AppSettings = oSettings;
			}			

		}

		public static void Clear_SheetList(CustomTable table)
		{
			foreach (Row r in table.Rows)
			{

				r.Delete();
			}
		}

		public static string Get_SheetName(Sheet oSheet)
		{
			string name = oSheet.Name;

			return oSheet.Name.Substring(0, name.IndexOf(":"));
		}

		public static bool SheetListExists()
		{
			bool Exists = false;

			AddinGlobal.oDwgDoc = (DrawingDocument)AddinGlobal.InventorApp.ActiveDocument;

            foreach (CustomTable table in AddinGlobal.oDwgDoc.Sheets[1].CustomTables)
            {
                foreach (AttributeSet oSet in table.AttributeSets)
                {
                    if (oSet.Name == "Table_id")
                    {
                        if (oSet.NameIsUsed["Name"])
                        {
                            if (oSet["Name"].Value == "SHEET LIST")
                            {
                                Exists = true;
                                goto BreakOut;                              
                            }
                        }
                    }
                }
            }

            BreakOut:

            return Exists;
		}

		public static CustomTable Get_SheetList()
		{

			CustomTable oTable = null;

            if (SheetListExists())
            {
                foreach (CustomTable table in AddinGlobal.oDwgDoc.Sheets[1].CustomTables)
                {
                    try
                    {
                        if (table.AttributeSets["Table_id"]["Name"].Value == "SHEET LIST")
                            oTable = table;
                    }
                    catch
                    {
                        //shitty I know...
                    }
                }
            }

			if (oTable != null)
				return oTable;
			else
			{
				return Create_NewSheetList();
			}

		}

		public static CustomTable Create_NewSheetList()
		{
			SheetList_Settings oSL = AddinGlobal.AppSettings;

			string[] col = new string[] { oSL.SheetNoColName, oSL.SheetNameColName };

			TransientGeometry oTG = AddinGlobal.InventorApp.TransientGeometry;
			Point2d loc = oTG.CreatePoint2d(AddinGlobal.oDwgDoc.ActiveSheet.Width / 2, AddinGlobal.oDwgDoc.ActiveSheet.Height / 2);

			CustomTable newTable = AddinGlobal.oDwgDoc.Sheets[1].CustomTables.Add(oSL.Title, loc, 2, 0, col);

			//Assign Table ID
			newTable.AttributeSets.Add("Table_id", true).Add("Name", ValueTypeEnum.kStringType, "SHEET LIST");

			newTable = Get_SheetListSettings(newTable);

			return newTable;
		}

		public static CustomTable Get_SheetListSettings(CustomTable table)
		{
			SheetList_Settings oSL = AddinGlobal.AppSettings;

			table.Columns[1].Title = oSL.SheetNoColName;
			table.Columns[2].Title = oSL.SheetNameColName;
			table.ShowTitle = oSL.ShowTitle;
			table.Title = oSL.Title;
			table.TableDirection = oSL.Direction;
			table.HeadingPlacement = oSL.HeadingPlacement;
			table.WrapAutomatically = oSL.EnableAutoWrap;
			table.WrapLeft = oSL.WrapLeft;
			table.MaximumRows = oSL.MaxRows;
			if (oSL.NumberOfSections != 0)
				table.NumberOfSections = oSL.NumberOfSections;

			return table;
		}

		public static int Get_TotalSheets()
		{
			int Total = 0;

			foreach (Sheet oSheet in AddinGlobal.oDwgDoc.Sheets)
			{
				if (!oSheet.ExcludeFromCount)
					Total++;
			}

			return Total;
		}

		public static SheetList_Settings Get_SavedSheetListObject()
		{
			SheetList_Settings oSheetList = new SheetList_Settings();

			try
			{
				oSheetList = (SheetList_Settings)XMLTools.Get_ObjectFromXML(AddinGlobal.AppFolder + AddinGlobal.SettingsFile, oSheetList);
			}
			catch
			{
				oSheetList = null;
			}

			return oSheetList;
		}

		public static void CreateUpdateEventListener()
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
					if (SheetListExists())
					{
						SheetList_ButtonEvents.CreateUpdate_SheetList();
					}
				}
			}

			HandlingCode = HandlingCodeEnum.kEventHandled;
		}

		public static void CheckForAppfolder()
		{
			if (!System.IO.Directory.Exists(AddinGlobal.AppFolder))
			{
				DirectoryInfo di = System.IO.Directory.CreateDirectory(AddinGlobal.AppFolder);
				di.Attributes = FileAttributes.Hidden;
			}
		}
	}
}
