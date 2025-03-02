﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Text;
using System.Threading;
using Maticsoft.Common.model;
using Maticsoft.Common.Util;

namespace Maticsoft.Common.model
{
    public class SerialPortInfo
    {
        /// <summary>
        /// 调度器的状态
        /// </summary>
        public enum SCHEDULER_STATE_ENUM
        {
            INITIAL,//初始状态
            WAIT_OPEN,//等待打开串口
            NORMAL,//正常运行
            WAIT_CLOSE,//等待关闭串口
            CLOSE//关闭
        };
        private SerialPort sp;//串口

        private Boolean isGetCMD;//是否已获取命令
        private Boolean isSendCMD;//是否已发送命令

        /// <summary>
        /// 向串口发送数据时的毫秒数——如果处理完票后用的时间超过了动态时间，不需要再休眠动态时间
        /// </summary>
        private Int64 send_data_millis = -1;

        /// <summary>
        /// 上次获取打印反馈数据时间
        /// </summary>
        private Int64 FbDataChannelData_millis = 0;

        /// <summary>
        /// 串口状态
        /// </summary>
        private GlobalConstants.COM_STATE sp_com_state = GlobalConstants.COM_STATE.CLOSE;
        
        /// <summary>
        /// 连续出票张数
        /// </summary>
        private Int64 continuous_ticket_num = 0;//

        private String orderId;//当前处理的订单号
        private lottery_ticket ticket;//要处理的彩票数据

        private GlobalConstants.PRINT_STATE_ENUM print_state;//当前的打印状态

        private Boolean isError;//是否有错误
        private String errorCode;//错误代码 
        private String errorState;//错误状态，UNTREATED:未处理；PROCESSED:已处理;
        private String errorMsg;//错误信息描述


        private store_machine macInfo;//出票机器信息
          

        /// <summary>
        /// 打印完成的票队列
        /// </summary>
        public Queue<KeyValuePair<String, String>> CompeletTicketIdStateQueue = new Queue<KeyValuePair<String, String>>();

        private int interruptState;//中断状态

        /// <summary>
        /// 打印机对象
        /// </summary>
        public SlipPrinter SLIP_PRINTER = new SlipPrinter ( GlobalConstants.PRINTER_MODEL_MAP [ Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PRINTER_MODEL] ], "USB" );


        #region 调度器初始化
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="sp">串口</param>
        /// <param name="isAvailable">串口目前是否可用(未打开或是被占用均为“N)</param>
        /// <param name="canNextStep">是否可进行下一步</param>
        /// <param name="macInfo">出票机器信息</param>
        /// <param name="ticket">要处理的彩票数据</param>
        public SerialPortInfo(SerialPort sp, store_machine macInfo)
        {
            this.sp = sp;
            this.IsError = false;
            this.MacInfo = macInfo;
            this.OrderId = String.Empty;

            this.IsGetCMD = false;
            this.IsSendCMD = true;

            this.INTERRUPT_STATE = 0;

            this.SCHEDULER_STATE = SerialPortInfo.SCHEDULER_STATE_ENUM.INITIAL;
        }

        #endregion 调度器初始化

        #region 刷新调度器的初始状态
        public void refresh()
        {
            try
            {
                this.IsError = false;
                //this.OrderId = String.Empty; 不能清空-因为现在的取票线程是按照这个id去取票的
                this.Ticket = null;

                this.INTERRUPT_STATE = 0;
                this.CompeletTicketIdStateQueue.Clear();
                this.SCHEDULER_STATE = SerialPortInfo.SCHEDULER_STATE_ENUM.INITIAL;
                this.sp_com_state = GlobalConstants.COM_STATE.CLOSE;

                //关闭串口
                if (this.Sp.IsOpen)
                {
                    try
                    {
                        this.Sp.Close();
                    }
                    catch (Exception)
                    {
                        this.IsError = true;
                        this.ErrorCode = "100002";
                        this.ErrorState = "UNTREATED";
                        this.ErrorMsg = String.Format("关闭串口{0}失败!", this.Sp.PortName);
                        //this.IsCanCheck = true;
                        this.PRINT_STATE = GlobalConstants.PRINT_STATE_ENUM.WAIT_CHECK;
                    }
                }
            }
            catch (Exception e)
            {               
                throw e;
            }            
        }
        #endregion 刷新调度器的初始状态

        public GlobalConstants.COM_STATE SP_COM_STATE
        {
            get { return sp_com_state; }
            set { sp_com_state = value; }
        }
        public GlobalConstants.PRINT_STATE_ENUM PRINT_STATE
        {
            get { return print_state; }
            set { print_state = value; }
        }

        private SCHEDULER_STATE_ENUM scheduler_state;//当前调度器资源的状态
        public SCHEDULER_STATE_ENUM SCHEDULER_STATE
        {
            get { return scheduler_state; }
            set { scheduler_state = value; }
        }
        public String OrderId
        {
            get { return orderId; }
            set
            {
                orderId = value;
            }
        }
        public SerialPort Sp
        {
            get { return sp; }
            set
            {
                sp = value;
            }
        }
        public Boolean IsError
        {
            get { return isError; }
            set
            {
                isError = value;
            }
        }
        public String ErrorMsg
        {
            get { return errorMsg; }
            set
            {
                errorMsg = value;
            }
        }
        public store_machine MacInfo
        {
            get { return macInfo; }
            set
            {
                macInfo = value;
            }
        }
        public lottery_ticket Ticket
        {
            get { return ticket; }
            set
            {
                ticket = value;
            }
        }

        public Boolean IsGetCMD
        {
            get { return isGetCMD; }
            set
            {
                isGetCMD = value;
            }
        }

        public String ErrorCode
        {
            get { return errorCode; }
            set
            {
                errorCode = value;
            }
        }
        public String ErrorState
        {
            get { return errorState; }
            set
            {
                errorState = value;
            }
        }

        public Int64 SEND_DATA_MILLIS
        {
            get { return send_data_millis; }
            set { send_data_millis = value; }
        }       

        public Boolean IsSendCMD
        {
            get { return isSendCMD; }
            set
            {
                isSendCMD = value;
            }
        }
        public int INTERRUPT_STATE
        {
            get { return interruptState; }
            set
            {
                interruptState = value;
            }
        }

        public long CONTINUOUS_TICKET_NUM
        {
            get
            {
                return continuous_ticket_num;
            }

            set
            {
                continuous_ticket_num = value;
            }
        }
        public Int64 FBDATACHANNELINIT_MILLIS
        {
            get { return FbDataChannelData_millis; }
            set { FbDataChannelData_millis = value; }
        }
    }
}
