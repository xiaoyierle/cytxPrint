﻿using System;
using System.Collections.Generic;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Common.model;
using Maticsoft.Controller;
using Maticsoft.demo.pagination;
using Maticsoft.Common.Util;

namespace Demo.pagination
{
    public class TabOrderRecord_Pagination : BasePagination<lottery_order>
    {
        private RecordController trcontroller = new RecordController();
        String startDateStr = "dateTime('now','start of day')";
        String endDateStr = "dateTime('now','start of day','+1 day')";

        public TabOrderRecord_Pagination(Int64 pno, Int64 psize, int oType, ControlsList clist, ModulePagingNEW mPaging)
            : base(pno, psize, clist, mPaging)
        {
            this.OrderType = oType;
        }

        public override void getPageNumberList()
        {
            try
            {
                if (this.orderType == 1)
                {
                    //加载订单
                    this.TotalDataCount = trcontroller.getAllFeedBackTicketedRecordNum(startDateStr, endDateStr);
                    this.DataList = trcontroller.getAllFeedBackTicketedRecord(startDateStr, endDateStr, this.getStartPageNo().ToString(), this.PageSize.ToString());
                }
                else if (this.orderType == 2)
                {
                    //加载订单
                    this.TotalDataCount = trcontroller.getAllNotFeedBackTicketedRecordNum(startDateStr, endDateStr);
                    this.DataList = trcontroller.getAllNotFeedBackTicketedRecord(startDateStr, endDateStr, this.getStartPageNo().ToString(), this.PageSize.ToString());
                }
                else if (this.orderType == 3)
                {
                    //加载订单
                    this.TotalDataCount = trcontroller.getAllCancelTicketedRecordNum(startDateStr, endDateStr);
                    this.DataList = trcontroller.getAllCancelTicketedRecord(startDateStr, endDateStr, this.getStartPageNo().ToString(), this.PageSize.ToString());
                }
                else
                {
                    //加载订单
                    this.TotalDataCount = trcontroller.getAllOverdueTicketedRecordNum(startDateStr, endDateStr);
                    this.DataList = trcontroller.getAllOverdueTicketedRecord(startDateStr, endDateStr, this.getStartPageNo().ToString(), this.PageSize.ToString());
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
                if (null != this.DataList && this.DataList.Count > 0)
                {
                    for (int i = 0; i < int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]); i++)
                    {
                        ItemOrderRecord etItem = (ItemOrderRecord)this.Controlslist.ControlList.Controls[i];
                        if (i < this.DataList.Count)
                        {
                            etItem.LotteryOrder = this.DataList[i];
                        }
                        else
                        {
                            etItem.LotteryOrder = null;
                        }
                    }
                }
                this.Controlslist.refreshLocation();
                this.ModulePaging.InitItem(this.getMaxPageNo(), this.getShowPageNO());
            }
            catch (Exception e)
            {
                throw e;
            }            
        }


        private int orderType = 1;
        public int OrderType
        {
            get { return orderType; }
            set { orderType = value; }
        }
    }
}
