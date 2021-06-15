using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace YxSoft.Core.AOP
{
    /// <summary>
    /// 自定义注解
    /// </summary>
    [AttributeUsage(AttributeTargets.Property| AttributeTargets.Class, AllowMultiple = true)]
    public class DtoMappingAttribute  : Attribute
    {
        /// <summary>
        /// 是否是id
        /// </summary>
        public KeyFlag Key { get; set; }
        /// <summary>
        /// 映射的实体属性的名称
        /// </summary>
        public string entityName { get; set; }
        /// <summary>
        /// 是否需要记录log
        /// </summary>
        public bool needLog { get; set; }
        /// <summary>
        /// 所映射实体的读写标志：0-禁止读写，1-仅可读，2-仅可写，3-可读写
        /// </summary>
        public RWFlag rwFlag { get; set; } = RWFlag.ReadWrite;
    }
    /// <summary>
    /// 读写标志
    /// </summary>
    [Flags]
    public enum RWFlag
    {
        /// <summary>
        /// 0-禁止读写
        /// </summary>
        Forbid = 0,
        /// <summary>
        /// 1-仅可读
        /// </summary>
        Read = 1,
        /// <summary>
        /// 2-仅可写
        /// </summary>
        Write = 2,
        /// <summary>
        /// 3-可读写
        /// </summary>
        ReadWrite = 3
    }
    /// <summary>
    /// 读写标志
    /// </summary>
    [Flags]
    public enum KeyFlag
    {
        /// <summary>
        /// 0-普通字段
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 1-物理主键
        /// </summary>
        Primary = 1,
        /// <summary>
        /// 2-逻辑主键
        /// </summary>
        Key = 2,
        /// <summary>
        /// 3-逻辑主键且物理主键
        /// </summary>
        PrimaryKey = 3
    }
}
