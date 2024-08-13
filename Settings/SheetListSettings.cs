using System;
using System.Collections.Generic;
using Inventor;
using Newtonsoft.Json;
using SheetList.Enums;

namespace SheetList
{
    public class SheetListSettings
    {
        public string Title { get; set; }
        public bool ShowTitle { get; set; }

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
        public TableAnchor Anchor { get; set; }
        public List<PropertyColumn> ColumnPropertyData { get; set; }
        
        public static readonly SheetListSettings Default = new()
        {
            Title = "SHEET LIST",
            ShowTitle = true,
            Direction = TableDirectionEnum.kTopDownDirection,
            HeadingPlacement = HeadingPlacementEnum.kHeadingAtTop,
            WrapLeft = false,
            EnableAutoWrap = false,
            MaxRows = 10,
            NumberOfSections = 1,
            Anchor = TableAnchor.Top,
            ColumnPropertyData =
            [
                new PropertyColumn(PropertySource.Sheet, "Sheet Number", 2.5),
                new PropertyColumn(PropertySource.Sheet, "Sheet Name", 5)
            ]
        };

    }
}
