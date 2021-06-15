using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using YxSoft.Core.DTO;

namespace YxSoft.Core
{
    /// <summary>
    /// 数据校验辅助类
    /// added by zhangjunjie at 2020-11-25 11:22
    /// </summary>
    public class ValidatetionHelper
    {
        /// <summary>
        /// 判定对象是否合法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleResult IsValid(object value,string separator1, string separator2)
        {
            var result = IsValid(value);

            if (result.IsVaild) 
            {
                return new SimpleResult() { code = 0, msg = "成功" };
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (ErrorMember errorMember in result.ErrorMembers)
            {
               var attDisplay = value.GetType().GetProperty(errorMember.MemberName).GetFirstAttribute<DisplayNameAttribute>();
                if(attDisplay!=null)
                {
                    sb.AppendLine(attDisplay.DisplayName + "：" + errorMember.ErrorMessage);
                }
                else
                {
                    sb.AppendLine(errorMember.MemberName + "：" + errorMember.ErrorMessage);
                }
            }          
            if (sb.Length<1)
            {
               return new SimpleResult() { code = 0, msg = "成功" };
            }
            else
            {
               return new SimpleResult() { code = 1, msg = sb.ToString() };
            }

        }
        /// <summary>
        /// 判定对象是否合法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ValidResult IsValid(object value)
        {
            ValidResult result = new ValidResult();
            try
            {
                var validationContext = new ValidationContext(value, null, null);
                var results = new List<ValidationResult>();
                result.IsVaild = Validator.TryValidateObject(value, validationContext, results, true);
                if (!result.IsVaild)
                {
                    result.ErrorMembers = new List<ErrorMember>();
                    foreach (var item in results)
                    {
                        result.ErrorMembers.Add(new ErrorMember()
                        {
                            ErrorMessage = item.ErrorMessage,
                            MemberName = item.MemberNames.FirstOrDefault()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsVaild = false;
                result.ErrorMembers = new List<ErrorMember>();
                result.ErrorMembers.Add(new ErrorMember()
                {
                    ErrorMessage = ex.Message,
                    MemberName = "内部错误"
                });
            }
            return result;
        }
    }
}
