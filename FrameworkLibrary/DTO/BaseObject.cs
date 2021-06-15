using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YxSoft.Core.AOP;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 实体DTO基类
    /// </summary>
    public class BaseObject
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        virtual public int _ID_ { get; set; }

        /// <summary>
        /// 全局ID
        /// </summary>
        virtual public string _GUID_ { get; set; }

        /// <summary>
        /// 脏标记
        /// </summary>
        virtual public bool isDirty { get; set; }

        /// <summary>
        /// 记录标记，【0】-状态正常，【1】-新建状态，【2】-修改状态，【-1】-已锁定，【-4】-已删除
        /// </summary>
        [DtoMapping(rwFlag = RWFlag.Read)]
        virtual public int _FLAG_ { get; set; }
        /// <summary>
        /// 修改日志
        /// </summary>
        virtual public string _LOG_ { get; set; }
        /// <summary>
        /// 修该次数
        /// </summary>
        [DtoMapping(entityName = "_UC_", rwFlag = RWFlag.Read)]
        virtual public int _UC_ { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DtoMapping(entityName = "_CT_", rwFlag = RWFlag.Read)]
        virtual public DateTime _CT_ { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        [DtoMapping(entityName = "_CUID_", rwFlag = RWFlag.Read)]
        virtual public int _CUID_ { get; set; }
        /// <summary>
        /// 创建用户真实姓名
        /// </summary>
        [DtoMapping(entityName = "_CUTN_", rwFlag = RWFlag.Read)]
        virtual public string _CUTN_ { get; set; }
        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        [DtoMapping(entityName = "_UT_")]
        virtual public DateTime _UT_ { get; set; }
        /// <summary>
        /// 最后一次更新用户ID
        /// </summary>
        [DtoMapping(entityName = "_UUID_")]
        virtual public int _UUID_ { get; set; }

        /// <summary>
        /// 最后一次更新用户真实姓名
        /// </summary>
        [DtoMapping(entityName = "_UUTN_")]
        virtual public string _UUTN_ { get; set; }

    }
}
