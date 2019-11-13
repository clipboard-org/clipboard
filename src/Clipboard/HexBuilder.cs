using System;
using System.Collections.Generic;
using System.Text;

namespace Clipboard
{
    internal static class HexBuilder
    {
        private static readonly uint[] _lookup32;

        static HexBuilder()
        {
            _lookup32 = new uint[256];

            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                _lookup32[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }
        }

        public static string Build(ReadOnlyMemory<byte> bytes)
        {
            var lookup32 = _lookup32;
            var result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes.Span[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }
    }
}
