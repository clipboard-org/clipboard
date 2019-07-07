using Clipboard.Abstraction;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Clipboard.Core
{
    internal class TextExtractor : ITextExtractor
    {
        private Func<string, IDocumentReader> _documentReaderFactory;

        public TextExtractor(Func<string, IDocumentReader> docuemntReaderFactory)
        {
            _documentReaderFactory = docuemntReaderFactory;
        }

        public string Extract(FileStream fileStream)
        {
            var reader = _documentReaderFactory.Invoke(GetContentType(fileStream.Name));
            return reader.Read(fileStream);
        }

        public string Extract(string filepath)
        {
            using (var fs = File.OpenRead(filepath))
            {
                var reader = _documentReaderFactory.Invoke(GetContentType(fs.Name));
                return reader.Read(fs);
            }
        }

        public Task<string> ExtractAsync(FileStream fileStream)
        {
            var reader = _documentReaderFactory.Invoke(GetContentType(fileStream.Name));
            return reader.ReadAsync(fileStream);
        }

        public Task<string> ExtractAsync(string filepath)
        {
            using (var fs = File.OpenRead(filepath))
            {
                var reader = _documentReaderFactory.Invoke(GetContentType(fs.Name));
                return reader.ReadAsync(fs);
            }
        }

        private string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
