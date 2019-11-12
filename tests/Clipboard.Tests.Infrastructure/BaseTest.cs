using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using Xunit;

namespace Clipboard.Tests.Infrastructure
{
    public abstract class BaseTest
    {
        protected readonly string _fileExtension;

        public BaseTest(string fileExtension)
        {
            _fileExtension = fileExtension;
        }

        [Fact]
        public void Extract_FromFilePath_NoException()
        {
            using (var extractor = TextExtractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}"))
            {
                var text = extractor.Extract();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public async Task Extract_FromFilePath_Async_NoException()
        {
            using (var extractor = TextExtractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}"))
            {
                var text = await extractor.ExtractAsync();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public void Extract_FromFileStream_NoException()
        {
            var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}");

            using (var extractor = TextExtractor.Open(fileStream))
            {
                var text = extractor.Extract();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public async Task Extract_FromFileStream_Async_NoException()
        {
            var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}");

            using (var extractor = TextExtractor.Open(fileStream))
            {
                var text = await extractor.ExtractAsync();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public void Extract_FromStreamAndContentType_NoException()
        {
            var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}");

            using (var extractor = TextExtractor.Open(fileStream, ContentTypeExtractor.Extract(fileStream.Name)))
            {
                var text = extractor.Extract();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public async Task Extract_FromStreamAndContentType_Async_NoException()
        {
            var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}");

            using (var extractor = TextExtractor.Open(fileStream,  ContentTypeExtractor.Extract(fileStream.Name)))
            {
                var text = await extractor.ExtractAsync();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public void Extract_FromFilePath_Missing_Exception()
        {
            var ex = Record.Exception(() =>
            {
                using (var extractor = TextExtractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}"))
                {
                    return extractor.Extract();
                }
            });

            Assert.NotNull(ex);
        }

        [Fact]
        public async Task Extract_FromFilePath_Async_Missing_Exception()
        {
            var ex = await Record.ExceptionAsync(() =>
            {
                using (var extractor = TextExtractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}"))
                {
                    return extractor.ExtractAsync();
                }
            });

            Assert.NotNull(ex);
        }

        [Fact]
        public void Extract_FromFileStream_Missing_Exception()
        {
            var ex = Record.Exception(() =>
            {
                var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}");

                using (var extractor = TextExtractor.Open(fileStream))
                {
                    return extractor.Extract();
                }
            });

            Assert.NotNull(ex);
        }

        [Fact]
        public async Task Extract_FromFileStream_Async_Missing_Exception()
        {
            var ex = await Record.ExceptionAsync(() =>
            {
                var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}");

                using (var extractor = TextExtractor.Open(fileStream))
                {
                    return extractor.ExtractAsync();
                }
            });

            Assert.NotNull(ex);
        }

        [Fact]
        public void Extract_FromStreamAndContentType_Missing_Exception()
        {
            var ex = Record.Exception(() =>
            {
                var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}");

                using (var extractor = TextExtractor.Open(fileStream, ContentTypeExtractor.Extract(fileStream.Name)))
                {
                    return extractor.Extract();
                }
            });

            Assert.NotNull(ex);
        }

        [Fact]
        public async Task Extract_FromStreamAndContentType_Async_Missing_Exception()
        {
            var ex = await Record.ExceptionAsync(() =>
            {
                var fileStream = File.OpenRead($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\missing.{_fileExtension}");

                using (var extractor = TextExtractor.Open(fileStream, ContentTypeExtractor.Extract(fileStream.Name)))
                {
                    return extractor.ExtractAsync();
                }
            });

            Assert.NotNull(ex);
        }
    }
}
