using Clipboard.Abstraction;
using Clipboard.OpenXml;
using Clipboard.Pdf;
using Clipboard.Text;

namespace Clipboard
{
    internal static class DocumentReaderFactory
    {
        public static IDocumentReader Create(string contentType)
        {
            switch(contentType)
            {
                case ContentTypeNames.Application.Pdf:
                    return new PdfReader();
                case ContentTypeNames.Application.Docx:
                case ContentTypeNames.Application.Dotx:
                case ContentTypeNames.Application.Dotm:
                    return new WordReader();
                case ContentTypeNames.Application.Xls :
                case ContentTypeNames.Application.Xlsx:
                case ContentTypeNames.Application.Xltx:
                case ContentTypeNames.Application.Xlsm:
                case ContentTypeNames.Application.Xltm:
                case ContentTypeNames.Application.Xlam:
                case ContentTypeNames.Application.Xlsb:
                    return new ExcelReader();
                case ContentTypeNames.Application.Ppt :
                    throw new System.Exception("Unsupported file type");
                case ContentTypeNames.Application.Pptx:
                case ContentTypeNames.Application.Potx:
                case ContentTypeNames.Application.Ppsx:
                case ContentTypeNames.Application.Ppam:
                case ContentTypeNames.Application.Pptm:
                case ContentTypeNames.Application.Potm:
                case ContentTypeNames.Application.Ppsm:
                    return new PowerpointReader();
                case ContentTypeNames.Text.Plain:
                    return new PlainReader();
                case ContentTypeNames.Text.Csv:
                    return new CsvReader();
                case ContentTypeNames.Text.Html:
                    return new HtmlReader();
                case ContentTypeNames.Text.Xml:
                    return new XmlReader();
                default:
                    throw new System.Exception("Unsupported file type");
            }
        }
    }
}
