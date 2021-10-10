using iAD.Utilities;
using Inventor;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SheetList
{
	/// <summary>
	/// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
	/// that all Inventor AddIns are required to implement. The communication between Inventor and
	/// the AddIn is via the methods on this interface.
	/// </summary>
	[GuidAttribute("3dc3068f-5de4-4b85-9efe-44b7ece560f3")]
	public class StandardAddInServer : Inventor.ApplicationAddInServer
	{

		// Inventor application object.
		//private Inventor.Application m_inventorApplication;

		public StandardAddInServer()
		{
		}

		#region ApplicationAddInServer Members

		public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
		{
			// This method is called by Inventor when it loads the addin.
			// The AddInSiteObject provides access to the Inventor Application object.
			// The FirstTime flag indicates if the addin is loaded for the first time.


			// Initialize AddIn members.
			AddinGlobal.InventorApp = addInSiteObject.Application;

			AddinGlobal.Logger = Logging.GetLogger<SheetList.StandardAddInServer>();

			AddinGlobal.Logger.LogInformation("Initializing Addin");

			if (!LicTools.CheckForValidUser(AddinGlobal.InventorApp, "Sheet List", AddinGlobal.AppId))
			{
				AddinGlobal.Logger.LogWarning("Invalid License");
				return;
			}

			//Get User Settings
			AddinGlobal.Logger.LogInformation("Getting saved settings");
			Tools.GetSavedSettings();

			//Create Event Listener
			AddinGlobal.Logger.LogInformation("Adding Event Listeners");
			Tools.CreateEventListener();

			try
			{
				Icon icon1 = new Icon(this.GetType(), "Resources.SheetList.ico");
				Icon icon1_sm = new Icon(icon1, 16, 16);
				InventorButton CreateUpdate_Button = new InventorButton("Create /\rUpdate", "cap_Create/Update", "Create / Update Sheet List", "Click to create / update the Sheet List in this document.", icon1, icon1_sm);
				CreateUpdate_Button.Execute = ButtonEvents.CreateUpdate_SheetList;

				Icon icon2 = new Icon(this.GetType(), "Resources.gear.ico");
				Icon icon2_sm = new Icon(icon2, 16, 16);
				InventorButton Configure_Button = new InventorButton("Configure", "cap_Configure", "Configure Sheet List", "Click to configure Sheet List", icon2_sm, icon2_sm);
				Configure_Button.Execute = ButtonEvents.Configure_SheetList;

				if (firstTime)
				{
					UserInterfaceManager uiMan = AddinGlobal.InventorApp.UserInterfaceManager;

					if (uiMan.InterfaceStyle == InterfaceStyleEnum.kRibbonInterface)
					{
						Ribbon ribbon = uiMan.Ribbons["Drawing"];
						RibbonTab tab = ribbon.RibbonTabs["id_TabAnnotate"];

						RibbonPanel panel = tab.RibbonPanels.Add("Sheet List", "cap_SheetListPanel", Guid.NewGuid().ToString());
						CommandControls controls = panel.CommandControls;

						controls.AddButton(CreateUpdate_Button.ButtonDef(), true, true);
						controls.AddButton(Configure_Button.ButtonDef(), false, true);

					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}


		}

		public void Deactivate()
		{
			// This method is called by Inventor when the AddIn is unloaded.
			// The AddIn will be unloaded either manually by the user or
			// when the Inventor session is terminated


			// Release objects.
			AddinGlobal.InventorApp = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}

		public void ExecuteCommand(int commandID)
		{
			// Note:this method is now obsolete, you should use the 
			// ControlDefinition functionality for implementing commands.
		}

		public object Automation
		{
			// This property is provided to allow the AddIn to expose an API 
			// of its own to other programs. Typically, this  would be done by
			// implementing the AddIn's API interface in a class and returning 
			// that class object through this property.

			get
			{

				return null;
			}
		}

		#endregion

	}
}
