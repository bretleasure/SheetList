using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;
using CAP.Utilities;

namespace SheetList
{
	public class SheetList_ButtonEvents
	{

		static Inventor.Application InvApp;

		public static void CreateUpdate_SheetList()
		{
			//Check Entitlement
			if (!Tools.CheckForValidUser("SheetList", AddinGlobal.AppId))
			{
				return;
			}

			//Check to make sure settings have been set
			if (AddinGlobal.AppSettings == null)
			{
				MessageBox.Show("Sheet List needs to be configured before being used.", "Configure Sheet List", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}

			InvApp = AddinGlobal.InventorApp;

			AddinGlobal.oDwgDoc = (DrawingDocument)InvApp.ActiveDocument;

			//Pass Inv

			InvApp.AssemblyOptions.DeferUpdate = true;

			Sheets oSheets = AddinGlobal.oDwgDoc.Sheets;

			CustomTable SLTable = SheetList_Tools.Get_SheetList();

			Point2d CurrentPosition = SLTable.Position;

			SLTable = SheetList_Tools.Get_SheetListSettings(SLTable);

			int CurrentRowQty = SLTable.Rows.Count;

			//Clear Table
			SheetList_Tools.Clear_SheetList(SLTable);

			//Reset Position, in case of bottom up scenario
			SLTable.Position = CurrentPosition;

			int count = 1;
			string name = "";

			double runningheight = 0;

			foreach (Sheet oSheet in oSheets)
			{
				name = SheetList_Tools.Get_SheetName(oSheet);

				if (!oSheet.ExcludeFromCount)
				{
					Row row = SLTable.Rows.Add();
					row[1].Value = count.ToString();
					row[2].Value = name;

					runningheight += row.Height;
					count++;
				}
			}

			double heightdiff = (SLTable.Rows.Count - CurrentRowQty) * SLTable.Rows[1].Height;

			runningheight += SLTable.ColumnHeaderTextStyle.FontSize + (SLTable.RowGap * 2);

			//Adjust Table Location if table is bottom up
			if (SLTable.TableDirection == TableDirectionEnum.kBottomUpDirection)
			{
				Point2d newloc = InvApp.TransientGeometry.CreatePoint2d(CurrentPosition.X, CurrentPosition.Y + heightdiff);

				SLTable.Position = newloc;
			}
			else
			{
				SLTable.Position = CurrentPosition;
			}

			InvApp.AssemblyOptions.DeferUpdate = false;
		}

		public static void Configure_SheetList()
		{
			//Check Entitlement
			if (!Tools.CheckForValidUser("SheetList", AddinGlobal.AppId))
			{
				return;
			}

			ConfigureUI oConfig = new ConfigureUI();
			oConfig.ShowDialog();
		}



	}
}
