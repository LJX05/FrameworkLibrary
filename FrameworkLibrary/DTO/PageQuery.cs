using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 分页类条件类
    /// </summary>
    public class PageQuery
    {

        /// <summary>
        /// 第几页，从1开始
        /// </summary>
        public int pageNumber { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 总记录行数
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 场景ID 
        /// </summary>
        public int sceneId { get; set; }
        /// <summary>
        /// 场景名称 
        /// </summary>
        public string sceneName { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string keyWord { get; set; }
        /// <summary>
        /// 从from
        /// </summary>
        public DateTime date1 { get; set; }
        /// <summary>
        /// 到to
        /// </summary>
        public DateTime date2 { get; set; }

        /// <summary>
        /// 自定义过滤条件
        /// </summary>
        public IList<FilterItem> listFilter { get; set; }

        /// <summary>
        /// 自定义排序字段
        /// </summary>
        public IList<OrderItem> listOrder { get; set; }
    }
}
