﻿using Maticsoft.BLL.ScanPortImage.imageStructure;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Maticsoft.BLL.ScanPortImage
{
    public class SPImageGlobal
    {
        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_GetSDKVersion(byte[] ListFilePath);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_GetSupportPrinters(byte[] lpPrinters, Int32 len);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_ConnectDevices(string prtName, String port, int timeout);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_CloseDevices(int objCode);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_QueryStatus(int objCode);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_PageStart(int objCode, bool graphicMode, int width, int height);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_PageEnd(int objCode, int tm);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 ASCII_CtrlBlackMark(int objCode);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 ASCII_PrintText(int objCode, string szText);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 ASCII_Print1DBarcode(int objCode, int bcType, int iWidth, int iHeight, int hri, string strData);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 ASCII_Print2DBarcode(int objCode, int type2D, string strPrint, int version, int ecc, int size);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 ASCII_CtrlCutPaper(int objCode, int cutWay, int postion);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_PageSend(int objCode);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_PrintFile(int objCode, string szPath);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_QueryPrinterFirmware(int objCode, byte[] szFirmware, int len);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_StartRecord(string path);

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_EndRecord();

        [DllImport("PrintSDK.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CON_PrintBMPBuffer(int objCode, int width, int height,byte[] buffer);


        //用于打印首页通道切换时打印机型号、工作端口、打印机状态等文本的设置
        public delegate void AlterPrinting();
        public static AlterPrinting alterPrinting = null;

        private static Boolean isPrintScanImage = true;

        public static Boolean IS_PRINT_SCAN_IMAGE
        {
            get { return SPImageGlobal.isPrintScanImage; }
            set
            {
                SPImageGlobal.isPrintScanImage = value;

                if (alterPrinting != null)
                {
                    alterPrinting();
                }
            }
        }
        

        /// <summary>
        /// //开始标识的起点X坐标
        /// </summary>
        public const int START_POINT_X = 5;        

        /// <summary>
        /// //开始标识的起点Y坐标
        /// </summary>
        public const int START_POINT_Y = 100;
        /// <summary>
        /// //大黑块的高度
        /// </summary>
        public const int BIG_BB_HIGH = 9;
        /// <summary>
        /// //小黑块的高度
        /// </summary>
        public const int PLAY_SMALL_BB_HIGH = 8;
        /// <summary>
        /// //小黑块的高度
        /// </summary>
        public const int SMALL_BB_HIGH = 11;
        /// <summary>
        /// //黑块的宽度——大小黑块的宽度一致
        /// </summary>
        public const int BB_WIDTH = 30;
        /// <summary>
        /// //两个黑块起点的高度差——是指起点的高度差，不是中间的空白
        /// </summary>
        public const int BB_HIGH = 42;

        /// <summary>
        /// //最左边的小横杠的起点的X值
        /// </summary>
        public const int LEFT_SMALL_BB_X = 45;

        /// <summary>
        /// //赛事块的宽度
        /// </summary>
        public const int RACE_BLOCK_WIDTH = 170;
        /// <summary>
        /// //两个小块之间的宽度间隔——同一排两个相邻的小块的起始点的距离
        /// </summary>
        public const int S2S_WIDTH = 42;

        /// <summary>
        /// //大块和小块之间的高度间隔
        /// </summary>
        public const int B2S_HIGH = 18;

        /****************北单********************************/
        /// <summary>
        /// //开始标识的起点X坐标——北单
        /// </summary>
        public const int START_POINT_X_BD = 54;

        /// <summary>
        /// 北单——头部两个黑块起始点之间的距离
        /// </summary>
        public const int BD_HEAD_BB_WIDTH = 44;

        /// <summary>
        /// 北单—比分—头部两个黑块起始点之间的距离
        /// </summary>
        public const int BDBF_HEAD_BB_WIDTH = 53;

        /// <summary>
        /// 北单——两个黑块之间的高度
        /// </summary>
        public const int BD_BB_HIGH = 48;

        /// <summary>
        /// 北单——两个黑块之间的高度
        /// </summary>
        public const int BD_BB_HIGH_SHORT = 61;

        /************************************************/

        /*******************双色球开始开始*****************************/

        /*******************双色球开始结束*****************************/

        /*******************江西双色球开始开始*****************************/

        /*******************江西双色球开始结束*****************************/

        /// <summary>
        /// 各种票面对应的玩法字符串
        /// </summary>
        public static Dictionary<String, String[]> PTYPE_HEAD_DESC_DICTIONARY = new Dictionary<string, string[]>() {
            {"JCLQ_SF_3G",new String[]{"0111110011100"}},//竞彩篮球-胜负-3关
            {"JCLQ_SF_6G",new String[]{"0011110011010"}},//竞彩篮球-胜负-6关
            {"JCLQ_SF_8G",new String[]{"1000101011110"}},//竞彩篮球-胜负-8关
            {"JCLQ_RFSF_3G",new String[]{"1111110011100"}},//竞彩篮球-让分胜负-3关
            {"JCLQ_RFSF_6G",new String[]{"1011110011010"}},//竞彩篮球-让分胜负-6关
            {"JCLQ_RFSF_8G",new String[]{"0100101011110"}},//竞彩篮球-让分胜负-8关
            {"JCLQ_DXF_3G",new String[]{"1100110011100"}},//竞彩篮球-大小分-3关
            {"JCLQ_DXF_6G",new String[]{"1000010011010"}},//竞彩篮球-大小分-6关
            {"JCLQ_DXF_8G",new String[]{"1100101011110"}},//竞彩篮球-大小分-8关
            {"JCLQ_SFC_3G",new String[]{"0001100100010"}},//竞彩篮球-胜分差-3关
            {"JCLQ_SFC_6G",new String[]{""}},//竞彩篮球-胜分差-6关
            {"JCLQ_SFC_8G",new String[]{""}},//竞彩篮球-胜分差-8关
            {"JCLQ_HHGG_3G",new String[]{"1010101011010","0010000000000"}},//竞彩篮球-混合过关-3关
            {"JCLQ_HHGG_6G",new String[]{"1010101001110","1010000000000"}},//竞彩篮球-混合过关-6关
            {"JCLQ_HHGG_8G",new String[]{"1010101011001","0110000000000"}},//竞彩篮球-混合过关-8关

            {"JCZQ_SPF_3G",new String[]{"1000110011100"}},//竞彩足球-胜平负-3关
            {"JCZQ_SPF_6G",new String[]{"0101110011010"}},//竞彩足球-胜平负-6关
            {"JCZQ_SPF_8G",new String[]{"0000101011110"}},//竞彩足球-胜平负-8关
            {"JCZQ_ZJQ_3G",new String[]{"1100100111100"}},//竞彩足球-总进球-3关
            {"JCZQ_ZJQ_6G",new String[]{"0001110000110"}},//竞彩足球-总进球-6关
            {"JCZQ_ZJQ_8G",new String[]{""}},//竞彩足球-总进球-8关(最多6关)
            {"JCZQ_BQC_3G",new String[]{"0100110000010"}},//竞彩足球-半全场-3关
            {"JCZQ_BQC_6G",new String[]{"1110110010110"}},//竞彩足球-半全场-6关
            {"JCZQ_BQC_8G",new String[]{""}},//竞彩足球-半全场-8关(最多4关)
            {"JCZQ_BF_3G",new String[]{"0100001101010"}},//竞彩足球-比分-3关
            {"JCZQ_BF_6G",new String[]{"0110110001001"}},//竞彩足球-比分-6关
            {"JCZQ_BF_8G",new String[]{""}},//竞彩足球-比分-8关(最多4关)
            {"JCZQ_HHGG_3G",new String[]{"1010101011110","0001000000000"}},//竞彩足球-混合过关-3关
            {"JCZQ_HHGG_6G",new String[]{"1010101011110","1001000000000"}},//竞彩足球-混合过关-6关
            {"JCZQ_HHGG_8G",new String[]{"1010101110001","0101000000000"}},//竞彩足球-混合过关-8关

            {"ECUP_GJ_GYJ",new String[]{"0000101111100"}},//欧洲杯冠军-冠亚军
            {"SSQ",new String[]{""}},//双色球

            {"BD_DC_SPF",new String[]{"000000000000000000011001000"}},//北单——单场胜平负
            {"BD_SX_DS",new String[]{"000000000011010010"}},//北单——上下单双
            {"BD_ZJQ",new String[]{"000000000011100110"}},//北单——总进球
            {"BD_BQC_SPF",new String[]{"000000000011110000"}},//北单——半全场
            {"BD_BF",new String[]{"000000000011111010"}},//北单——比分


            {"R9_14C",new String[]{"0011100010010"}},//任九十四场
            {"14C_5Z",new String[]{"0011110100001"}},//十四场——5注
            {"4CJQ",new String[]{"0110000101100"}},//四场进球
            {"6CBQC",new String[]{"0110000000010"}},//六场半全场
            {"DLT_PT",new String[]{"1110111000001"}},//大乐透
            {"DLT_DT",new String[]{"0110001000010"}},//大乐透_胆拖
            {"PL3_PL5",new String[]{"1001000010110"}},//排列3排列5
            {"QXC",new String[]{"1011010001001"}},//七星彩
            {"11X5_RX",new String[]{"0010111000010"}},//11选5任选
            {"11X5_QX",new String[]{"0010101111110"}},//11选5前选
        };


        /// <summary>
        /// 过关方式
        /// </summary>
        public static Dictionary<int, String[][]> GGFS_STR_ARRAY = new Dictionary<int, string[][]>() { 
        {3,new String[][]{new String[]{"2c1","3c1","3c3","3c4","0c0","0c0","0c0","1c1"}}},
        {6,new String[][]{new String[]{"4c1","4c4","4c5","4c6","4c11","0c0","0c0","1c1"},
            new String[]{"5c1","5c5","5c6","5c10","5c16","5c20","5c26","0c0"},
            new String[]{"6c1","6c6","6c7","6c15","6c20","6c22","6c35","6c42"},
            new String[]{"6c50","6c57","0c0","0c0","0c0","0c0","0c0","0c0"}}},
        {8,new String[][]{new String[]{"7c1","7c7","7c8","7c21","7c35","7c120","0c0","1c1"},
            new String[]{"8c1","8c8","8c9","8c28","8c56","8c70","8c247"}}}
        };

        /// <summary>
        /// 过关方式——北单
        /// </summary>
        public static Dictionary<String, String[][]> GGFS_STR_ARRAY_BD = new Dictionary<String, string[][]>() { 
        {"BD_DC_SPF",new String[][]{
            new String[]{"0c0","0c0","1c1","两关","三关","2c1"},
        new String[]{"2c3","3c1","3c4","3c7","4c1","4c5"},
        new String[]{"4c11","4c15","5c1","5c6","5c16","5c26"},
        new String[]{"5c31","6c1","6c7","6c22","6c42","6c57"},
        new String[]{"6c63","7c1","8c1","9c4","10c1","11c1"},
        new String[]{"12c1","13c1","14c1","15c1","0c0","0c0"}}},
        {"BD_BQC_SPF",new String[][]{
            new String[]{"0c0","0c0","0c0","0c0","1c1","两关","三关","2c1","2c3"},
            new String[]{"3c1","3c4","3c7","4c1","4c5","4c11","4c15","5c1","5c6"},
            new String[]{"5c16","5c26","5c31","6c1","6c7","6c22","6c42","6c57","6c63"}}},
        {"BD_ZJQ",new String[][]{
            new String[]{"0c0","0c0","0c0","0c0","1c1","两关","三关","2c1","2c3"},
            new String[]{"3c1","3c4","3c7","4c1","4c5","4c11","4c15","5c1","5c6"},
            new String[]{"5c16","5c26","5c31","6c1","6c7","6c22","6c42","6c57","6c63"}}},
        {"BD_BF",new String[][]{
            new String[]{"1c1","2c1","2c3"},
            new String[]{"3c1","3c4","3c7"},
            new String[]{"两关"}}},
        {"BD_SX_DS",new String[][]{
            new String[]{"0c0","0c0","0c0","0c0","1c1","两关","三关","2c1","2c3"},
            new String[]{"3c1","3c4","3c7","4c1","4c5","4c11","4c15","5c1","5c6"},
            new String[]{"5c16","5c26","5c31","6c1","6c7","6c22","6c42","6c57","6c63"}}}
        };

        /// <summary>
        /// 采种-玩法-串关   对应的票面结构
        /// </summary>
        public static Dictionary<String, SPImageStructure> SPISTRUCTURE_DICTIONARY = new Dictionary<string, SPImageStructure>() {
            //竞彩篮球
            {"10_1_3",new JCStructure("JCLQ","SF",3)},
            {"10_1_6",new JCStructure("JCLQ","SF",6)},
            {"10_1_8",new JCStructure("JCLQ","SF",8)},  
            {"10_2_3",new JCStructure("JCLQ","RFSF",3)},
            {"10_2_6",new JCStructure("JCLQ","RFSF",6)},
            {"10_2_8",new JCStructure("JCLQ","RFSF",8)},
            {"10_4_3",new JCStructure("JCLQ","DXF",3)},
            {"10_4_6",new JCStructure("JCLQ","DXF",6)},
            {"10_4_8",new JCStructure("JCLQ","DXF",8)},
            {"10_3_3",new JCStructure("JCLQ","SFC",3)},
            {"10_3_6",new JCStructure("JCLQ","SFC",6)},
            {"10_3_8",new JCStructure("JCLQ","SFC",8)},
            {"10_6_3",new JCStructure("JCLQ","HHGG",3)},
            {"10_6_6",new JCStructure("JCLQ","HHGG",6)},
            {"10_6_8",new JCStructure("JCLQ","HHGG",8)},

            //竞彩足球
            {"9_1_3",new JCStructure("JCZQ","SPF",3)},
            {"9_1_6",new JCStructure("JCZQ","SPF",6)},
            {"9_1_8",new JCStructure("JCZQ","SPF",8)},  
            {"9_3_3",new JCStructure("JCZQ","ZJQ",3)},
            {"9_3_6",new JCStructure("JCZQ","ZJQ",6)},
            {"9_3_8",new JCStructure("JCZQ","ZJQ",8)},
            {"9_4_3",new JCStructure("JCZQ","BQC",3)},
            {"9_4_6",new JCStructure("JCZQ","BQC",6)},
            {"9_4_8",new JCStructure("JCZQ","BQC",8)},
            {"9_5_3",new JCStructure("JCZQ","BF",3)},
            {"9_5_6",new JCStructure("JCZQ","BF",6)},
            {"9_5_8",new JCStructure("JCZQ","BF",8)},
            {"9_6_3",new JCStructure("JCZQ","HHGG",3)},
            {"9_6_6",new JCStructure("JCZQ","HHGG",6)},
            {"9_6_8",new JCStructure("JCZQ","HHGG",8)},

            {"9_10_11",new ECUP_GJ_GYJ_Structure("ECUP_GJ_GYJ")},

            {"21_2_1",new BD_SPF_Structure("BD_DC_SPF")},//北单——单场胜平负
            {"21_6_1",new BD_SXPDS_Structure("BD_SX_DS")},//北单——上下单双
            {"21_5_1",new BD_ZJQ_Structure("BD_ZJQ")},//北单——总进球
            {"21_4_1",new BD_BQC_Structure("BD_BQC_SPF")},//北单——半全场
            {"21_3_1",new BD_BF_Structure("BD_BF")},//北单——比分


            {"5_6_1",new R914CSstructure("R9_14C")},//任九14场
            {"5_14c_5",new SFC_5Z_Structure("14C_5Z")},//14场——5注
            {"8_1_2",new SCJQStructure("4CJQ")},//四场进球
            {"7_1_2",new LCBQCStructure("6CBQC")},//六场半全场
            {"4_1_2",new DLT_DS_FS_Structure("DLT_PT")},//大乐透
            {"4_0_3",new DLT_DT_Structure("DLT_DT")},//大乐透
            {"2_1_2",new PL5Structure("PL3_PL5")},//排列3排列5
            {"3_1_2",new QXCStructure("QXC")},//七星彩
            
            {"11_1_2",new SSQStructure("SSQ")},//双色球

            {"100_RX",new SYX5Structure("11X5_RX")},//11选5任选
            {"100_QX",new SYX5Structure("11X5_QX")},//11选5前选
        };
    }
}
