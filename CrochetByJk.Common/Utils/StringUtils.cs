using System.Text;

namespace CrochetByJk.Common.Utils
{
    public static class StringUtils
    {
        public static string RemoveSpecialCharacters(this string str)
        {
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                    sb.Append(c);
            }
            return sb.ToString();
        }
        public static string RemoveWhiteSpace(this string str)
        {
            var sb = new StringBuilder(str.Length);
            foreach (var c in str)
            {
                if (!char.IsWhiteSpace(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}