using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.IOC
{
    /// <summary>
    /// 属性的自动注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoInjectionAttribute : Attribute
    {
        public string uniqueness { get; set; } = string.Empty;
    }
}
