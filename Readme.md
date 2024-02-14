![open-graph-preview-img](https://raw.githubusercontent.com/bretleasure/SheetList/82c43b4a4916f0fe861f9885fe8dba133a491d8a/img/open-graph-preview-img.png)

## Usage

The addin can either be used via the commmand buttons added to the Inventor UI or by using the API with your own code. 

## Installation

### [Download and Install from the Autodesk App Store](https://apps.autodesk.com/INVNTOR/en/Detail/Index?id=5342673156831821071&appLang=en&os=Win64)

### Install from GitHub
1. Download the SheetListAddin-vX.X.X.zip file from the latest release
2. Unzip the contents into `C:\ProgramData\Autodesk\ApplicationPlugins`
3. First time starting Inventor the Addin may need to be unblocked.
   * Go to Tools Tab > Options Panel > Add-ins
     * Find the addin in the Available Add-Ins list and select it. Then Uncheck the *Block* checkbox and check *Load/Unloaded* and *Load Automatically*

### If using the Addin's API outside of iLogic:

The SheetList.dll needs to be added as a reference to your project

* NuGet package available on [NuGet.org](https://www.nuget.org/packages/SheetList) and [GitHub Packages](https://github.com/bretleasure/SheetList/pkgs/nuget/SheetList)

## Addin UI

The Sheet List panel is added to the Annotate tab when a drawing document is open.

## Using the API

The `GetSheetListAddin()` extension method for `Inventor.Aplication` can be used to get the instance of `SheetListAutomation`.

`SheetListAutomation` includes the following methods that can be used:

| Method Name | Description                                     |
| - |-------------------------------------------------|
| CreateSheetList(`Sheet`, `Point2d`) | Creates a sheetlist using the default settings  |
| CreateSheetList(`Sheet`, `Point2d`, `SheetListSettings`) | Creates a sheetlist using the provided settings |

### Extension Methods
Extension methods on the `Sheet` type can also be used. Using these extension methods does not require getting an instance of `SheetListAutomation`.

| Method Name | Description |
| - | - |
| CreateSheetList(`Point2d`) | Creates a sheetlist using the default settings |
| CreateSheetList(`Point2d`, `SheetListSettings`) | Creates a sheetlist using the provided settings |

### C#
```csharp
var dwgDoc = (DrawingDocument)inventorApp.Documents.Open(@"C:\Work\MyDrawing.idw");

//Using the SheetListAutomation method

var sheetListAddin = inventorApp.GetSheetListAddin();

sheetListAddin.CreateSheetList(dwgDoc.ActiveSheet);

//Using the Extension Methods

dwgDoc.ActiveSheet.CreateSheetList();
```

### iLogic
```vb
AddReference "SheetList"
Imports SheetList

Dim dwgDoc As DrawingDocument
dwgDoc = ThisDoc.Document

'Using the SheetListAutomation Method

Dim sheetListAddin As SheetListAutomation
sheetListAddin = ThisApplication.GetSheetListAddin()

sheetListAddin.CreateSheetList(dwgDoc.ActiveSheet)

'Using the Extension Methods

dwgDoc.ActiveSheet.CreateSheetList()
```

## Configuration

If using the addin in the Inventor UI, settings can be set by clicking the Configure button in the Sheet List Ribbon Panel. If using the addin API, customizations are made by passing in `SheetListSettings` into the `CreateSheetList` methods.


> [!IMPORTANT]
> The API methods will not use the same settings that are set using the addin's configure window.


### `SheetListSettings`

| Setting | Type | Description |
| - | --- | --- |
| Title | string | Title of the Sheet List Table |
| ShowTitle | boolean | Whether the Title is shown on the Table |
| ColumnNames | string[] | Column names for the table |
| ColumnWidths | double[] | Column widths (in centimeters) for the table |
| Direction | TableDirectionEnum | Direction of the Table (Top Down / Bottom up) |
| HeadingPlacement | HeadingPlacementEnum | Placement of the column headings |
| WrapLeft | boolean | Wrap table to the left |
| EnableAutoWrap | boolean | Enables auto wrapping of the table base on MaxRows and NumberOfSections |
| MaxRows | int | Maximum number of rows before the table is wrapped |
| NumberOfSections | int | The number of vertical sections the table should wrap to |

## Privacy Policy

This application does not collect or store any personal data.
