﻿namespace Demo
{
    partial class TabErrorTicket
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabErrorTicket));
            this.controlsList = new Demo.ControlsList();
            this.picBoxWaiting = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTitleOperation = new System.Windows.Forms.Label();
            this.lbTitleTime = new System.Windows.Forms.Label();
            this.lbTitleDetails = new System.Windows.Forms.Label();
            this.lbTitleLicense = new System.Windows.Forms.Label();
            this.moduleTitlebar = new Demo.ModuleTitlebar();
            this.modulePagingNEW = new ModulePagingNEW();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxWaiting)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsList
            // 
            this.controlsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(242)))));
            this.controlsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlsList.E = null;
            this.controlsList.Gap = 0;
            this.controlsList.GapX = 0;
            this.controlsList.HideScrollbar = false;
            this.controlsList.IsAutoHideScroll = true;
            this.controlsList.IsItemOrder = false;
            this.controlsList.Location = new System.Drawing.Point(0, 62);
            this.controlsList.Margin = new System.Windows.Forms.Padding(4);
            this.controlsList.Name = "controlsList";
            this.controlsList.Size = new System.Drawing.Size(950, 503);
            this.controlsList.TabIndex = 0;
            this.controlsList.Load += new System.EventHandler(this.controlsList_Load);
            // 
            // picBoxWaiting
            // 
            this.picBoxWaiting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(242)))));
            this.picBoxWaiting.Image = global::Demo.Properties.Resources.waiting_gray;
            this.picBoxWaiting.Location = new System.Drawing.Point(450, 273);
            this.picBoxWaiting.Name = "picBoxWaiting";
            this.picBoxWaiting.Size = new System.Drawing.Size(35, 35);
            this.picBoxWaiting.TabIndex = 4;
            this.picBoxWaiting.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BackgroundImage = global::Demo.Properties.Resources.line_3;
            this.panel1.Controls.Add(this.lbTitleOperation);
            this.panel1.Controls.Add(this.lbTitleTime);
            this.panel1.Controls.Add(this.lbTitleDetails);
            this.panel1.Controls.Add(this.lbTitleLicense);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(950, 30);
            this.panel1.TabIndex = 2;
            // 
            // lbTitleOperation
            // 
            this.lbTitleOperation.AutoSize = true;
            this.lbTitleOperation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitleOperation.ForeColor = System.Drawing.Color.DimGray;
            this.lbTitleOperation.Location = new System.Drawing.Point(854, 9);
            this.lbTitleOperation.Name = "lbTitleOperation";
            this.lbTitleOperation.Size = new System.Drawing.Size(29, 12);
            this.lbTitleOperation.TabIndex = 9;
            this.lbTitleOperation.Text = "操作";
            // 
            // lbTitleTime
            // 
            this.lbTitleTime.AutoSize = true;
            this.lbTitleTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitleTime.ForeColor = System.Drawing.Color.DimGray;
            this.lbTitleTime.Location = new System.Drawing.Point(666, 9);
            this.lbTitleTime.Name = "lbTitleTime";
            this.lbTitleTime.Size = new System.Drawing.Size(53, 12);
            this.lbTitleTime.TabIndex = 8;
            this.lbTitleTime.Text = "出票时间";
            // 
            // lbTitleDetails
            // 
            this.lbTitleDetails.AutoSize = true;
            this.lbTitleDetails.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitleDetails.ForeColor = System.Drawing.Color.DimGray;
            this.lbTitleDetails.Location = new System.Drawing.Point(345, 9);
            this.lbTitleDetails.Name = "lbTitleDetails";
            this.lbTitleDetails.Size = new System.Drawing.Size(53, 12);
            this.lbTitleDetails.TabIndex = 7;
            this.lbTitleDetails.Text = "彩种详情";
            // 
            // lbTitleLicense
            // 
            this.lbTitleLicense.AutoSize = true;
            this.lbTitleLicense.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitleLicense.ForeColor = System.Drawing.Color.DimGray;
            this.lbTitleLicense.Location = new System.Drawing.Point(48, 9);
            this.lbTitleLicense.Name = "lbTitleLicense";
            this.lbTitleLicense.Size = new System.Drawing.Size(53, 12);
            this.lbTitleLicense.TabIndex = 6;
            this.lbTitleLicense.Text = "彩种名称";
            // 
            // moduleTitlebar
            // 
            this.moduleTitlebar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("moduleTitlebar.BackgroundImage")));
            this.moduleTitlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.moduleTitlebar.Location = new System.Drawing.Point(0, 0);
            this.moduleTitlebar.Name = "moduleTitlebar";
            this.moduleTitlebar.remind = null;
            this.moduleTitlebar.Size = new System.Drawing.Size(950, 32);
            this.moduleTitlebar.TabIndex = 3;
            // 
            // modulePagingNEW
            // 
            this.modulePagingNEW.BackColor = System.Drawing.Color.Transparent;
            this.modulePagingNEW.BtnFirstClick = null;
            this.modulePagingNEW.BtnJumpClick = null;
            this.modulePagingNEW.BtnLastClick = null;
            this.modulePagingNEW.BtnNextClick = null;
            this.modulePagingNEW.BtnPreviousClick = null;
            this.modulePagingNEW.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.modulePagingNEW.Enabled = false;
            this.modulePagingNEW.JumpPage = 0;
            this.modulePagingNEW.Location = new System.Drawing.Point(285, 497);
            this.modulePagingNEW.MaxPage = 0;
            this.modulePagingNEW.Name = "modulePagingNEW";
            this.modulePagingNEW.NowPage = 0;
            this.modulePagingNEW.Size = new System.Drawing.Size(940, 24);
            this.modulePagingNEW.TabIndex = 0;
            // 
            // TabErrorTicket
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.picBoxWaiting);
            this.Controls.Add(this.controlsList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.moduleTitlebar);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "TabErrorTicket";
            this.Size = new System.Drawing.Size(950, 565);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxWaiting)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ControlsList controlsList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTitleOperation;
        private System.Windows.Forms.Label lbTitleTime;
        private System.Windows.Forms.Label lbTitleDetails;
        private System.Windows.Forms.Label lbTitleLicense;
        private ModuleTitlebar moduleTitlebar;
        private ModulePagingNEW modulePagingNEW;
        private System.Windows.Forms.PictureBox picBoxWaiting;
    }
}
