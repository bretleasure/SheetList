using System;
using System.Linq;
using Inventor;
using SheetList.Enums;

namespace SheetList
{
    internal class SheetList
    {
        private readonly SheetListSettings _settings;

        public SheetList(CustomTable existingSheetList, SheetListSettings settings, string[] data)
        {
            _settings = settings;
            Position = existingSheetList.Position;
            ParentSheet = existingSheetList.Parent as Sheet;
            
            if (existingSheetList.Columns.Count != settings.ColumnPropertyData.Count)
            {
                ColumnWidths = settings.ColumnPropertyData.Select(c => c.ColumnWidth).ToArray();
            }
            else
            {
                ColumnWidths = existingSheetList.Columns.Cast<Column>()
                    .Select(c => c.Width).ToArray();
            }
            
            TranslationYModifier = existingSheetList.GetTableHeight();
            
            Initialize(settings, data);
        }

        public SheetList(SheetListSettings settings, Sheet sheet, Point2d position, string[] data)
        {
            _settings = settings;
            Position = position;
            ParentSheet = sheet;
            ColumnWidths = settings.ColumnPropertyData.Select(c => c.ColumnWidth).ToArray();
            
            //Set Modifier to 0 so that the table is translated using its own height
            TranslationYModifier = 0;
            Initialize(settings, data);
        }

        void Initialize(SheetListSettings settings, string[] data)
        {
            Data = data;
            Title = settings.Title;
            ColumnNames = settings.ColumnPropertyData.Select(c => c.ColumnName).ToArray();
            ShowTitle = settings.ShowTitle;
            TableDirection = settings.Direction;
            HeadingPlacement = settings.HeadingPlacement;
            WrapLeft = settings.WrapLeft;
            WrapAutomatically = settings.EnableAutoWrap;
            MaxRows = settings.MaxRows;
            NumberOfSections = settings.NumberOfSections;
        }

        private string[] Data { get; set; }
        private int RowQty => Data.Count() / ColumnNames.Count();
        private double[] ColumnWidths { get; set; }
        private string Title { get; set; }
        private string[] ColumnNames { get; set; }
        private bool ShowTitle { get; set; }
        private TableDirectionEnum TableDirection { get; set; }
        private HeadingPlacementEnum HeadingPlacement { get; set; }
        private bool WrapLeft { get; set; }
        private bool WrapAutomatically { get; set; }
        private int MaxRows { get; set; }
        private int NumberOfSections { get; set; }
        private Point2d Position { get; set; }
        private Sheet ParentSheet { get; }
        private CustomTable SheetListTable { get; set; }
        
        /// <summary>
        /// Translation in Y direction
        /// </summary>
        private double TranslationYModifier { get; }

        public CustomTable Create()
        {
            SheetListTable = ParentSheet.CustomTables.Add(Title, Position, ColumnNames.Length, RowQty, ColumnNames, Data, ColumnWidths);
            SheetListTable.ShowTitle = ShowTitle;
            SheetListTable.TableDirection = TableDirection;
            SheetListTable.HeadingPlacement = HeadingPlacement;
            SheetListTable.WrapAutomatically = WrapAutomatically;
            SheetListTable.WrapLeft = WrapLeft;
            SheetListTable.MaximumRows = MaxRows;
            if (NumberOfSections > 0 && AddinServer.AppSettings.ControlNumberOfSections)
            {
                SheetListTable.NumberOfSections = NumberOfSections;
            }
            
            SheetListTable.SaveAttributesToTable();
            
            //Translate Table Location
            if (_settings.Anchor == TableAnchor.Bottom)
            {
                var tableHeight = SheetListTable.GetTableHeight();
                var heightDiff = tableHeight - TranslationYModifier;

                var newPosition = AddinServer.InventorApp.TransientGeometry.CreatePoint2d(Position.X, Position.Y + heightDiff);
                SheetListTable.Position = newPosition;
            }

            return SheetListTable;
        }
    }
}