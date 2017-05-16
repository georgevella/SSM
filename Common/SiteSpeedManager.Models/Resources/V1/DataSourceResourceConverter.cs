using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteSpeedManager.Models.Resources.V1
{
    public class DataSourceResourceConverter : JsonConverter
    {
        private static readonly Dictionary<string, DataSourceType> DataSourceTypeAsSerializableValues;

        static DataSourceResourceConverter()
        {
            DataSourceTypeAsSerializableValues = Enum.GetValues(typeof(DataSourceType))
                .Cast<DataSourceType>()
                .ToDictionary(x => x.GetSerializableValue());
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var token = JToken.FromObject(value);
            token.WriteTo(writer);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            var typeProperty = obj.GetValue("type", StringComparison.OrdinalIgnoreCase) as JValue;
            if (typeProperty == null)
                return null;

            if (!DataSourceTypeAsSerializableValues.TryGetValue(typeProperty.Value.ToString(), out DataSourceType type))
            {
                return null;
            }

            DataSourceResource dataSource;

            switch (type)
            {
                case DataSourceType.GrafanaDb:
                    dataSource = new GrafanaDataSourceResource();
                    break;
                case DataSourceType.InfluxDb:
                    dataSource = new InfluxDbDataSourceResource();
                    break;
                case DataSourceType.S3Bucket:
                    throw new NotImplementedException();
                default:
                    return null;
            }

            using (var r = obj.CreateReader())
            {
                serializer.Populate(r, dataSource);
            }

            return dataSource;
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }
}