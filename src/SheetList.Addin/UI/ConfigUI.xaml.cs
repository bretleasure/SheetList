// ConfigWindow.xaml.cs

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Inventor;
using SheetList.Enums;

namespace SheetList.UI
{
    public partial class ConfigUI : Window
    {
        private readonly SheetListAddinSettings _documentSettings;
        public SheetListAddinSettings _addinSettings { get; private set; }
        private DataGridRow _draggedRow;

        private SheetListSettings _sheetListSettings { get; set; }

        public bool ShowTitle { get; set; }
        public string TableTitle { get; set; }
        public TableDirectionEnum Direction { get; set; }
        public HeadingPlacementEnum HeadingPlacement { get; set; }
        public TableAnchor Anchor { get; set; }
        public bool EnableAutomaticWrap { get; set; }
        public bool WrapLeft { get; set; }
        public int MaxRows { get; set; }
        public int NumberOfSections { get; set; }
        public bool ControlMaxRows { get; set; }
        public bool ControlNumberOfSections { get; set; }
        public bool UpdateBeforeSave { get; set; }
        public ObservableCollection<PropertyColumn> ColumnData { get; set; }

        public ConfigUI(SheetListAddinSettings addinSettings, SheetListAddinSettings documentSettings)
        {
            _documentSettings = documentSettings;
            ImportSettings(addinSettings);
        }

        void ImportSettings(SheetListAddinSettings addinSettings)
        {
            _addinSettings = addinSettings;
            _sheetListSettings = addinSettings?.SheetListSettings;

            ShowTitle = _sheetListSettings.ShowTitle;
            TableTitle = _sheetListSettings.Title;
            Direction = _sheetListSettings.Direction;
            HeadingPlacement = _sheetListSettings.HeadingPlacement;
            Anchor = _sheetListSettings.Anchor;
            EnableAutomaticWrap = _sheetListSettings.EnableAutoWrap;
            WrapLeft = _sheetListSettings.WrapLeft;
            MaxRows = _sheetListSettings.MaxRows;
            NumberOfSections = _sheetListSettings.NumberOfSections;
            ControlMaxRows = _addinSettings.ControlMaxRows;
            ControlNumberOfSections = _addinSettings.ControlNumberOfSections;
            UpdateBeforeSave = _addinSettings.UpdateBeforeSave;
            ColumnData = new ObservableCollection<PropertyColumn>(_sheetListSettings.ColumnPropertyData);
            
            InitializeComponent();
            DataContext = this;
            dataGrid.ItemsSource = ColumnData;
        }

        public Dictionary<TableDirectionEnum, string> TableDirectionsWithTitles { get; } = new()
        {
            {TableDirectionEnum.kTopDownDirection, "Add Rows to Bottom"},
            {TableDirectionEnum.kBottomUpDirection, "Add Rows to Top"}
        };
        
        public Dictionary<HeadingPlacementEnum, string> HeadingPlacementWithTitles { get; } = new()
        {
            {HeadingPlacementEnum.kHeadingAtTop, "Heading at Top"},
            {HeadingPlacementEnum.kHeadingAtBottom, "Heading at Bottom"},
            {HeadingPlacementEnum.kNoHeading, "No Heading"}
        };
        
        public Dictionary<bool, string> WrapDirectionWithTitles { get; } = new()
        {
            {true, "Wrap Left"},
            {false, "Wrap Right"}
        };
        
        public Dictionary<TableAnchor, string> AnchorWithTitles { get; } = new()
        {
            {TableAnchor.Top, "Top"},
            {TableAnchor.Bottom, "Bottom"},
        };

        private void Btn_ChooseColumns_OnClick(object sender, RoutedEventArgs e)
        {
            var colChooser = new ColumnBuilder(ColumnData)
            {
                Owner = this
            };
            colChooser.ShowDialog();
        }

        private void Btn_Save_OnClick(object sender, RoutedEventArgs e)
        {
            _sheetListSettings.ShowTitle = ShowTitle;
            _sheetListSettings.Title = TableTitle;
            _sheetListSettings.Direction = Direction;
            _sheetListSettings.HeadingPlacement = HeadingPlacement;
            _sheetListSettings.Anchor = Anchor;
            _sheetListSettings.EnableAutoWrap = EnableAutomaticWrap;
            _sheetListSettings.WrapLeft = WrapLeft;
            _sheetListSettings.MaxRows = MaxRows;
            _sheetListSettings.NumberOfSections = NumberOfSections;
            _sheetListSettings.ColumnPropertyData = ColumnData.ToList();
            AddinServer.AppSettings.ControlMaxRows = ControlMaxRows;
            AddinServer.AppSettings.ControlNumberOfSections = ControlNumberOfSections;
            AddinServer.AppSettings.UpdateBeforeSave = UpdateBeforeSave;
            AddinServer.AppSettings.SheetListSettings = _sheetListSettings;
            SheetListTools.SaveSettings();
            SheetListTools.CreateEventListener();
            Close();
        }

        private void Btn_Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_Import_OnClick(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                Title = "Import Settings",
                Filter = "JSON Files (*.json)|*.json",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,
            };

            openDialog.ShowDialog();

            try
            {
                if (string.IsNullOrWhiteSpace(openDialog.FileName))
                {
                    return;
                }
                var importedSettings = SheetListTools.ImportSettings(openDialog.FileName);

                //reload Window settings
                ImportSettings(importedSettings);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Could not import settings. " + ex.Message);
            }
        }

        private void Btn_Export_OnClick(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Title = "Export Settings",
                Filter = "JSON Files (*.json)|*.json",
                FileName = "SheetListSettings.json",
                OverwritePrompt = true,
                AddExtension = true
            };
            
            saveDialog.ShowDialog();
            
            SheetListTools.ExportSettings(saveDialog.FileName);
        }
    }
}