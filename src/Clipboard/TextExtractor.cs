using Clipboard.Abstraction;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Clipboard.Core
{
    internal class TextExtractor : IDisposable
    {
        private readonly FileStream _fileStream;

        public TextExtractor(FileStream fileStream)
        {
            _fileStream = fileStream;
        }

        public TextExtractor(string filePath)
        {
            _fileStream = File.OpenRead(filePath);
        }

        public void Dispose()
        {
            _fileStream.Dispose();
        }

        public string Extract()
        {            
            var reader = DocumentReaderFactory.Create(GetContentType(_fileStream.Name));
            return reader.Read(_fileStream);
        }

        public Task<string> ExtractAsync()
        {
            var reader = DocumentReaderFactory.Create(GetContentType(_fileStream.Name));
            return reader.ReadAsync(_fileStream);
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
