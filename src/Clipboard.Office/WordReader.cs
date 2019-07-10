using Clipboard.Abstraction;
using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Clipboard.Office
{
    public class WordReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            var app = new Application();
            try
            {
                Document doc = app.Documents.Open(fileStream.Name);
                string allWords = doc.Content.Text;
                doc.Close();
                app.Quit();

                return allWords;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task<string> ReadAsync(FileStream fileStream)
        {
            return System.Threading.Tasks.Task.FromResult(Read(fileStream));
        }
    }
}
