﻿namespace Demo
{
    partial class TabOrderRecord
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
            this.lbTitleOperation = new System.Windows.Forms.Label();
            this.lbTitleTime = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTitleDetails = new System.Windows.Forms.Label();
            this.lbTitleLicense = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.controlsList = new Demo.ControlsList();
            this.moduleTitlebar = new Demo.ModuleTitlebar();
            this.modulePagingNEW = new ModulePagingNEW();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitleOperation
            // 
            this.lbTitleOperation.AutoSize = true;
            this.lbTitleOperation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitleOperation.ForeColor = System.Drawing.Color.DimGray;
            this.lbTitleOperation.Location = new System.Drawing.Point(844, 9);
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
            this.lbTitleTime.Location = new System.Drawing.Point(689, 9);
            this.lbTitleTime.Name = "lbTitleTime";
            this.lbTitleTime.Size = new System.Drawing.Size(53, 12);
            this.lbTitleTime.TabIndex = 8;
            this.lbTitleTime.Text = "出票时间";
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
            this.panel1.TabIndex = 7;
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Demo.Properties.Resources.ct;
            this.panel2.Controls.Add(this.btnBack);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 531);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(950, 34);
            this.panel2.TabIndex = 9;
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::Demo.Properties.Resources.fh;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBack.ForeColor = System.Drawing.Color.Transparent;
            this.btnBack.Location = new System.Drawing.Point(876, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(70, 25);
            this.btnBack.TabIndex = 5;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
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
            this.modulePagingNEW.TabIndex = 6;
            // 
            // controlsList
            // 
            this.controlsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.controlsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlsList.E = null;
            this.controlsList.Gap = 0;
            this.controlsList.GapX = 0;
            this.controlsList.HideScrollbar = false;
            this.controlsList.IsAutoHideScroll = false;
            this.controlsList.IsItemOrder = false;
            this.controlsList.Location = new System.Drawing.Point(0, 62);
            this.controlsList.Margin = new System.Windows.Forms.Padding(4);
            this.controlsList.Name = "controlsList";
            this.controlsList.Size = new System.Drawing.Size(950, 469);
            this.controlsList.TabIndex = 6;
            // 
            // moduleTitlebar
            // 
            this.moduleTitlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.moduleTitlebar.Location = new System.Drawing.Point(0, 0);
            this.moduleTitlebar.Name = "moduleTitlebar";
            this.moduleTitlebar.remind = null;
            this.moduleTitlebar.Size = new System.Drawing.Size(950, 32);
            this.moduleTitlebar.TabIndex = 8;
            // 
            // TabOrderRecord
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.controlsList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.moduleTitlebar);
            this.Name = "TabOrderRecord";
            this.Size = new System.Drawing.Size(950, 565);
            this.Load += new System.EventHandler(this.TabOrderRecord_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitleOperation;
        private ControlsList controlsList;
        private System.Windows.Forms.Label lbTitleTime;
        private ModuleTitlebar moduleTitlebar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTitleDetails;
        private System.Windows.Forms.Label lbTitleLicense;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBack;
        private ModulePagingNEW modulePagingNEW;

    }
}
