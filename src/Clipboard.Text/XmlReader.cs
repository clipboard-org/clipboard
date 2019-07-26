using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Clipboard.Abstraction;

namespace Clipboard.Text
{
    public sealed class XmlReader : IDocumentReader
    {
        public string Read(Stream stream)
        {
            var doc = new XmlDocument();
            doc.Load(stream);
            return doc.InnerText;
        }

        public Task<string> ReadAsync(Stream stream)
        {
            return Task.FromResult(Read(stream));
        }
    }
}
