using Inventor;
using System.Linq;
using System.Threading.Tasks;

namespace SheetList.Extensions
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

        public static Task<CustomTable> CreateSheetList(this Sheet sheet, Point2d position)
            => sheet.CreateSheetList(position, SheetListSettings.Default);
        public static Task<CustomTable> CreateSheetList(this Sheet sheet, Point2d position, SheetListSettings settings)
        {
            return AddinServer.AppAutomation.CreateSheetList(sheet, position, settings);
        }
    }
}
