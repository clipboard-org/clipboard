using System.IO;
using System.Threading.Tasks;
using Clipboard.Abstraction;
using PdfSharpCore.Pdf.IO;

namespace Clipboard.Pdf
{
    public sealed class PdfReader : IDocumentReader
    {
        public string Read(Stream stream)
        {
            using (var document = PdfSharpCore.Pdf.IO.PdfReader.Open(stream, PdfDocumentOpenMode.ReadOnly))
            {
                return document.Pages.ExtractText();
            }
        }

        public Task<string> ReadAsync(Stream stream)
        {
            return Task.FromResult(Read(stream));
        }
    }
}
