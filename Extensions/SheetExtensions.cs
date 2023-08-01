using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
