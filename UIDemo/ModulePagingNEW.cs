﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    public partial class ModulePagingNEW : UserControl
    {

        public ModulePagingNEW()//(int size)
        {
            //需要初始化的参数
            // 1. this.pageRange 页面容量
            // 2. this.loadPage 加载页面的方法
            // 3. this.totalItemCount 页面元素总数
            InitializeComponent();
            this.Enabled = false;

            if (this.Parent != null)
            {
                this.panel2.Location = new Point((this.Parent.Width - this.panel2.Width) / 2, 0);
                //选择控件宽度以适应不同界面，1为464，2为900，3为940
                //switch (size)
                //{
                //    case 1:
                //        this.Width = 646;
                //        this.panel2.Location = new Point(143, 0);
                //        break;
                //    case 2:
                //        this.Width = 900;
                //        this.panel2.Location = new Point(269, 0);
                //        break;
                //    case 3:
                //        this.Width = 940;
                //        this.panel2.Location = new Point(363, 0);
                //        break;
                //    default:
                //        break;
                //}
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="maxPNo"></param>
        /// <param name="pNo"></param>
        public void InitItem(Int64 maxPNo, Int64 pNo)
        {
            this.MaxPage = (int)maxPNo;
            this.NowPage = (int)pNo;

            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                {
                    InitItemHandler(maxPNo, pNo);
                }));
            }
            else
            {
                InitItemHandler(maxPNo, pNo);
            }
        }

        private void InitItemHandler(Int64 maxPNo, Int64 pNo)
        {
            this.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.tBoxNowPage.Enabled = false;
            this.btnJump.Enabled = false;

            this.tBoxNowPage.Text = pNo.ToString();
            this.lbTotalPageNumber.Text = String.Format("第{0}页，共{1}页", this.NowPage.ToString(), maxPNo.ToString());

            if (maxPNo <= 1)//没有数据的情况、只有一页的情况
            {
                //改变显示即可，都不能操作
                //如果页数小于2，隐藏分页控件
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler(delegate(object o, EventArgs e)
                    {
                        this.Visible = false;
                    }));
                }
                else
                {
                    this.Visible = false;
                }

                return;
            }
            else//多页的情况
            {
                this.Enabled = true;
                this.Visible = true;
                this.tBoxNowPage.Enabled = true;//只要有多页，跳转都可操作
                this.btnJump.Enabled = true;

                if (pNo == 1)//如果现在是第一页，那么下一页和尾页可操作
                {
                    this.btnNext.Enabled = true;
                    this.btnLast.Enabled = true;
                }
                else if (pNo == maxPNo)//如果现在是最后一页，那么上一页和首页可操作
                {
                    this.btnFirst.Enabled = true;
                    this.btnPrevious.Enabled = true;
                }
                else//都可操作
                {
                    this.btnNext.Enabled = true;
                    this.btnLast.Enabled = true;
                    this.btnFirst.Enabled = true;
                    this.btnPrevious.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 最大页数
        /// </summary>
        private int maxPage;
        public int MaxPage
        {
            get { return maxPage; }
            set { maxPage = value; }
        }

        /// <summary>
        /// 当前页数
        /// </summary>
        private int nowPage;
        public int NowPage
        {
            get
            {
                return nowPage;
            }
            set { nowPage = value; }
        }

        private int jumpPage;//要跳转的页面
        public int JumpPage
        {
            get { return jumpPage; }
            set { jumpPage = value; }
        }

        private EventHandler btnFirstClick;
        public EventHandler BtnFirstClick
        {
            get { return btnFirstClick; }
            set
            {
                btnFirstClick = value;
                this.btnFirst.Click += BtnFirstClick;
            }
        }
        private EventHandler btnPreviousClick;
        public EventHandler BtnPreviousClick
        {
            get { return btnPreviousClick; }
            set
            {
                btnPreviousClick = value;
                this.btnPrevious.Click += btnPreviousClick;
            }
        }

        private EventHandler btnNextClick;
        public EventHandler BtnNextClick
        {
            get { return btnNextClick; }
            set
            {
                btnNextClick = value;
                this.btnNext.Click += btnNextClick;
            }
        }

        private EventHandler btnLastClick;
        public EventHandler BtnLastClick
        {
            get { return btnLastClick; }
            set
            {
                btnLastClick = value;
                this.btnLast.Click += btnLastClick;
            }
        }

        private EventHandler btnJumpClick;
        public EventHandler BtnJumpClick
        {
            get { return btnJumpClick; }
            set
            {
                btnJumpClick = value;
                this.btnJump.Click += btnJumpClick;
            }
        }



        private void btnFirst_MouseHover(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.firstPageHover;
        }

        private void btnFirst_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.firstPage;
        }

        private void btnPrevious_MouseHover(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.previousPageHover;
        }

        private void btnPrevious_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.previousPage;
        }

        private void btnNext_MouseHover(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.nextPageHover;
        }

        private void btnNext_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.nextPage;
        }

        private void btnLast_MouseHover(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.lastPageHover;
        }

        private void btnLast_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackgroundImage = global::Demo.Properties.Resources.lastPage;
        }

        /// <summary>
        /// 保证要跳转的页面是合法的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tBoxNowPage_TextChanged(object sender, EventArgs e)
        {
            int page = 1;
            if (int.TryParse(this.tBoxNowPage.Text, out page))
            {
                if (page < 1 || page > this.MaxPage)
                {
                    this.tBoxNowPage.Text = this.NowPage.ToString();
                }
            }
            else
            {
                this.tBoxNowPage.Text = this.NowPage.ToString();
            }

            int.TryParse(this.tBoxNowPage.Text, out page);
            this.JumpPage = page;
        }

        private void btnJump_MouseHover(object sender, EventArgs e)
        {
            this.btnJump.BackgroundImage = global::Demo.Properties.Resources.locatePageHover;
        }

        private void btnJump_MouseLeave(object sender, EventArgs e)
        {
            this.btnJump.BackgroundImage = global::Demo.Properties.Resources.locatePage;
        }

        private void ModulePagingNEW_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Dock = DockStyle.Bottom;
        }
    }
}
