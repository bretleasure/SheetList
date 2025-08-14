using System.Collections.Generic;
using System.Linq;

namespace Inventor
{
	internal static class DocumentExtensions
	{
		internal static List<string> GetPropertyNames(this Document doc)
		{
			return doc.GetProperties()
				.Select(pd => pd.Name)
				.ToList();
		}
		
		internal static string GetPropertyValue(this Document doc, string propertyName)
		{
			return doc.GetProperties()
				.FirstOrDefault(pd => pd.Name == propertyName)?.Value.ToString();
		}

		private static List<Property> GetProperties(this Document doc)
		{
			return doc.PropertySets
				.Cast<PropertySet>()
				.SelectMany(ps => ps.Cast<Property>())
				.ToList();
		}
	}
}