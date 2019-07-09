using Clipboard.Abstraction;
using Clipboard.IFilter;
using Clipboard.OpenXml;
using Clipboard.Pdf;
using System.Runtime.InteropServices;

namespace Clipboard.Core
{
    public static class DocumentReaderFactory
    {
        public static IDocumentReader Create(string contentType)
        {
            switch(contentType)
            {
                case ContentTypeNames.Application.Pdf:
                    return new PdfReader();
                case ContentTypeNames.Application.doc :
                case ContentTypeNames.Application.docx:
                case ContentTypeNames.Application.dotx:
                case ContentTypeNames.Application.docm:
                case ContentTypeNames.Application.dotm:
                    return new WordReader();
                case ContentTypeNames.Application.xls :
                case ContentTypeNames.Application.xlsx:
                case ContentTypeNames.Application.xltx:
                case ContentTypeNames.Application.xlsm:
                case ContentTypeNames.Application.xltm:
                case ContentTypeNames.Application.xlam:
                case ContentTypeNames.Application.xlsb:
                    return new ExcelReader();
                case ContentTypeNames.Application.ppt :
                case ContentTypeNames.Application.pps :
                case ContentTypeNames.Application.pptx:
                case ContentTypeNames.Application.potx:
                case ContentTypeNames.Application.ppsx:
                case ContentTypeNames.Application.ppam:
                case ContentTypeNames.Application.pptm:
                case ContentTypeNames.Application.potm:
                case ContentTypeNames.Application.ppsm:
                    return new PowerpointReader();
                default:
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        return new IFilterReader();

                    throw new System.Exception("Unsupported file type");
            }
        }
    }
}
