using Inventor;

namespace SheetList.Buttons
{
    internal class ConfigureButton : InventorButton
    {
        public override void Execute(NameValueMap context)
        {
            var oConfig = new ConfigureUI();
            oConfig.ShowDialog();
        }

        public override string GetButtonName() => "Configure";

        public override string GetDescriptionText() => "Configure Sheet List";

        public override string GetToolTipText() => "Click to configure Sheet List.";

        public override string GetLargeIconResourceName() => "SheetList.Buttons.Icons.edit-light-32px.bmp";
        
        public override string GetDarkThemeLargeIconResourceName() => "SheetList.Buttons.Icons.edit-dark-32px.bmp";

        public override string GetSmallIconResourceName() => "SheetList.Buttons.Icons.edit-light-16px.bmp";
        
        public override string GetDarkThemeSmallIconResourceName() => "SheetList.Buttons.Icons.edit-dark-16px.bmp";
    }
}

