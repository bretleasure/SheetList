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
    }
}