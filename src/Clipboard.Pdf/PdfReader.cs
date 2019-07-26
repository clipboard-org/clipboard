using System.IO;
using System.Text;
using System.Threading.Tasks;
using Clipboard.Abstraction;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace Clipboard.Pdf
{
    public sealed class PdfReader : IDocumentReader
    {
        public string Read(Stream stream)
        {
            using (var pdfReader = new iText.Kernel.Pdf.PdfReader(stream))
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

        public Task<string> ReadAsync(Stream stream)
        {
            return Task.FromResult(Read(stream));
        }
    }
}
