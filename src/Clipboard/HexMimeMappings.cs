namespace Clipboard
{
    internal static class HexMimeMappings
    {
        private const string Pdf = "255044462D";
        private const string Ppt = "A0461DF0";
        private const string Rtf = "7B5C727466";

        public static bool TryGetValue(string hex, out string value)
        {
            if (hex.StartsWith(Pdf))
            {
                value = ContentType.Application.Pdf;
                return true;
            }                
            else if (hex.StartsWith(Ppt))
            {
                value = ContentType.Application.Ppt;
                return true;
            }
            else if (hex.StartsWith(Rtf))
            {
                value = ContentType.Application.Rtf;
                return true;
            }
            else
            {
                value = null;
                return false;
            }            
        }
    }
}
