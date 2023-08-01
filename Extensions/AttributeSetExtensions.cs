using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace SheetList.Extensions
{
    public static class AttributeSetExtensions
    {
        public static void AssignAttributeValue(this AttributeSet attributeSet, string attributeName, string attributeValue)
        {
            if (attributeSet.NameIsUsed[attributeName])
            {
                //Attribute Exists, Assign Value
                attributeSet[attributeName].Value = attributeValue;
            }
            else
            {
                //Attribute Doesn't Exist, Create then Assign Value
                Inventor.Attribute newAttribute = attributeSet.Add(attributeName, ValueTypeEnum.kStringType, attributeValue);
                newAttribute.Value = attributeValue;
            }
        }
    }
}