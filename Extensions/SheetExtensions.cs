using Inventor;
using System.Linq;

namespace SheetList.Extensions
{
    public static class SheetExtensions
    {
        public static string GetSheetName(this Sheet oSheet)
        {
            return oSheet.Name.Split(':').First();
        }

        public static string GetSheetNumber(this Sheet sheet)
        {
            return sheet.Name.Split(':').Last();
        }
    }
}
