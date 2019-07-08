using Clipboard.Abstraction;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard.Pdf
{
    public class PdfReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            using (var pdfReader = new iText.Kernel.Pdf.PdfReader(fileStream))
            using (var document = new PdfDocument(pdfReader))
            {
                var sb = new StringBuilder();

                for (int i = 1; i <= document.GetNumberOfPages(); i++)
                {
                    var page = document.GetPage(i);
                    var text = PdfTextExtractor.GetTextFromPage(page);

                    sb.AppendLine(text);
                }

                return sb.ToString();
            }
        }

        public Task<string> ReadAsync(FileStream fileStream)
        {
            return Task.FromResult(Read(fileStream));
        }
    }
}
