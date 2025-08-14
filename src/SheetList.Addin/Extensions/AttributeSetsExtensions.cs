using SheetList;

namespace Inventor
{
    internal static class AttributeSetsExtensions
    {
        /// <summary>
        /// Retrieves an <see cref="AttributeSet"/> by name from the given <see cref="AttributeSets"/> collection.
        /// If the specified attribute set does not exist, it will be created.
        /// </summary>
        /// <param name="attributeSets">The collection of <see cref="AttributeSets"/> to search within.</param>
        /// <param name="attributeSetName">The name of the attribute set to retrieve or create.</param>
        /// <returns>The <see cref="AttributeSet"/> corresponding to the specified name.</returns>
        internal static AttributeSet GetAttributeSet(this AttributeSets attributeSets, string attributeSetName)
        {
            if (!attributeSets.NameIsUsed[attributeSetName])
            {
                attributeSets.Add(attributeSetName, true);
            }

            return attributeSets[attributeSetName];
        }
    }
}