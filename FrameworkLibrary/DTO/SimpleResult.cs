using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.DTO
{
    public class SimpleResult
    {
        /// <summary>
        /// 0:成功，其他：为异常代码
        /// </summary>
        virtual public int code { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        virtual public string msg { get; set; }
        /// <summary>
        /// 数据体
        /// </summary>
        virtual public object data { get; set; }
    }
}
