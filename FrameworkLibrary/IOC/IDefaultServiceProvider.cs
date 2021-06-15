using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.IOC
{
    /// <summary>
    /// 自己扩展的默认
    /// </summary>
   internal interface IDefaultServiceProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="uniqueness">唯一标识</param>
        /// <returns></returns>
      object GetService(Type serviceType, string uniqueness);
      /// <summary>
      /// 获取服务容器
      /// 反转注入
      /// </summary>
      /// <returns></returns>
      IServiceCollection GetServiceCollection();
    }
}
