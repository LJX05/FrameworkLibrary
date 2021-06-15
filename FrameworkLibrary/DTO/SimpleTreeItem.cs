using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 简单树数据模型
    /// </summary>
    public class SimpleTreeItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        virtual public string name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        virtual public string label { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        virtual public string id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        virtual public string pid { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        virtual public IList<SimpleTreeItem> children { get; set; }
        /// <summary>
        /// 
        /// </summary>
        virtual public object tag { get; set; }
    }
}
