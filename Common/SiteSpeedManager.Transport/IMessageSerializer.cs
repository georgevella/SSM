using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SiteSpeedManager.Transport
{
    public interface IMessageSerializer<TContent>
    {
        string SerializeAsString(TContent content);
        TContent DeserializeFromString(string raw);
    }

    internal class MessageSerializer<TContent> : IMessageSerializer<TContent>
    {
        private readonly JsonSerializer _serializer;

        public MessageSerializer()
        {
            _serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters =
                {
                    new StringEnumConverter(),
                },
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public string SerializeAsString(TContent content)
        {
            using (var sw = new StringWriter())
            {
                using (var tw = new JsonTextWriter(sw))
                {
                    _serializer.Serialize(tw, content);
                    tw.Flush();
                }

                return sw.ToString();
            }
        }

        public TContent DeserializeFromString(string raw)
        {
            using (var sr = new StringReader(raw))
            using (var tr = new JsonTextReader(sr))
            {
                return _serializer.Deserialize<TContent>(tr);
            }
        }
    }
}