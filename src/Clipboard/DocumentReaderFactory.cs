using Clipboard.Abstraction;
using Clipboard.IFilter;
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
                    return new Pdf.PdfReader();
                case ContentTypeNames.Application.Docx:
                case ContentTypeNames.Application.Dotx:
                case ContentTypeNames.Application.Dotm:
                    return new OpenXml.WordReader();
                case ContentTypeNames.Application.Xls :
                case ContentTypeNames.Application.Xlsx:
                case ContentTypeNames.Application.Xltx:
                case ContentTypeNames.Application.Xlsm:
                case ContentTypeNames.Application.Xltm:
                case ContentTypeNames.Application.Xlam:
                case ContentTypeNames.Application.Xlsb:
                    return new OpenXml.ExcelReader();
                case ContentTypeNames.Application.Ppt :
                    throw new System.Exception("Unsupported file type");
                case ContentTypeNames.Application.Pptx:
                case ContentTypeNames.Application.Potx:
                case ContentTypeNames.Application.Ppsx:
                case ContentTypeNames.Application.Ppam:
                case ContentTypeNames.Application.Pptm:
                case ContentTypeNames.Application.Potm:
                case ContentTypeNames.Application.Ppsm:
                    return new OpenXml.PowerpointReader();
                case ContentTypeNames.Text.Plain:
                    return new Text.PlainReader();
                case ContentTypeNames.Text.Csv:
                    return new Text.CsvReader();
                case ContentTypeNames.Text.Html:
                    return new Text.HtmlReader();
                default:
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        return new IFilterReader();

                    throw new System.Exception("Unsupported file type");
            }
        }
    }
}
