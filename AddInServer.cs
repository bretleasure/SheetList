using Inventor;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SheetList.Buttons;
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

			AddinGlobal.Automation = new SheetListAutomation();

			//Get User Settings
			SheetListTools.LoadSavedSettings();

			//Create Event Listener
			SheetListTools.CreateEventListener();

			// AddinGlobal.InventorApp.ApplicationEvents.OnApplicationOptionChange += UpdateButtons;

			try
            {
                var createButton = new CreateSheetListButton();
                var configureButton = new ConfigureButton();

				if (firstTime)
				{
					UserInterfaceManager uiMan = AddinGlobal.InventorApp.UserInterfaceManager;

					if (uiMan.InterfaceStyle == InterfaceStyleEnum.kRibbonInterface)
					{
						Ribbon ribbon = uiMan.Ribbons["Drawing"];
						RibbonTab tab = ribbon.RibbonTabs["id_TabAnnotate"];

						RibbonPanel panel = tab.RibbonPanels.Add("Sheet List", "sl_Panel", Guid.NewGuid().ToString());
						CommandControls controls = panel.CommandControls;

						var create = controls.AddButton(createButton.Definition, true, true);
						var config = controls.AddButton(configureButton.Definition, false, true);
						
						AddinGlobal.Buttons = new List<InventorButton>
						{
							createButton,
							configureButton
						};
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}

        }
		
		// public void UpdateButtons(EventTimingEnum beforeOrAfter, NameValueMap context, out HandlingCodeEnum handlingCode)
		// {
		// 	if (beforeOrAfter == EventTimingEnum.kAfter)
		// 	{
		// 		foreach (var button in AddinGlobal.Buttons)
		// 		{
		// 			button.UpdateIcons();
		// 		}
  //               
		// 		handlingCode = HandlingCodeEnum.kEventHandled;
		// 	}
  //           
		// 	handlingCode = HandlingCodeEnum.kEventNotHandled;
		// }

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
				return AddinGlobal.Automation;
			}
		}

		#endregion

	}
}
