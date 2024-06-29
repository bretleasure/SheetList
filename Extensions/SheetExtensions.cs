using System.Collections;
using System.Collections.Generic;
using Inventor;
using System.Linq;
using System.Threading.Tasks;
using SheetList;

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

        public static Task<CustomTable> CreateSheetList(this Sheet sheet, Point2d position)
            => sheet.CreateSheetList(position, SheetListSettings.Default);
        public static Task<CustomTable> CreateSheetList(this Sheet sheet, Point2d position, SheetListSettings settings)
        {
            return AddinServer.AppAutomation.CreateSheetList(sheet, position, settings);
        }
    }
}
