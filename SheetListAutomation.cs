using Inventor;
using SheetList.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheetList
{
    public class SheetListAutomation : AddInAutomation
    {
        public Task<CustomTable> CreateSheetList() => CreateSheetList(SheetListSettings.Default);
        public Task<CustomTable> CreateSheetList(SheetListSettings settings)
        {
            var dwgDoc = (DrawingDocument)AddinGlobal.InventorApp.ActiveDocument;

            AddinGlobal.InventorApp.AssemblyOptions.DeferUpdate = true;

            var data = dwgDoc.GetSheetListData();

            SheetList sheetList;
            if (dwgDoc.TryGetExistingSheetList(out var existingSheetList))
            {
                sheetList = new SheetList(existingSheetList, settings, data);

                //Delete Existing Sheet List to be replaced by a new one
                existingSheetList.Delete();
            }
            else
            {
                sheetList = new SheetList(settings, dwgDoc.ActiveSheet, data);
            }

            AddinGlobal.InventorApp.AssemblyOptions.DeferUpdate = false;

            return Task.Run(sheetList.Create);
        }
    }
}
