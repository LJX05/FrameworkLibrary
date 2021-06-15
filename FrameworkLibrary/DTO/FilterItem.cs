using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 排序字段
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 排序 1-升序，2-降序
        /// </summary>
        public int sort { get; set; }
    }
    /// <summary>
    /// 筛选条件
    /// </summary>
    public class FilterItem :IComparer<FilterItem> 
    {
        /// <summary>
        /// 过滤条件
        /// 等于=1 ;
        /// 不等于=2;
        /// 包含=3;
        /// 不包含=4;
        /// 开始于=8;
        /// 结束于=10;
        /// 为空=5;
        /// 不为空= 6;
        /// 大于=7;
        /// 大于等于= 8;
        /// 小于=9;
        /// 小于等于=10;
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string[] values { get; set; }
        /// <summary>
        ///  类型
        /// </summary>
        public string formType { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string name { get; set; }
   

        public int Compare(FilterItem x, FilterItem y)
        {
            throw new NotImplementedException();
        }
    }
}
