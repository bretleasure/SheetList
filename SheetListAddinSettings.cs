namespace SheetList
{
    internal class SheetListAddinSettings
    {
        public bool UpdateBeforeSave { get; set; } = false;
        public bool ControlMaxRows { get; set; } = false;
        public bool ControlNumberOfSections { get; set; } = true;
        public SheetListSettings SheetListSettings { get; set; }
    }
}