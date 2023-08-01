using Inventor;

namespace SheetList.Extensions
{
    public static class InventorApplicationExtensions
    {
        public static dynamic GetSheetListAddin(this Application inventor)
        {
            var addin = inventor.ApplicationAddIns.ItemById["{3dc3068f-5de4-4b85-9efe-44b7ece560f3}"];

            if (addin != null)
            {
                addin.Activate();

                return addin.Automation;
            }
            else
                return null;
        }
    }
}