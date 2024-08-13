using Inventor;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SheetList
{
    public class SheetListAutomation : AddInAutomation
    {
        public Task<CustomTable> CreateSheetList(Sheet sheet, Point2d position) => CreateSheetList(sheet, position, SheetListSettings.Default);
        public Task<CustomTable> CreateSheetList(Sheet sheet, Point2d position, SheetListSettings settings)
        {
            if (sheet.Parent is DrawingDocument dwgDoc)
            {
                var data = dwgDoc.GetSheetListData(settings);

                if (dwgDoc.TryGetExistingSheetList(out _))
                {
                    return Task.FromException<CustomTable>(new Exception("Sheet list already exists on another sheet"));
                }
                
                var sheetList = new SheetList(settings, sheet, position, data);

                return Task.Run(sheetList.Create);
            }

            return Task.FromException<CustomTable>(new Exception("Active document is not a drawing document"));
        }

        public Task<CustomTable> UpdateSheetList(SheetListSettings settings, DrawingDocument dwgDoc)
        {
            if (dwgDoc.TryGetExistingSheetList(out var existingSheetList))
            {
                return UpdateSheetList(existingSheetList, settings, dwgDoc);
            }
            
            return Task.FromException<CustomTable>(new Exception("Existing sheet list not found"));
        }
        
        public Task<CustomTable> UpdateSheetList(CustomTable existingSheetList, SheetListSettings settings, DrawingDocument dwgDoc)
        {
            var data = dwgDoc.GetSheetListData(settings);
            
            var sheetList = new SheetList(existingSheetList, settings, data);
            
            //Delete Existing Sheet List to be replaced by a new one
            existingSheetList.Delete();

            return Task.Run(sheetList.Create);
        }
    }
}
