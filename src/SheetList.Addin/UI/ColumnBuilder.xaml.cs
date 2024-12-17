using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Inventor;
using SheetList.Enums;

namespace SheetList.UI
{
	public partial class ColumnBuilder : Window
	{
		public ObservableCollection<PropertyColumn> SelectedColumns { get; set; }

		public ColumnBuilder(ObservableCollection<PropertyColumn> selectedColumns)
		{
			SelectedColumns = selectedColumns;
			DataContext = this;
			InitializeComponent();
			
			// lb_SelectedProperties.ItemsSource = SelectedColumns;
		}

		public PropertySource Source { get; set; }

		public Dictionary<PropertySource, string> PropertySourceItems
		{
			get
			{
				var items = new Dictionary<PropertySource, string>()
				{
					{ PropertySource.None, "Select Source" },
					{ PropertySource.Sheet, PropertySource.Sheet.ToFriendlyString() },
					{ PropertySource.Drawing, PropertySource.Drawing.ToFriendlyString() }
				};
				
				if (ActiveSheetReferenceDoc != null)
				{
					items.Add(PropertySource.SheetDocument, PropertySource.SheetDocument.ToFriendlyString());
				}

				if (ActiveDrawingDoc.ActiveSheet.HasTitleBlock())
				{
					items.Add(PropertySource.TitleBlock, PropertySource.TitleBlock.ToFriendlyString());
				}

				return items;
			}
		}

		private List<PropertyColumn> Properties
			=> PropertyNames.Select(p => new PropertyColumn(Source, p))
				.OrderBy(p => p.PropertyName)
				.ToList();
		
		private List<string> PropertyNames
		{
			get
			{
				return Source switch
				{
					PropertySource.Drawing =>  ActiveDrawingDoc.GetPropertyNames(),
					PropertySource.SheetDocument => ActiveSheetReferenceDoc.GetPropertyNames(),
					PropertySource.Sheet => ActiveDrawingDoc.ActiveSheet.GetPropertyNames(),
					PropertySource.TitleBlock => ActiveDrawingDoc.ActiveSheet.GetTitleBlockPromptedTextBoxes().Select(t => t.Key).ToList(),
					_ => Enumerable.Empty<string>().ToList()
				};
			}
		}

		private DrawingDocument ActiveDrawingDoc => AddinServer.InventorApp.ActiveDocument as DrawingDocument;

		private Document ActiveSheetReferenceDoc
		{
			get
			{
				var dwgDoc = ActiveDrawingDoc as DrawingDocument;
				return dwgDoc?.ActiveSheet.GetSheetDocument();
			}
		}

		private void Cb_Source_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				lb_Properties.ItemsSource = null;
				lb_Properties.ItemsSource = Properties;
			}
			catch
			{
				// ignored - workaround for when the window is not yet loaded
			}
		}

		private void Btn_AddProp_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedProp = lb_Properties.SelectedItem;
			if (selectedProp != null)
			{
				SelectedColumns.Add(selectedProp as PropertyColumn);
			}
		}

		private void Btn_RemoveProp_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedProp = lb_SelectedProperties.SelectedItem;
			if (selectedProp != null)
			{
				SelectedColumns.Remove(selectedProp as PropertyColumn);
			}
		}

		private void Btn_MoveUp_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedIndex = lb_SelectedProperties.SelectedIndex;
			if (selectedIndex > 0)
			{
				var selectedItem = lb_SelectedProperties.SelectedItem as PropertyColumn;
				SelectedColumns.RemoveAt(selectedIndex);
				SelectedColumns.Insert(selectedIndex - 1, selectedItem);
				lb_SelectedProperties.SelectedIndex = selectedIndex - 1;
			}
		}

		private void Btn_MoveDown_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedIndex = lb_SelectedProperties.SelectedIndex;
			if (selectedIndex < lb_SelectedProperties.Items.Count - 1 && selectedIndex >= 0)
			{
				var selectedItem = lb_SelectedProperties.SelectedItem as PropertyColumn;
				SelectedColumns.RemoveAt(selectedIndex);
				SelectedColumns.Insert(selectedIndex + 1, selectedItem);
				lb_SelectedProperties.SelectedIndex = selectedIndex + 1;
			}
		}

		private void Btn_OK_OnClick(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}
	}

}