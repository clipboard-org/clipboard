using Clipboard.Abstraction;
using System.IO;
using System.Threading.Tasks;

namespace Clipboard.Text
{
    public class PlainReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            using (var sr = new StreamReader(fileStream))
            {
                return sr.ReadToEnd();
            }
        }

        public async Task<string> ReadAsync(FileStream fileStream)
        {
            using (var sr = new StreamReader(fileStream))
            {
                return await sr.ReadToEndAsync();
            }
        }
    }
}
