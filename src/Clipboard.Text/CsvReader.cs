using System.IO;
using System.Text;
using System.Threading.Tasks;
using Clipboard.Abstraction;

namespace Clipboard.Text
{
    public sealed class CsvReader : IDocumentReader
    {
        public string Read(Stream stream)
        {
            using (var sr = new StreamReader(stream))
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

        public Task<string> ReadAsync(Stream stream)
        {
            return Task.FromResult(Read(stream));
        }
    }
}
