using System;
using System.IO;
using System.IO.Packaging;
using Microsoft.AspNetCore.StaticFiles;

namespace Clipboard
{
    internal static class ContentTypeExtractor
    {
        public static string Extract(ReadOnlyMemory<byte> bytes)
        {
            if (bytes.Length < 6)
                throw new UnsupportedFileException("Unable to indentify content type of file");

            var hex = HexBuilder.Build(bytes.Slice(0, 6));

            // Check if its an openxml file.
            if (hex.StartsWith("504B0304") || hex.StartsWith("504B0506") || hex.StartsWith("504B0708"))
            {
                using(var ms = new MemoryStream(bytes.GetUnderlyingArray().Array))
                using (var package = Package.Open(ms))
                {
                    if (package.PartExists(new Uri("/word/document.xml", UriKind.Relative)))
                        return ContentType.Application.Docx;
                    else if (package.PartExists(new Uri("/xl/workbook.xml", UriKind.Relative)))
                        return ContentType.Application.Xlsx;
                    else if (package.PartExists(new Uri("/ppt/presentation.xml", UriKind.Relative)))
                        return ContentType.Application.Pptx;
                    else
                        throw new UnsupportedFileException("Unable to indentify content type of file");
                }
            }

            if (!HexMimeMappings.TryGetValue(hex, out var type))
                throw new UnsupportedFileException("Unable to indentify content type of file");

            return type;
        }

        public static string Extract(byte[] bytes)
        {
            return Extract(bytes.AsMemory());
        }

        public static string Extract(string filename)
        {
            var provider = new FileExtensionContentTypeProvider(new CustomMimeMappings());

            if (!provider.TryGetContentType(filename, out var contentType))
            {
                contentType = ContentType.Application.Octet;
            }
            return contentType;
        }

        public static string Extract(Stream stream)
        {
            var bytes = new byte[stream.Length];
            
            stream.Read(bytes, 0, (int)stream.Length);

            stream.Position = 0;

            return Extract(bytes);
        }
    }
}
