using System.IO;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SiteSpeedController.Master.Services.Transport
{
    public interface IMessageSerializer<in TContent>
    {
        string SerializeAsString(TContent content);
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
    }
}