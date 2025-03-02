﻿using Maticsoft.BLL.proController;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Demo
{
    public partial class FrmReTicketList : Form
    {
        private int startIndex = 0;
        private int endIndex = 0;

        IList<Maticsoft.Common.model.lottery_ticket> ltList = null;
        public FrmReTicketList(IList<Maticsoft.Common.model.lottery_ticket> lticketList)
        {
            InitializeComponent();
            ltList = lticketList;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.btnPrintFocused;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.btnPrintUnfocused;
        }

        private void FrmReTicketList_Load(object sender, EventArgs e)
        {
            //初始化串口下拉框
            foreach (Maticsoft.Controller.Scheduler.Scheduler sischeduler in Maticsoft.Controller.Scheduler.Scheduler.SerialInteriorSchedulerList)
            {
                Maticsoft.Common.model.ComboboxItem item = new Maticsoft.Common.model.ComboboxItem ( sischeduler, Maticsoft.BLL.ScanPortImage.SPImageGlobal.IS_PRINT_SCAN_IMAGE ? ( sischeduler.SPINFO.SLIP_PRINTER.M_CONNECTION_WAY + "*****" + sischeduler.SPINFO.SLIP_PRINTER.M_NAME ) :
                ( sischeduler.SPINFO.MacInfo.com_name + "***" + sischeduler.SPINFO.MacInfo.machine_name ) );
                this.cBoxCOM.Items.Add(item);
            }

            if (this.cBoxCOM.Items.Count > 0)
            {
                this.cBoxCOM.SelectedIndex = 0;
            }

            this.tBox_reTicketMsg.Text = "";//清空显示区
            this.tBox_reTicketMsg.Text += String.Format("*****正在打印第{0}至第{1}票*****", this.startIndex, this.endIndex) + Environment.NewLine;
        }

        /// <summary>
        /// 重新出票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReTicket_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(reprint));
        }

        private void reprint(object state)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(delegate(object o, EventArgs e2)
                {
                    reprint02();
                }));
            }
            else
            {
                reprint02();
            }
        }


        private void reprint02()
        {
            Maticsoft.Controller.Scheduler.Scheduler sischeduler = (Maticsoft.Controller.Scheduler.Scheduler)((Maticsoft.Common.model.ComboboxItem)this.cBoxCOM.SelectedItem).Key;
            this.btnReTicket.Enabled = false;
            if ( Global.IS_WORKING )
            {
                MsgBox.getInstance ( ).Show ( "为保证出单正常,请先到首页停止出单!" );
                this.btnReTicket.Enabled = true;
                return;
            }

            if (Maticsoft.BLL.ScanPortImage.SPImageGlobal.IS_PRINT_SCAN_IMAGE)//打印投注单
            {
                this.tBox_reTicketMsg.Text += "*****打开打印机设备成功*****" + Environment.NewLine;
                foreach ( Maticsoft.Common.model.lottery_ticket lt in this.ltList )
                {
                    //先判断单子是否能出
                    Maticsoft.Common.model.lottery_ticket ltcopy = lt.copy ( );
                    if ( lt.order_id > 0 )
                    {
                        ltcopy.bet_code = Maticsoft.Common.dencrypt.DESEncrypt.Decrypt ( ltcopy.bet_code, Maticsoft.Common.Util.GlobalConstants.KEY );
                    }

                    if ( Maticsoft.BLL.ScanPortImage.ScanPortImageUtil.slipIsSupport ( ltcopy ) )
                    {
                        List<Maticsoft.Common.model.lottery_ticket> ltlist = Maticsoft.BLL.ScanPortImage.ScanPortImageUtil.splitLotteryTicket ( ltcopy );
                        int round = 1;
                        bool issucc = true;
                        foreach ( Maticsoft.Common.model.lottery_ticket item in ltlist )
                        {
                            //检查打印机状态
                            sischeduler.SPINFO.SLIP_PRINTER.M_STATE = Maticsoft.BLL.ScanPortImage.SPImageGlobal.CON_QueryStatus ( sischeduler.SPINFO.SLIP_PRINTER.M_OBJID );
                            if ( sischeduler.SPINFO.SLIP_PRINTER.M_STATE == 3 )
                            {
                                issucc = false;
                            }
                            else
                            {
                                Bitmap bt = Maticsoft.BLL.ScanPortImage.ScanPortImageUtil.creatScanPortImage ( item, round.ToString ( ) + "/" + ltlist.Count );
                                if ( null == bt )//投注单不支持
                                {

                                }
                                else
                                {
                                    bt.Save ( "testprint02.bmp", System.Drawing.Imaging.ImageFormat.Bmp );
                                    int i = Maticsoft.BLL.ScanPortImage.SPImageGlobal.CON_PrintFile ( sischeduler.SPINFO.SLIP_PRINTER.M_OBJID, "testprint02.bmp" );
                                    int ii = Maticsoft.BLL.ScanPortImage.SPImageGlobal.ASCII_CtrlCutPaper ( sischeduler.SPINFO.SLIP_PRINTER.M_OBJID, 66, 50 );
                                    int tryCnt = 3;//最多试3次
                                    while ( tryCnt > 0 )
                                    {
                                        if ( Maticsoft.BLL.ScanPortImage.SPImageGlobal.CON_PageSend ( sischeduler.SPINFO.SLIP_PRINTER.M_OBJID ) != 0 )
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
                    }
                    else
                    {
                        this.tBox_reTicketMsg.Text += "投注单不支持该票面内容,请手工打票!" + sischeduler.SPINFO.SLIP_PRINTER.M_STATE + Environment.NewLine;
                    }
                }
                MsgBox.getInstance ( ).Show ( "已重新发送投注单打印数据,请确认是否已成功打印!" );
                this.btnReTicket.Enabled = true;
            }
            else
            {
                try
                {
                    this.tBox_reTicketMsg.Text += "*****开始检测串口信息*****" + Environment.NewLine;
                    if ( null != sischeduler.SPINFO.Sp )
                    {
                        //检测串口            
                        if ( !sischeduler.SPINFO.Sp.IsOpen )
                        {
                            MsgBox.getInstance ( ).Show ( "串口资源未打开!" );
                        }
                        else
                        {
                            foreach ( Maticsoft.Common.model.lottery_ticket lt in this.ltList )
                            {
                                sischeduler.SPINFO.Ticket = lt;
                                this.tBox_reTicketMsg.Text += string.Format ( "*****向彩机传输出票数据,票号:{0}*****" + Environment.NewLine, lt.ticket_id );
                                Thread.Sleep ( 5000 );
                                BaseProController bpc = new BaseProController ( sischeduler.SPINFO );
                                bpc.ReTicketProcessHandler ( );
                            }
                            MsgBox.getInstance ( ).Show ( "已成功发送数据,请确认是否已经出票!" );
                        }
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
        private  bool digitalIntervalInit(Maticsoft.Common.model.SerialPortInfo spinfo)
        {
            bool result = true;
            if (Maticsoft.Common.Global.SLC_DICTIONARY.ContainsKey(spinfo.MacInfo.speed_level.ToString()))
            {
                byte[] sendcmdF0 = new byte[256], sendcmdF1 = new byte[256];
                int sendcmdlength = 0;
                String Intervals = Maticsoft.Common.Global.SLC_DICTIONARY[spinfo.MacInfo.speed_level.ToString()].digital_interval.ToString("X2");

                byte[] startCommandF0 = Maticsoft.Common.Util.CommandProcessor.HexDataToCommand(new String[] { "F0", Intervals });
                Array.Copy(startCommandF0, 0, sendcmdF0, sendcmdlength, startCommandF0.Length);

                sendcmdlength += 2;
                sendcmdF0 = Maticsoft.Common.Util.CommandProcessor.packCommand(sendcmdF0, Maticsoft.Common.Util.GlobalConstants.cmdSign_KV[Maticsoft.Common.Util.GlobalConstants.BASE_CMD.KEYBOARD], sendcmdlength);

                String cmd = Maticsoft.Common.Util.CommandProcessor.bytesToHexString(sendcmdF0);
                result = Maticsoft.Common.Util.SerialPortUtil.writeData(spinfo.Sp, sendcmdF0, sendcmdlength + 10);
                if (result)
                {
                    sendcmdlength = 0;
                    byte[] startCommandF1 = Maticsoft.Common.Util.CommandProcessor.HexDataToCommand(new String[] { "F1", Intervals });
                    Array.Copy(startCommandF1, 0, sendcmdF1, sendcmdlength, startCommandF1.Length);

                    sendcmdlength += 2;
                    sendcmdF1 = Maticsoft.Common.Util.CommandProcessor.packCommand(sendcmdF1, Maticsoft.Common.Util.GlobalConstants.cmdSign_KV[Maticsoft.Common.Util.GlobalConstants.BASE_CMD.KEYBOARD], sendcmdlength);
                    cmd = Maticsoft.Common.Util.CommandProcessor.bytesToHexString(sendcmdF1);
                    result = Maticsoft.Common.Util.SerialPortUtil.writeData(spinfo.Sp, sendcmdF1, sendcmdlength + 10);
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public int StartIndex
        {
            get { return startIndex; }
            set { startIndex = value; }
        }
        public int EndIndex
        {
            get { return endIndex; }
            set { endIndex = value; }
        }
    }
}
