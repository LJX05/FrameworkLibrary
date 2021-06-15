using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 简单属性模型
    /// </summary>
    public class SimpleProperty
    {
        /// <summary>
        /// 属性名称
        /// 1、规则同变量命名
        /// 2、建议采用小驼峰法命名，如：滚筒直径 rollerDiameter ；标称惯量 diw；
        /// </summary>
        virtual public string name { get; set; }

        /// <summary>
        /// 属性标题
        /// 本地化标题，可以包含空格和特殊字符
        /// </summary>
        virtual public string title { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        virtual public object value { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        virtual public string unit { get; set; }
    }
}
