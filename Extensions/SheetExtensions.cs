using System.Collections;
using System.Collections.Generic;
using Inventor;
using System.Linq;
using System.Threading.Tasks;
using SheetList;
using SheetList.Enums;

namespace Inventor
{
    public static class SheetExtensions
    {
        internal static string GetSheetName(this Sheet oSheet)
        {
            return oSheet.Name.Split(':').First();
        }

        internal static string GetSheetNumber(this Sheet sheet)
        {
            return sheet.Name.Split(':').Last();
        }
        
        internal static string GetRevision(this Sheet sheet)
        {
            return sheet.Revision;
        }

        internal static string GetLatestRevisionDate(this Sheet sheet, int dateColumnIndex)
        {
            var revTable = sheet.RevisionTables
                .Cast<RevisionTable>()
                .FirstOrDefault();

            if (revTable == null)
            {
                return string.Empty;
            }
            
            var latestRevRow = revTable.GetLatestRevisionTableRow();
            return latestRevRow[dateColumnIndex]?.Text ?? string.Empty;
        }
        
        internal static string[] GetSheetData(this Sheet sheet, SheetListSettings settings)
        {
            var data = new List<string>();
            
            foreach (var propType in settings.SheetPropertyTypes)
            {
                switch (propType)
                {
                    case SheetProperty.SheetName:
                        data.Add(sheet.GetSheetName());
                        break;
                    case SheetProperty.SheetNumber:
                        data.Add(sheet.GetSheetNumber());
                        break;
                    case SheetProperty.Revision:
                        data.Add(sheet.GetRevision());
                        break;
                    case SheetProperty.RevisionDate:
                        data.Add(sheet.GetLatestRevisionDate(0));
                        break;
                }
            }

            return data.ToArray();
        }

        public static Task<CustomTable> CreateSheetList(this Sheet sheet, Point2d position)
            => sheet.CreateSheetList(position, SheetListSettings.Default);
        public static Task<CustomTable> CreateSheetList(this Sheet sheet, Point2d position, SheetListSettings settings)
        {
            return AddinServer.AppAutomation.CreateSheetList(sheet, position, settings);
        }
    }
}
