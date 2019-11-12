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
            _contentType =  ContentTypeExtractor.Extract(fileStream.Name);
            _disposeStream = false;
        }

        private TextExtractor(string filePath)
        {
            _stream = File.OpenRead(filePath);
            _contentType = ContentTypeExtractor.Extract(Path.GetFileName(filePath));
            _disposeStream = true;
        }

        private TextExtractor(Stream stream, string contentType)
        {
            _stream = stream;
            _contentType = contentType;
            _disposeStream = false;
        }

        private TextExtractor(Stream stream)
        {
            _stream = stream;
            _contentType = ContentTypeExtractor.Extract(stream);
            _disposeStream = false;
        }

        private TextExtractor(byte[] bytes)
        {
            _stream = new MemoryStream(bytes);
            _contentType = ContentTypeExtractor.Extract(bytes);
            _disposeStream = true;
        }

        private TextExtractor(ReadOnlyMemory<byte> bytes)
        {
            _stream = new MemoryStream(bytes.GetUnderlyingArray().Array);
            _contentType = ContentTypeExtractor.Extract(bytes);
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

        public static TextExtractor Open(Stream stream, string contentType)
        {
            return new TextExtractor(stream, contentType);
        }

        public static TextExtractor Open(Stream stream)
        {
            return new TextExtractor(stream);
        }

        public static TextExtractor Open(byte[] bytes)
        {
            return new TextExtractor(bytes);
        }

        public static TextExtractor Open(ReadOnlyMemory<byte> bytes)
        {
            return new TextExtractor(bytes);
        }

        public static TextExtractor Open(Memory<byte> bytes)
        {
            return new TextExtractor(bytes);
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
    }
}
