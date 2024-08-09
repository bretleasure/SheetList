using System;

namespace SheetList.Enums
{
	public enum PropertySource
	{
		Sheet,
		Drawing,
		SheetDocument,
		None
	}
	
	public static class PropertySourceExtensions
	{
		public static string ToFriendlyString(this PropertySource source)
		{
			return source switch
			{
				PropertySource.Sheet => "Sheet",
				PropertySource.Drawing => "Drawing",
				PropertySource.SheetDocument => "Sheet Document",
				PropertySource.None => "None",
				_ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
			};
		}
	}
}