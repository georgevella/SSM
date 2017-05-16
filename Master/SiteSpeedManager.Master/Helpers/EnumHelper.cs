using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace SiteSpeedManager.Master.Helpers
{

    public static class EnumHelper
    {
        /// <summary>
        /// Gets the type of the attribute of.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumVal">The enum value.</param>
        /// <returns></returns>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes<T>(false);
            return attributes.FirstOrDefault();
        }

        public static string GetSerializableValue(this Enum enumVal)
        {
            var enumMember = enumVal.GetAttributeOfType<EnumMemberAttribute>();
            if (enumMember == null)
                return enumVal.ToString();

            return enumMember.Value;
        }
        public static string GetDisplayName(this Enum enumVal)
        {
            var displayName = enumVal.GetAttributeOfType<DisplayNameAttribute>();
            if (displayName == null)
                return enumVal.ToString();

            return displayName.DisplayName;
        }
    }
}