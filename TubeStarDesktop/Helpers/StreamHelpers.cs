using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubeStar
{
    public static class StreamHelpers
    {
        public static byte[] StreamToBytes(Stream source)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                source.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static Stream BytesToStream(byte[] source)
        {
            return new MemoryStream(source);
        }
    }
}
