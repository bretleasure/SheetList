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
        public string[] ColumnNames { get; set; } = new string[] { "SHEET #", "SHEET NAME" };
        public List<SheetProperty> SheetPropertyTypes { get; set; }
        
        /// <summary>
        /// Column index of the revision table date (only used if SheetProperty <see cref="SheetProperty.RevisionDate"/> is used
        /// </summary>
        public int RevisionTableDateColumnIndex { get; set; }

        /// <summary>
        /// Columns widths provided in centimeters
        /// </summary>
        public double[] ColumnWidths { get; set; } = new double[] { 2.5, 5 };

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

        [JsonIgnore]
        public Func<DrawingDocument, SheetListSettings, string[]> TableDataBuilder { get; set; } = (dwgDoc, settings) => dwgDoc.GetSheetListData(settings);

        public static readonly SheetListSettings Default = new()
        {
            Title = "SHEET LIST",
            ShowTitle = true,
            ColumnNames = ["SHEET #", "SHEET NAME"],
            SheetPropertyTypes = [SheetProperty.SheetNumber, SheetProperty.SheetName],
            ColumnWidths = new double[] { 2.5, 5 },
            Direction = TableDirectionEnum.kTopDownDirection,
            HeadingPlacement = HeadingPlacementEnum.kHeadingAtTop,
            WrapLeft = false,
            EnableAutoWrap = false,
            MaxRows = 10,
            NumberOfSections = 1,
            Anchor = TableAnchor.Top,
            // TableDataBuilder = (dwgDoc, settings) => dwgDoc.GetSheetListData(settings)
        };

    }
}
