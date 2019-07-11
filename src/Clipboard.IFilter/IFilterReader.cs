using Clipboard.Abstraction;
using Clipboard.Interop;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Clipboard.IFilter
{
    public class IFilterReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            try
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
            catch(Exception e)
            {
                throw new Exception("Unsupported file type");
            }
        }

        public async Task<string> ReadAsync(FileStream fileStream)
        {
            try
            {
                var tempFile = $"{Path.GetTempPath()}{fileStream.Name}";

                using (var ms = new MemoryStream())
                {
                    await fileStream.CopyToAsync(ms);
                    File.WriteAllBytes(tempFile, ms.ToArray());
                }

                using (var r = new FilterReader(tempFile))
                {
                    return await r.ReadToEndAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unsupported file type");
            }            
        }
    }
}
