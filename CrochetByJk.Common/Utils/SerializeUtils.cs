using System;
using System.IO;
using Newtonsoft.Json;

namespace CrochetByJk.Common.Utils
{
    public static class SerializeUtils
    {
        public class HttpPostedFileConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var stream = (Stream)value;
                using (var sr = new BinaryReader(stream))
                {
                    var buffer = sr.ReadBytes((int)stream.Length);
                    writer.WriteValue(Convert.ToBase64String(buffer));
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override bool CanRead => false;

            public override bool CanConvert(Type objectType)
            {
                return objectType.IsSubclassOf(typeof(Stream));
            }
        }
    }
}