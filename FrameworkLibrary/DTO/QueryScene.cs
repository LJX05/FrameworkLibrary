using System;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 查询场景
    /// </summary>
    public class QueryScene
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        virtual public int sceneId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        virtual public int type { get; set; }
        /// <summary>
        /// 场景名称
        /// </summary>
        virtual public string name { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        virtual public long userId { get; set; }
        /// <summary>
        /// 排序ID
        /// </summary>
        virtual public int sort { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        virtual public string data { get; set; }
        /// <summary>
        /// 1隐藏
        /// </summary>
        virtual public int isHide { get; set; }
        /// <summary>
        /// 1系统0自定义
        /// </summary>
        virtual public int isSystem { get; set; }
        /// <summary>
        /// 系统参数
        /// </summary>
        virtual public string bydata { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        virtual public DateTime createTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        virtual public DateTime? updateTime { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        virtual public int? isDefault { get; set; }

        //{"sceneId":812,"type":4,"name":"全部产品","userId":14780,"sort":0,"data":"","isHide":0,"isSystem":1,"bydata":"all","createTime":"2020-09-29 13:47:34","updateTime":"2020-11-04 01:25:15","isDefault":null}
    }
}
