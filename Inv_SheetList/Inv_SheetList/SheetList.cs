using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace Inv_SheetList
{
    public class SheetList
    {
        public bool ValidId;

        public string Title;
        public bool ShowTitle;

        public string SheetNoColName;
        public string SheetNameColName;

        /// <summary>
        /// kTopDownDirection = 46081,
        /// kBottomUpDirection = 46082
        /// </summary>
        public TableDirectionEnum Direction;

        /// <summary>
        /// kHeadingAtTop = 46337,
        /// kHeadingAtBottom = 46338,
        /// kNoHeading = 46339
        /// </summary>
        public HeadingPlacementEnum HeadingPlacement;

        /// <summary>
        /// Wrap Direction.  Default is Wrap Right
        /// </summary>
        public bool WrapLeft;

        public bool EnableAutoWrap;

        public int MaxRows;
        public int NumberOfSections;

    }
}
