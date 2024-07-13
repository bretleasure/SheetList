using System;
using System.Collections.Generic;
using System.Linq;
using SheetList;
using SheetList.Enums;

namespace Inventor
{
    public static class DrawingDocumentExtensions
    {
        internal static bool TryGetExistingSheetList(this DrawingDocument dwgDoc, out CustomTable sheetList)
        {
            sheetList = dwgDoc.GetExistingSheetList();
            return sheetList != null;
        }

        internal static CustomTable GetExistingSheetList(this DrawingDocument dwgDoc)
        {
            var sheetListTables = dwgDoc.AllCustomTables()
                .Where(t => t.IsSheetList())
                .ToList();

            if (sheetListTables.Count() > 1)
            {
                throw new Exception("More than 1 sheet list table was found");
            }

            return sheetListTables.FirstOrDefault();
        }

        internal static List<CustomTable> AllCustomTables(this DrawingDocument dwgDoc)
        {
            return dwgDoc.Sheets.Cast<Sheet>()
                .SelectMany(sh => sh.CustomTables.Cast<CustomTable>())
                .ToList();
        }

        internal static string[] GetSheetListData(this DrawingDocument dwgDoc, SheetListSettings settings)
        {
            var sheetsData = dwgDoc.GetActiveSheets()
                .Select(sheet => sheet.GetSheetData(settings));

            return sheetsData
                .SelectMany(value => value)
                .ToArray();
        }

        internal static List<Sheet> GetActiveSheets(this DrawingDocument dwgDoc)
        {
            return dwgDoc.Sheets.Cast<Sheet>()
                .Where(s => !s.ExcludeFromCount)
                .ToList();
        }
        
        public static void UpdateSheetList(this DrawingDocument dwgDoc, SheetListSettings settings)
        {
            _ = AddinServer.AppAutomation.UpdateSheetList(settings, dwgDoc);
        }
    }
}
