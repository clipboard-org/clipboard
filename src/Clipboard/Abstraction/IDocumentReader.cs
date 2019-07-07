using System.IO;
using System.Threading.Tasks;

namespace Clipboard.Abstraction
{
    internal interface IDocumentReader
    {
        Task<string> ReadAsync(FileStream fileStream);
        string Read(FileStream fileStream);
    }
}
