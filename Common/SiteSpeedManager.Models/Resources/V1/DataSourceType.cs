using System.ComponentModel;
using System.Runtime.Serialization;

namespace SiteSpeedManager.Models.Resources.V1
{
    public enum DataSourceType
    {
        [DisplayName("Grafana")]
        [EnumMember(Value = "grafana")]
        GrafanaDb,

        [DisplayName("InfluxDB")]
        [EnumMember(Value = "influxdb")]
        InfluxDb,

        [DisplayName("Amazon S3 Bucket")]
        [EnumMember(Value = "s3")]
        S3Bucket
    }
}