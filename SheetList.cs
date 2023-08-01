using System;
using System.Linq;
using Inventor;
using SheetList.Extensions;

namespace SheetList
{
    public class SheetList
    {
        public SheetList(CustomTable existingSheetList, SheetListSettings settings, string[] data)
        {
            Position = existingSheetList.Position;
            ParentSheet = existingSheetList.Parent as Sheet;
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
            ColumnWidths = settings.ColumnWidths.Split(',')
                .Select(s => s.Trim())
                .Select(double.Parse).ToArray();
            Initialize(settings, data);
        }

        void Initialize(SheetListSettings settings, string[] data)
        {
            Data = data;
            Title = settings.Title;
            ColumnNames = new string[]{ settings.SheetNoColName, settings.SheetNameColName };
            ShowTitle = settings.ShowTitle;
            TableDirection = settings.Direction;
            HeadingPlacement = settings.HeadingPlacement;
            WrapLeft = settings.WrapLeft;
            WrapAutomatically = settings.EnableAutoWrap;
            MaxRows = settings.MaxRows;
            NumberOfSections = settings.NumberOfSections;
        }
        
        public string[] Data { get; set; }
        public int RowQty => Data.Count() / ColumnNames.Count();
        public double[] ColumnWidths { get; set; }
        public string Title { get; set; }
        public string[] ColumnNames { get; set; }
        public bool ShowTitle { get; set; }
        public TableDirectionEnum TableDirection { get; set; }
        public HeadingPlacementEnum HeadingPlacement { get; set; }
        public bool WrapLeft { get; set; }
        public bool WrapAutomatically { get; set; }
        public int MaxRows { get; set; }
        public int NumberOfSections { get; set; }
        public Point2d Position { get; set; }
        public Sheet ParentSheet { get; }

        private Action<CustomTable> TranslatePosition
        {
            get
            {
                return (sheetList) => { };
            }
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