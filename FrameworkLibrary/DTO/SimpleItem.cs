using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 简单列表数据模型
    /// </summary>
    public class SimpleItem
    {
        /// <summary>
        /// 项名
        /// </summary>
        virtual public string text { get; set; }
        /// <summary>
        /// 项值
        /// </summary>
        virtual public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        virtual public object tag { get; set; }
    }
}
