using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Inventor;

namespace Inv_SheetList
{
    public class AddinGlobal
    {
        public static Inventor.Application InventorApp;

        public static string RibbonPanelId;
        public static RibbonPanel RibbonPanel;
        public static List<InventorButton> ButtonList = new List<InventorButton>();

        private static string mClassId;
        public static string ClassId
        {
            get
            {
                if (!string.IsNullOrEmpty(mClassId))
                    return AddinGlobal.mClassId;
                else
                    throw new System.Exception("The addin server class id hasn't been gotten yet!");
            }
            set { AddinGlobal.mClassId = value; }
        }

        public static void GetAddinClassId(Type t)
        {
            GuidAttribute guidAtt = (GuidAttribute)GuidAttribute.GetCustomAttribute(t, typeof(GuidAttribute));
            mClassId = "{" + guidAtt.Value + "}";
        }
    }
}