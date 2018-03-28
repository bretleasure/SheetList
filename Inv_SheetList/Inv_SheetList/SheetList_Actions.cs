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
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using Inventor;

#endregion

namespace Inv_SheetList
{
    public class SheetList_Actions
    {
        static Inventor.Application InvApp;

        static DrawingDocument dwgDoc;

        public static void CreateUpdate_SheetList()
        {

            InvApp = AddinGlobal.InventorApp;

            dwgDoc = (DrawingDocument)InvApp.ActiveDocument;

            Sheets oSheets = dwgDoc.Sheets;

            CustomTable oSheetList = Get_SheetList();

            int TotalSheets = Get_TotalSheets();

            //Clear Table
            Clear_SheetList(oSheetList);

            int count = 1;
            string name = "";

            foreach (Sheet oSheet in oSheets)
            {
                name = Get_SheetName(oSheet);

                if (!oSheet.ExcludeFromCount)
                {
                    Row row = oSheetList.Rows.Add();
                    row[1].Value = count.ToString();
                    row[2].Value = name;

                    count++;
                }
            }

        }

        public static void ShowConfig()
        {
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
            string[] col = new string[] { "SHEET #", "SHEET NAME" };

            TransientGeometry oTG = InvApp.TransientGeometry;
            Point2d loc = oTG.CreatePoint2d();

            CustomTable newTable = dwgDoc.Sheets[1].CustomTables.Add("Sheet List", loc, 2, 0, col);

            //Assign Table ID
            newTable.AttributeSets.Add("Table_id", true).Add("Name", ValueTypeEnum.kStringType, "SHEET LIST");

            //Assign Table Name
            newTable.Title = "SHEET LIST";

            

            return newTable;
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
    }
}
