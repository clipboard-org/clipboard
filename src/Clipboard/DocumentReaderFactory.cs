using Clipboard.Abstraction;
using Clipboard.IFilter;
using Clipboard.Pdf;
using System.Runtime.InteropServices;

namespace Clipboard
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
                case ContentTypeNames.Application.docm:
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        return new Office.WordReader();
                    throw new System.Exception("Unsupported file type");
                case ContentTypeNames.Application.docx:
                case ContentTypeNames.Application.dotx:
                case ContentTypeNames.Application.dotm:
                    return new OpenXml.WordReader();
                case ContentTypeNames.Application.xls :
                case ContentTypeNames.Application.xlsx:
                case ContentTypeNames.Application.xltx:
                case ContentTypeNames.Application.xlsm:
                case ContentTypeNames.Application.xltm:
                case ContentTypeNames.Application.xlam:
                case ContentTypeNames.Application.xlsb:
                    return new OpenXml.ExcelReader();
                case ContentTypeNames.Application.ppt :
                    throw new System.Exception("Unsupported file type");
                case ContentTypeNames.Application.pptx:
                case ContentTypeNames.Application.potx:
                case ContentTypeNames.Application.ppsx:
                case ContentTypeNames.Application.ppam:
                case ContentTypeNames.Application.pptm:
                case ContentTypeNames.Application.potm:
                case ContentTypeNames.Application.ppsm:
                    return new OpenXml.PowerpointReader();
                default:
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        return new IFilterReader();

                    throw new System.Exception("Unsupported file type");
            }
        }
    }
}
