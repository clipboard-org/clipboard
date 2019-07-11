using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Clipboard.Tests.Infrastructure
{
    public abstract class BaseTest
    {
        private readonly string _fileExtension;

        public BaseTest(string fileExtension)
        {
            _fileExtension = fileExtension;
        }

        [Fact]
        public void Extract_NoException()
        {
            using (var extractor = TextExtractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}"))
            {
                var text = extractor.Extract();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        [Fact]
        public async Task Extract_Async_NoException()
        {
            using (var extractor = TextExtractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\example.{_fileExtension}"))
            {
                var text = await extractor.ExtractAsync();
                Assert.False(string.IsNullOrEmpty(text));
            }
        }

        //[Fact]
        //public void Extract_Corrupt_Exception()
        //{
        //    using (var extractor = TextExtractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\corrupt.{_fileExtension}"))
        //    {
        //        var ex = Record.Exception(() => extractor.Extract());
        //        Assert.NotNull(ex);
        //    }
        //}

        //[Fact]
        //public async Task Extract_Async_Corrupt_Exception()
        //{
        //    using (var extractor = TextExtractor.Open($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\assets\\corrupt.{_fileExtension}"))
        //    {
        //        var ex = await Record.ExceptionAsync(() => extractor.ExtractAsync());
        //        Assert.NotNull(ex);
        //    }
        //}

        [Fact]
        public void Extract_Missing_Exception()
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
        public async Task Extract_Async_Missing_Exception()
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
    }
}
