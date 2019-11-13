using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Extractor = Clipboard.TextExtractor;

namespace Clipboard.Tests.TextExtractor
{
    public abstract class FileTests
    {
        protected readonly string _fileExtension;

        public FileTests(string fileExtension)
        {
            _fileExtension = fileExtension;
        }

        [Fact]
        public void Extract_From_FilePath_NoException()
        {
            using (var extractor = Extractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}"))
            {
                var text = extractor.Extract();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public async Task Extract_From_FilePath_Async_NoException()
        {
            using (var extractor = Extractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}"))
            {
                var text = await extractor.ExtractAsync();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public void Extract_From_FileStream_NoException()
        {
            var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}");

            using (var extractor = Extractor.Open(fileStream))
            {
                var text = extractor.Extract();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public async Task Extract_From_FileStream_Async_NoException()
        {
            var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}");

            using (var extractor = Extractor.Open(fileStream))
            {
                var text = await extractor.ExtractAsync();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public void Extract_From_Stream_And_ContentType_NoException()
        {
            var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}");

            using (var extractor = Extractor.Open(fileStream, ContentTypeExtractor.Extract(fileStream.Name)))
            {
                var text = extractor.Extract();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public async Task Extract_From_Stream_And_ContentType_Async_NoException()
        {
            var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}");

            using (var extractor = Extractor.Open(fileStream,  ContentTypeExtractor.Extract(fileStream.Name)))
            {
                var text = await extractor.ExtractAsync();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public void Extract_From_File_Path_Missing_Exception()
        {
            var ex = Record.Exception(() =>
            {
                using (var extractor = Extractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}"))
                {
                    return extractor.Extract();
                }
            });

            Assert.NotNull(ex);
        }

        [Fact]
        public async Task Extract_From_File_Path_Async_Missing_Exception()
        {
            var ex = await Record.ExceptionAsync(() =>
            {
                using (var extractor = Extractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}"))
                {
                    return extractor.ExtractAsync();
                }
            });

            Assert.NotNull(ex);
        }

        [Fact]
        public void Extract_From_FileStream_Missing_Exception()
        {
            var ex = Record.Exception(() =>
            {
                var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}");

                using (var extractor = Extractor.Open(fileStream))
                {
                    return extractor.Extract();
                }
            });

            Assert.NotNull(ex);
        }

        [Fact]
        public async Task Extract_From_FileStream_Async_Missing_Exception()
        {
            var ex = await Record.ExceptionAsync(() =>
            {
                var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}");

                using (var extractor = Extractor.Open(fileStream))
                {
                    return extractor.ExtractAsync();
                }
            });

            Assert.NotNull(ex);
        }

        [Fact]
        public void Extract_From_Stream_And_ContentType_Missing_Exception()
        {
            var ex = Record.Exception(() =>
            {
                var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}");

                using (var extractor = Extractor.Open(fileStream, ContentTypeExtractor.Extract(fileStream.Name)))
                {
                    return extractor.Extract();
                }
            });

            Assert.NotNull(ex);
        }

        [Fact]
        public async Task Extract_From_Stream_And_ContentType_Async_Missing_Exception()
        {
            var ex = await Record.ExceptionAsync(() =>
            {
                var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}");

                using (var extractor = Extractor.Open(fileStream, ContentTypeExtractor.Extract(fileStream.Name)))
                {
                    return extractor.ExtractAsync();
                }
            });

            Assert.NotNull(ex);
        }
    }
}
