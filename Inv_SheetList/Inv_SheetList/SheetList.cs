using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv_SheetList
{
    class SheetList
    {
        string Title;
        bool ShowTitle;

        string SheetNoColName;
        string SheetNameColName;

        /// <summary>
        /// Bottom/Top
        /// Add new rows to bottom or top of list
        /// </summary>
        string Direction;

        /// <summary>
        /// Top/Bottom/None
        /// </summary>
        string Heading;

        /// <summary>
        /// Left/Right
        /// </summary>
        string WrapDirection;

        bool EnableAutoWrap;

        bool WrapByMaxRows;
        bool WrapByNumberOfSections;

        int MaxRows;
        int NumberOfSections;

    }
}
