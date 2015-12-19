using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TubeStar
{
    public static class SerializationHelpers
    {
        public static string ToXml(Object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartDocument();

                    XmlSerializer ser = new XmlSerializer(obj.GetType(), "");
                    ser.Serialize(writer, obj);

                    writer.WriteEndDocument();
                    writer.Flush();

                    stream.Position = 0;

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static T FromXml<T>(string xml)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(xml);
                    writer.Flush();

                    stream.Position = 0;

                    using (XmlTextReader reader = new XmlTextReader(stream))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(T));
                        return (T)ser.Deserialize(reader);
                    }
                }
            }
        }
    }
}
