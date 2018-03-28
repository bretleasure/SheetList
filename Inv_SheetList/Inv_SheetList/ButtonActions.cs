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
    public class ButtonActions
    {
        static Inventor.Application InvApp;

        static DrawingDocument dwgDoc;

        public static void Button1_Execute()
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



//Dim Page As Integer
// Page = 0 'Reset Page Count
//Dim Row As Integer

// Row = 0 'Reset Row Count
//Dim TableRows As Integer

// TableRows = 0 'Reset Table Rows


//oSheetlist.MaximumRows = 40 'Set Maxium Rows Per Column
//TableRows = oSheetlist.Rows.Count


//For Each oSheet In oSheets
// Row += 1

// Page += 1

// fposition = InStr(oSheet.Name, ":") ' Count Number of Characters to the :

// fposition = fposition - 1 ' Minus 1 to get everything before :

// If TableRows<TotalPages Then ' If There arent enough Table Rows, Add More

// oSheetlist.Rows.Add
// End If
// If oSheet.ExcludeFromCount = False Then

// oSheetlist.Rows.Item(Row).Item(1).Value = Page

// oSheetlist.Rows.Item(Row).Item(2).Value = Left(oSheet.Name, fposition) 'Get Sheetname before the :

// End If
//Next

//TableRows = oSheetlist.Rows.Count


//If TableRows > TotalPages Then ' If there are more Table Rows than Pages, Delete Until They Match

// Do Until TableRows = TotalPages

// TableRows -= 1

// oSheetlist.Rows.Item(TotalPages + 1).Delete

// Loop
//End If


//'Set Location for SheetList
//Dim oBorder As Border = oDoc.Sheets.Item(1).Border
//Dim oPoint As Point2d


//Dim SheetList As CustomTable
//SheetList = ActiveSheet.Sheet.CustomTables.Item(1)


//Dim YVal As Double
//YVal = oBorder.RangeBox.MinPoint.Y + (SheetList.Rows.Item(1).Height * (SheetList.Rows.Count + 1)) + 2.2225



//oPoint = ThisApplication.TransientGeometry.Createpoint2d(oBorder.RangeBox.MinPoint.X, YVal)


//SheetList.ShowTitle = False
//SheetList.Position = oPoint


//ActiveSheet = ThisDrawing.Sheet(CurrentSheet)


//Catch
//End Try

//End Sub

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

            List<string> n = new List<string>();
            n.ToArray();

            

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
