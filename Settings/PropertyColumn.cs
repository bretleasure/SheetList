using SheetList.Enums;

namespace SheetList
{
	public class PropertyColumn
	{
		public PropertyColumn(PropertySource source, string propertyName, double columnWidth = 2.5)
		{
			Source = source;
			PropertyName = propertyName;
			ColumnName = propertyName;
			ColumnWidth = columnWidth;
			DisplayValue = $"{Source.ToFriendlyString()} - {PropertyName}";
		}
		
		public PropertySource Source { get; set; }
		public string PropertyName { get; set; }
		public string ColumnName { get; set; }
		public double ColumnWidth { get; set; }
		public string DisplayValue { get; set; }
	}
}