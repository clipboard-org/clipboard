using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Clipboard.Abstraction;

namespace Clipboard.Text
{
    public class XmlReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            var doc = new XmlDocument();
            doc.Load(fileStream);
            return doc.InnerText;
        }

        public Task<string> ReadAsync(FileStream fileStream)
        {
            return Task.FromResult(Read(fileStream));
        }
    }
}
