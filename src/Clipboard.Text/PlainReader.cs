using System.IO;
using System.Threading.Tasks;
using Clipboard.Abstraction;

namespace Clipboard.Text
{
    public sealed class PlainReader : IDocumentReader
    {
        public string Read(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }

        public async Task<string> ReadAsync(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return await sr.ReadToEndAsync();
            }
        }
    }
}
