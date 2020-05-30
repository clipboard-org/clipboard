using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Extractor = Clipboard.TextExtractor;

namespace Clipboard.Tests.TextExtractor
{
    public abstract class MagicNumberTests : FileTests
    {
        public MagicNumberTests(string fileExtension) : base(fileExtension)
        {
        }

        [Fact]
        public void Extract_From_Bytes_NoException()
        {
            using (var file = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/assets/example.{_fileExtension}"))
            {
                var buffer = new byte[file.Length];
                file.Read(buffer);

                using (var extractor = Extractor.Open(buffer))
                {
                    var text = extractor.Extract();
                    Assert.False(string.IsNullOrEmpty(text));
                }
            }            
        }

        [Fact]
        public async Task Extract_From_Bytes_Async_NoException()
        {
            using (var file = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/assets/example.{_fileExtension}"))
            {
                var buffer = new byte[file.Length];
                file.Read(buffer);

                using (var extractor = Extractor.Open(buffer))
                {
                    var text = await extractor.ExtractAsync();
                    Assert.False(string.IsNullOrEmpty(text));
                }
            }
        }

        [Fact]
        public void Extract_From_ReadOnlyMemory_NoException()
        {
            using (var file = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/assets/example.{_fileExtension}"))
            {
                var buffer = new byte[file.Length];
                file.Read(buffer);

                using (var extractor = Extractor.Open((ReadOnlyMemory<byte>)buffer.AsMemory()))
                {
                    var text = extractor.Extract();
                    Assert.False(string.IsNullOrEmpty(text));
                }
            }
        }

        [Fact]
        public async Task Extract_From_ReadOnlyMemory_Async_NoException()
        {
            using (var file = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/assets/example.{_fileExtension}"))
            {
                var buffer = new byte[file.Length];
                file.Read(buffer);

                using (var extractor = Extractor.Open((ReadOnlyMemory<byte>)buffer.AsMemory()))
                {
                    var text = await extractor.ExtractAsync();
                    Assert.False(string.IsNullOrEmpty(text));
                }
            }
        }

        [Fact]
        public void Extract_From_Stream_NoException()
        {
            using (var file = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/assets/example.{_fileExtension}"))
            using (var extractor = Extractor.Open((Stream)file))
            {
                var text = extractor.Extract();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public async Task Extract_From_Stream_Async_NoException()
        {
            using (var file = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/assets/example.{_fileExtension}"))
            using (var extractor = Extractor.Open((Stream)file))
            {
                var text = await extractor.ExtractAsync();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }
    }
}
