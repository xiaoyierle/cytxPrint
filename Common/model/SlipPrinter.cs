﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Maticsoft.Common.model
{
   public class SlipPrinter
    {
       public Dictionary<int, String> printerState = new Dictionary<int, string>() {
       {0,"状态正常"},{1,"打印机缺纸"},{2,"打印纸将尽"},{3,"打印机未连接"}};
        /// <summary>
        /// 打印机编号——后续操作使用
        /// </summary>
       public int M_OBJID { get; set; }
       /// <summary>
       /// 打印机名称
       /// </summary>
       public String M_NAME { get; set; }

       /// <summary>
       /// 打印机连接方式
       /// </summary>
       public String M_CONNECTION_WAY { get; set; }

       /// <summary>
       /// 打印机状态
       /// </summary>
       public int M_STATE { get; set; }

       public SlipPrinter(String m_name,String c_way) { 
           this.M_NAME = m_name;
           this.M_CONNECTION_WAY = c_way;
           this.M_STATE = 3;
       }
    }
}
