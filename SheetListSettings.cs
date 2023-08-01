using Inventor;

namespace SheetList
{
    public class SheetListSettings
    {
        public string Title { get; set; }
        public bool ShowTitle { get; set; }

        public string SheetNoColName { get; set; }
        public string SheetNameColName { get; set; }

        /// <summary>
        /// Columns widths provided in centimeters as a comma delimited string
        /// </summary>
        public string ColumnWidths { get; set; } = "2.5,5";

        /// <summary>
        /// kTopDownDirection = 46081,
        /// kBottomUpDirection = 46082
        /// </summary>
        public TableDirectionEnum Direction { get; set; }

        /// <summary>
        /// kHeadingAtTop = 46337,
        /// kHeadingAtBottom = 46338,
        /// kNoHeading = 46339
        /// </summary>
        public HeadingPlacementEnum HeadingPlacement { get; set; }

        /// <summary>
        /// Wrap Direction.  Default is Wrap Right
        /// </summary>
        public bool WrapLeft { get; set; }

        public bool EnableAutoWrap { get; set; }

        public int MaxRows { get; set; }
        public int NumberOfSections { get; set; }
        public bool ControlMaxRows { get; set; } = false;
        public bool ControlNumberOfSections { get; set; } = true;

        //TODO: CREATE SEPARATE SHEET LIST SETTINGS FROM ADDIN SETTINGS (UPDATE BEFORE SAVE, CONTROL MAX ROWS, CONTROLNUMBEROFSECTIONS SHOULD BE APP SETTINGS)
        public bool UpdateBeforeSave { get; set; } = false;

        public static readonly SheetListSettings Default = new SheetListSettings
        {
            Title = "SHEET LIST",
            ShowTitle = true,
            SheetNoColName = "SHEET #",
            SheetNameColName = "SHEET NAME",
            ColumnWidths = "2.5,5",
            Direction = TableDirectionEnum.kTopDownDirection,
            HeadingPlacement = HeadingPlacementEnum.kHeadingAtTop,
            WrapLeft = false,
            EnableAutoWrap = false,
            MaxRows = 10,
            NumberOfSections = 1,
            ControlMaxRows = false,
            ControlNumberOfSections = true,
            UpdateBeforeSave = false
        };

    }
}
