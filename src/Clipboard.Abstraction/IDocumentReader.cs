using System.IO;
using System.Threading.Tasks;

namespace Clipboard.Abstraction
{
    public interface IDocumentReader
    {
        Task<string> ReadAsync(Stream stream);
        string Read(Stream stream);
    }
}
