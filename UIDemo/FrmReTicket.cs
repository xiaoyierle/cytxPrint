﻿using Maticsoft.BLL.log;
using Maticsoft.BLL.proController;
using Maticsoft.BLL.ScanPortImage;
using Maticsoft.Common;
using Maticsoft.Common.dencrypt;
using Maticsoft.Common.model;
using Maticsoft.Common.model.httpmodel;
using Maticsoft.Common.Util;
using Maticsoft.Controller;
using Maticsoft.Controller.Scheduler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace Demo
{
    public partial class FrmReTicket : Form
    {
        lottery_ticket lt = null;
        public FrmReTicket(lottery_ticket lticket)
        {
            InitializeComponent();
            lt = lticket;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = this.btnReTicket.BackgroundImage = global::Demo.Properties.Resources.btnPrintFocused;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = this.btnReTicket.BackgroundImage = global::Demo.Properties.Resources.btnPrintUnfocused;
        }

        private void FrmReTicket_Load(object sender, EventArgs e)
        {
            //初始化串口下拉框
            foreach (Scheduler sischeduler in Scheduler.SerialInteriorSchedulerList)
            {
                ComboboxItem item = new ComboboxItem ( sischeduler, SPImageGlobal.IS_PRINT_SCAN_IMAGE ? ( sischeduler.SPINFO.SLIP_PRINTER.M_CONNECTION_WAY + "*****" + sischeduler.SPINFO.SLIP_PRINTER.M_NAME ) :
                ( sischeduler.SPINFO.MacInfo.com_name + "***" + sischeduler.SPINFO.MacInfo.machine_name ) );
                this.cBoxCOM.Items.Add(item);
            }

            if (this.cBoxCOM.Items.Count > 0)
            {
                this.cBoxCOM.SelectedIndex = 0;
            }
        }


        /// <summary>
        /// 重新出票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReTicket_Click(object sender, EventArgs e)
        {
            this.tBox_reTicketMsg.Text = "";//清空显示区
            Scheduler sischeduler = (Scheduler)((ComboboxItem)this.cBoxCOM.SelectedItem).Key;
            if ( Global.IS_WORKING )//首页是开始出票状态
            {
                MsgBox.getInstance ( ).Show ( "为保证出单正常,请先到首页停止出单!" );
                this.btnReTicket.Enabled = true;
                return;
            }

            this.btnReTicket.Enabled = false;
            if (SPImageGlobal.IS_PRINT_SCAN_IMAGE)//打印投注单
            {
                if ( sischeduler.SPINFO.SLIP_PRINTER.M_OBJID == 0 || sischeduler.SPINFO.SLIP_PRINTER.M_STATE == 3 )
                {//打开失败
                    MsgBox.getInstance ( ).Show ( "打印机设备连接失败!" );
                }
                else
                {
                    //先判断单子是否能出
                    lottery_ticket ltcopy = this.lt.copy ( );
                    ltcopy.bet_code = DESEncrypt.Decrypt ( ltcopy.bet_code, GlobalConstants.KEY );
                    List<Bitmap> bmplist = new List<Bitmap> ( );

                    if ( ScanPortImageUtil.slipIsSupport ( ltcopy ) )
                    {
                        List<lottery_ticket> ltlist = ScanPortImageUtil.splitLotteryTicket ( ltcopy );
                        for ( int i = 1; i <= ltlist.Count; i++ )
                        {
                            bmplist.Add ( ScanPortImageUtil.creatScanPortImage ( ltlist [ i - 1 ], i.ToString ( ) + "/" + ltlist.Count ) );
                        }
                    }
                    else
                    {
                        //打印出投注内容，方便店主直接手敲
                        bmplist.Add ( ScanPortImageUtil.creatScanPortImage02 ( ltcopy ) );
                    }

                    int round = 1;
                    bool issucc = true;
                    foreach ( Bitmap item in bmplist )
                    {
                        //检查打印机状态
                        if ( SPImageGlobal.CON_QueryStatus ( sischeduler.SPINFO.SLIP_PRINTER.M_OBJID ) == 3 )
                        {
                            issucc = false;
                        }
                        else
                        {
                            item.Save ( "testprint02.bmp", ImageFormat.Bmp );
                            int i = SPImageGlobal.CON_PrintFile ( sischeduler.SPINFO.SLIP_PRINTER.M_OBJID, "testprint02.bmp" );
                            int ii = SPImageGlobal.ASCII_CtrlCutPaper ( sischeduler.SPINFO.SLIP_PRINTER.M_OBJID, 66, 50 );
                            int tryCnt = 3;//最多试3次
                            while ( tryCnt > 0 )
                            {
                                if ( SPImageGlobal.CON_PageSend ( sischeduler.SPINFO.SLIP_PRINTER.M_OBJID ) != 0 )
                                {
                                    this.tBox_reTicketMsg.Text += "第" + ( 4 - tryCnt ) + "次尝试成功!" + sischeduler.SPINFO.SLIP_PRINTER.M_STATE + Environment.NewLine;
                                    break;
                                }
                                else
                                {
                                    this.tBox_reTicketMsg.Text += "第" + ( 4 - tryCnt ) + "次尝试失败!" + sischeduler.SPINFO.SLIP_PRINTER.M_STATE + Environment.NewLine;
                                }
                                tryCnt--;
                                issucc = tryCnt != 0;
                            }

                            if ( issucc )
                            {
                                round++;
                                Thread.Sleep ( 10 );
                            }
                            else//已经出错了，直接退出
                            {
                                break;
                            }
                        }
                    }
                    //无返馈出票结果
                    this.tBox_reTicketMsg.Text += "重新打印投注单" + ( issucc ? "成功" : "失败" ) + sischeduler.SPINFO.SLIP_PRINTER.M_STATE + Environment.NewLine;
                    MsgBox.getInstance ( ).Show ( "已重新发送投注单打印数据,请确认是否已成功打印!" );
                }
                this.btnReTicket.Enabled = true;
            }
            else
            {
                try
                {
                    this.tBox_reTicketMsg.Text += "*****开始检测串口信息*****" + Environment.NewLine;
                    if (sischeduler.SPINFO.Sp.IsOpen )
                    {
                        sischeduler.SPINFO.Ticket = this.lt;

                        this.tBox_reTicketMsg.Text += "*****串口信息可用*****" + Environment.NewLine;
                        this.tBox_reTicketMsg.Text += "*****向彩机传输出票数据*****" + Environment.NewLine;
                        Thread.Sleep ( 50 );
                        BaseProController bpc = new BaseProController ( sischeduler.SPINFO );
                        bpc.ReTicketProcessHandler ( );
                        MsgBox.getInstance ( ).Show ( "已成功发送数据,请确认是否已经出票!" );
                    }
                    else
                    {
                        this.tBox_reTicketMsg.Text += "*****串口信息不可用*****" + Environment.NewLine;
                    }
                }
                catch (Exception)
                {
                    //串口已经打开
                    this.tBox_reTicketMsg.Text += "*****操作出现错误*****" + Environment.NewLine;
                }
                finally
                {
                    this.btnReTicket.Enabled = true;
                }
            }            
        }

        /// <summary>
        /// 数字间隔初始化
        /// </summary>
        /// <param name="s"></param>
        private  bool digitalIntervalInit(SerialPortInfo spinfo)
        {
            bool result = true;
            if (Global.SLC_DICTIONARY.ContainsKey(spinfo.MacInfo.speed_level.ToString()))
            {
                byte[] sendcmdF0 = new byte[256], sendcmdF1 = new byte[256];
                int sendcmdlength = 0;
                String Intervals = Global.SLC_DICTIONARY[spinfo.MacInfo.speed_level.ToString()].digital_interval.ToString("X2");

                byte[] startCommandF0 = CommandProcessor.HexDataToCommand(new String[] { "F0", Intervals });
                Array.Copy(startCommandF0, 0, sendcmdF0, sendcmdlength, startCommandF0.Length);

                sendcmdlength += 2;
                sendcmdF0 = CommandProcessor.packCommand(sendcmdF0, GlobalConstants.cmdSign_KV[GlobalConstants.BASE_CMD.KEYBOARD], sendcmdlength);

                String cmd = CommandProcessor.bytesToHexString(sendcmdF0);
                result = SerialPortUtil.writeData(spinfo.Sp, sendcmdF0, sendcmdlength + 10);
                if (result)
                {
                    sendcmdlength = 0;
                    byte[] startCommandF1 = CommandProcessor.HexDataToCommand(new String[] { "F1", Intervals });
                    Array.Copy(startCommandF1, 0, sendcmdF1, sendcmdlength, startCommandF1.Length);

                    sendcmdlength += 2;
                    sendcmdF1 = CommandProcessor.packCommand(sendcmdF1, GlobalConstants.cmdSign_KV[GlobalConstants.BASE_CMD.KEYBOARD], sendcmdlength);
                    cmd = CommandProcessor.bytesToHexString(sendcmdF1);
                    result = SerialPortUtil.writeData(spinfo.Sp, sendcmdF1, sendcmdlength + 10);
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
