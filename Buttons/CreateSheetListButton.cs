using Inventor;
using SheetList.Extensions;
using System.Windows.Forms;
using Inventor.InternalNames.Ribbon;

namespace SheetList.Buttons
{
    internal class CreateSheetListButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {
            //Check to make sure settings have been set
            if (AddinServer.AppSettings == null)
            {
                MessageBox.Show("Sheet List needs to be configured before being used.", "Configure Sheet List", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (inventor.ActiveDocument is DrawingDocument dwgDoc)
            {
                if (dwgDoc.TryGetExistingSheetList(out var existingSheetList))
                {
                    _ = AddinServer.AppAutomation.UpdateSheetList(existingSheetList, AddinServer.AppSettings.SheetListSettings, dwgDoc).Result;
                }
                else
                {
                    var sheet = dwgDoc.ActiveSheet;
                    var position = inventor.TransientGeometry.CreatePoint2d(sheet.Width / 2, sheet.Height / 2);
                    _ = AddinServer.AppAutomation.CreateSheetList(sheet, position, AddinServer.AppSettings.SheetListSettings).Result;
                }
            }
        }

        protected override string GetRibbonName() => InventorRibbons.Drawing;

        protected override string GetRibbonTabName() => DrawingRibbonTabs.Annotate;

        protected override string GetRibbonPanelName() => "Sheet List";

        protected override string GetButtonName() => "Create /\rUpdate";

        protected override string GetDescriptionText() => "Create / Update Sheet List";

        protected override string GetToolTipText() => "Click to create / update the Sheet List in this document.";

        protected override string GetLargeIconResourceName() => "SheetList.Buttons.Icons.add-light-32px.bmp";
        protected override string GetDarkThemeLargeIconResourceName() => "SheetList.Buttons.Icons.add-dark-32px.bmp";

        protected override string GetSmallIconResourceName() => "SheetList.Buttons.Icons.add-light-16px.bmp";
        protected override string GetDarkThemeSmallIconResourceName() => "SheetList.Buttons.Icons.add-dark-16px.bmp";
        internal override int SequenceNumber => 0;
    }
}
