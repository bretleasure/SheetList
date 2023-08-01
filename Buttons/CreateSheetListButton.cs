using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SheetList.Buttons
{
    public class CreateSheetListButton : InventorButton
    {
        public override void Execute(NameValueMap context)
        {
            //Check to make sure settings have been set
            if (AddinGlobal.AppSettings == null)
            {
                MessageBox.Show("Sheet List needs to be configured before being used.", "Configure Sheet List", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            AddinGlobal.Automation.CreateSheetList(AddinGlobal.AppSettings);
        }

        public override string GetButtonName() => "Create /\rUpdate";

        public override string GetDescriptionText() => "Create / Update Sheet List";

        public override string GetToolTipText() => "Click to create / update the Sheet List in this document.";

        public override string GetIconResourceName() => "Icons.SheetList.ico";
    }
}
