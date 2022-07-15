using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool ControlMaxRows { get; set; }
        public bool ControlNumberOfSections { get; set; }

        public bool UpdateBeforeSave { get; set; }

    }
}
