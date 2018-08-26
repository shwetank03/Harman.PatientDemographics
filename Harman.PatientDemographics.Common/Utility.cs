using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Harman.PatientDemographics.Common
{
    /// <summary>
    /// Helper class to serialize/deserialize any [Serializable] object in a centralized way.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Internal utility method to convert an UTF8 Byte Array to an UTF8 string.
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        private static string Utf8ByteArrayToString(byte[] characters)
        {
            return new UTF8Encoding().GetString(characters);
        }

        /// <summary>
        /// Internal utility method to convert an UTF8 string to an UTF8 Byte Array.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static byte[] StringToUtf8ByteArray(string xml)
        {
            return new UTF8Encoding().GetBytes(xml);
        }

        /// <summary>
        /// Serialize a [Serializable] object of type T into an XML/UTF8 string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(this T item)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var sw = new StringWriter())
            {
                using (var writer = new XmlTextWriter(sw))
                {
                    writer.Formatting = System.Xml.Formatting.Indented;
                    serializer.WriteObject(writer, item);
                    serializer.WriteObject(writer, item);
                    writer.Flush();
                    return sw.ToString();
                }
            }
        }

        /// <summary>
        /// De-serialize an XML/UTF8 string into an object of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Deserialize<T>(this string rawXml)
        {
            using (var reader = XmlReader.Create(new StringReader(rawXml)))
            {
                var formatter0 =
                    new DataContractSerializer(typeof(T));
                return (T)formatter0.ReadObject(reader);
            }
        }

    }
}
