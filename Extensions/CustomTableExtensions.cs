using Inventor;
using System.Linq;

namespace SheetList.Extensions
{
    public static class CustomTableExtensions
    {
        public static bool IsSheetList(this CustomTable table)
        {
            var attributeSet = table.AttributeSets.Cast<AttributeSet>()
                .FirstOrDefault(set => set.Name == AppConstants.TableAttributeSetName);

            if (attributeSet != null && attributeSet.NameIsUsed[AppConstants.AttributeName])
            {
                return attributeSet[AppConstants.AttributeName].Value.ToString() == AppConstants.TableId;
            }

            return false;
        }
        
        public static double GetTableHeight(this CustomTable sheetList)
        {
            return sheetList.Rows.Cast<Row>()
                .Sum(row => row.Height);
        }

        public static void SaveAttributesToTable(this CustomTable table)
        {
            if (!table.AttributeSets.NameIsUsed[AppConstants.TableAttributeSetName])
            {
                table.AttributeSets.Add(AppConstants.TableAttributeSetName, true);
            }

            var attributeSet = table.AttributeSets[AppConstants.TableAttributeSetName];

            attributeSet.AssignAttributeValue(AppConstants.AttributeName, AppConstants.TableId);
        }
    }
}
