using System;
using System.Security.Cryptography;
using System.Text;

namespace TubeStar
{
    public class BaseManager
    {
        protected static string GetSigniture(string uri)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var data = sha1.ComputeHash(Encoding.UTF8.GetBytes(uri + "1bd02a94aece40fd8984ca9c18ea29f1"));

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                    sb.Append(data[i].ToString("x2"));

                return sb.ToString();
            }
        }
    }
}