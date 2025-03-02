﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Maticsoft.Common.model;
using Maticsoft.Common.Util;

namespace Demo
{
    public partial class ItemTicket : UserControl
    {
        private bool isWaiting;
        private string state;
        private lottery_ticket _lotteryTicket;

        public ItemTicket()
        {
            InitializeComponent();
            this.X = this.panel1.Location.X;
        }

        public ItemTicket(lottery_ticket lt)
            : this()
        {
            this.LotteryTicket = lt;
        }

        private void TicketItem_Load(object sender, EventArgs e)
        {
        }

        private void refreshHandler()
        {
            try
            {
                this.IsWaiting = false;//每次重新赋新的lottery_ticket对象时，都要把等待图动态图隐藏
                this.panel1.Width = (this.LotteryTicket.ticket_id.ToString().Length - 1) * 9 + 14;
                this.panel1.Location = new Point((this.X - (this.LotteryTicket.ticket_id.ToString().Length - 1) * 9), this.panel1.Location.Y);
                this.lbIndex.Text = this.LotteryTicket.ticket_id.ToString();
                string[] betCode2ShowArray;
                try
                {
                    if (this.LotteryTicket.order_id < 0)
                    {
                        betCode2ShowArray = BetCodeTranslationUtil.betCode2ShowArrayNoEncrypt(this.LotteryTicket.bet_code, this.LotteryTicket.license_id.ToString(), this.LotteryTicket.play_type);
                    }
                    else
                    {
                        betCode2ShowArray = BetCodeTranslationUtil.betCode2ShowArray(this.LotteryTicket.bet_code, this.LotteryTicket.license_id.ToString(), this.LotteryTicket.play_type);
                    }
                }
                catch (Exception)
                {
                    betCode2ShowArray = new string[] { "未知", "------" };
                }
                this.lbDetails.Text = betCode2ShowArray[0];
                this.lbDetails2.Text = betCode2ShowArray[1];
                this.lbBet.Text = this.LotteryTicket.bet_num + "注";
                this.lbMultiple.Text = this.LotteryTicket.multiple + "倍";
                this.State = this.LotteryTicket.ticket_state;
                this.Size = new Size(this.Size.Width, this.lbDetails2.Size.Height + 60);

                //加戳（错、撤、逾）
                //if (this.LotteryTicket.ticket_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR.ToString()) ||
                //    this.LotteryTicket.ticket_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_WAITING_PRINT.ToString()) ||
                //    this.LotteryTicket.ticket_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PRINTTING.ToString()) ||
                //    this.LotteryTicket.ticket_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_PAUSE.ToString()) ||
                //    this.LotteryTicket.ticket_state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString()))
                //{
                //    this.picSeal.BackgroundImage = global::Demo.Properties.Resources.sealError;
                //    if (this.lbDetails2.Size.Height > this.picSeal.Height)
                //    {
                //        this.Size = new Size(this.Size.Width, this.lbDetails2.Size.Height + 60);
                //    }
                //    else
                //    {
                //        this.Size = new Size(this.Size.Width, this.picSeal.Height + 60);
                //    }
                //}
                //else if (this.LotteryTicket.ticket_state.Equals(GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString()))
                //{
                //    this.picSeal.BackgroundImage = global::Demo.Properties.Resources.sealCancel;
                //    if (this.lbDetails2.Size.Height > this.picSeal.Height)
                //    {
                //        this.Size = new Size(this.Size.Width, this.lbDetails2.Size.Height + 60);
                //    }
                //    else
                //    {
                //        this.Size = new Size(this.Size.Width, this.picSeal.Height + 60);
                //    }
                //}
                //else if (this.LotteryTicket.ticket_state.Equals(GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString()))
                //{
                //    this.picSeal.BackgroundImage = global::Demo.Properties.Resources.sealExpired;
                //    if (this.lbDetails2.Size.Height > this.picSeal.Height)
                //    {
                //        this.Size = new Size(this.Size.Width, this.lbDetails2.Size.Height + 60);
                //    }
                //    else
                //    {
                //        this.Size = new Size(this.Size.Width, this.picSeal.Height + 60);
                //    }
                //}
                //else
                //{
                //    this.picSeal.BackgroundImage = null;
                //    this.Size = new Size(this.Size.Width, this.lbDetails2.Size.Height + 60);
                //}
            }
            catch (Exception)
            {
                if (null == this.LotteryTicket)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                        {
                            this.Height = 0;
                        }));
                    }
                    else
                    {
                        this.Height = 0;
                    }
                }
            }
        }


        private void refreshTicketInfo()
        {
            try
            {
                if (null != this._lotteryTicket)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                        {
                            refreshHandler();
                        }));
                    }
                    else
                    {
                        refreshHandler();
                    }
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                        {
                            this.Height = 0;
                        }));
                    }
                    else
                    {
                        this.Height = 0;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PictureBox PicStatus
        {
            get { return this.picStatus; }
        }

        public lottery_ticket LotteryTicket
        {
            get { return _lotteryTicket; }
            set
            {
                _lotteryTicket = value;
                refreshTicketInfo();
            }
        }

        public string State
        {
            get { return state; }
            set
            {
                state = value;
                this.LotteryTicket.ticket_state = state;
                if (state.Equals(GlobalConstants.ORDER_TICKET_STATE.PRINTTING_COMPLETE.ToString()) ||
                    state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR_COMPLETE.ToString()) ||
                    state.Equals(GlobalConstants.ORDER_TICKET_STATE.MANUAL_COMPLETE.ToString()) ||
                    state.Equals(GlobalConstants.ORDER_TICKET_STATE.RE_COMPLETE.ToString()))
                {
                    this.picStatus.BackgroundImage = global::Demo.Properties.Resources.wc;
                    this.IsWaiting = false;
                }
                else if (state.Equals(GlobalConstants.ORDER_TICKET_STATE.ERROR.ToString()) ||
                        state.Equals(GlobalConstants.ORDER_TICKET_STATE.CANCEL.ToString()) ||
                        state.Equals(GlobalConstants.ORDER_TICKET_STATE.EXPIRED.ToString()))
                {
                    this.picStatus.BackgroundImage = global::Demo.Properties.Resources.xxx;
                    this.IsWaiting = false;
                }
                else if (state.Equals("100"))
                {
                    this.IsWaiting = true;
                }
                else
                {
                    this.picStatus.BackgroundImage = global::Demo.Properties.Resources.wwc;
                    this.IsWaiting = false;
                }
            }
        }

        public bool IsWaiting
        {
            get { return isWaiting; }
            set
            {
                isWaiting = value;
                if (isWaiting)
                {
                    if (!this.picIsWaiting.Visible)
                    {
                        if (this.picIsWaiting.InvokeRequired)
                        {
                            this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                            {
                                this.picIsWaiting.Visible = true;
                                this.picIsWaiting.Location = new Point(this.picIsWaiting.Location.X, (this.plMiddle.Height - this.picIsWaiting.Height) / 2);
                            }));
                        }
                        else
                        {
                            this.picIsWaiting.Visible = true;
                            this.picIsWaiting.Location = new Point(this.picIsWaiting.Location.X, (this.plMiddle.Height - this.picIsWaiting.Height) / 2);
                        }
                    }
                }
                else
                {
                    if (this.picIsWaiting.Visible)
                    {
                        if (this.picIsWaiting.InvokeRequired)
                        {
                            this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                            {
                                this.picIsWaiting.Visible = false;
                            }));
                        }
                        else
                        {
                            this.picIsWaiting.Visible = false;
                        }
                    }
                }
            }
        }
    }
}
