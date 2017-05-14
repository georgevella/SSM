using System.Runtime.Serialization;

namespace SiteSpeedManager.Models.SiteSpeed
{
    public enum BrowserType
    {
        [EnumMember(Value = "chrome")]
        Chrome,
        [EnumMember(Value = "firefox")]
        Firefox
    }
}