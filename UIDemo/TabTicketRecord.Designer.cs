﻿namespace Demo
{
    partial class TabTicketRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabTicketRecord));
            this.lbOrderId = new System.Windows.Forms.Label();
            this.plHeader = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbLicenseTag = new System.Windows.Forms.Label();
            this.lbTotalErrorTicketNum = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.lbTotalTicketNumTag = new System.Windows.Forms.Label();
            this.lbOrderPrice = new System.Windows.Forms.Label();
            this.picLicense = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbOrderPriceTag = new System.Windows.Forms.Label();
            this.lbLicense = new System.Windows.Forms.Label();
            this.plTitle = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTicketId = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.controlsList = new Demo.ControlsList();
            this.moduleTitlebar = new Demo.ModuleTitlebar();
            this.modulePagingNEW = new ModulePagingNEW();
            this.plHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLicense)).BeginInit();
            this.panel3.SuspendLayout();
            this.plTitle.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbOrderId
            // 
            this.lbOrderId.AutoSize = true;
            this.lbOrderId.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOrderId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbOrderId.Location = new System.Drawing.Point(141, 11);
            this.lbOrderId.Name = "lbOrderId";
            this.lbOrderId.Size = new System.Drawing.Size(80, 16);
            this.lbOrderId.TabIndex = 15;
            this.lbOrderId.Text = "(123456)";
            // 
            // plHeader
            // 
            this.plHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.plHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("plHeader.BackgroundImage")));
            this.plHeader.Controls.Add(this.label2);
            this.plHeader.Controls.Add(this.textBox1);
            this.plHeader.Controls.Add(this.textBox2);
            this.plHeader.Controls.Add(this.label5);
            this.plHeader.Controls.Add(this.pictureBox1);
            this.plHeader.Controls.Add(this.label4);
            this.plHeader.Controls.Add(this.label3);
            this.plHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.plHeader.Location = new System.Drawing.Point(0, 100);
            this.plHeader.Name = "plHeader";
            this.plHeader.Size = new System.Drawing.Size(950, 26);
            this.plHeader.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(632, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "从第";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(665, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(50, 21);
            this.textBox1.TabIndex = 22;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(765, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(50, 21);
            this.textBox2.TabIndex = 23;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(570, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 28;
            this.label5.Text = "多票重打：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Demo.Properties.Resources.qrEnter;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(850, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 25);
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(818, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "票";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(717, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 26;
            this.label3.Text = "票 至 第";
            // 
            // lbLicenseTag
            // 
            this.lbLicenseTag.AutoSize = true;
            this.lbLicenseTag.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLicenseTag.ForeColor = System.Drawing.Color.Black;
            this.lbLicenseTag.Location = new System.Drawing.Point(14, 11);
            this.lbLicenseTag.Name = "lbLicenseTag";
            this.lbLicenseTag.Size = new System.Drawing.Size(51, 16);
            this.lbLicenseTag.TabIndex = 12;
            this.lbLicenseTag.Text = "彩种:";
            // 
            // lbTotalErrorTicketNum
            // 
            this.lbTotalErrorTicketNum.AutoSize = true;
            this.lbTotalErrorTicketNum.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTotalErrorTicketNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(135)))), ((int)(((byte)(234)))));
            this.lbTotalErrorTicketNum.Location = new System.Drawing.Point(642, 11);
            this.lbTotalErrorTicketNum.Name = "lbTotalErrorTicketNum";
            this.lbTotalErrorTicketNum.Size = new System.Drawing.Size(164, 16);
            this.lbTotalErrorTicketNum.TabIndex = 11;
            this.lbTotalErrorTicketNum.Text = "{0}张（出票{1}张）";
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::Demo.Properties.Resources.fh;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(850, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(70, 25);
            this.btnBack.TabIndex = 18;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lbTotalTicketNumTag
            // 
            this.lbTotalTicketNumTag.AutoSize = true;
            this.lbTotalTicketNumTag.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTotalTicketNumTag.ForeColor = System.Drawing.Color.Black;
            this.lbTotalTicketNumTag.Location = new System.Drawing.Point(580, 11);
            this.lbTotalTicketNumTag.Name = "lbTotalTicketNumTag";
            this.lbTotalTicketNumTag.Size = new System.Drawing.Size(68, 16);
            this.lbTotalTicketNumTag.TabIndex = 10;
            this.lbTotalTicketNumTag.Text = "总票数:";
            // 
            // lbOrderPrice
            // 
            this.lbOrderPrice.AutoSize = true;
            this.lbOrderPrice.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOrderPrice.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbOrderPrice.Location = new System.Drawing.Point(341, 11);
            this.lbOrderPrice.Name = "lbOrderPrice";
            this.lbOrderPrice.Size = new System.Drawing.Size(164, 16);
            this.lbOrderPrice.TabIndex = 7;
            this.lbOrderPrice.Text = "{0}元（出票{1}元）";
            // 
            // picLicense
            // 
            this.picLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picLicense.Location = new System.Drawing.Point(18, 2);
            this.picLicense.Name = "picLicense";
            this.picLicense.Size = new System.Drawing.Size(64, 64);
            this.picLicense.TabIndex = 2;
            this.picLicense.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(233)))), ((int)(((byte)(237)))));
            this.panel3.Controls.Add(this.lbOrderId);
            this.panel3.Controls.Add(this.lbLicenseTag);
            this.panel3.Controls.Add(this.lbTotalErrorTicketNum);
            this.panel3.Controls.Add(this.lbTotalTicketNumTag);
            this.panel3.Controls.Add(this.lbOrderPrice);
            this.panel3.Controls.Add(this.lbOrderPriceTag);
            this.panel3.Controls.Add(this.lbLicense);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(92, 15);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(836, 38);
            this.panel3.TabIndex = 3;
            // 
            // lbOrderPriceTag
            // 
            this.lbOrderPriceTag.AutoSize = true;
            this.lbOrderPriceTag.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOrderPriceTag.ForeColor = System.Drawing.Color.Black;
            this.lbOrderPriceTag.Location = new System.Drawing.Point(298, 11);
            this.lbOrderPriceTag.Name = "lbOrderPriceTag";
            this.lbOrderPriceTag.Size = new System.Drawing.Size(51, 16);
            this.lbOrderPriceTag.TabIndex = 6;
            this.lbOrderPriceTag.Text = "金额:";
            // 
            // lbLicense
            // 
            this.lbLicense.AutoSize = true;
            this.lbLicense.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLicense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(135)))), ((int)(((byte)(234)))));
            this.lbLicense.Location = new System.Drawing.Point(62, 11);
            this.lbLicense.Name = "lbLicense";
            this.lbLicense.Size = new System.Drawing.Size(76, 16);
            this.lbLicense.TabIndex = 4;
            this.lbLicense.Text = "竞彩足球";
            // 
            // plTitle
            // 
            this.plTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.plTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.plTitle.Controls.Add(this.picLicense);
            this.plTitle.Controls.Add(this.panel3);
            this.plTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTitle.Location = new System.Drawing.Point(0, 32);
            this.plTitle.Name = "plTitle";
            this.plTitle.Size = new System.Drawing.Size(950, 68);
            this.plTitle.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Demo.Properties.Resources.ct;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbTicketId);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 531);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(950, 34);
            this.panel1.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "票号:";
            // 
            // tbTicketId
            // 
            this.tbTicketId.Location = new System.Drawing.Point(57, 6);
            this.tbTicketId.Name = "tbTicketId";
            this.tbTicketId.Size = new System.Drawing.Size(100, 21);
            this.tbTicketId.TabIndex = 20;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::Demo.Properties.Resources.btnSearch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(164, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(49, 24);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // controlsList
            // 
            this.controlsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.controlsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlsList.E = null;
            this.controlsList.Gap = 20;
            this.controlsList.GapX = 35;
            this.controlsList.HideScrollbar = false;
            this.controlsList.IsAutoHideScroll = false;
            this.controlsList.IsItemOrder = false;
            this.controlsList.Location = new System.Drawing.Point(0, 126);
            this.controlsList.Name = "controlsList";
            this.controlsList.Size = new System.Drawing.Size(950, 405);
            this.controlsList.TabIndex = 23;
            // 
            // moduleTitlebar
            // 
            this.moduleTitlebar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("moduleTitlebar.BackgroundImage")));
            this.moduleTitlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.moduleTitlebar.Location = new System.Drawing.Point(0, 0);
            this.moduleTitlebar.Name = "moduleTitlebar";
            this.moduleTitlebar.remind = null;
            this.moduleTitlebar.Size = new System.Drawing.Size(950, 32);
            this.moduleTitlebar.TabIndex = 24;
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
            this.modulePagingNEW.Location = new System.Drawing.Point(0, 0);
            this.modulePagingNEW.MaxPage = 0;
            this.modulePagingNEW.Name = "modulePagingNEW";
            this.modulePagingNEW.NowPage = 0;
            this.modulePagingNEW.Size = new System.Drawing.Size(940, 24);
            this.modulePagingNEW.TabIndex = 0;
            // 
            // TabTicketRecord
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.controlsList);
            this.Controls.Add(this.plHeader);
            this.Controls.Add(this.plTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.moduleTitlebar);
            this.Name = "TabTicketRecord";
            this.Size = new System.Drawing.Size(950, 565);
            this.Load += new System.EventHandler(this.TabTicketRecord_Load);
            this.plHeader.ResumeLayout(false);
            this.plHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLicense)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.plTitle.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ControlsList controlsList;
        private System.Windows.Forms.Label lbOrderId;
        private System.Windows.Forms.Panel plHeader;
        private System.Windows.Forms.Label lbLicenseTag;
        private System.Windows.Forms.Label lbTotalErrorTicketNum;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lbTotalTicketNumTag;
        private System.Windows.Forms.Label lbOrderPrice;
        private ModuleTitlebar moduleTitlebar;
        private System.Windows.Forms.PictureBox picLicense;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbOrderPriceTag;
        private System.Windows.Forms.Label lbLicense;
        private System.Windows.Forms.Panel plTitle;
        private System.Windows.Forms.Panel panel1;
        private ModulePagingNEW modulePagingNEW;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTicketId;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
