using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Clipboard;

namespace QuickStart
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var filePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\example.pdf";

            using (var extractor = TextExtractor.Open(filePath))
            {
                Console.WriteLine(await extractor.ExtractAsync());
            }

            Console.ReadLine();
        }
    }
}
