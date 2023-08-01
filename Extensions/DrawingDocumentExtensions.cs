using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SheetList.Extensions
{
    public static class DrawingDocumentExtensions
    {
        public static bool TryGetExistingSheetList(this DrawingDocument dwgDoc, out CustomTable sheetList)
        {
            sheetList = dwgDoc.GetExistingSheetList();
            return sheetList != null;
        }

        public static CustomTable GetExistingSheetList(this DrawingDocument dwgDoc)
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

        public static List<CustomTable> AllCustomTables(this DrawingDocument dwgDoc)
        {
            return dwgDoc.Sheets.Cast<Sheet>()
                .SelectMany(sh => sh.CustomTables.Cast<CustomTable>())
                .ToList();
        }

        public static string[] GetSheetListData(this DrawingDocument dwgDoc)
        {
            var sheetsData = dwgDoc.GetActiveSheets()
                .Select(s => new string[] { s.GetSheetNumber(), s.GetSheetName() });

            return sheetsData
                .SelectMany(value => value)
                .ToArray();
        }

        public static List<Sheet> GetActiveSheets(this DrawingDocument dwgDoc)
        {
            return dwgDoc.Sheets.Cast<Sheet>()
                .Where(s => !s.ExcludeFromCount)
                .ToList();
        }
    }
}
