// ConfigWindow.xaml.cs

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Inventor;
using SheetList.Enums;

namespace SheetList.UI
{
    public partial class ConfigUI : Window
    {
        public SheetListAddinSettings _addinSettings { get; }
        private ObservableCollection<MyData> _data;
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

        public ConfigUI(SheetListAddinSettings addinSettings)
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
            
            InitializeComponent();
            DataContext = this;
            _data = new ObservableCollection<MyData>
            {
                new MyData { ColumnName = "SHEET #", Property = "SheetNumber", ColumnWidth = 1 },
                new MyData { ColumnName = "NAME", Property = "SheetName", ColumnWidth = 1 },
                new MyData { ColumnName = "REVISION", Property = "SheetRevision", ColumnWidth = 1 }
            };
            dataGrid.ItemsSource = _data;
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

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _draggedRow = FindVisualParent<DataGridRow>(e.OriginalSource as DependencyObject);
        }

        private void DataGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _draggedRow != null)
            {
                DragDrop.DoDragDrop(_draggedRow, _draggedRow.Item, DragDropEffects.Move);
            }
        }

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            if (_draggedRow != null)
            {
                var targetRow = FindVisualParent<DataGridRow>(e.OriginalSource as DependencyObject);
                if (targetRow != null)
                {
                    var draggedItem = _draggedRow.Item as MyData;
                    var targetItem = targetRow.Item as MyData;

                    int draggedIndex = _data.IndexOf(draggedItem);
                    int targetIndex = _data.IndexOf(targetItem);

                    _data.Move(draggedIndex, targetIndex);
                }
            }
        }

        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;

            if (parentObject is T parent) return parent;
            return FindVisualParent<T>(parentObject);
        }
    }

    public class MyData
    {
        public string ColumnName { get; set; }
        public string Property { get; set; }
        public double ColumnWidth { get; set; }
    }
}