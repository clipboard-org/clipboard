using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Threading.Tasks;
using Clipboard.Abstraction;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;

namespace Clipboard.OpenXml
{
    public sealed class PowerpointReader : IDocumentReader
    {
        public string Read(Stream stream)
        {
            var sb = new StringBuilder();

            using (var doc = PresentationDocument.Open(Package.Open(stream)))
            {
                foreach (var slide in doc.PresentationPart.SlideParts)
                {
                    foreach (var text in slide.Slide.Descendants<Text>())
                    {
                        sb.Append(text.Text);
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
