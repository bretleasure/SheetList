﻿using Inventor;
using SheetList.Extensions;
using System.Windows.Forms;

namespace SheetList.Buttons
{
    internal class CreateSheetListButton : InventorButton
    {
        public override void Execute(NameValueMap context)
        {
            //Check to make sure settings have been set
            if (AddinGlobal.AppSettings == null)
            {
                MessageBox.Show("Sheet List needs to be configured before being used.", "Configure Sheet List", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (AddinGlobal.InventorApp.ActiveDocument is DrawingDocument dwgDoc)
            {
                if (dwgDoc.TryGetExistingSheetList(out var _))
                {
                    AddinGlobal.Automation.UpdateSheetList(AddinGlobal.AppSettings.SheetListSettings, dwgDoc);
                }
                else
                {
                    var sheet = dwgDoc.ActiveSheet;
                    var position = AddinGlobal.InventorApp.TransientGeometry.CreatePoint2d(sheet.Width / 2, sheet.Height / 2);
                    AddinGlobal.Automation.CreateSheetList(sheet, position, AddinGlobal.AppSettings.SheetListSettings);
                }
            }
        }

        public override string GetButtonName() => "Create /\rUpdate";

        public override string GetDescriptionText() => "Create / Update Sheet List";

        public override string GetToolTipText() => "Click to create / update the Sheet List in this document.";
        
        public override string GetLargeIconResourceName() => "SheetList.Buttons.Icons.add-light-32px.bmp";
        public override string GetDarkThemeLargeIconResourceName() => "SheetList.Buttons.Icons.add-dark-32px.bmp";

        public override string GetSmallIconResourceName() => "SheetList.Buttons.Icons.add-light-16px.bmp";
        public override string GetDarkThemeSmallIconResourceName() => "SheetList.Buttons.Icons.add-dark-16px.bmp";
    }
}
