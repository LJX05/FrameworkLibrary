using System;
using System.Collections.Generic;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 操作日志实体
    /// </summary>
    public class OperLog
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public virtual int LogId { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        public virtual int ObjectType { get; set; }
        /// <summary>
        /// 对象ID
        /// </summary>
        public virtual int ObjectId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual int UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public virtual string TrueName { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public virtual string IP { get; set; }
        /// <summary>
        /// 行为ID
        /// </summary>
        public virtual int Event { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public virtual string Description { get; set; }
    }
}
