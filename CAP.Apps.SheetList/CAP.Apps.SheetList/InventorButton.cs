using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using System.Windows.Forms;
using stdole;

namespace CAP.Apps.SheetList
{
	public class InventorButton
	{
		private ButtonDefinition oButtonDef;
		public Action Execute;

		public InventorButton(string DisplayName, string InternalName, string DescriptionText, string ToolTipText, Icon LargeIcon, Icon SmallIcon)
		{
			Create(DisplayName, InternalName, DescriptionText, ToolTipText, LargeIcon, SmallIcon);
		}

		void Create(string DisplayName, string InternalName, string DescriptionText, string ToolTipText, Icon LargeIcon, Icon SmallIcon)
		{


			stdole.IPictureDisp LargeIconIPictureDisp = null;
			stdole.IPictureDisp SmallIconIpictureDisp = null;

			if (LargeIcon != null)
			{
				LargeIconIPictureDisp = IconToPicture(LargeIcon);
				SmallIconIpictureDisp = IconToPicture(SmallIcon);
			}

			oButtonDef = AddinGlobal.InventorApp.CommandManager.ControlDefinitions.AddButtonDefinition(DisplayName, InternalName, CommandTypesEnum.kEditMaskCmdType, null, DescriptionText, ToolTipText, SmallIconIpictureDisp, LargeIconIPictureDisp);


			oButtonDef.Enabled = true;
			oButtonDef.OnExecute += oButtonDef_OnExecute;

		}

		private static stdole.IPictureDisp IconToPicture(Icon Icon)
		{
			return ImageConverter.ImageToPicture(Icon.ToBitmap());
		}

		private void oButtonDef_OnExecute(NameValueMap Context)
		{
			//throw new NotImplementedException();
			if (Execute != null)
				Execute();
			else
				MessageBox.Show("Nothing to execute");
		}

		public ButtonDefinition ButtonDef()
		{
			return oButtonDef;
		}

		private class ImageConverter : AxHost
		{
			public ImageConverter() : base(String.Empty) { }

			public static stdole.IPictureDisp ImageToPicture(Image image)
			{
				return (stdole.IPictureDisp)GetIPictureDispFromPicture(image);
			}

			public static stdole.IPictureDisp IconToPicture(Icon icon)
			{
				return ImageToPicture(icon.ToBitmap());
			}

			public static Image PictureToImage(stdole.IPictureDisp picture)
			{
				return GetPictureFromIPicture(picture);
			}

			public static Icon PictureToIcon(stdole.IPictureDisp picture)
			{
				Bitmap bitmap = new Bitmap(PictureToImage(picture));
				return System.Drawing.Icon.FromHandle(bitmap.GetHicon());
			}
		}
	}
}
