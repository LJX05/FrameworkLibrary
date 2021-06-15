using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YxSoft.Core
{
    /// <summary>
    /// 注解扩展方法主要版本低导致
    /// </summary>
    public static class AttributeExtend
    {
        /// <summary>
        /// 获取指定注解
        /// </summary>
        public static T GetFirstAttribute<T>(this MemberInfo member, bool inherit = false) where T : Attribute
        {

            return (T)member.GetCustomAttributes(typeof(T), inherit).FirstOrDefault();
        }
    }
}
