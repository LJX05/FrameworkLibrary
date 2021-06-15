using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace YxSoft.Core
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        public static string GetDisplayName(this Enum enumType)
        {
            var member = enumType.GetType().GetMember(enumType.ToString())
                             .First();
            var vs = (DisplayAttribute)member.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
            if (vs == null)
            {
                return "";
            }

            return vs.Name;
        }
        /// <summary>
        /// 从枚举中获取Description
        /// </summary>
        /// <param name="enumName">需要获取枚举描述的枚举</param>
        /// <returns>描述内容</returns>
        public static string GetDisplayDescription(this Enum enumType)
        {
            
            var member = enumType.GetType().GetMember(enumType.ToString())
                             .First();
            var vs = (DisplayAttribute)member.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
            if (vs == null)
            {
                return "";
            }

            return vs.Description;
        }

        /// <summary>
        /// 从枚举中获取Description
        /// </summary>
        /// <param name="enumName">需要获取枚举描述的枚举</param>
        /// <returns>描述内容</returns>
        public static string GetDisplayGroupName(this Enum enumType)
        {
            var member = enumType.GetType().GetMember(enumType.ToString())
                             .First();
            var vs = (DisplayAttribute)member.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
            if (vs == null)
            {
                return "";
            }

            return vs.GroupName;
        }
    }

}
