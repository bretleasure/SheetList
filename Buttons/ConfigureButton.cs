using System.Windows.Interop;
using Inventor;
using Inventor.InternalNames.Ribbon;
using SheetList.UI;

namespace SheetList.Buttons
{
	internal class ConfigureButton : InventorButton
	{
		protected override void Execute(NameValueMap context, Inventor.Application inventor)
		{
			var oConfig = new ConfigUI(AddinServer.AppSettings);
			oConfig.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
			var inventorWindowPosition = InventorWindowHelper.GetInventorWindowPosition(inventor);
			oConfig.Left = inventorWindowPosition.X + 250; // Adjust the offset as needed
			oConfig.Top = inventorWindowPosition.Y + 250;  // Adjust the offset as needed
			oConfig.ShowDialog();
		}

		protected override string GetRibbonName() => InventorRibbons.Drawing;

		protected override string GetRibbonTabName() => DrawingRibbonTabs.Annotate;

		protected override string GetRibbonPanelName() => "Sheet List";

		protected override string GetButtonName() => "Configure";

		protected override string GetDescriptionText() => "Configure Sheet List";

		protected override string GetToolTipText() => "Click to configure Sheet List.";

		protected override string GetLargeIconResourceName() => "SheetList.Buttons.Icons.edit-light-32px.bmp";

		protected override string GetDarkThemeLargeIconResourceName() => "SheetList.Buttons.Icons.edit-dark-32px.bmp";

		protected override string GetSmallIconResourceName() => "SheetList.Buttons.Icons.edit-light-16px.bmp";

		protected override string GetDarkThemeSmallIconResourceName() => "SheetList.Buttons.Icons.edit-dark-16px.bmp";

		protected override bool UseLargeIcon => false;
		internal override int SequenceNumber => 2;
	}
}