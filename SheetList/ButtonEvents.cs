using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;

namespace SheetList
{
	public class ButtonEvents
	{

		static Inventor.Application invApp;

		public static void CreateUpdate_SheetList()
		{
			//Check to make sure settings have been set
			if (AddinGlobal.AppSettings == null)
			{
				MessageBox.Show("Sheet List needs to be configured before being used.", "Configure Sheet List", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}

			invApp = AddinGlobal.InventorApp;

			var transientGeometry = AddinGlobal.InventorApp.TransientGeometry;

			AddinGlobal.oDwgDoc = (DrawingDocument)invApp.ActiveDocument;

			invApp.AssemblyOptions.DeferUpdate = true;

			var data = Tools.GetSheetListData();

			if (Tools.TryGetExistingSheetList(out var existingSheetList))
            {
				var position = existingSheetList.Position;
				var existingTableHeight = Tools.GetSheetListHeight(existingSheetList);

				var col1Width = existingSheetList.Columns[1].Width;
				var col2Width = existingSheetList.Columns[2].Width;

				existingSheetList.Delete();

				var sheetList = Tools.CreateSheetList(position, data, new double[] { col1Width, col2Width });

				//Adjust Table Location if table is bottom up
				if (sheetList.TableDirection == TableDirectionEnum.kBottomUpDirection)
				{
					var tableHeight = Tools.GetSheetListHeight(sheetList);
					var oldNewHeightDiff = tableHeight - existingTableHeight;

					var newPosition = transientGeometry.CreatePoint2d(position.X, position.Y + oldNewHeightDiff);
					sheetList.Position = newPosition;
				}
			}
            else
            {				
				var position = transientGeometry.CreatePoint2d(AddinGlobal.oDwgDoc.ActiveSheet.Width / 2, AddinGlobal.oDwgDoc.ActiveSheet.Height / 2);

				Tools.CreateSheetList(position, data, new double[] { 2.5, 5 });
			}


			invApp.AssemblyOptions.DeferUpdate = false;
		}

		public static void Configure_SheetList()
		{
			ConfigureUI oConfig = new ConfigureUI();
			oConfig.ShowDialog();
		}
	}
}
