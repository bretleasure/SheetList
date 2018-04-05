#region Namespaces

using System;
using System.Text;
using System.Linq;
using System.Xml;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using CAP.Utilities;

using Inventor;

#endregion

namespace CAP.Apps.SheetList
{
    public class SheetList_Actions
    {
        static Inventor.Application InvApp;

        static DrawingDocument dwgDoc;

        public static void CreateUpdate_SheetList()
        {
            //Check Entitlement
            if (!Tools.CheckForValidUser("Sheet List", AddinGlobal.AppId))
            {
                return;
            }
            

            //Check to make sure settings have been set
            if (AddinGlobal.oSheetList == null)
            {
                MessageBox.Show("Sheet List needs to be configured before being used.", "Configure Sheet List", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            InvApp = AddinGlobal.InventorApp;

            dwgDoc = (DrawingDocument)InvApp.ActiveDocument;

            Sheets oSheets = dwgDoc.Sheets;            

            CustomTable SLTable = Get_SheetList();

            Point2d CurrentPosition = SLTable.Position;

            SLTable = Get_SheetListSettings(SLTable);

            int CurrentRowQty = SLTable.Rows.Count;

            //Clear Table
            Clear_SheetList(SLTable);

            //Reset Position, in case of bottom up scenario
            SLTable.Position = CurrentPosition;

            int count = 1;
            string name = "";

            double runningheight = 0;

            foreach (Sheet oSheet in oSheets)
            {
                name = Get_SheetName(oSheet);

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

        }

        public static void ShowConfig()
        {
            //Check Entitlement
            if (!Tools.CheckForValidUser("Sheet List", AddinGlobal.AppId))
            {
                return;
            }
                

            ConfigureUI config = new ConfigureUI();
            config.ShowDialog();
        }


        static void Clear_SheetList(CustomTable table)
        {
            foreach (Row r in table.Rows)
            {

                r.Delete();
            }
        }

        static string Get_SheetName(Sheet oSheet)
        {
            string name = oSheet.Name;

            return oSheet.Name.Substring(0, name.IndexOf(":"));
        }

        static CustomTable Get_SheetList()
        {
            CustomTable oTable = null;

            foreach (CustomTable table in dwgDoc.Sheets[1].CustomTables)
            {
                if (table.AttributeSets["Table_id"]["Name"].Value == "SHEET LIST")
                    oTable = table;
            }

            if (oTable != null)
                return oTable;
            else
            {
                return Create_NewSheetList();
            }

        }

        static CustomTable Create_NewSheetList()
        {
            SheetList oSL = AddinGlobal.oSheetList;

            string[] col = new string[] { oSL.SheetNoColName, oSL.SheetNameColName };

            TransientGeometry oTG = InvApp.TransientGeometry;
            Point2d loc = oTG.CreatePoint2d(dwgDoc.ActiveSheet.Width / 2, dwgDoc.ActiveSheet.Height / 2);

            CustomTable newTable = dwgDoc.Sheets[1].CustomTables.Add(oSL.Title, loc, 2, 0, col);

            //Assign Table ID
            newTable.AttributeSets.Add("Table_id", true).Add("Name", ValueTypeEnum.kStringType, "SHEET LIST");

            newTable = Get_SheetListSettings(newTable);

            return newTable;
        }

        static CustomTable Get_SheetListSettings(CustomTable table)
        {
            SheetList oSL = AddinGlobal.oSheetList;

            table.Columns[1].Title = oSL.SheetNoColName;
            table.Columns[2].Title = oSL.SheetNameColName;
            table.ShowTitle = oSL.ShowTitle;
            table.Title = oSL.Title;
            table.TableDirection = oSL.Direction;
            table.HeadingPlacement = oSL.HeadingPlacement;
            table.WrapAutomatically = oSL.EnableAutoWrap;
            table.WrapLeft = oSL.WrapLeft;
            table.MaximumRows = oSL.MaxRows;
            if (oSL.NumberOfSections != 0)
                table.NumberOfSections = oSL.NumberOfSections;

            return table;
        }

        static int Get_TotalSheets()
        {
            int Total = 0;

            foreach (Sheet oSheet in dwgDoc.Sheets)
            {
                if (!oSheet.ExcludeFromCount)
                    Total++;                
            }

            return Total;
        }

        public static SheetList Get_SavedSheetListObject()
        {
            SheetList oSheetList = new SheetList();

            try
            {
                oSheetList = (SheetList)XMLTools.Get_ObjectFromXML(AddinGlobal.AppFolder + AddinGlobal.SettingsFile, oSheetList);
            }
            catch
            {
                oSheetList = null;
            }

            return oSheetList;
        }

        
    }
}
