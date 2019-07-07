using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard.Abstraction
{
    public interface ITextExtractor
    {
        Task<string> ExtractAsync(FileStream fileStream);
        Task<string> ExtractAsync(string filepath);
        string Extract(FileStream fileStream);
        string Extract(string filepath);
    }
}
