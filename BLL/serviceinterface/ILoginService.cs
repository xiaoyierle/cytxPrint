﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Maticsoft.BLL.serviceinterface
{
   public interface ILoginService
    {
        int LoginRequest(string userName, string password);

        /// <summary>
        /// 清除机器对应的数据——机器表、机器支持彩种表、机器可出票彩种表
        /// </summary>
        /// <returns></returns>
        Boolean clearMachineInfo();
    }
}
