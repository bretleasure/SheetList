using System;
using System.Drawing;
using System.Windows.Forms;
using Inventor;

namespace SheetList.Buttons
{
    public abstract class InventorButton
    {
        internal InventorButton()
        {
            var largeIcon = new Icon(GetType(), GetIconResourceName());
            var smallIcon = new Icon(largeIcon, 16, 16);

            var largeIconIPictureDisp = IconToPicture(largeIcon);
            var smallIconIpictureDisp = IconToPicture(smallIcon);

            Definition = AddinGlobal.InventorApp.CommandManager.ControlDefinitions.AddButtonDefinition(GetButtonName(), GetInternalName(),
                CommandType, null, GetDescriptionText(), GetToolTipText(), smallIconIpictureDisp, largeIconIPictureDisp);
            Definition.Enabled = true;
            Definition.OnExecute += Execute;
        }

        private static stdole.IPictureDisp IconToPicture(Icon icon)
        {
            return ImageConverter.IconToPicture(icon);
        }

        public abstract void Execute(NameValueMap context);
        public abstract string GetButtonName();
        public virtual string GetInternalName() => Guid.NewGuid().ToString();
        public abstract string GetDescriptionText();
        public abstract string GetToolTipText();
        public abstract string GetIconResourceName();
        public virtual CommandTypesEnum CommandType => CommandTypesEnum.kEditMaskCmdType;

        public ButtonDefinition Definition { get; private set; }
        public bool Enabled
        {
            get => Definition.Enabled;
            set => Definition.Enabled = value;
        }

        private class ImageConverter : AxHost
        {
            public ImageConverter() : base(string.Empty) { }

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
                return Icon.FromHandle(bitmap.GetHicon());
            }

        }
    }
}