using Clipboard.Abstraction;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard.OpenXml
{
    public class WordReader : IDocumentReader
    {
        public string Read(FileStream fileStream)
        {
            var sb = new StringBuilder();
            using (var doc = WordprocessingDocument.Open(fileStream, false))
            {
                var body = doc.MainDocumentPart.Document.Body;
                foreach(var element in body.Elements())
                {
                    sb.Append(InternalRead(element));
                }
            }

            return sb.ToString();
        }

        public Task<string> ReadAsync(FileStream fileStream)
        {
            return Task.FromResult(Read(fileStream));
        }

        private string InternalRead(OpenXmlElement element)
        {
            var sb = new StringBuilder();
            foreach (OpenXmlElement currentElement in element.Elements())
            {
                switch (currentElement.LocalName)
                {
                    // Text 
                    case "t":
                        sb.Append(currentElement.InnerText);
                        break;


                    case "cr":                          // Carriage return 
                    case "br":                          // Page break 
                        sb.Append(Environment.NewLine);
                        break;


                    // Tab 
                    case "tab":
                        sb.Append("\t");
                        break;


                    // Paragraph 
                    case "p":
                        sb.Append(InternalRead(currentElement));
                        sb.AppendLine(Environment.NewLine);
                        break;


                    default:
                        sb.Append(InternalRead(currentElement));
                        break;
                }
            }


            return sb.ToString();
        }
    }
}
