﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO.Ports;

namespace Maticsoft.Common.Util
{
    public class GlobalConstants
    {
        public const String KEY = "CYTX-FHWC";             

        /// <summary>
        /// 日志类别
        /// </summary>
        public enum LOGTYPE_ENUM
        {
            SYSTEM_OPERATION,//系统运行
            OWNER_OPERATOR,//店主操作
            EXCEOTION,//异常
            TICKET_LOG,//出票日志
            FEEDBACK_LOG,//通讯日志
            REQUEST_TICKETS_LOG//请求票数据日志
        }

        /// <summary>
        /// 串口状态
        /// </summary>
        public enum COM_STATE { 
            CLOSE,//关闭
            OPEN_SUCC,//打开成功
            OPEN_FAIL,//打开失败
            FBDATACHANNELINIT_WAIT,//等待初始化数据通道
            FBDATACHANNELINIT_SUCC,//初始化数据通道成功
            FBDATACHANNELINIT_FAIL,//初始化数据通道失败
            COMMUNICATIONS_NORMAL,//通讯正常
            COMMUNICATION_ABNORMAL//通讯异常
        }

        public static Dictionary<COM_STATE, String> COM_STATE_DICTIONARY = new Dictionary<COM_STATE, string>() { 
        {COM_STATE.CLOSE,"关闭"},{COM_STATE.OPEN_SUCC,"打开成功"},{COM_STATE.OPEN_FAIL,"打开失败"},
        {COM_STATE.FBDATACHANNELINIT_WAIT,"检查通讯"},
        {COM_STATE.FBDATACHANNELINIT_SUCC,"检查通讯正常"},{COM_STATE.FBDATACHANNELINIT_FAIL,"检查通讯异常"},
        {COM_STATE.COMMUNICATIONS_NORMAL,"通讯正常"},{COM_STATE.COMMUNICATION_ABNORMAL,"通讯异常"}
        };

        /// <summary>
        /// 网络状态
        /// </summary>
        public static class WEB_STATE
        {
            public const string NORMAL = "网络通畅";
            public const string UNUSUAL = "网络异常";
        }

        public static class TRANSCODE
        {
            public const string REQUEST_TICKETS = "1000";//取票
            public const string FEEDBACK = "1001";//反馈
            public const string LOGIN = "1003";//登录
            public const string SYNC = "1002";//同步
            public const string LICENSE = "1004";//更新彩种
        }

        //是否正在排序
        public static bool SORTING = false;

        #region 订单和彩票的状态
        //订单、彩票状态(订单和彩票通用)
        public static class ORDER_TICKET_STATE
        {
            public const int AWAITING_ALLOT = 1;//等待分配com口
            public const int AWAITING_PRINT = 2;//等待出票
            public const int PRINTTING = 3;//出票中
            public const int PAUSE = 4;//暂停等待
            public const int PRINTTING_COMPLETE = 5;//出票完成
            public const int ERROR = 6;//错漏票
            public const int ERROR_WAITING_PRINT = 7;//错漏票(等待出票)
            public const int ERROR_PRINTTING = 8;//错漏票(出票中)
            public const int ERROR_PAUSE = 9;//错漏票(暂停)
            public const int ERROR_COMPLETE = 10;//错漏票(出票完成)
            public const int MANUAL_WAITING_PRINT = 11;//手工票(等待处理)
            public const int MANUAL_PRINTTING = 12;//手工票(处理中)
            public const int MANUAL_COMPLETE = 13;//手工出票成功
            public const int RE_WAITING_PRINT = 14;//重打票(等待处理)
            public const int RE_PRINTTING = 15;//重打票(处理中)
            public const int RE_COMPLETE = 16;//重打出票成功
            public const int CANCEL = 17;//撤票、撤单
            public const int EXPIRED = 18;//逾期
        }

        public static Dictionary<int, string> ORDER_TICKET_STATE_TEXT_DIC = new Dictionary<int, string>
        {
            {1,"等待分配"},
            {2,"等待出票"},
            {3,"出票中"},
            {4,"暂停等待"},
            {5,"出票完成"},
            {6,"错漏票"},
            {7,"等待出票(错)"},
            {8,"出票中(错)"},
            {9,"暂停(错)"},
            {10,"出票完成(错)"},
            {11,"等待处理(手)"},
            {12,"处理中(手)"},
            {13,"成功(手)"},
            {14,"等待重打"},
            {15,"正在重打"},
            {16,"重打完成"},
            {17,"撤消"},
            {18,"逾期"}
        };

        /// <summary>
        /// 打印状态
        /// </summary>
       public enum PRINT_STATE_ENUM {
            INIT,//初始状态
            WAIT_TICKET,//等待读票
            WAIT_PRINT,//等待打印
            WAIT_PRINT_RESULT,//等待打印结果
            WAIT_CHECK,//等待检查
        }

        /// <summary>
        /// 订单上的错漏票处理标识(只有当betstate字段为错漏票时，以下的这些值在订单上的err_ticket_sign字段上才有意义——不过一般情况下，当betstate不为错漏票时，err_ticket_sign的值应该为0)
        /// </summary>
        public static class ORDER_ERR_SIGN
        {
            public const int NO_OPERATION = 0;//未选择
            public const int IN_PROGRESS = 1;//操作中
        }

        /// <summary>
        /// 彩票上的错漏票处理标识(只有当ticketstate字段为错漏票时，以下的这些值在err_ticket_sign字段上才有意义——不过一般情况下，当ticketstate不为错漏票时，err_ticket_sign的值应该为0)
        /// </summary>
        public static class TICKET_ERR_SIGN
        {
            public const int NO_OPERATION = 0;//未选择
            public const int TICKET_AGAIN = 1;//重新出票
            public const int CANCEL = 2;//撤票
            public const int COMPLETE_TICKET = 3;//确定已出票
        }

        #endregion

        /// <summary>
        /// 数据保存时间常量枚举
        /// </summary>
        public static Dictionary<String, String> DataKeepTimeDic = new Dictionary<string, string>() { { "1", "7天" }, { "2", "15天" }, { "3", "1个月" }, { "4", "3个月" }, { "5", "6个月" }, { "6", "12个月" } };
        /// <summary>
        /// 数据保存时间常量对应SQL
        /// </summary>
        public static Dictionary<String, String> DataKeepTimeSQLDic = new Dictionary<string, string>() { { "1", "-7 day" }, { "2", "-15 day" }, { "3", "-1 month" }, { "4", "-3 month" }, { "5", "-6 month" }, { "6", "-12 month" } };

        /// <summary>
        /// 是和否的标识
        /// </summary>
        public static class TrueFalseSign
        {
            public const String FALSE = "0";//否
            public const String TRUE = "1";//是
        }

        /// <summary>
        /// 中断状态
        /// </summary>
        public static class InterruptState
        {
            public const int INTERRUPT_NOT = 0;//不处于中断状态中
            public const int INTERRUPT_WAIT_TICKETTHREAD = 1;//等待出票线程处理
            public const int INTERRUPT_WAIT_ORDERTHREAD = 2;//等待订单线程处理
        }

        /// <summary>
        /// 机器类别
        /// </summary>
        public static class Machine_Type
        {
            public const int SPORTS_LOTTERY = 1;//体彩机型
            public const int WELFARE_LOTTERY = 2;//福彩机型
            public const int HIGH_FREQUENCY = 3;//高频机型
        }

        /// <summary>
        /// 登录的返回结果
        /// </summary>
        public static class ResultCode
        {
            public const string SUCCESS = "100000";
            public const string PASSWORD_USERNAME_ERROR = "100001";
        }
        public static Dictionary<String, String> machineTypeDictionary = new Dictionary<string, string>() { { Machine_Type.SPORTS_LOTTERY.ToString(), "体彩机型" }, { Machine_Type.WELFARE_LOTTERY.ToString(), "福彩机型" }, { Machine_Type.HIGH_FREQUENCY.ToString(), "高频机型" } };

        #region 速度等级
        public static Dictionary<String, String> SpeedLevelDictionary = new Dictionary<string, string>() { { "1", "一档" }, { "2", "二档" }, { "3", "三档" }, { "4", "四档" }, { "5", "五档" }, { "6", "六档" }, { "7", "七档" }, { "8", "八档" } };
        #endregion

        #region 延时配置
        //延时初始化
        // 50,100,200,300,400,500,600,700,800,900,1000,1500,2000,2500,3000,3500——(ms)
        public static String[] delay_Time_Variable = { "50", "100", "200", "300", "400", "500", "600", "700", "800", "900", "1000", "1500", "2000", "2500", "3000", "3500" };

        public static Dictionary<String, String> delay_Time_Dictionary = new Dictionary<string, string> {
        {"D01","E0"},{"D02","E1"},{"D03","E2"},{"D04","E3"},{"D05","E4"},
        {"D06","E5"},{"D07","E6"},{"D08","E7"},{"D09","E8"},{"D10","E9"},
        {"D11","EA"},{"D12","EB"},{"D13","EC"},{"D14","ED"},{"D15","EE"},{"D16","EF"}
        };
        #endregion

        #region 键盘命令转换
        public static Dictionary<String, byte> keyBoardKeyValue = new Dictionary<string, byte> {
            {"F1",0xB0},{"F2",0xB1},{"F3",0xB2},{"F4",0xB3},{"F5",0xB4},{"F6",0xB5},{"F7",0xB6},{"F8",0xB7},{"F9",0xB8},{"F10",0xB9},{"F11",0xBA},{"F12",0xBB},
            {"Esc",0x1B},{"Tab",0x09},{"Backspace",0x08},{"UArrow",0xC0},{"LArrow",0xC1},{"DArrow",0xC2},{"RArrow",0xC3},
            {"End",0xC7},{"PgUp",0xC5},{"PgDn",0xC8},{"Ins",0xC5},{"Del",0xC6},{"CapsL",0xC9}, { "Enter",0x48}, { "Win",0xD3}
        };
        #endregion

        #region 命令
        //命令标识
        public static class BASE_CMD
        {
            public const String KEYBOARD = "KEYBOARD";//键盘
            public const String HIGHSPEED = "HIGHSPEED";//高速

            #region 公用命令
            public static String CMD_HEAD = "FF-11-FF-13";//命令头部
            public static String CMD_END = "FF-0D-FF-0A";//命令尾部
            public static String RECEIVE_START_CONTRL = "FF-11-FF-13-43"; // 接收开始字节, 控制数据
            public static String RECEIVE_START_PRINT = "FF-11-FF-13-50"; // 接收开始字节, 打印数据

            // 键盘已收到数据
            public static String KEYBOARD_RECEVICEDATA = "FF-11-FF-13-43-BB-37-02-02-3A-FF-0D-FF-0A";
            // 键盘已发送数据
            public static String KEYBOARD_SENDDATA = "FF-11-FF-13-43-BB-37-01-01-3A-FF-0D-FF-0A";
            // 键盘正在延时
            public static String KEYBOARD_DELAY_ING = "FF-11-FF-13-43-BB-37-01-01-E0-FF-0D-FF-0A";
            #endregion 公用命令
        }
        public static Dictionary<String, String> cmdSign_KV = new Dictionary<string, string> { { "KEYBOARD", "4B" }, { "HIGHSPEED", "53" }, { "", "" } };

        /// <summary>
        /// 命令字符数组
        /// </summary>
        public static class CMDByteArrays
        {
            //命令固定部分
            public static byte[] cmdhead = { 0xFF, 0x11, 0xFF, 0x13 };//命令头部
            public static byte[] cmdend = { 0xFF, 0x0D, 0xFF, 0x0A };//命令尾部            

            public static byte[] receive_Start_Contrl = { 0xFF, 0x11, 0xFF, 0x13, 0x43 }; // 接收开始字节, 控制数据
            public static byte[] receive_Start_Print = { 0xFF, 0x11, 0xFF, 0x13, 0x50 }; // 接收开始字节, 打印数据
            public static byte[] receive_End = { 0xFF, 0x0D, 0xFF, 0x0A };       // 接收结束字节

            public static byte[] GTCP86_PrintData_End = { 0x1D, 0x56, 0x42, 0x00 }; // 接收的票号数据结尾 
            public static byte[] GTC8_PrintData_End = { 0x1D, 0x56, 0x42, 0x00 }; // 接收的票号数据结尾  
            public static byte[] KS230_PrintData_End = { 0x1D, 0x56, 0x01 };       // 接收的票号数据结尾    
            public static byte[] LA600_PrintData_End = { 0x1D, 0x56, 0x42 };       // 接收的票号数据结尾  
            public static byte[] LA600_PrintData_End_A = { 0x1D, 0x56, 0x01 };       // 接收的票号数据结尾
            public static byte[] ITDNew_USB_PrintData_End = { 0x1D, 0x56, 0x42, 0x00 }; // 接收的票号数据结尾 
            public static byte[] ITDOld_USB_PrintData_End = { 0x1D, 0x56, 0x42, 0x00 }; // 接收的票号数据结尾 
            public static byte[] ITDSerial_PrintData_End = { 0x1D, 0x56, 0x42, 0x00 }; // 接收的票号数据结尾
        }



        #endregion

        #region 错误
        /// <summary>
        /// 错误命令
        /// </summary>
        public static class ERROR_CMD {
            // 错误命令头部 !
            public static String ERROR_CMD_HEAD = "FF-11-FF-13-43-BB-38-01";

            // 主机收到读票机数据后, 数据转发器的USB端口已关闭 !
            public static String MACHINE_READ_ED_USBERROR = "FF-11-FF-13-43-BB-38-01-01-3A-FF-0D-FF-0A";
            // 主机收到读票机数据后, 无玩法类型票和玩法类型错误票 
            public static String MACHINE_READ_ED_PLAYERROR = "FF-11-FF-13-43-BB-38-01-02-3B-FF-0D-FF-0A";
            // 主机收到读票机数据后, 原始数据错误
            public static String MACHINE_READ_ED_DATAERROR = "FF-11-FF-13-43-BB-38-01-03-3C-FF-0D-FF-0A";
            // 数据转发器内存检查错误 
            public static String MACHINE_READ_ED_RAMERROR = "FF-11-FF-13-43-BB-38-01-04-3D-FF-0D-FF-0A";
            // 主机收到读票机数据后, 数据校验错误 
            public static String MACHINE_READ_ED_CHECKERROR1 = "FF-11-FF-13-43-BB-38-01-05-3E-FF-0D-FF-0A";
            // 主机收到读票机数据后, 数据校验错误
            public static String MACHINE_READ_ED_CHECKERROR2 = "FF-11-FF-13-43-BB-38-01-06-3F-FF-0D-FF-0A";
            
        }
        //错误代码及其对应的描述,到时候错误检测线程会根据不同的错误代码给出不同的处理方式
        public static class ERROR_CODE
        {
            public const String OPEN_PORT_FAIL = "100001";//打开串口失败
            public const String CLOSE_PORT_FAIL = "100002";//关闭串口失败
            public const String SEND_DATA_FAIL = "100003";//发送数据失败
            public const String RECEVICE_PORTCLOSE = "100004";//读反馈数据,串口已关闭
            public const String RECEVICE_TIMEOUT = "100005";//超时未读到反馈数据
            public const String RECEVICE_INCOMPLETE = "100006";//反馈数据不完整

            public const String KEYBOARD_NOT_RECEIVED_DATA = "100007";//键盘未接收到数据
            public const String KEYBOARD_NOT_SEND_DATA = "100008";//键盘未发送数据

            public const String DIGITALINTERVAL_INIT_FAIL = "100009";//数据间隔初始化失败
            public const String FEEDBACKDATACHANNEL_INIT_FAIL = "100010";//彩机数据反馈通道初始化失败

            public const String PARSER_LOAD_FAIL = "100011";//加载解析程序失败

            public const String FEEDBACK_DATA_INCOMPLETE = "100012";//反馈数据不完整，没有取到完整的反馈命令

            public const String ANALYTICAL_DATA_ANOMALIES = "100013";//解析数据时异常

            public const String COMPARISON_DATA_FAIL = "100014";//比对失败            


            // 主机收到读票机数据后, 数据转发器的USB端口已关闭 !
            public const String MACHINE_READ_ED_USBERROR = "200001";
            // 主机收到读票机数据后, 无玩法类型票和玩法类型错误票 
            public const String MACHINE_READ_ED_PLAYERROR = "200002";
            // 主机收到读票机数据后, 原始数据错误
            public const String MACHINE_READ_ED_DATAERROR = "200003";
            // 数据转发器内存检查错误 
            public const String MACHINE_READ_ED_RAMERROR = "200004";
            // 主机收到读票机数据后, 数据校验错误 
            public const String MACHINE_READ_ED_CHECKERROR1 = "200005";
            // 主机收到读票机数据后, 数据校验错误
            public const String MACHINE_READ_ED_CHECKERROR2 = "200006";

            //取不到出票控制流程，不支持该彩种
            public const String NO_HAS_PROCONTROLLER = "300001";

            //投注单不支持该彩票内容
            public const String SLIP_NO_PROCONTROLLER = "400001";

            //打印机连接异常
            public const String PRINTER_CONNECTION_EXCEPTION = "500001";
            //打印机缺纸
            public const String PRINTER_IS_OUT_OF_PAPER = "500002";
            //打印机打开失败
            public const String PRINTER_OPEN_FAILED = "500003";
            //打印机打印失败
            public const String PRINTER_WORK_FAILED = "500004";
        }

        public static Dictionary<String, String> ErrorCodeDictionary = new Dictionary<string, string> {
            { ERROR_CODE.OPEN_PORT_FAIL, "串口{0}打开失败，线未插好或是串口资源被占用，请检查！" },
            { ERROR_CODE.CLOSE_PORT_FAIL, "关闭串口{0}失败！" },
            { ERROR_CODE.SEND_DATA_FAIL, "{0}发送数据失败，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.RECEVICE_PORTCLOSE, "{0}读反馈数据时串口已异常关闭，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.RECEVICE_TIMEOUT, "{0}超时未读到反馈数据，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.RECEVICE_INCOMPLETE, "{0}反馈数据不完整，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.KEYBOARD_NOT_RECEIVED_DATA, "{0}键盘未接收到数据，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.KEYBOARD_NOT_SEND_DATA, "{0}键盘未发送数据，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.DIGITALINTERVAL_INIT_FAIL, "{0}数据间隔初始化失败！" },
            { ERROR_CODE.FEEDBACKDATACHANNEL_INIT_FAIL, "{0}彩机数据反馈通道初始化失败，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.PARSER_LOAD_FAIL, "{0}加载票花解析程序失败，请升级数据或是联系客服人员解决！" },
            { ERROR_CODE.FEEDBACK_DATA_INCOMPLETE, "{0}读取的反馈不完整，请检查实际出票后重新点击出票按钮继续出票！" },
            { ERROR_CODE.ANALYTICAL_DATA_ANOMALIES, "{0}解析反馈数据时异常，请升级数据或是联系客服人员解决！" },
            { ERROR_CODE.COMPARISON_DATA_FAIL, "{0}反馈票面数据比对失败，请处理后重新点击出票按钮继续出票！" },

            { ERROR_CODE.MACHINE_READ_ED_USBERROR, "{0}主机收到读票机数据后, 数据转发器的USB端口已关闭 ，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.MACHINE_READ_ED_PLAYERROR, "{0}主机收到读票机数据后, 无玩法类型票和玩法类型错误票!" },
            { ERROR_CODE.MACHINE_READ_ED_DATAERROR, "{0}主机收到读票机数据后, 原始数据错误，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.MACHINE_READ_ED_RAMERROR, "{0}数据转发器内存检查错误，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.MACHINE_READ_ED_CHECKERROR1, "{0}主机收到读票机数据后, 数据校验错误，请检查设备是否正常后重新点击出票按钮继续出票！" },
            { ERROR_CODE.MACHINE_READ_ED_CHECKERROR2, "{0}主机收到读票机数据后, 数据校验错误，请检查设备是否正常后重新点击出票按钮继续出票！" },

            { ERROR_CODE.NO_HAS_PROCONTROLLER, "{0}取不到出票控制流程，不支持该彩种，请升级数据或是联系客服人员解决！" },

            { ERROR_CODE.SLIP_NO_PROCONTROLLER, "投注单不支持该彩票内容！" },
            { ERROR_CODE.PRINTER_CONNECTION_EXCEPTION, "打印机连接异常！" },
            { ERROR_CODE.PRINTER_IS_OUT_OF_PAPER, "打印机缺纸！" },
            { ERROR_CODE.PRINTER_OPEN_FAILED, "打印机打开失败！" },
            { ERROR_CODE.PRINTER_WORK_FAILED, "打印机打印失败！" },
        };

        public static Dictionary<int, string> WebExceptionDictionary = new Dictionary<int, string>
        {
            { 0, "未遇到任何错误。" },
            { 1, "名称解析服务未能解析主机名。" },
            { 2, "连接失败。" },
            { 3, "没有从远程服务器接收到完整响应。" },
            { 4, "未能将完整请求发送到远程服务器。" },
            { 5, "该请求是管线请求，连接未接收到响应即被关闭。" },
            { 7, "从服务器接收到的响应完成了，但它指示了一个协议级错误。例如，HTTP 协议错误（如 401 访问被拒绝）使用此状态。" },
            { 8, "连接关闭。" },
            { 9, "未能验证服务器证书。" },
            { 10, "使用 SSL 建立连接时发生错误。" },
            { 11, "此服务器响应不是有效的 HTTP 响应。" },
            { 12, "Keep-alive标头的请求连接被意外关闭。" },
            { 13, "内部异常请求挂起。" },
            { 14, "在请求的超时期限内未收到任何响应。" },
            { 15, "此服务器响应不是有效的 HTTP 响应。" },
            { 16, "发生未知类型的异常。" },
            { 17, "当发送请求或从服务器接收响应时，会接收到超出指定限制的消息。" },
            { 18, "未找到指定的缓存项。" },
            { 19, "缓存策略不允许该请求。一般而言，当请求不可缓存或有效策略禁止向服务器发送请求时会发生这种情况。如果请求方法暗示请求正文存在，请求方法需要与服务器直接交互，或者请求包含条件标头，则您可能会收到此状态。" },
            { 20, "代理不允许此请求。" }
        };

        /// <summary>
        /// 错误的处理状态
        /// </summary>
        public static class ErrorState
        {
            public const String UNTREATED = "UNTREATED";//未处理
            public const String PROCESSED = "PROCESSED";//处理
        }

        #endregion
        //图片
        public static Dictionary<string, string> ImgDictionary = new Dictionary<string, string>
        {
            {"1","pai3"},
            {"2","pai5"},
            {"3","qixing"},
            {"4","daletou"},
            {"5","shengfucai"},
            {"6","renxuan9"},
            {"7","banquan"},
            {"8","jinqiu"},
            {"9","jingzu"},
            {"10","jinglan"},
            {"11","shuangseqiu"},
            {"12","3d"},
            {"13","qile"},
            {"21","beidan"},
            {"101","11yunduojin"}
        };

        public static class FeedbackResult
        {
            public const string SUCCESS = "100000";
            public const string NONEXIST = "100100";
        }

        //订单反馈状态
        public static class FeedbackState
        {
            public const int NOT_FEEDBACK = 0;//未反馈
            public const int SUCCESS = 1;//反馈成功
            public const int FAILED = 2;//反馈失败
            public const int FAILED_PROCESSED = 3;//反馈失败,已处理
        }

        /// <summary>
        /// 停止位
        /// </summary>
        public static Dictionary<string, StopBits> StopBitsDic = new Dictionary<string, StopBits>
        {
            {"None",StopBits.None},
            {"1",StopBits.One},
            {"1.5",StopBits.OnePointFive},
            {"2",StopBits.Two}
        };

        /// <summary>
        /// 校验位
        /// </summary>
        public static Dictionary<string, Parity> ParityDic = new Dictionary<string, Parity>
        {
            {"None",Parity.None},
            {"Even",Parity.Even},
            {"Odd",Parity.Odd},
            {"Mark",Parity.Mark},
            {"Space",Parity.Space}
        };

        public static string ConnectionStringLog { get; set; }

        public static string ConnectionStringBll { get; set; }

        /// <summary>
        /// 打印机型号
        /// </summary>
        public class PRINTER_MODEL {
            public const string RGK532 = "RGK532";//
            public const string RGP80B = "RGP80B";//
            public const string RGP80A = "RGP80A";//
        }
        /// <summary>
        /// 打印机型号
        /// </summary>
        public static Dictionary<string, string> PRINTER_MODEL_MAP = new Dictionary<string, string>()
        {
            {PRINTER_MODEL.RGK532,"RG-K532"},
            {PRINTER_MODEL.RGP80B,"RG-P80B"},
            {PRINTER_MODEL.RGP80A,"RG-P80A"}
        };

        /// <summary>
        /// 打印方式
        /// </summary>
        public class PRINT_TYPE {
            public const string MACHINE = "MACHINE";//彩机出票
            public const string PRINTER = "PRINTER";//打印机出单
            public const string PRINTER_QRCODE = "PRINTER_QRCODE";//打印二维码
        }
        /// <summary>
        /// 打印方式
        /// </summary>
        public static Dictionary<string, string> PRINT_TYPE_MAP = new Dictionary<string, string> ( )
        {
            {"MACHINE","彩机出票"},
            {"PRINTER","打印机出单"},
            {"PRINTER_QRCODE","打印二维码"}
        };

        /// <summary>
        /// 服务器类型
        /// </summary>
        public class SERVER_TYPE {
            public const string O2O = "O2O";//彩机出票
            public const string RD = "RD";//打印机出单
        }
        /// <summary>
        /// 服务器类型
        /// </summary>
        public static Dictionary<string, string> SERVER_TYPE_MAP = new Dictionary<string, string> ( )
        {
            {"76test","76测试服务器"},
            {"FLW","葫芦娃服务器"},
            {"O2O","O2O服务器"},
            {"RD","热点服务器"}
        };
        /// <summary>
        /// 服务器地址
        /// </summary>
        public static Dictionary<String,String> SERVER_URL_MAP = new Dictionary<string, string> ( )
        {
            {"76test","http://115.28.236.76:83/cycomm.do"},
            {"FLW","http://print.zhiliaocai.com/cycomm.do"},
            {"O2O","http://print.cp020.com/cycomm.do"},
            {"RD",  "http://print.cyrd360.com/cycomm.do"}
        };

        /// <summary>
        /// 系统配置表的各项key值
        /// </summary>
        public static class SYSTEM_CONFIG_KEYS
        {
            public const string PRINTER_MODEL = "PRINTER_MODEL";//打印机机型
            public const string PRINT_TYPE = "PRINT_TYPE";//打印方式
            public const string PROVINCES_CODE = "PROVINCES_CODE";//选择的省市编码
            public const string SERVER_TYPE = "SERVER_TYPE";//服务器类型
            public const string IS_AUTO_FEEDBACK = "IS_AUTO_FEEDBACK";//是否自动反馈
            public const string PAGE_SIZE = "PAGE_SIZE";//分页默认大小
            public const string DATA_KEEP_TIME = "DATA_KEEP_TIME";//数据保存时间
            public const string CONTROLDATA_UPDATE_DATE = "CONTROLDATA_UPDATE_DATE";//控制数据更新时间
            public const string LAST_LOGIN_NAME = "LAST_LOGIN_NAME";//最后登陆的用户名
            public const string AUDIO_ORDER = "AUDIO_ORDER";//来单提示音
            public const string AUDIO_ERROR = "AUDIO_ERROR";//错漏票提示音
            public const string AUDIO_MANUAL = "AUDIO_MANUAL";//有手工单提示音
            public const string AUDIO_FBACK = "AUDIO_FBACK";//需手工反馈提示音
        }

        public static Dictionary<String,String> SYSTEM_CONFIG_KEYS_MAP = new Dictionary<string, string> ( ) { 
            {SYSTEM_CONFIG_KEYS.PRINT_TYPE,"是否打印机出投注单"},
            {SYSTEM_CONFIG_KEYS.PRINTER_MODEL,"打印方式"},
            {SYSTEM_CONFIG_KEYS.PROVINCES_CODE,"选择的省市编码"},
            {SYSTEM_CONFIG_KEYS.SERVER_TYPE,"服务器类型"},
            {SYSTEM_CONFIG_KEYS.IS_AUTO_FEEDBACK,"是否自动反馈"},
            {SYSTEM_CONFIG_KEYS.PAGE_SIZE,"分页默认大小"},
            {SYSTEM_CONFIG_KEYS.DATA_KEEP_TIME,"数据保存时间"},
            {SYSTEM_CONFIG_KEYS.CONTROLDATA_UPDATE_DATE,"控制数据更新时间"},
            {SYSTEM_CONFIG_KEYS.LAST_LOGIN_NAME,"最后登陆的用户名"},
            {SYSTEM_CONFIG_KEYS.AUDIO_ORDER,"来单提示音"},
            {SYSTEM_CONFIG_KEYS.AUDIO_ERROR,"错漏票提示音"},
            {SYSTEM_CONFIG_KEYS.AUDIO_MANUAL,"有手工单提示音"},
            {SYSTEM_CONFIG_KEYS.AUDIO_FBACK,"需手工反馈提示音"}
        };
    }
}
