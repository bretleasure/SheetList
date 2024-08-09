using System.Collections.Generic;
using System.Linq;

namespace Inventor
{
	public static class DocumentExtensions
	{
		public static List<string> GetPropertyNames(this Document doc)
		{
			return doc.PropertySets
				.Cast<PropertySet>()
				.SelectMany(ps => ps.Cast<Property>())
				.Select(pd => pd.Name)
				.ToList();
		}
	}
}