using Clipboard.Abstraction;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard.OpenXml
{
    public class PowerpointReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            var sb = new StringBuilder();

            using (var doc = PresentationDocument.Open(Package.Open(fileStream)))
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

        public Task<string> ReadAsync(FileStream fileStream)
        {
            return Task.FromResult(Read(fileStream));
        }
    }
}
