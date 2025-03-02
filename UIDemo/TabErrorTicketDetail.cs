﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Demo.pagination;
using Maticsoft.Common;
using Maticsoft.Common.model;
using Maticsoft.Common.Util;
using Maticsoft.Controller;
using Maticsoft.BLL.log;

namespace Demo
{
    public partial class TabErrorTicketDetail : UserControl
    {
        private Panel plParent = null;
        private lottery_order lorder = null;

        private TabETDetail_Pagination tetdp;

        public lottery_order Lorder
        {
            get { return lorder; }
            set { lorder = value; }
        }
        ErrorTicketController etcontroller = new ErrorTicketController();

        public TabErrorTicketDetail(Panel p,lottery_order lo)
        {
            InitializeComponent();
            plParent = p;
            this.Lorder = lo;
            tetdp = new TabETDetail_Pagination(0, int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]), this.Lorder, this.controlsList, this.modulePagingNEW);
            for (int i = 0; i < int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]); i++)
            {
                ItemErrorTicketDetail ietd = new ItemErrorTicketDetail("",null);
                this.controlsList.Add(ietd);
            }
            this.controlsList.showPaging = this.ShowPaging;
            this.controlsList.Add(this.modulePagingNEW);
        }

        private void ShowPaging(bool isVisible)
        {
            if (this.modulePagingNEW.Visible != isVisible && this.modulePagingNEW.MaxPage > 1)
            {
                if (this.modulePagingNEW.InvokeRequired)
                {
                    this.modulePagingNEW.Invoke(new EventHandler(delegate(object o, EventArgs e)
                    {
                        this.modulePagingNEW.Visible = isVisible;
                    }));
                }
                else
                {
                    this.modulePagingNEW.Visible = isVisible;
                }
            }
        }

        //返回
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.plParent.Controls.Add(new TabErrorTicket(this.plParent));
            this.plParent.Controls.Remove(this);
        }

        private void CYTXLotteryErrorTicketDetailTab_Load(object sender, EventArgs e)
        {
            this.moduleTitlebar.remind = "当前位置 > 错漏票 > 错漏票详情";
            this.picLicense.BackgroundImage = SysUtil.GetLicenseImg(this.Lorder.license_id.ToString());
            this.lbLicense.Text = SysUtil.licenseNameTranslation(this.Lorder.license_id.ToString());
            this.lbOrderId.Text = Lorder.id.ToString();
            this.lbOrderPrice.Text = String.Format(this.lbOrderPrice.Text, new String[] { Lorder.total_money.ToString(), (Lorder.total_money - Lorder.ticket_money-Lorder.canceled_money).ToString() });

            this.lbTotalErrorTicketNum.Text = String.Format(this.lbTotalErrorTicketNum.Text,new String[]{Lorder.total_tickets_num.ToString(),Lorder.errticket_num.ToString()});

            //初始化线程，只执行一次
            ThreadPool.QueueUserWorkItem(new WaitCallback(PreSetting));
        }

        private void PreSetting(object obj)
        {
            int choseCount = 0;//记录进度
            this.tetdp.initFormDataList(null);
            if (null != tetdp.DataList && tetdp.DataList.Count != 0)
            {
                for (int index = 0; index < tetdp.DataList.Count; index++)
                {
                    if (tetdp.DataList[index].err_ticket_sign != GlobalConstants.TICKET_ERR_SIGN.NO_OPERATION)
                    {
                        choseCount++;
                    }
                }
            }

            this.lbProgress.Invoke(new EventHandler(delegate(object o, EventArgs e)
            {
                this.lbProgress.Text = choseCount + "/" + Lorder.errticket_num;
            }));
            
        }


        private bool isAllRepeat = false;
        private bool isAllCancel = false;
        private bool isAllSure = false;

        public bool IsAllRepeat
        {
            get { return isAllRepeat; }
            set
            {
                isAllRepeat = value;
                if (isAllRepeat)
                {
                    this.picAllRepeat.BackgroundImage = global::Demo.Properties.Resources.qxz;
                    this.IsAllCancel = false;
                    this.IsAllSure = false;
                }
                else
                {
                    this.picAllRepeat.BackgroundImage = global::Demo.Properties.Resources.mxz;
                }
            }
        }
        public bool IsAllCancel
        {
            get { return isAllCancel; }
            set
            {
                isAllCancel = value;
                if (isAllCancel)
                {
                    this.picAllCancel.BackgroundImage = global::Demo.Properties.Resources.qxz;
                    this.IsAllRepeat = false;
                    this.IsAllSure = false;
                }
                else
                {
                    this.picAllCancel.BackgroundImage = global::Demo.Properties.Resources.mxz;
                }
            }
        }
        public bool IsAllSure
        {
            get { return isAllSure; }
            set
            {
                isAllSure = value;
                if (isAllSure)
                {
                    this.picAllSure.BackgroundImage = global::Demo.Properties.Resources.qxz;
                    this.IsAllRepeat = false;
                    this.IsAllCancel = false;
                }
                else
                {
                    this.picAllSure.BackgroundImage = global::Demo.Properties.Resources.mxz;
                }
            }
        }
        /// <summary>
        /// 全部重出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picAllRepeat_Click(object sender, EventArgs e)
        {
            this.IsAllRepeat = !this.IsAllRepeat;
            //for (int i = 0; i < int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]); i++)
            //{
            //    ((ItemErrorTicketDetail)this.controlsList.ControlList.Controls[i]).IsRepeat = this.IsAllRepeat;
            //}
            //try
            //{
            //    etcontroller.saveAllOperatingResult(this.Lorder, GlobalConstants.TICKET_ERR_SIGN.TICKET_AGAIN);
            //    LogUtil.getInstance().addLogDataToQueue("处理错漏票>>>" + Lorder.id.ToString(), GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
            //    MsgBox.getInstance().Show("错漏票已处理完成!", "提示", MsgBox.MyButtons.OK);
            //    this.plParent.Controls.Add(new TabErrorTicket(this.plParent));
            //    this.plParent.Controls.Remove(this);
            //}
            //catch (Exception ce)
            //{
            //    MsgBox.getInstance().Show(ce.Message, "提示", MsgBox.MyButtons.OK);
            //}
        }

        /// <summary>
        /// 全部撤票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picAllCancel_Click(object sender, EventArgs e)
        {
            this.IsAllCancel = !this.IsAllCancel;
            //for (int i = 0; i < int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]); i++)
            //{
            //    ((ItemErrorTicketDetail)this.controlsList.ControlList.Controls[i]).IsCancel = this.IsAllCancel;
            //}
            //try
            //{
            //    etcontroller.saveAllOperatingResult(this.Lorder, GlobalConstants.TICKET_ERR_SIGN.CANCEL);
            //    LogUtil.getInstance().addLogDataToQueue("处理错漏票>>>" + Lorder.id.ToString(), GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
            //    MsgBox.getInstance().Show("错漏票已处理完成!", "提示", MsgBox.MyButtons.OK);
            //    this.plParent.Controls.Add(new TabErrorTicket(this.plParent));
            //    this.plParent.Controls.Remove(this);
            //}
            //catch (Exception ce)
            //{
            //    MsgBox.getInstance().Show(ce.Message, "提示", MsgBox.MyButtons.OK);
            //}
        }

        /// <summary>
        /// 全部确认出票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picAllSure_Click(object sender, EventArgs e)
        {
            this.IsAllSure = !this.IsAllSure;
            //for (int i = 0; i < int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]); i++)
            //{
            //    ((ItemErrorTicketDetail)this.controlsList.ControlList.Controls[i]).IsSure = this.IsAllSure;
            //}
            //try
            //{
            //    etcontroller.saveAllOperatingResult(this.Lorder, GlobalConstants.TICKET_ERR_SIGN.COMPLETE_TICKET);
            //    LogUtil.getInstance().addLogDataToQueue("处理错漏票>>>" + Lorder.id.ToString(), GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
            //    MsgBox.getInstance().Show("错漏票已处理完成!", "提示", MsgBox.MyButtons.OK);
            //    this.plParent.Controls.Add(new TabErrorTicket(this.plParent));
            //    this.plParent.Controls.Remove(this);
            //}
            //catch (Exception ce)
            //{
            //    MsgBox.getInstance().Show(ce.Message, "提示", MsgBox.MyButtons.OK);
            //}
        }


        //点击暂存时处理
        private void scratchbtn_Click(object sender, EventArgs e)
        {
            int choseCount = 0;//记录进度
            Dictionary<String, String> param = new Dictionary<string, string>();
            foreach (Control item in this.controlsList.ControlList.Controls)
            {
                if (item.GetType() == typeof(ItemErrorTicketDetail))
                {
                    ItemErrorTicketDetail it = (ItemErrorTicketDetail)item;

                    if (null != it.Lticket && it.getErrorTicketSign() == GlobalConstants.TICKET_ERR_SIGN.NO_OPERATION)
                    {
                        choseCount++;
                    }

                    param.Add(it.getTicketId(), it.getErrorTicketSign().ToString());
                }
            }

            try {
                etcontroller.stagingOperatingResult(this.Lorder.id.ToString(), param);
                this.lbProgress.Text = choseCount + "/" + Lorder.errticket_num;
                MsgBox.getInstance().Show("已暂存选择到数据库!","暂存结果", MsgBox.MyButtons.OK);
            }catch(Exception ce){
                MsgBox.getInstance().Show(ce.Message, "暂存结果", MsgBox.MyButtons.OK);
            }
            
        }

        //确认保存
        private void sureSavebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsAllCancel)
                {
                    if (MsgBox.getInstance().Show("您是否要进行'整单撤票'？", "提示", MsgBox.MyButtons.OKCancel) == MsgBox.MsgDialogResult.OK)
                    {
                        etcontroller.saveAllOperatingResult(this.Lorder, GlobalConstants.TICKET_ERR_SIGN.CANCEL);
                        LogUtil.getInstance().addLogDataToQueue("处理错漏票>>>" + Lorder.id.ToString(), GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                        this.plParent.Controls.Add(new TabErrorTicket(this.plParent));
                        this.plParent.Controls.Remove(this);
                    }
                    return;
                }
                else if (this.IsAllRepeat)
                {
                    if (MsgBox.getInstance().Show("您是否要进行'整单重打'？", "提示", MsgBox.MyButtons.OKCancel) == MsgBox.MsgDialogResult.OK)
                    {
                        etcontroller.saveAllOperatingResult(this.Lorder, GlobalConstants.TICKET_ERR_SIGN.TICKET_AGAIN);
                        LogUtil.getInstance().addLogDataToQueue("处理错漏票>>>" + Lorder.id.ToString(), GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                        this.plParent.Controls.Add(new TabErrorTicket(this.plParent));
                        this.plParent.Controls.Remove(this);
                    }
                    return;
                }
                else if (this.IsAllSure)
                {
                    if (MsgBox.getInstance().Show("您是否要进行'整单出票'？", "提示", MsgBox.MyButtons.OKCancel) == MsgBox.MsgDialogResult.OK)
                    {
                        etcontroller.saveAllOperatingResult(this.Lorder, GlobalConstants.TICKET_ERR_SIGN.COMPLETE_TICKET);
                        LogUtil.getInstance().addLogDataToQueue("处理错漏票>>>" + Lorder.id.ToString(), GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                        this.plParent.Controls.Add(new TabErrorTicket(this.plParent));
                        this.plParent.Controls.Remove(this);
                    }
                    return;
                }
            }
            catch (Exception ce)
            {
                MsgBox.getInstance().Show(ce.Message, "提示", MsgBox.MyButtons.OK);
            }

            Dictionary<String, String[]> param = new Dictionary<string, string[]>();
            foreach (Control item in this.controlsList.ControlList.Controls)
            {
                if (item.GetType() == typeof(ItemErrorTicketDetail))
                {
                    ItemErrorTicketDetail it = (ItemErrorTicketDetail)item;

                    if (null != it.Lticket)
                    {
                        if (it.getErrorTicketSign() == GlobalConstants.TICKET_ERR_SIGN.NO_OPERATION)
                        {
                            MsgBox.getInstance().Show(it.getTicketId() + "号票未选择处理结果!", "提示", MsgBox.MyButtons.OK);
                            return;
                        }

                        param.Add(it.getTicketId(), new String[] { it.getErrorTicketSign().ToString(), it.getTicketBetMoney() });
                    }
                }
            }

            try
            {
                bool finished = false;
                etcontroller.saveOperatingResult(this.Lorder, param, ref finished);
                LogUtil.getInstance().addLogDataToQueue("处理错漏票>>>"+Lorder.id.ToString(), GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                MsgBox.getInstance().Show("错漏票已处理完成!", "提示", MsgBox.MyButtons.OK);
                this.plParent.Controls.Add(new TabErrorTicket(this.plParent));
                if (finished)
                {
                    this.plParent.Controls.Remove(this);
                }
            }
            catch (Exception ce)
            {
                MsgBox.getInstance().Show(ce.Message, "提示", MsgBox.MyButtons.OK);
            }
        }

        /// <summary>
        /// 跳转到指定页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jumpHandler(object sender, EventArgs e)
        {
            this.tetdp.PageNo = this.modulePagingNEW.NowPage - 1;
            //往界面加载数据，只执行一次
            ThreadPool.QueueUserWorkItem(new WaitCallback(PreSetting));
        }

        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fristPageHandler(object sender, EventArgs e)
        {
            this.tetdp.PageNo = 0;
            //往界面加载数据，只执行一次
            ThreadPool.QueueUserWorkItem(new WaitCallback(PreSetting));
        }
        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lastPageHandler(object sender, EventArgs e)
        {
            this.tetdp.PageNo = this.tetdp.getMaxPageNo() - 1;
            //往界面加载数据，只执行一次
            ThreadPool.QueueUserWorkItem(new WaitCallback(PreSetting));
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextPageHandler(object sender, EventArgs e)
        {
            this.tetdp.PageNo += 1;
            //往界面加载数据，只执行一次
            ThreadPool.QueueUserWorkItem(new WaitCallback(PreSetting));
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousPageHandler(object sender, EventArgs e)
        {
            this.tetdp.PageNo -= 1;
            //往界面加载数据，只执行一次
            ThreadPool.QueueUserWorkItem(new WaitCallback(PreSetting));
        }
    }
}
