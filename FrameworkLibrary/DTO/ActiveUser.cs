using System;
using System.Collections.Generic;
using System.Linq;

namespace YxSoft.Core.DTO
{
    /// <summary>
    /// 活跃的用户
    /// </summary>
    public class ActiveUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 机构ID
        /// </summary>
        public long? OrgId { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        public long? StaffId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string TrueName { get; set; }        
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public IList<long> JoinedRoles { get; set; }
        /// <summary>
        /// 所属角色名称
        /// </summary>
        public string JoinedRolesDesc { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 最后一次访问的地址
        /// </summary>
        public string LastVisitedUrl { get; set; }
        /// <summary>
        /// 最后一次访问的模块名称
        /// </summary>
        public string LastVisitedModel { get; set; }

        /// <summary>
        /// 最后一次访问的模块ID
        /// </summary>
        public long LastVisitedFuncID { get; set; }

        /// <summary>
        /// 最后一次访问的时间
        /// </summary>
        public DateTime LastVisitedTime { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

         
        /// <summary>
        ///  密码有效期截止日期
        /// </summary>
        public virtual DateTime? MMYXQZ
        {
            get;
            set;
        }
          
        /// <summary>
        /// 账户有效期截止日期
        /// </summary>
        public virtual DateTime? ZHYXQZ
        {
            get;
            set;
        }
        /// <summary>
        /// 最后一访问IP地址
        /// </summary>
        public string LastLoginIP { get;  set; }
    }
}