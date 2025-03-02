﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Maticsoft.Common.model;
using Maticsoft.Controller;
using Maticsoft.Common;
using System.Threading;
using Demo.pagination;
using Maticsoft.Common.Util;

namespace Demo
{
    public partial class TabErrorTicket : UserControl
    {
        private Panel plParent = null;
        TabErrorTicket_Pagination tetp = null;

        public TabErrorTicket(Panel panel)
        {
            InitializeComponent();
            plParent = panel;

            for (int i = 0; i < int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]); i++)
            {
                ItemErrorTicket etItem = new ItemErrorTicket(plParent,null);
                this.controlsList.Add(etItem);
            }
            tetp = new TabErrorTicket_Pagination(0, int.Parse(Global.SYSTEM_CONFIG_MAP [ GlobalConstants.SYSTEM_CONFIG_KEYS.PAGE_SIZE]), this.controlsList, this.modulePagingNEW);
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

        private void controlsList_Load(object sender, EventArgs e)
        {
            this.moduleTitlebar.remind = "当前位置 > 错漏票";
            //往界面加载数据，只执行一次
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.tetp.initFormDataList), this.picBoxWaiting);
        }
    }
}
