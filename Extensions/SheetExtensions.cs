using System;
using System.Collections;
using System.Collections.Generic;
using Inventor;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SheetList;
using SheetList.Enums;

namespace Inventor
{
    public static class SheetExtensions
    {
        internal static string GetSheetName(this Sheet oSheet)
        {
            return oSheet.Name.Split(':').First();
        }

        internal static string GetSheetNumber(this Sheet sheet)
        {
            return sheet.Name.Split(':').Last();
        }
        
        internal static string GetRevision(this Sheet sheet)
        {
            return sheet.Revision;
        }

        internal static string GetLatestRevisionDate(this Sheet sheet, int dateColumnIndex)
        {
            var revTable = sheet.RevisionTables
                .Cast<RevisionTable>()
                .FirstOrDefault();

            if (revTable == null)
            {
                return string.Empty;
            }
            
            var latestRevRow = revTable.GetLatestRevisionTableRow();
            return latestRevRow[dateColumnIndex]?.Text ?? string.Empty;
        }
        
        internal static string[] GetSheetData(this Sheet sheet, SheetListSettings settings)
        {
            var data = new List<string>();

            foreach (var prop in settings.ColumnPropertyData)
            {
                switch (prop.Source)
                {
                    case PropertySource.Sheet:
                        data.Add(sheet.GetPropertyValue(prop.PropertyName) ?? string.Empty);
                        break;
                    case PropertySource.Drawing:
                        data.Add(((DrawingDocument)sheet.Parent).GetPropertyValue(prop.PropertyName) ?? string.Empty);
                        break;
                    case PropertySource.SheetDocument:
                        data.Add(sheet.GetSheetDocument()?.GetPropertyValue(prop.PropertyName) ?? string.Empty);
                        break;
                    case PropertySource.TitleBlock:
                        data.Add(sheet.GetTitleBlockPromptedTextValue(prop.PropertyName) ?? string.Empty);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return data.ToArray();
        }

        internal static string GetPropertyValue(this Sheet sheet, string propertyName)
        {
            return propertyName switch
            {
                SheetProperties.Name => sheet.GetSheetName(),
                SheetProperties.Number => sheet.GetSheetNumber(),
                SheetProperties.Revision => sheet.GetRevision(),
                _ => string.Empty
            };
        }

        internal static List<string> GetPropertyNames(this Sheet sheet)
        {
            return new List<string>
            {
                SheetProperties.Name,
                SheetProperties.Number,
                SheetProperties.Revision
            };
        }
        
        internal static Document GetSheetDocument(this Sheet sheet)
        {
            return sheet.DrawingViews
                .Cast<DrawingView>()
                .FirstOrDefault()?
                .ReferencedDocumentDescriptor.ReferencedDocument as Document;
        }
        
        internal static bool HasTitleBlock(this Sheet sheet)
            => sheet.TitleBlock != null;

        internal static Dictionary<string, TextBox> GetTitleBlockPromptedTextBoxes(this Sheet sheet)
        {
            if (sheet.HasTitleBlock())
            {
                var titleBlockDef = sheet.TitleBlock.Definition;
                var promptBoxes = titleBlockDef.Sketch.TextBoxes
                    .Cast<TextBox>()
                    .Where(t => t.FormattedText.Contains("<Prompt"))
                    .ToList();

                var prompts = promptBoxes.ToDictionary(p =>
                {
                    var match = Regex.Match(p.FormattedText, @">([\s\S]*?)<\/Prompt>");
                    return match.Success ? match.Groups[1].Value : string.Empty;
                });
                        
                
                return prompts.Where(p => !string.IsNullOrEmpty(p.Key))
                    .ToDictionary(p => p.Key, p => p.Value);
            }

            return new Dictionary<string, TextBox>();
        }

        internal static string GetTitleBlockPromptedTextValue(this Sheet sheet, string promptText)
        {
            var prompts = sheet.GetTitleBlockPromptedTextBoxes();
            return prompts.TryGetValue(promptText, out var prompt) ? sheet.TitleBlock.GetResultText(prompt) : string.Empty;
        }

        public static Task<CustomTable> CreateSheetList(this Sheet sheet, Point2d position)
            => sheet.CreateSheetList(position, SheetListSettings.Default);
        public static Task<CustomTable> CreateSheetList(this Sheet sheet, Point2d position, SheetListSettings settings)
        {
            return AddinServer.AppAutomation.CreateSheetList(sheet, position, settings);
        }
    }
}
