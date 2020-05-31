using System.Collections.Generic;
using System.Text;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.Content;
using PdfSharpCore.Pdf.Content.Objects;

namespace Clipboard.Pdf
{
    public static class PdfSharpExtensions
    {
        public static string ExtractText(this PdfPages pages)
        {
            var sb = new StringBuilder();

            foreach (var page in pages)
                sb.Append(page.ExtractText());

            return sb.ToString();
        }
        public static string ExtractText(this PdfPage page)
        {
            var sb = new StringBuilder();

            foreach (var text in ContentReader.ReadContent(page).ExtractText())
                sb.Append(text);

            return sb.ToString();
        }

        public static IEnumerable<string> ExtractText(this CObject cObject)
        {
            if (cObject is COperator)
            {
                var cOperator = cObject as COperator;
                if (cOperator.OpCode.Name == OpCodeName.Tj.ToString() ||
                    cOperator.OpCode.Name == OpCodeName.TJ.ToString())
                {
                    foreach (var cOperand in cOperator.Operands)
                        foreach (var txt in ExtractText(cOperand))
                            yield return txt;
                }
            }
            else if (cObject is CSequence)
            {
                var cSequence = cObject as CSequence;
                foreach (var element in cSequence)
                    foreach (var txt in ExtractText(element))
                        yield return txt;
            }
            else if (cObject is CString)
            {
                var cString = cObject as CString;
                yield return cString.Value;
            }
        }
    }
}
