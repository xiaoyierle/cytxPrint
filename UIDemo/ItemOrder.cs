﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Maticsoft.Common;
using Maticsoft.Common.model;
using Maticsoft.Common.Util;
using Maticsoft.Controller;
using System.Collections.Generic;
using Maticsoft.BLL.log;

namespace Demo
{
    public partial class ItemOrder : UserControl
    {
        private Point originalLocation = new Point();
        public Point OriginalLocation
        {
            get { return originalLocation; }
            set { originalLocation = value; }
        }

        //当完成数量改变时，需要刷新界面的显示，用来刷新的委托
        public delegate void RefreshProgress();
        private RefreshProgress refreshProgressHandler = null;

        ItemOrder previous = null;
        ItemOrder next = null;

        private long complete_ticket_num;
        public long COMPLETE_TICKET_NUM
        {
            get { return complete_ticket_num; }
            set
            {
                this.complete_ticket_num = value;
                if (null != this.refreshProgressHandler)
                {
                    refreshProgressHandler();
                }
            }
        }
        private long left_time;

        public ItemOrder Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        public ItemOrder Next
        {
            get { return next; }
            set { next = value; }
        }

        public bool isSingle { set; get; }

        public long Left_time
        {
            get { return left_time; }
            set
            {
                left_time = value < 0 ? 0 : value;
                if (this.lb2.InvokeRequired)
                {
                    this.Lb2.Invoke(new EventHandler(delegate(object o, EventArgs e)
                    {
                        this.Lb2.Text = this.left_time == 0 ? "有逾期票" : DateUtil.secondToHHmmss(this.left_time, 1);
                    }));
                }
                else
                {
                    this.Lb2.Text = this.left_time == 0 ? "有逾期票" : DateUtil.secondToHHmmss(this.left_time, 1);
                }

            }
        }

        private bool selected = false;

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (selected)
                {
                    this.BackgroundImage = null;
                    this.BackgroundImage = global::Demo.Properties.Resources.hover_x;
                }
                else
                {
                    this.BackColor = Color.Transparent;
                    this.Pattern(int.Parse(this.lotteryOrder.bet_state));
                }
            }
        }
        //提供给外部调用来修改控件界面的一些属性
        private delegate void ItemOrderBetStateDelegate();//代理
        //提供给外部调用来修改控件界面的一些属性

        public void securityThreadBetState(int bet_state)
        {
            ItemOrderBetStateDelegate scd = delegate
            {
                Pattern(bet_state);
            };
            this.BeginInvoke(scd);
        }

        private readonly PrintTicketController printController = new PrintTicketController();
        private TabPrint tabPrintControl = null;//父控件
        public TabPrint TabPrintControl
        {
            get { return tabPrintControl; }
            set { tabPrintControl = value; }
        }
        public ItemOrder(TabPrint tp, RefreshProgress rPHandler)
        {
            InitializeComponent();
            TabPrintControl = tp;
            this.refreshProgressHandler = rPHandler;

            this.Pic1.Click += new EventHandler(orderItem_Click);
            this.Lb1.Click += new EventHandler(orderItem_Click);
            this.Lb2.Click += new EventHandler(orderItem_Click);
            this.Lb3.Click += new EventHandler(orderItem_Click);
            this.Lb4.Click += new EventHandler(orderItem_Click);
            this.plTicket.Click += new EventHandler(orderItem_Click);
        }
        public ItemOrder()
        {
            InitializeComponent();
        }

        delegate void Del();
        private lottery_order lotteryOrder;

        public lottery_order LotteryOrder
        {
            get { return lotteryOrder; }
            set
            {
                lotteryOrder = value;


                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                    {
                        this.Pic1.BackgroundImage = SysUtil.GetLicenseImg(value.license_id.ToString());

                        //如果是错漏票，在lb1后追加“(错票)”
                        if (value.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR.ToString()) ||
                            value.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString()) ||
                            value.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString()) ||
                            value.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString()) ||
                            value.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString()))
                        {
                            this.Lb4.Text = "(错漏票)";

                        }
                        this.Lb1.Text = value.username;
                        TimeSpan span = (TimeSpan)(Convert.ToDateTime(value.stop_time) - DateTime.Now);
                        this.Left_time = (long)span.TotalSeconds;

                        this.Lb3.Text = value.total_tickets_num.ToString();
                        this.Pattern(int.Parse(value.bet_state));//设置控件为'正在打印'的样式
                        this.COMPLETE_TICKET_NUM = this.lotteryOrder.errticket_num + this.lotteryOrder.canceled_num + this.lotteryOrder.ticket_num + this.LotteryOrder.expired_num;
                    }));
                }
                else
                {
                    this.Pic1.BackgroundImage = SysUtil.GetLicenseImg(value.license_id.ToString());

                    //如果是错漏票，在lb1后追加“(错票)”
                    if (value.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR.ToString()) ||
                        value.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString()) ||
                        value.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString()) ||
                        value.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString()) ||
                        value.bet_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString()))
                    {
                        this.Lb4.Text = "(错漏票)";

                    }
                    this.Lb1.Text = value.username;
                    TimeSpan span = (TimeSpan)(Convert.ToDateTime(value.stop_time) - DateTime.Now);
                    this.Left_time = (long)span.TotalSeconds;

                    this.Lb3.Text = value.total_tickets_num.ToString();
                    this.Pattern(int.Parse(value.bet_state));//设置控件为'正在打印'的样式
                    this.COMPLETE_TICKET_NUM = this.lotteryOrder.errticket_num + this.lotteryOrder.canceled_num + this.lotteryOrder.ticket_num + this.LotteryOrder.expired_num;
                }
            }
        }

        public Label Lb1
        {
            get { return this.lb1; }
        }
        public Label Lb2
        {
            get { return this.lb2; }
        }
        public Label Lb3
        {
            get { return this.lb3; }
        }
        public Label Lb4
        {
            get { return this.lb4; }
        }
        public Panel Pic1
        {
            get { return this.plLicenseImg; }
        }
        public Panel Pic2
        {
            get { return this.plTicket; }
        }
        public PictureBox Pic3
        {
            get { return this.pic3; }
        }

        //把控件设置为'正在打印'样式
        private void Pattern(int betState)
        {
            if (!this.LotteryOrder.bet_state.Equals(betState.ToString()))
                this.LotteryOrder.bet_state = betState.ToString();
            switch (betState)
            {
                //case GlobalConstants.ORDER_TICKET_STATE.AWAITING_PRINT://等待打印（正常票）
                //    this.BackgroundImage = global::Demo.Properties.Resources.l_bg;
                //    break;
                case GlobalConstants.ORDER_TICKET_STATE.PRINTTING://正在打印（正常票）
                    this.Lb1.ForeColor = Color.White;
                    this.Lb1.Font = new Font(this.Lb1.Font, FontStyle.Bold);
                    this.Lb2.ForeColor = Color.White;
                    this.Lb3.ForeColor = Color.White;
                    this.Pic2.BackgroundImage = global::Demo.Properties.Resources.d_piao;
                    //this.picState.BackgroundImage = global::Demo.Properties.Resources.pauseTicketSignal;
                    this.Pic3.Visible = false;
                    this.BackgroundImage = global::Demo.Properties.Resources.l_x_bg;
                    break;
                case GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT://等待打印（错漏票）
                    //this.Lb1.ForeColor = Color.White;
                    //this.Lb2.ForeColor = Color.White;
                    //this.Lb3.ForeColor = Color.White;
                    //this.Pic2.BackgroundImage = global::Demo.Properties.Resources.w_piao;
                    //this.BackgroundImage = global::Demo.Properties.Resources.l_bg;
                    //this.picState.BackgroundImage = global::Demo.Properties.Resources.errorTicketSignal;
                    //this.Pic3.Visible = false;
                    //this.BackgroundImage = null;
                    this.BackgroundImage = global::Demo.Properties.Resources.l_bg;
                    break;
                case GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING://正在打印（错漏票）
                    this.Lb1.ForeColor = Color.White;
                    this.Lb1.Font = new Font(this.Lb1.Font, FontStyle.Bold);
                    this.Lb2.ForeColor = Color.White;
                    this.Lb3.ForeColor = Color.White;
                    this.Pic2.BackgroundImage = global::Demo.Properties.Resources.d_piao;
                    //this.picState.BackgroundImage = global::Demo.Properties.Resources.pauseTicketSignal;
                    this.Pic3.Visible = false;
                    this.BackgroundImage = global::Demo.Properties.Resources.l_x_bg;
                    break;
                    //this.Lb1.ForeColor = Color.White;
                    //this.Lb2.ForeColor = Color.White;
                    //this.Lb3.ForeColor = Color.White;
                    //this.Pic2.BackgroundImage = global::Demo.Properties.Resources.w_piao;
                    //this.Pic3.Visible = false;
                    //this.BackgroundImage = null;
                    //this.BackColor = Color.Red;
                    //break;
                //case GlobalConstants.ORDER_TICKET_STATE.PAUSE://暂停
                //    this.BackgroundImage = global::Demo.Properties.Resources.l_bg;
                //    this.Lb1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(68)))), ((int)(((byte)(118)))));
                //    this.Lb2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(68)))), ((int)(((byte)(118)))));
                //    this.Lb3.ForeColor = Color.White;
                //    this.Pic2.BackgroundImage = global::Demo.Properties.Resources.w_piao;
                //    this.Pic3.Visible = true;
                //    this.BackgroundImage = global::Demo.Properties.Resources.l_bg;
                //    this.BackColor = Color.Transparent;
                //    break;
                //case GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE://错漏票暂停
                //    this.BackgroundImage = global::Demo.Properties.Resources.l_bg;
                //    this.Lb1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(68)))), ((int)(((byte)(118)))));
                //    this.Lb2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(68)))), ((int)(((byte)(118)))));
                //    this.Lb3.ForeColor = Color.White;
                //    this.Pic2.BackgroundImage = global::Demo.Properties.Resources.w_piao;
                //    this.Pic3.Visible = true;
                //    this.BackgroundImage = global::Demo.Properties.Resources.l_bg;
                //    this.BackColor = Color.Transparent;
                //    break;
                    //this.BackgroundImage = global::Demo.Properties.Resources.l_bg;
                    //break;
                default:





                    this.Lb1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(68)))), ((int)(((byte)(118)))));
                    this.Lb1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.Lb2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(133)))), ((int)(((byte)(148)))));
                    //this.Lb3.ForeColor = Color.White;
                    this.Pic2.BackgroundImage = global::Demo.Properties.Resources.w_piao;
                    this.Pic3.Visible = true;






                    this.BackgroundImage = global::Demo.Properties.Resources.l_bg;
                    break;
            }
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_Click(object sender, EventArgs e)
        {
            printController.updateIsInPrintForm(new List<lottery_order>() { this.LotteryOrder }, false);

            if (this.printController.orderToManualProcess(this.LotteryOrder.id.ToString()))
            {
                LogUtil.getInstance().addLogDataToQueue("把订单" + this.LotteryOrder.id + "置为手工处理成功!", GlobalConstants.LOGTYPE_ENUM.OWNER_OPERATOR);
                if (int.Parse(this.LotteryOrder.bet_state) == GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT)
                {
                    Global.ERROR_TICKET_NUM--;
                }

                //把订单从界面中移除
                Control c = this;
                while (c.GetType() != typeof(ControlsList))
                {
                    c = c.Parent;
                }
                ((ControlsList)c).RemoveObj(this);


                if (TabPrintControl.mOrderItem.LotteryOrder.id == this.LotteryOrder.id)//删除的刚好是现在界面上显示的
                {
                    if (TabPrintControl.OrderItemControlsList.ControlList.Controls.Count == 0)//就一个订单的情况，清掉所有数据
                    {
                        //TabPrintControl.mOrderItem.LotteryOrder = null;
                        TabPrintControl.mOrderItem = null;
                    }
                    else
                    {
                        TabPrintControl.mOrderItem = (ItemOrder)TabPrintControl.OrderItemControlsList.ControlList.Controls[0];
                    }
                }

            }

        }


        Bitmap bmRedHover = global::Demo.Properties.Resources.cha_hover;
        Bitmap bmGreyHover = global::Demo.Properties.Resources.cha;
        private void setDelBtnImg(int HoverOrLeave)
        {
            //GreyHover:2 RedHover:1 Leave:0
            switch (HoverOrLeave)
            {
                case 0:
                    this.Pic3.Image = null;
                    break;
                case 1:
                    if (this.Pic3.Image != bmRedHover)
                        this.Pic3.Image = bmRedHover;
                    break;
                case 2:
                    if (this.Pic3.Image != bmGreyHover)
                        this.Pic3.Image = bmGreyHover;
                    break;
                default:
                    break;
            }
        }
        private void pic3_MouseHover(object sender, EventArgs e)
        {
            setDelBtnImg(1);
        }

        private void pic3_MouseLeave(object sender, EventArgs e)
        {
            setDelBtnImg(0);
        }

        private void controlList_MouseHover(object sender, EventArgs e)
        {
            setDelBtnImg(2);
        }

        private void controlList_MouseLeave(object sender, EventArgs e)
        {
            setDelBtnImg(0);
        }

        private void pic1_MouseHover(object sender, EventArgs e)
        {
            setDelBtnImg(2);
        }

        private void lb1_MouseHover(object sender, EventArgs e)
        {
            setDelBtnImg(2);
        }

        private void lb2_MouseHover(object sender, EventArgs e)
        {
            setDelBtnImg(2);
        }

        private void plTicket_MouseHover(object sender, EventArgs e)
        {
            setDelBtnImg(2);
        }

        private void lb3_MouseHover(object sender, EventArgs e)
        {
            setDelBtnImg(2);
        }

        private void lb3_TextChanged(object sender, EventArgs e)
        {
            int charNum = this.Lb3.Text.Length;
            switch (charNum)
            {
                case 1:
                    this.Lb3.Location = new Point(9, this.Lb3.Location.Y);
                    break;
                case 2:
                    this.Lb3.Location = new Point(6, this.Lb3.Location.Y);
                    break;
                case 3:
                    this.Lb3.Location = new Point(5, this.Lb3.Location.Y);
                    break;
                case 4:
                    this.Lb3.Location = new Point(3, this.Lb3.Location.Y);
                    break;
                default:
                    this.Lb3.Location = new Point(0, this.Lb3.Location.Y);
                    break;
            }
        }


        private void orderItem_Click(object sender, EventArgs e)
        {
            //如果正在出票中，不做处理
            if ( ( null == TabPrintControl.sIScheduler ) ? false : ( TabPrintControl.sIScheduler.SPINFO.SCHEDULER_STATE == SerialPortInfo.SCHEDULER_STATE_ENUM.NORMAL ) )
            {
                return;
            }
            //如果现在就是已经选中的，不做处理
            if (this.Selected)
            {
                return;
            }

            if (!Global.isCanClickItemOrder)
            {
                return;
            }

            foreach (ItemOrder oi in TabPrintControl.OrderItemControlsList.ControlList.Controls)
            {
                int i =TabPrintControl.OrderItemControlsList.ControlList.Controls.Count;
                //if (oi.Selected == true)
                //{
                    oi.Selected = false;
                //}
            }
            this.Selected = true;

            //重新赋值去刷新界面
            TabPrintControl.mOrderItem = this;
        }

        private void timer_leftTime_Tick(object sender, EventArgs e)
        {
            if (this.Left_time > 0)
            {
                this.Left_time--;
            }

        }

        internal bool Swap(ItemOrder oi)
        {
            SwapOriginalLocation(this, oi);

            ItemOrder first = null;
            ItemOrder second = null;
            if (this.next == oi)
            {
                first = this;
                second = oi;
            }
            else
            {
                first = oi;
                second = this;
            }

            if (first.previous == null && second.next != null)
            {
                ItemOrder n = second.Next;

                second.Previous = null;
                second.Next = first;

                first.Previous = second;
                first.Next = n;

                n.Previous = first;
            }
            else if (first.previous != null && second.next == null)
            {
                first.Previous.Next = second;
                second.Previous = first.Previous;

                second.Next = first;
                first.Previous = second;

                first.Next = null;
            }
            else if (first.previous != null && second.next != null)
            {
                first.Previous.Next = second;
                second.Next.Previous = first;

                first.Next = second.Next;
                second.Previous = first.Previous;

                first.Previous = second;
                second.Next = first;
            }
            else if (first.previous == null && second.next == null)
            {
                first.Previous = second;
                second.Next = first;

                first.Next = null;
                second.Previous = null;
            }

            return true;
        }

        //使拖动中的ItemOrder回到OriginalLocation
        internal void BackToOriginalLocation()
        {
            this.Location = this.originalLocation;
        }

        private void ItemOrder_Load(object sender, EventArgs e)
        {
            originalLocation = this.Location;
        }

        //交换两个ItemOrder的OriginalLocation，并移动其中一个元素的Location
        internal void SwapOriginalLocation(ItemOrder first, ItemOrder second)
        {
            Point tempPoint = first.OriginalLocation;
            first.OriginalLocation = second.OriginalLocation;
            second.OriginalLocation = tempPoint;
            second.Location = second.OriginalLocation;
        }

        public void fordidClick()
        {
            this.Pic1.Click -= new EventHandler(this.orderItem_Click);
            this.Lb1.Click -= new EventHandler(this.orderItem_Click);
            this.Lb2.Click -= new EventHandler(this.orderItem_Click);
            this.Lb3.Click -= new EventHandler(this.orderItem_Click);
            this.Lb4.Click -= new EventHandler(this.orderItem_Click);
            this.plTicket.Click -= new EventHandler(this.orderItem_Click);
            this.Click -= new EventHandler(this.orderItem_Click);
        }

        public void allowClick()
        {
            this.Pic1.Click += new EventHandler(this.orderItem_Click);
            this.Lb1.Click += new EventHandler(this.orderItem_Click);
            this.Lb2.Click += new EventHandler(this.orderItem_Click);
            this.Lb3.Click += new EventHandler(this.orderItem_Click);
            this.Lb4.Click += new EventHandler(this.orderItem_Click);
            this.plTicket.Click += new EventHandler(this.orderItem_Click);
            this.Click += new EventHandler(this.orderItem_Click);
        }
    }
}
