using System.Collections.Generic;
using System.Linq;

namespace Inventor
{
	public static class DocumentExtensions
	{
		public static List<string> GetPropertyNames(this Document doc)
		{
			return doc.GetProperties()
				.Select(pd => pd.Name)
				.ToList();
		}
		
		public static string GetPropertyValue(this Document doc, string propertyName)
		{
			return doc.GetProperties()
				.FirstOrDefault(pd => pd.Name == propertyName)?.Value.ToString();
		}

		public static List<Property> GetProperties(this Document doc)
		{
			return doc.PropertySets
				.Cast<PropertySet>()
				.SelectMany(ps => ps.Cast<Property>())
				.ToList();
		}
	}
}