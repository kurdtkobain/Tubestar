using System;
using System.Net;
using Newtonsoft.Json;

namespace TubeStar
{
    public class HttpHelpers
    {
        public static string UrlEncoding(string str)
        {
            return str
                .Replace("%", "%25")
                .Replace(" ", "%20")
                .Replace("!", "%21")
                .Replace("\"", "%22")
                .Replace("#", "%23")
                .Replace("$", "%24")
                .Replace("&", "%26")
                .Replace("'", "%27")
                .Replace("(", "%28")
                .Replace(")", "%29")
                .Replace("*", "%2A")
                .Replace("+", "%2B")
                .Replace(",", "%2C")
                .Replace("-", "%2D")
                .Replace(".", "%2E")
                .Replace("/", "%2F")
                .Replace(":", "%3A")
                .Replace(";", "%3B")
                .Replace("<", "%3C")
                .Replace("=", "%3D")
                .Replace(">", "%3E")
                .Replace("?", "%3F")
                .Replace("@", "%40")
                .Replace("[", "%5B")
                .Replace("\\", "%5C")
                .Replace("]", "%5D")
                .Replace("^", "%5E")
                .Replace("_", "%5F")
                .Replace("`", "%60")
                .Replace("{", "%7B")
                .Replace("|", "%7C")
                .Replace("}", "%7D")
                .Replace("~", "%7E");
        }
    }
}