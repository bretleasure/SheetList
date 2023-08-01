using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace SheetList.Buttons
{
    public class ConfigureButton : InventorButton
    {
        public override void Execute(NameValueMap context)
        {
            var oConfig = new ConfigureUI();
            oConfig.ShowDialog();
        }

        public override string GetButtonName() => "Configure";

        public override string GetDescriptionText() => "Configure Sheet List";

        public override string GetToolTipText() => "Click to configure Sheet List.";

        public override string GetIconResourceName() => "Icons.gear.ico";
    }
}
