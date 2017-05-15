using Glyde.Web.Api.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SiteSpeedManager.Models.Resources.V1
{
    [Resource("data-source-types")]
    public class DataSourceTypeResource : Resource<string>
    {

    }

    public enum DataSourceType
    {
        GrafanaDb,
        InfluxDb,
        S3Bucket
    }

    [JsonConverter(typeof(DataSourceResourceConverter))]
    public abstract class DataSourceResource : Resource<string>
    {
        public DataSourceType Type { get; set; }
    }

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
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class DbDataSourceResource : DataSourceResource
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool HasCredentials { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsDefault { get; set; }
    }

    public class InfluxDbDataSourceResource : DbDataSourceResource
    {
        public string Database { get; set; }
    }

    public class GrafanaDataSourceResource : DbDataSourceResource
    {
        public int HttpPort { get; set; }
        public string Namespace { get; set; }
    }
}