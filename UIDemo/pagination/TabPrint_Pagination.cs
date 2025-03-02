﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Maticsoft.Common;
using Maticsoft.Common.model;
using Maticsoft.Common.Util;
using Maticsoft.Controller;
using Maticsoft.demo.pagination;
using Maticsoft.BLL.log;

namespace Demo.pagination
{
    public class TabPrint_Pagination : BasePagination<lottery_ticket>
    {
        public PrintTicketController etc = new PrintTicketController();

        private ItemOrder iOrder;
        public ItemOrder IOrder
        {
            get { return iOrder; }
            set
            {
                try
                {
                    if (null == iOrder && null == value)
                    {//初始化生成的时候会执行
                        return;
                    }
                    else
                    {
                        this.iOrder = value;
                        if (null != value)
                        {
                            this.PageNo = 0;
                            //切换新的订单
                            ThreadPool.QueueUserWorkItem(new WaitCallback(initFormDataList));
                        }
                        else
                        {
                            for (int i = 0; i < int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]); i++)
                            {
                                ((ItemTicket)this.Controlslist.ControlList.Controls[i]).LotteryTicket = null;
                            }

                            this.TotalDataCount = 0;
                            this.PageNo = 0;

                            //清空票列表的数据
                            if (this.DataList != null)
                            {
                                this.DataList.Clear();
                            }

                            this.ModulePaging.InitItem(this.getMaxPageNo(), this.getShowPageNO());
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }                
            }
        }

        public TabPrint_Pagination(Int64 pno, Int64 psize, ItemOrder io, ControlsList clist, ModulePagingNEW mPaging)
            : base(pno, psize, clist, mPaging)
        {
            this.IOrder = io;
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateDataToTickets));
        }

        public override void getPageNumberList()
        {
            try
            {
                if (this.IOrder.LotteryOrder.IsSingle)
                {
                    //加载订单下的票
                    //彩票列表——这里比较特殊，因为重打票订单和错漏票订单都不会显示所有的彩票，
                    //无法判断指定的彩票id是在第几页，需要一次加载到内存中
                    if (this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString()) ||
                            this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString()))
                    {
                        if (null != this.DataList && this.DataList.Count != 0)
                        { return; }
                        List<String> stateList = new List<string>() {
                        GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString(),
                        GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString()};
                        this.TotalDataCount = etc.getSingleTicketsNumByOrderIdAndStates(this.IOrder.LotteryOrder.id.ToString(), stateList);
                        this.DataList = etc.getSingleTicketsByOrderIdAndStatesPagination(this.IOrder.LotteryOrder.id.ToString(), stateList, this.getStartPageNo(), this.TotalDataCount);
                    }
                    else if (this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString()) ||
                        this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString()) ||
                        this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString()))
                    {
                        if (null != this.DataList && this.DataList.Count != 0)
                        { return; }
                        List<String> stateList = new List<string>() {
                    GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString(),
                    GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString(),
                    GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString()};
                        this.TotalDataCount = etc.getSingleTicketsNumByOrderIdAndStates(this.IOrder.LotteryOrder.id.ToString(), stateList);
                        this.DataList = etc.getSingleTicketsByOrderIdAndStatesPagination(this.IOrder.LotteryOrder.id.ToString(), stateList, this.getStartPageNo(), this.TotalDataCount);
                    }
                    else
                    {
                        this.TotalDataCount = etc.getSingleTicketsNumByOrderId(this.IOrder.LotteryOrder.id.ToString());
                        this.DataList = etc.getSingleTicketsByOrderIdPagination(this.IOrder.LotteryOrder.id.ToString(), this.getStartPageNo(), this.PageSize);
                    }
                }
                else
                {
                    //加载订单下的票
                    //彩票列表——这里比较特殊，因为重打票订单和错漏票订单都不会显示所有的彩票，
                    //无法判断指定的彩票id是在第几页，需要一次加载到内存中
                    if (this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString()) ||
                            this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString()))
                    {
                        if (null != this.DataList && this.DataList.Count != 0)
                        { return; }
                        List<String> stateList = new List<string>() {
                        GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString(),
                        GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString()};
                        this.TotalDataCount = etc.getTicketsNumByOrderIdAndStates(this.IOrder.LotteryOrder.id.ToString(), stateList);
                        this.DataList = etc.getTicketsByOrderIdAndStatesPagination(this.IOrder.LotteryOrder.id.ToString(), stateList, this.getStartPageNo(), this.TotalDataCount);
                    }
                    else if (this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString()) ||
                        this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString()) ||
                        this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString()))
                    {
                        if (null != this.DataList && this.DataList.Count != 0)
                        { return; }
                        List<String> stateList = new List<string>() {
                    GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString(),
                    GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString(),
                    GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString()};
                        this.TotalDataCount = etc.getTicketsNumByOrderIdAndStates(this.IOrder.LotteryOrder.id.ToString(), stateList);
                        this.DataList = etc.getTicketsByOrderIdAndStatesPagination(this.IOrder.LotteryOrder.id.ToString(), stateList, this.getStartPageNo(), this.TotalDataCount);
                    }
                    else
                    {
                        this.TotalDataCount = etc.getTicketsNumByOrderId(this.IOrder.LotteryOrder.id.ToString());
                        this.DataList = etc.getTicketsByOrderIdPagination(this.IOrder.LotteryOrder.id.ToString(), this.getStartPageNo(), this.PageSize);
                    }
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="state"></param>
        public override void initFormDataList(object state)
        {
            try
            {
                this.getPageNumberList();
                int startNo = (this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString()) ||
                    this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString()) || this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString()) ||
                    this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString()))
                    ? (int)(this.PageNo * int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE])) : 0;

                if (null != this.DataList && this.DataList.Count > 0)
                {
                    for (int i = 0; i < int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]); i++)
                    {
                        ItemTicket etItem = (ItemTicket)this.Controlslist.ControlList.Controls[i];
                        if (i < this.DataList.Count)
                        {
                            etItem.LotteryTicket = this.DataList[i + startNo];
                        }
                        else
                        {
                            etItem.LotteryTicket = null;
                        }
                    }
                }
                this.Controlslist.refreshLocation();
                this.ModulePaging.InitItem(this.getMaxPageNo(), this.getShowPageNO());
            }
            catch (Exception e)
            {
                LogUtil.getInstance().addLogDataToQueue(e.StackTrace, GlobalConstants.LOGTYPE_ENUM.EXCEOTION);
                //throw e;
            }            
        }

        /// <summary>
        /// 更新出票区域的各个票的出票状态
        /// </summary>
        /// <param name="state"></param>
        private void UpdateDataToTickets(object state)
        {            
            while (true)
            {
                try
                {
                    if (null != this.IOrder && null != this.IOrder.TabPrintControl.sIScheduler)
                    {
                        if (null != this.IOrder.TabPrintControl.sIScheduler.SPINFO.CompeletTicketIdStateQueue && this.IOrder.TabPrintControl.sIScheduler.SPINFO.CompeletTicketIdStateQueue.Count > 0)
                        {
                            KeyValuePair<string, string> dicTemp = this.IOrder.TabPrintControl.sIScheduler.SPINFO.CompeletTicketIdStateQueue.Dequeue();
                            if (!String.IsNullOrEmpty(dicTemp.Key))
                            {
                                this.calculateCurrentPageIndex(dicTemp);
                            }
                        }
                    }
                    else {
                        this.IOrder.TabPrintControl.sIScheduler.SPINFO.CompeletTicketIdStateQueue.Clear();
                    }
                }
                catch{
                }
                
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 计算当前打印的票应该在第几页
        /// </summary>
        private void calculateCurrentPageIndex(KeyValuePair<string, string> idAndState)
        {
            try
            {
                if (null == this.IOrder.LotteryOrder)
                {
                    return;
                }
                

                //票区域是否有ticketId的票
                bool flag = false;
                Int64 tid = -1; //计算的票位置，当临时票id使用

                for (int i = 0; i < int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]); i++)
                {
                    ItemTicket ti = (ItemTicket)this.Controlslist.ControlList.Controls[i];
                    if (null != ti.LotteryTicket && ti.LotteryTicket.ticket_id.ToString().Equals(idAndState.Key))
                    {
                        flag = true;
                        ti.State = idAndState.Value;
                        this.IOrder.COMPLETE_TICKET_NUM++;
                        this.Controlslist.traceThePrintingItem(ti);
                        break;
                    }
                }

                if (!flag)//当前页面中不存在刚打印的票
                {
                    //重新加载
                    if (this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_WAITING_PRINT.ToString()) ||
                    this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_PRINTTING.ToString()) || this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString()) ||
                    this.IOrder.LotteryOrder.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString()))
                    {
                        for (int i = 0; i < this.DataList.Count; i++)
                        {
                            if (idAndState.Key.Equals(this.DataList[i].ticket_id.ToString()))
                            {
                                tid = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Int64.TryParse(idAndState.Key, out tid);
                    }

                    if (tid == -1)//证明现在加载到内存中的票列表中没有这张票
                    {
                        return;
                    }

                    //计算现在应该在第几页
                    this.PageNo = (Int64)Math.Ceiling((double)(tid / this.PageSize));
                    initFormDataList(null);

                    //刷新界面重新赋值，(如果有值——正常情况下都会有值的)然后再改变
                    calculateCurrentPageIndex(idAndState);
                }
            }
            catch (Exception e)
            {
                throw e;
            }            
        }
    }
}
