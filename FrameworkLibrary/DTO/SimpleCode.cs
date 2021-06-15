using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 简单结果数据对象
    /// </summary>
    public class SimpleCode
    {
        /// <summary>
        /// 数据项ID
        /// </summary>
        virtual public int id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        virtual public string name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        virtual public  string code{ get; set; }
        /// <summary>
        /// 种类
        /// </summary>
        virtual public  string kind { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        virtual public DateTime time { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        virtual public string producer { get; set; }
        /// <summary>
        /// 附加对象
        /// </summary>
        virtual public object tag { get; set; }
    }
}
