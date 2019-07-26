using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Clipboard
{
    public sealed class TextExtractor : IDisposable
    {
        private readonly Stream _stream;
        private readonly string _contentType;
        private readonly bool _disposeStream;

        private TextExtractor(FileStream fileStream)
        {
            _stream = fileStream;
            _contentType = GetContentType(fileStream.Name);
            _disposeStream = false;
        }

        private TextExtractor(string filePath)
        {
            _stream = File.OpenRead(filePath);
            _contentType = GetContentType(Path.GetFileName(filePath));
            _disposeStream = true;
        }

        private TextExtractor(Stream stream, string contentType)
        {
            _stream = stream;
            _contentType = contentType;
            _disposeStream = false;
        }

        public static TextExtractor Open(string filePath)
        {
            return new TextExtractor(filePath);
        }

        public static TextExtractor Open(FileStream fileStream)
        {
            return new TextExtractor(fileStream);
        }

        public static TextExtractor Open(Stream stream, string contentType)
        {
            return new TextExtractor(stream, contentType);
        }

        public void Dispose()
        {
            if (_disposeStream)
                _stream.Dispose();
        }

        public string Extract()
        {            
            var reader = DocumentReaderFactory.Create(_contentType);
            return reader.Read(_stream);
        }

        public Task<string> ExtractAsync()
        {
            var reader = DocumentReaderFactory.Create(_contentType);
            return reader.ReadAsync(_stream);
        }

        internal static string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider(new CustomMimeMappings());

            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = ContentTypeNames.Application.Octet;
            }
            return contentType;
        }
    }
}
