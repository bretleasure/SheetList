using Inventor;

namespace Inventor
{
    internal static class AttributeSetExtensions
    {
        internal static void AssignAttributeValue(this AttributeSet attributeSet, string attributeName, string attributeValue)
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
        
        private static bool TryGetAttribute(this AttributeSet attributeSet, string attributeName, out Inventor.Attribute attribute)
        {
            if (attributeSet.NameIsUsed[attributeName])
            {
                attribute = attributeSet[attributeName];
                return attribute != null;
            }

            attribute = null;
            return false;
        }

        internal static bool TryGetAttributeValue(this AttributeSet attributeSet, string attributeName, out string attributeValue)
        {
           if (attributeSet.TryGetAttribute(attributeName, out var attribute))
           {
               attributeValue = attribute.Value.ToString();
               return true;
           }
            
           attributeValue = string.Empty;
           return false;
        }
    }
}