using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Clipboard
{
    public class TextExtractor : IDisposable
    {
        private readonly FileStream _fileStream;
        private bool _disposeStream;

        private TextExtractor(FileStream fileStream)
        {
            _fileStream = fileStream;
            _disposeStream = false;
        }

        private TextExtractor(string filePath)
        {
            _fileStream = File.OpenRead(filePath);
            _disposeStream = true;
        }

        public static TextExtractor Open(string filePath)
        {
            return new TextExtractor(filePath);
        }

        public static TextExtractor Open(FileStream fileStream)
        {
            return new TextExtractor(fileStream);
        }

        public void Dispose()
        {
            if (_disposeStream)
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
                contentType = ContentTypeNames.Application.Octet;
            }
            return contentType;
        }
    }
}
