using System.Runtime.Serialization;

namespace SiteSpeedManager.Models.SiteSpeed
{
    public enum ConnectivityProfile
    {
        [EnumMember(Value = "3g")]
        Normal3G,
        [EnumMember(Value = "3gfast")]
        Fast3G,
        [EnumMember(Value = "3gslow")]
        Slow3G,
        [EnumMember(Value = "2g")]
        Normal2G,
        [EnumMember(Value = "cable")]
        Cable,
        [EnumMember(Value = "native")]
        Native,
        [EnumMember(Value = "custom")]
        Custom,
    }
}