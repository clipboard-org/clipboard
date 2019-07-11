using System.IO;
using System.Text;
using System.Threading.Tasks;
using Clipboard.Abstraction;

namespace Clipboard.Text
{
    public class CsvReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            using (var sr = new StreamReader(fileStream))
            {
                var sb = new StringBuilder();

                while(!sr.EndOfStream)
                {
                    var c = sr.Read();
                    if (c == ';' || c == ',')
                        sb.Append(" ");
                    else
                        sb.Append(c);
                }

                return sb.ToString();
            }
        }

        public Task<string> ReadAsync(FileStream fileStream)
        {
            return Task.FromResult(Read(fileStream));
        }
    }
}
