using Inventor;
using System.Linq;
using SheetList;

namespace Inventor
{
    internal static class CustomTableExtensions
    {
        internal static bool IsSheetList(this CustomTable table)
        {
            var attributeSet = table.AttributeSets.Cast<AttributeSet>()
                .FirstOrDefault(set => set.Name == AppConstants.TableAttributeSetName);

            if (attributeSet != null && attributeSet.NameIsUsed[AppConstants.AttributeName])
            {
                return attributeSet[AppConstants.AttributeName].Value.ToString() == AppConstants.TableId;
            }

            return false;
        }
        
        internal static double GetTableHeight(this CustomTable sheetList)
        {
            return sheetList.Rows.Cast<Row>()
                .Sum(row => row.Height);
        }

        internal static void SaveAttributesToTable(this CustomTable table)
        {
            var attributeSet = table.AttributeSets.GetAttributeSet(AppConstants.TableAttributeSetName);

            attributeSet.AssignAttributeValue(AppConstants.AttributeName, AppConstants.TableId);
        }
    }
}
