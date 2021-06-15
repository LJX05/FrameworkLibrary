using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 数据合法性检查结果类
    /// added by zhangjunjie at 2020-11-25 11:22
    /// </summary>
    public class ValidResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ErrorMember> ErrorMembers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsVaild { get; set; }
    }
    /// <summary>
    /// 错误对象信息
    /// </summary>
    public class ErrorMember
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 错误对象名称
        /// </summary>
        public string MemberName { get; set; }
    }
}
