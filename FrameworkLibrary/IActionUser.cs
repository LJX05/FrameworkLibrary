using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YxSoft.Core.DTO;

namespace YxSoft.Core
{
    /// <summary>
    /// 当前活动用户接口
    /// </summary>
    public interface IActionUser
    {
        ActiveUser activeUser();
    }
}
