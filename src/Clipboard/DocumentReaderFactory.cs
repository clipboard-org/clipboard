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
                case ContentType.Application.Pdf:
                    return new PdfReader();
                case ContentType.Application.Docx:
                case ContentType.Application.Dotx:
                case ContentType.Application.Dotm:
                    return new WordReader();
                case ContentType.Application.Xls :
                case ContentType.Application.Xlsx:
                case ContentType.Application.Xltx:
                case ContentType.Application.Xlsm:
                case ContentType.Application.Xltm:
                case ContentType.Application.Xlam:
                case ContentType.Application.Xlsb:
                    return new ExcelReader();
                case ContentType.Application.Ppt :
                    throw new UnsupportedFileException("Unsupported file type");
                case ContentType.Application.Pptx:
                case ContentType.Application.Potx:
                case ContentType.Application.Ppsx:
                case ContentType.Application.Ppam:
                case ContentType.Application.Pptm:
                case ContentType.Application.Potm:
                case ContentType.Application.Ppsm:
                    return new PowerpointReader();
                case ContentType.Text.Plain:
                    return new PlainReader();
                case ContentType.Text.Csv:
                    return new CsvReader();
                case ContentType.Text.Html:
                    return new HtmlReader();
                case ContentType.Text.Xml:
                    return new XmlReader();
                default:
                    throw new UnsupportedFileException("Unsupported file type");
            }
        }
    }
}
