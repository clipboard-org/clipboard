using System.IO;
using System.Threading.Tasks;

namespace Clipboard.Abstraction
{
    public interface IDocumentReader
    {
        Task<string> ReadAsync(FileStream fileStream);
        string Read(FileStream fileStream);
    }
}
