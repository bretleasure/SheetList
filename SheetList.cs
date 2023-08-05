using System;
using System.Linq;
using Inventor;
using SheetList.Extensions;

namespace SheetList
{
    internal class SheetList
    {
        public SheetList(CustomTable existingSheetList, SheetListSettings settings, string[] data)
        {
            Position = existingSheetList.Position;
            ParentSheet = existingSheetList.Parent as Sheet;
            ColumnNames = existingSheetList.Columns.Cast<Column>()
                .Select(c => c.Title)
                .ToArray();
            ColumnWidths = existingSheetList.Columns.Cast<Column>()
                .Select(c => c.Width).ToArray();
            TranslatePosition = (sheetList) =>
            {
                //Adjust Table Location if table is bottom up
                if (sheetList.TableDirection == TableDirectionEnum.kBottomUpDirection)
                {
                    var tableHeight = sheetList.GetTableHeight();
                    var oldNewHeightDiff = tableHeight - existingSheetList.GetTableHeight();

                    var newPosition = AddinGlobal.InventorApp.TransientGeometry.CreatePoint2d(Position.X, Position.Y + oldNewHeightDiff);
                    sheetList.Position = newPosition;
                }
            };
            Initialize(settings, data);
        }

        public SheetList(SheetListSettings settings, Sheet sheet, Point2d position, string[] data)
        {
            Position = position;
            ParentSheet = sheet;
            ColumnWidths = settings.ColumnWidths;
            Initialize(settings, data);
        }

        void Initialize(SheetListSettings settings, string[] data)
        {
            Data = data;
            Title = settings.Title;
            ColumnNames = settings.ColumnNames;
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

        private Action<CustomTable> TranslatePosition
        {
            get => sheetList => { };
            set { }
        }

        public CustomTable Create()
        {
            var sheetList = ParentSheet.CustomTables.Add(Title, Position, 2, RowQty, ColumnNames, Data, ColumnWidths);
            sheetList.ShowTitle = ShowTitle;
            sheetList.TableDirection = TableDirection;
            sheetList.HeadingPlacement = HeadingPlacement;
            sheetList.WrapAutomatically = WrapAutomatically;
            sheetList.WrapLeft = WrapLeft;
            sheetList.MaximumRows = MaxRows;
            if (NumberOfSections > 0)
            {
                sheetList.NumberOfSections = NumberOfSections;
            }

            TranslatePosition.Invoke(sheetList);

            sheetList.SaveAttributesToTable();

            return sheetList;
        }
    }
}