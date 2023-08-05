using Inventor;
using SheetList.Extensions;
using System;
using System.Threading.Tasks;

namespace SheetList
{
    public class SheetListAutomation : AddInAutomation
    {
        public Task<CustomTable> CreateSheetList(Sheet sheet, Point2d position) => CreateSheetList(SheetListSettings.Default, sheet, position);
        public Task<CustomTable> CreateSheetList(SheetListSettings settings, Sheet sheet, Point2d position)
        {
            if (sheet.Parent is DrawingDocument dwgDoc)
            {
                var data = settings.TableDataBuilder.Invoke(dwgDoc);

                SheetList sheetList;
                if (dwgDoc.TryGetExistingSheetList(out var existingSheetList))
                {
                    sheetList = new SheetList(existingSheetList, settings, data);

                    //Delete Existing Sheet List to be replaced by a new one
                    existingSheetList.Delete();
                }
                else
                {
                    sheetList = new SheetList(settings, sheet, position, data);
                }

                return Task.Run(sheetList.Create);
            }

            return Task.FromException<CustomTable>(new Exception("Active document is not a drawing document"));
        }

        public Task<CustomTable> UpdateSheetList(SheetListSettings settings, DrawingDocument dwgDoc)
        {
            if (dwgDoc.TryGetExistingSheetList(out var existingSheetList))
            {
                var sheetList = new SheetList(existingSheetList, settings, dwgDoc.GetSheetListData());

                //Delete Existing Sheet List to be replaced by a new one
                existingSheetList.Delete();

                return Task.Run(sheetList.Create);
            }

            return Task.FromException<CustomTable>(new Exception("Existing sheet list not found"));
        }
    }
}
