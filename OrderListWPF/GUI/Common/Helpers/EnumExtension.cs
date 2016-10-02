using System;
using System.ComponentModel;

namespace OrderListWPF.GUI.Common.Helpers
{
    /// <summary>
    /// Extension for easy get enum value's attribute
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Get attribute calues collection for enum value by attr type
        /// </summary>
        /// <param name="item">enum value</param>
        /// <param name="attributeType">attribute type</param>
        /// <returns></returns>
        private static object[] GetAttributes(object item, Type attributeType)
        {
            var fi = item?.GetType().GetField(item.ToString());
            if (fi == null) return new object[0];
            var attributes = fi.GetCustomAttributes(attributeType, false);
            return (attributes.Length > 0) ? attributes : new object[0];
        }

        /// <summary>
        /// Get first attribute value by attr type for enum value
        /// </summary>
        /// <typeparam name="T">attribute type</typeparam>
        /// <param name="item">enum value</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this Enum item)
            where T: Attribute
        {
            var atr = GetAttributes(item, typeof(T));

            return atr.Length > 0 ? (T)Convert.ChangeType(atr[0], typeof(T)) : null; 
        }

        /// <summary>
        /// Get Description attr value for enum value
        /// </summary>
        /// <param name="item">enum value</param>
        /// <returns></returns>
        public static string GetDescription(this Enum item)
        {
            var atr = GetAttribute<DescriptionAttribute>(item);
            return atr?.Description;
        }
    }
}
