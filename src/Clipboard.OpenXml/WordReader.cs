using Clipboard.Abstraction;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Clipboard.OpenXml
{
    public class WordReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            throw new NotImplementedException();
        }

        public Task<string> ReadAsync(FileStream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}
