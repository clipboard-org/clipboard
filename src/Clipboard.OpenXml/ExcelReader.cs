using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Threading.Tasks;
using Clipboard.Abstraction;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Clipboard.OpenXml
{
    public class ExcelReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            var sb = new StringBuilder();
            using(var document = SpreadsheetDocument.Open(Package.Open(fileStream)))
            {
                foreach(var sheet in document.WorkbookPart.WorksheetParts)
                {
                    foreach(var cell in sheet.Worksheet.Descendants<Cell>())
                    {
                        sb.Append(cell.CellFormula?.Text ?? cell.CellValue.Text);
                        sb.Append(" ");
                    }
                }
            }

            return sb.ToString();
        }

        public Task<string> ReadAsync(FileStream fileStream)
        {
            return Task.FromResult(Read(fileStream));
        }
    }
}
