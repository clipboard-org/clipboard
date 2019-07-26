using System.IO;
using System.Text;
using System.Threading.Tasks;
using Clipboard.Abstraction;
using HtmlAgilityPack;

namespace Clipboard.Text
{
    public sealed class HtmlReader : IDocumentReader
    {
        public string Read(Stream stream)
        {
            var sb = new StringBuilder();
            var doc = new HtmlDocument();
            doc.Load(stream);
            Read(doc.DocumentNode, sb);

            return sb.ToString();
        }

        public Task<string> ReadAsync(Stream stream)
        {
            return Task.FromResult(Read(stream));
        }

        private void ReadInner(HtmlNode node, StringBuilder sb)
        {
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    // don't output comments
                    return;

                case HtmlNodeType.Document:
                    Read(node, sb);
                    break;

                case HtmlNodeType.Text:
                    // script and style must not be output
                    var parentName = node.ParentNode.Name;
                    if ((parentName == "script") || (parentName == "style"))
                        return;

                    // get text
                    var html = ((HtmlTextNode)node).Text;

                    // is it in fact a special closing node output as text?
                    if (HtmlNode.IsOverlappedClosingElement(html))
                        return;

                    // check the text is meaningful and not a bunch of whitespaces
                    if (html.Trim().Length > 0)
                    {
                        sb.Append(HtmlEntity.DeEntitize(html));
                    }
                    break;

                case HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "p":
                            // treat paragraphs as crlf
                            sb.Append("\r\n");
                            break;
                    }

                    if (node.HasChildNodes)
                    {
                        Read(node, sb);
                    }
                    break;
            }
        }

        private void Read(HtmlNode node, StringBuilder sb)
        {
            foreach(var child in node.ChildNodes)
            {
                ReadInner(child, sb);
            }
        }
    }
}
