using System;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clipboard.Abstraction;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Clipboard.OpenXml
{
    public sealed class ExcelReader : IDocumentReader
    {
        public string Read(Stream stream)
        {
            var sb = new StringBuilder();
            using(var document = SpreadsheetDocument.Open(Package.Open(stream)))
            {
                var stringValues = document.WorkbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ToArray();

                foreach (var sheet in document.WorkbookPart.WorksheetParts)
                {    
                    foreach(var cell in sheet.Worksheet.Descendants<Cell>())
                    {
                        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                        {
                            var index = int.Parse(cell.CellValue.Text);
                            sb.Append(stringValues[index].InnerText);                            
                        }
                        else if(cell.CellFormula != null)
                        {
                            sb.Append(cell.CellFormula.Text);
                        }
                        else
                        {
                            sb.Append(cell.CellValue.Text);
                        }

                        sb.Append(Environment.NewLine);
                    }
                }
            }

            return sb.ToString();
        }

        public Task<string> ReadAsync(Stream stream)
        {
            return Task.FromResult(Read(stream));
        }
    }
}
