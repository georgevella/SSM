using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SiteSpeedManager.Models.Resources.V1
{
    public class DataSourceResourceConverter : JsonConverter
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}