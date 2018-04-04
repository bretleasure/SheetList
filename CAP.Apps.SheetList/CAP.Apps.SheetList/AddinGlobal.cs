using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Inventor;
using CAP.Utilities;

namespace CAP.Apps.SheetList
{
    public class AddinGlobal
    {
        public static Inventor.Application InventorApp;

        public static SheetList oSheetList;

        public static string AppFolder = Tools.GetAppFolder("Sheet List");

        public static string SettingsFile = Tools.GetHexString("Sheet List Settings") + ".xml";

        public static string AppId = "5342673156831821071";


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