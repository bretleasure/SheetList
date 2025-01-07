using System.Linq;

namespace Inventor
{
	public static class RevisionTableExtensions
	{
		public static RevisionTableRow GetLatestRevisionTableRow(this RevisionTable revTable)
		{
			return revTable.RevisionTableRows
				.Cast<RevisionTableRow>()
				.LastOrDefault();
		}
		
		public static int GetColumnIndexByName(this RevisionTable revTable, string columnName)
		{
			return revTable.RevisionTableColumns
				.Cast<RevisionTableColumn>()
				.Select((col, index) => new { col, index })
				.Where(x => x.col.Title == columnName)
				.Select(x => x.index)
				.FirstOrDefault();
		}
	}
}