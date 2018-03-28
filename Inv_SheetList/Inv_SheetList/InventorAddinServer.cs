#region Namespaces

using System;
using System.Text;
using System.Linq;
using System.Xml;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using Inventor;

#endregion

namespace Inv_SheetList
{
    [GuidAttribute("3dc3068f-5de4-4b85-9efe-44b7ece560f3")]
    public class InventorAddinServer : Inventor.ApplicationAddInServer
    {
        public InventorAddinServer()
        {
        }

        #region Implementations of the Interface Members 

        /// <summary>
        /// Do initializations in it such as caching the application object, registering event handlers, and adding ribbon buttons.
        /// </summary>
        /// <param name="siteObj">The entry object for the addin.</param>
        /// <param name="loaded1stTime">Indicating whether the addin is loaded for the 1st time.</param>
        public void Activate(Inventor.ApplicationAddInSite siteObj, bool loaded1stTime)
        {
            AddinGlobal.InventorApp = siteObj.Application;

            try
            {
                AddinGlobal.GetAddinClassId(this.GetType());

                Icon icon1 = new Icon(this.GetType(), "Resources.addin.ico"); //Change it if necessary but make sure it's embedded.

                InventorButton button1 = new InventorButton("Button 1", "InventorAddinServer.Button_" + Guid.NewGuid().ToString(), 
                                                            "Button 1 description", "Button 1 tooltip", icon1, icon1,
                                                            CommandTypesEnum.kShapeEditCmdType, ButtonDisplayEnum.kDisplayTextInLearningMode);

                button1.SetBehavior(true, true, true);
                button1.Execute = ButtonActions.Button1_Execute;

                if (loaded1stTime == true)
                {
                    UserInterfaceManager uiMan = AddinGlobal.InventorApp.UserInterfaceManager;
                    if (uiMan.InterfaceStyle == InterfaceStyleEnum.kRibbonInterface) //kClassicInterface support can be added if necessary.
                    {
                        Inventor.Ribbon ribbon = uiMan.Ribbons["Drawing"];
                        RibbonTab tab = ribbon.RibbonTabs["id_TabAnnotate"]; //Change it if necessary.

                        AddinGlobal.RibbonPanelId = "{c681eade-f4da-404f-b011-aba1e04cad9b}";
                        AddinGlobal.RibbonPanel = tab.RibbonPanels.Add("Sheet List", "InventorAddinServer.RibbonPanel_" + Guid.NewGuid().ToString(), AddinGlobal.RibbonPanelId, String.Empty, true);

                        CommandControls cmdCtrls = AddinGlobal.RibbonPanel.CommandControls;
                        cmdCtrls.AddButton(button1.ButtonDef, button1.DisplayBigIcon, button1.DisplayText, "", button1.InsertBeforeTarget);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            // TODO: Add more initialization code below.

        }

        /// <summary>
        /// Do cleanups in it such as releasing COM objects or forcing the GC to Collect when necessary.
        /// </summary>
        public void Deactivate()
        {
            // Add more cleanup work below if necessary, e.g. remove even handlers, flush and close log files, etc.


            // Release COM objects
            //

            // Add more FinalReleaseComObject() calls for COM objects you know, e.g.
            //if (comObj != null) Marshal.FinalReleaseComObject(comObj);

            // Release the COM objects maintained by InventorNetAddinWizard.
            foreach (InventorButton button in AddinGlobal.ButtonList)
            {
                if (button.ButtonDef != null)
                    button.ButtonDef = null; //Marshal.FinalReleaseComObject(button.ButtonDef);
            }
            if (AddinGlobal.RibbonPanel != null)
                AddinGlobal.RibbonPanel = null; //Marshal.FinalReleaseComObject(AddinGlobal.RibbonPanel);
            if (AddinGlobal.InventorApp != null)
                AddinGlobal.InventorApp = null; //Marshal.FinalReleaseComObject(AddinGlobal.InventorApp);
        }

        /// <summary>
        /// Deprecated. Use the ControlDefinition instead to execute commands.
        /// </summary>
        /// <param name="commandID"></param>
        public void ExecuteCommand(int commandID)
        {
        }

        /// <summary>
        /// Implement it if wanting to expose your own automation interface. 
        /// </summary>
		public object Automation
        {
            get { return null; }
        }

        #endregion


    }
}
