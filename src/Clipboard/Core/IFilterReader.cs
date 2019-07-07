using Clipboard.Abstraction;
using Clipboard.Interop;
using System.IO;
using System.Threading.Tasks;

namespace Clipboard.Core
{
    internal class IFilterReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            var tempFile = $"{Path.GetTempPath()}{fileStream.Name}";

            using (var ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                File.WriteAllBytes(tempFile, ms.ToArray());
            }

            using (var r = new FilterReader(tempFile))
            {
                return r.ReadToEnd();
            }
        }

        public async Task<string> ReadAsync(FileStream fileStream)
        {
            var tempFile = $"{Path.GetTempPath()}{fileStream.Name}";

            using (var ms = new MemoryStream())
            {
                await fileStream.CopyToAsync(ms);
                File.WriteAllBytes(tempFile, ms.ToArray());
            }

            using (var r = new FilterReader(tempFile))
            {
                return  await r.ReadToEndAsync();
            }
        }
    }
}
